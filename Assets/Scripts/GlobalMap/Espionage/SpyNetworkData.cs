using GlobalMap.Factions;

namespace GlobalMap.Espionage
{
	public class SpyNetworkData
	{
	    public Faction faction;
		public byte level;
		public float spyPoints;
		public float visibility;

		int _guardStatePointer;
		GuardState[] _guardStates;

		const int GUARD_STATE_COUNT = 4;



        public SpyNetworkData(Faction faction)
        {
            this.faction = faction;
			_guardStates = faction.factionData.guardStates;
        }

		public void ChangeGuardState()
		{
			_guardStatePointer++;
			_guardStatePointer %= GUARD_STATE_COUNT;
		}

		public GuardState GetGuardState(int pointer)
		{
			int index = (_guardStatePointer + pointer) % GUARD_STATE_COUNT;
			return _guardStates[index];
		}
    }
}