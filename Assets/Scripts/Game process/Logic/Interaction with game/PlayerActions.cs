using System;
using UnityEngine;

namespace Merge.GameProcess.Logic.InteractionWithGame
{
	public sealed class PlayerActions : MonoBehaviour
	{
		[SerializeField] private SlotClick _slotClick;
		[SerializeField] private SlotDragAndDrop _slotDragAndDrop;

		public event Action<Slot> SlotClicked;
		public event Action<Slot> SlotDragStarted;
		public event Action<Slot, Slot> SlotDragFinished;

		private void Awake()
		{
			_slotClick.SlotClicked += HandleSlotClicked;
			_slotDragAndDrop.SlotDragStarted += HandleSlotDragStarted;
			_slotDragAndDrop.SlotDragFinished += HandleSlotDragFinished;
		}

		private void HandleSlotClicked(Slot slot) => SlotClicked?.Invoke(slot);

		private void HandleSlotDragStarted(Slot slot) => SlotDragStarted?.Invoke(slot);

		private void HandleSlotDragFinished(Slot slot, Slot targetSlot) => SlotDragFinished?.Invoke(slot, targetSlot);
	}
}
