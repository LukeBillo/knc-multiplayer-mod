using System;
using KingdomsAndCastles.Mods.Multiplayer.ErrorHandling.Exceptions;
using KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.Packets;

namespace KingdomsAndCastles.Mods.Multiplayer.Networking.Protocol.PacketHandlers
{
    public interface IPacketHandler
    {
        short HandlesPacketType { get; }
        Packet Invoke(Packet request);
    }
    
    public abstract class PacketHandler<TRequest, TResponse> : IPacketHandler
        where TRequest : Packet, new()
        where TResponse : Packet, new()
    {
        public abstract short HandlesPacketType { get; }
        
        public Packet Invoke(Packet packet)
        {
            if (!(packet is TRequest requestPacket))
            {
                throw new PacketHandlerInvocationException($"The {nameof(Packet)} passed to {nameof(IPacketHandler)}.{nameof(Invoke)} could not be cast to the derived packet type");
            }
            
            return Handle(requestPacket);
        }

        public abstract TResponse Handle(TRequest request);
    }
}