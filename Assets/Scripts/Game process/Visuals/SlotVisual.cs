using Merge.GameProcess.Logic;
using UnityEngine;

namespace Merge.GameProcess.Visuals
{
	public sealed class SlotVisual : MonoBehaviour
	{
		[SerializeField] private GameObject _iconGameObject;
		[SerializeField] private GameObject _selectedGameObject;
		[SerializeField] private GameObject _orderRequisite;
		[SerializeField] private GameObject _draggedGameObject;

		private Slot _slot;
		private SpriteRenderer _iconRenderer;

		private void Awake()
		{
			_slot = GetComponent<Slot>();
			_iconRenderer = _iconGameObject.GetComponent<SpriteRenderer>();

			_slot.ItemChanged += HandleItemChanged;
			_slot.SelectionChanged += HandleSelectionChanged;
			_slot.OrderRequisitionChanged += HandleOrderRequisitionChanged;
			_slot.DraggingStarted += HandleDraggingStarted;
			_slot.DraggingEnded += HandleDraggingEnded;
		}

		private void HandleItemChanged(Item item) => _iconRenderer.sprite = item?.ItemSO.Sprite;

		private void HandleSelectionChanged(bool selected) => _selectedGameObject.SetActive(selected);

		private void HandleOrderRequisitionChanged(bool requisite) => _orderRequisite.SetActive(requisite);

		private void HandleDraggingStarted() => _draggedGameObject.SetActive(true);

		private void HandleDraggingEnded() => _draggedGameObject.SetActive(false);
	}
}
