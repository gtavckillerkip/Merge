using System;
using UnityEngine;

namespace Merge.GameProcess.Logic.InteractionWithGame
{
	public sealed class SlotClick : MonoBehaviour
	{
		[SerializeField] private MouseInteraction _mouseInteraction;

		public event Action<Slot> SlotClicked;

		private void Awake()
		{
			_mouseInteraction.SlotClicked += HandleSlotClicked;
		}

		private void HandleSlotClicked(Slot slot) => SlotClicked?.Invoke(slot);
	}
}
