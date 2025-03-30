using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations
{
	[CreateAssetMenu(menuName = "Scriptable objects/ItemPackSO")]
	public sealed class ItemPackSO : ScriptableObject
	{
		public ItemSO[] Items;
	}
}
