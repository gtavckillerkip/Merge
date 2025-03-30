using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations
{
	[CreateAssetMenu(menuName = "Scriptable objects/ItemsLibrarySO")]
	public sealed class ItemsLibrarySO : ScriptableObject
	{
		[field: SerializeField] public ItemSO[] Items { get; private set; }

		[field: SerializeField] public InventoryItemSO InventoryItemSO { get; private set; }
		[field: SerializeField] public BinItemSO BinItemSO { get; private set; }
	}
}
