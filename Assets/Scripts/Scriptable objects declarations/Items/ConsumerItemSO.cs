using System;
using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations
{
	[CreateAssetMenu(menuName = "Scriptable objects/Items/ConsumerItemSO")]
	public sealed class ConsumerItemSO : ItemSO
	{
		[Serializable]
		public struct ItemCapacityCost
		{
			public ItemSO Item;
			public int Cost;
		}

		public bool CanConsume = false;

		public int MaxCapacity = 1;

		public ItemCapacityCost[] Items = new ItemCapacityCost[0];

		public ConsumingItemFullCapacityHandlerSO FullCapacityHandler;
	}
}
