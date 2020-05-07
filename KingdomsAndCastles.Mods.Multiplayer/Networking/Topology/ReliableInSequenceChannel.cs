namespace KingdomsAndCastles.Mods.Multiplayer.Networking.Topology
{
    public class ReliableInSequenceChannel : IChannel
    {
        private int Id { get; set; }
        
        public ReliableInSequenceChannel(int id)
        {
            Id = id;
        }
    }
}