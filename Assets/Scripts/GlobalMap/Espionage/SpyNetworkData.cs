using GlobalMap.Factions;

namespace GlobalMap.Espionage
{
	public class SpyNetworkData
	{
	    public Faction faction;
		public byte level;
		public float spyPoints;
		public float visibility;

        public SpyNetworkData(Faction faction)
        {
            this.faction = faction;
        }
    }
}