using Merge.Common;
using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations.GameProcess
{
	[CreateAssetMenu(menuName = "Scriptable objects/Game process/EconomicResourcesSO")]
	public sealed class EconomicResourcesSO : ScriptableObject
	{
		public Energy Energy;
		public Wallet Wallet;
	}
}
