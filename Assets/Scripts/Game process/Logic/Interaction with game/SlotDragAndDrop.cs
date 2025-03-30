using Merge.Common.Enums;
using System;
using UnityEngine;

namespace Merge.GameProcess.Logic.InteractionWithGame
{
	public sealed class SlotDragAndDrop : MonoBehaviour
	{
		[SerializeField] private MouseInteraction _mouseInteraction;

		[SerializeField] private GameObject _draggableGameObject;
		private SpriteRenderer _draggableGameObjectSpriteRenderer;

		private Item _draggingItem;

		public event Action<Slot> SlotDragStarted;
		public event Action<Slot, Slot> SlotDragFinished;

		private void Awake()
		{
			_draggableGameObjectSpriteRenderer = _draggableGameObject.GetComponent<SpriteRenderer>();

			_mouseInteraction.SlotDragStarted += HandleSlotDragStarted;
			_mouseInteraction.SlotDragFinished += HandleSlotDragFinished;
		}

		private void Update()
		{
			if (_draggableGameObject != null && _draggableGameObject.activeSelf)
			{
				var cursorWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				_draggableGameObject.transform.position = new Vector3(cursorWorldPoint.x, cursorWorldPoint.y, _draggableGameObject.transform.position.z);
			}
		}

		private void HandleSlotDragStarted(Slot slot)
		{
			if (slot.Item != null && slot.SlotType == SlotType.Simple)
			{
				_draggingItem = slot.Item;
				_draggableGameObjectSpriteRenderer.sprite = slot.Item.ItemSO.Sprite;
				_draggableGameObject.SetActive(true);

				slot.IsDragging = true;

				SlotDragStarted?.Invoke(slot);
			}
		}

		private void HandleSlotDragFinished(Slot slot)
		{
			if (_draggingItem == null)
			{
				return;
			}

			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hit = Physics2D.Raycast(ray.origin, ray.direction);

			Slot targetSlot = null;

			if (hit && hit.collider != null)
			{
				targetSlot = hit.collider.GetComponent<Slot>();
			}

			_draggableGameObject.SetActive(false);
			_draggingItem = null;

			slot.IsDragging = false;

			SlotDragFinished?.Invoke(slot, targetSlot);
		}
	}
}
