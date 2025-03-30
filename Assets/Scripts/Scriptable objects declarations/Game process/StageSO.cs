using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations.GameProcess
{
	[CreateAssetMenu(menuName = "Scriptable objects/Game process/StageSO")]
	public sealed class StageSO : ScriptableObject
	{
		public OrderSO[] Orders;
	}
}
