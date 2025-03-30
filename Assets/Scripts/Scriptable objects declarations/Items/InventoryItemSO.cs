using Merge.GameProcess.Logic;
using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations
{
	[CreateAssetMenu(menuName = "Scriptable objects/Items/InventoryItemSO")]
	public sealed class InventoryItemSO : ItemSO
	{
		private readonly Item[,] _items = new Item[2, 5];
	}
}
