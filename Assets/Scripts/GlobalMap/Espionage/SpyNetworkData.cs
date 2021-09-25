using GlobalMap.Factions;

namespace GlobalMap.Espionage
{
	public struct SpyNetworkData
	{
	    public Faction faction;
		public byte level;
		public float spyPoints;
		public float detectionThreat;

        public SpyNetworkData(Faction faction) : this()
        {
            this.faction = faction;
        }
    }
}