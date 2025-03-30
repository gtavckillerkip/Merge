using System;
using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations.GameProcess
{
	[CreateAssetMenu(menuName = "Scriptable objects/Game process/GameProgressSO")]
	public sealed class GameProgressSO : ScriptableObject
	{
		public struct ItemSO_TimePlaced
		{
			public ItemSO ItemSO;
			public DateTime TimePlaced;
		}

		public ItemSO[,] PlayingFieldItems;
		public ItemSO[,] InventoryItems;
		public ItemSO_TimePlaced[] BinItems;

		public StageSO Stage;

		public OrderSO[] OrdersNotPassed;

		public EconomicResourcesSO Economics;
	}
}
