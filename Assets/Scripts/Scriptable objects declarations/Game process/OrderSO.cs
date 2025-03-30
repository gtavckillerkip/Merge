using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations.GameProcess
{
	[CreateAssetMenu(menuName = "Scriptable objects/Game process/OrderSO")]
	public sealed class OrderSO : ScriptableObject
	{
		public ItemSO[] Items;
		public int Cost;
	}
}
