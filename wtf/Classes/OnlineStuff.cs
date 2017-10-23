using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Lidgren.Network;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace wtf
{
    public class OnlineStuff
    {
        IPID ipId;
        NetPeerConfiguration peerConfig;
        NetPeer peer;
        Game1 game;
        string massage;
        public bool connecting, isServer;

        public OnlineStuff(Game1 game_)
        {
            game = game_;

            peerConfig = new NetPeerConfiguration("TestGame");
            peerConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            peerConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            peerConfig.EnableMessageType(NetIncomingMessageType.Data);
            peerConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            peerConfig.EnableMessageType(NetIncomingMessageType.StatusChanged);
            peerConfig.EnableMessageType(NetIncomingMessageType.DebugMessage);
            peerConfig.EnableMessageType(NetIncomingMessageType.WarningMessage);
            peerConfig.AcceptIncomingConnections = true;
            peerConfig.Port = 8000;
            peer = new NetPeer(peerConfig);
            peer.Start();
            connecting = false;
            isServer = false;
            ipId = new IPID();
        }

        public void Update(float a_es)
        {
            if (connecting)
            { peer.DiscoverKnownPeer(ipId.ips[0], 8000); }
            HandleWebConnections();
        }

        public void StartConnection(string IP)
        {
            ipId.AddID(IP);
            connecting = true;
        }

        public void SendMessage(int[] ids, string msg)
        {
            var message = peer.CreateMessage();
            message.Write(msg);
            foreach (var id in ids)
                peer.SendMessage(message, peer.Connections[id], NetDeliveryMethod.ReliableOrdered);
        }

        public void HandleWebConnections()
        {
            NetIncomingMessage message;
            while ((message = peer.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        HandleData(message);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        switch (message.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                Debug.WriteLine("Connected!");
                                break;
                            case NetConnectionStatus.Disconnected:
                                Debug.WriteLine("Disconnected...");
                                break;
                        }
                        break;

                    case NetIncomingMessageType.ConnectionApproval:
                        message.SenderConnection.Approve();
                        break;

                    case NetIncomingMessageType.DiscoveryResponse:
                        peer.Connect(message.SenderEndPoint);
                        connecting = false;
                        break;

                    case NetIncomingMessageType.DiscoveryRequest:
                        var msg = peer.CreateMessage();
                        msg.Write("hi");
                        peer.SendDiscoveryResponse(msg, message.SenderEndPoint);
                        peer.Connect(message.SenderEndPoint);
                        isServer = true;
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        Debug.WriteLine(message.ReadString());
                        break;


                    case NetIncomingMessageType.WarningMessage:
                        Debug.WriteLine(message.ReadString());
                        break;

                    default:
                        Debug.WriteLine("unhandled message with type: "
                            + message.MessageType);
                        break;
                }
                peer.Recycle(message);
            }
        }

        public void HandleData(NetIncomingMessage msg)
        {
            string message = msg.PeekString();
            switch (message[0])
            {
                case '0':
                    massage = message.Substring(2);
                    break;
            }

        }

        public string getData()
        {
            return massage;
        }

        public void Draw(SpriteBatch a_sb, SpriteFont font)
        {
            if (massage != null)
            { a_sb.DrawString(font, massage, Vector2.Zero, Color.White); }
            if (isServer)
                a_sb.DrawString(font, "ass", Vector2.Zero, Color.White);
        }
    }

    public class IPID
    {
        int IDCount;
        public List<string> ips;
        public List<int> ids;
        public IPID()
        {
            IDCount = 0;
            ips = new List<string>();
            ids = new List<int>();
        }
        public void AddID(string ip_)
        {
            //make sure I don't already have that IP
            bool hasIp = false;
            foreach(string ip in ips)
            {
                if(ip == ip_) { hasIp = true; }
            }
            if (!hasIp)
            {
                IDCount++;
                ips.Add(ip_);
                ids.Add(IDCount);
            }
        }
        void RemoveID(string ip_)
        {
            for(int x = 0; x < ips.Count; x++)
            {
                if(ips[x] == ip_)
                {
                    ids.RemoveAt(x);
                    ips.RemoveAt(x);
                    break;
                }
            }
        }
    }
}