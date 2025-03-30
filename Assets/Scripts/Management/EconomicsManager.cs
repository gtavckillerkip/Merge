using Merge.GameProcess.Logic;
using Merge.Patterns;
using Merge.ScriptableObjectsDeclarations.GameProcess;
using UnityEngine;

namespace Merge.Management
{
	public sealed class EconomicsManager : SingletonMB<EconomicsManager>
	{
		[SerializeField] private EnergyRestoreTimer _energyRestoreTimer;

		[SerializeField] private EconomicResourcesSO _economicResourcesSO;

		protected override void SingletonAwake()
		{
			_energyRestoreTimer.CountedDownOnce += HandleCountedDownOnce;
			Gameplay.ItemProduced += HandleItemPlaced;
			Gameplay.ItemRemoved += HandleItemRemoved;
			Gameplay.ItemRestored += HandleItemRestored;
		}

		private void Start()
		{
			if (_economicResourcesSO.Energy.Value < _economicResourcesSO.Energy.MaxValue)
			{
				_energyRestoreTimer.StartCountDown();
			}
		}

		public int GetEnergyAmount() => _economicResourcesSO.Energy.Value;

		private void HandleCountedDownOnce()
		{
			_economicResourcesSO.Energy.Value++;

			if (_economicResourcesSO.Energy.Value >= _economicResourcesSO.Energy.MaxValue)
			{
				_energyRestoreTimer.StopCountDown();
			}
		}

		private void HandleItemPlaced(Item item)
		{
			_economicResourcesSO.Energy.Value--;

			if (_energyRestoreTimer.IsCounting == false && _economicResourcesSO.Energy.Value < _economicResourcesSO.Energy.MaxValue)
			{
				_energyRestoreTimer.StartCountDown();
			}
		}

		private void HandleItemRemoved(Item item)
		{
			// add money
		}

		private void HandleItemRestored(Item item)
		{
			// subtract money
		}
	}
}
