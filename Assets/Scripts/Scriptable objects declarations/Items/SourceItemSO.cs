using System;
using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations
{
	[CreateAssetMenu(menuName = "Scriptable objects/Items/SourceItemSO")]
	public sealed class SourceItemSO : ItemSO
	{
		[Serializable]
		public struct ProductChanceRange
		{
			public ItemSO Item;
			public Vector2Int Range;
		}

		public bool CanProduce = false;

		public bool IsFinite = false;

		public int Capacity = 1;

		public ProductChanceRange[] Products = new ProductChanceRange[0];
	}
}
