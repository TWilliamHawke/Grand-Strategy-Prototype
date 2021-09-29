using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Espionage
{
		[System.Serializable]
		public class SpyNetworkLevel
		{
			public int requiredSpyPoints;
			public List<SpyAction> activeActions;
		}
}