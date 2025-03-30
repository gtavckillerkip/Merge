using Merge.GameProcess.Logic;
using Merge.Patterns;
using Merge.ScriptableObjectsDeclarations;
using UnityEngine;

namespace Merge.Management
{
	public sealed class GameHandler : SingletonMB<GameHandler>
	{
		//#region Management
		//[field: SerializeField] public EconomicsManager EconomicsManager { get; private set; }

		//[field: SerializeField] public OrdersManager OrdersManager { get; private set; }
		//#endregion

		[field: SerializeField] public PlayingField PlayingField { get; private set; }
		
		[field: SerializeField] public ItemsLibrarySO ItemsLibrary { get; private set; }

		protected override void SingletonAwake() { }
	}
}
