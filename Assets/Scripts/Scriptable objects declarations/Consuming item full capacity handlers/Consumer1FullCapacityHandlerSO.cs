using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations
{
	[CreateAssetMenu(menuName = "Scriptable objects/Consumers full capacity handlers/Consumer1FullCapacityHandlerSO")]
	public sealed class Consumer1FullCapacityHandlerSO : ConsumingItemFullCapacityHandlerSO
	{
		public override void Handle()
		{
			Debug.Log("consumer 1 full capacity handled");
		}
	}
}
