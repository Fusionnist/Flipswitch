using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Lidgren.Network;

namespace wtf
{
    public class OnlineStuff
    {
        NetPeerConfiguration peerConfig;
        NetPeer peer;

        public OnlineStuff()
        {
            peerConfig = new NetPeerConfiguration("TestGame");
            peerConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            peerConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            peerConfig.EnableMessageType(NetIncomingMessageType.Data);
            peerConfig.EnableMessageType(NetIncomingMessageType.StatusChanged);
            peerConfig.EnableMessageType(NetIncomingMessageType.DebugMessage);
            peerConfig.AcceptIncomingConnections = true;
            peerConfig.Port = 8000;
            peer = new NetPeer(peerConfig);
            peer.Start();
        }

        public void Update(float a_es)
        {
            if (peer.ConnectionsCount == 0)
            { peer.DiscoverKnownPeer("176.135.163.41", 8000); }
        }

        public int HandleWebConnections()
        {
            NetIncomingMessage message;
            while ((message = peer.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        return 1;

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

                    case NetIncomingMessageType.DiscoveryResponse:
                        peer.Connect("176.135.163.41", 8000, peer.CreateMessage("yes"));
                        break;

                    case NetIncomingMessageType.DiscoveryRequest:
                        var msg = peer.CreateMessage();
                        msg.Write("hi");
                        peer.SendDiscoveryResponse(msg, message.SenderEndPoint);
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        Console.WriteLine(message.ReadString());
                        break;

                    default:
                        Console.WriteLine("unhandled message with type: "
                            + message.MessageType);
                        break;
                }
                peer.Recycle(message);
            }
            return 0;
        }
    }
}