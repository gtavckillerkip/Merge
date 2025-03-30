using System;
using System.Collections;
using UnityEngine;

namespace Merge.GameProcess.Logic
{
	public sealed class EnergyRestoreTimer : MonoBehaviour
	{
		private WaitForSeconds _energyRestoreTimer;

		[SerializeField] private int _energyRestoreTimeInSeconds;

		private Coroutine _energyRestoreCoroutine;

		public event Action Started;
		public event Action CountedDownOnce;
		public event Action Interrupted;

		public bool IsCounting { get; private set; }

		private void Awake()
		{
			_energyRestoreTimer = new(_energyRestoreTimeInSeconds);
		}

		public void StartCountDown()
		{
			_energyRestoreCoroutine = StartCoroutine(RestoreEnergyTimewise());
			IsCounting = true;
			Started?.Invoke();
		}

		public void StopCountDown()
		{
			StopCoroutine(_energyRestoreCoroutine);
			IsCounting = false;
			Interrupted?.Invoke();
		}

		private IEnumerator RestoreEnergyTimewise()
		{
			while (true)
			{
				yield return _energyRestoreTimer;
				CountedDownOnce?.Invoke();
			}
		}
	}
}
