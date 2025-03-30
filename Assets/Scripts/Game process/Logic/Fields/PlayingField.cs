using Merge.Management;
using Merge.ScriptableObjectsDeclarations;
using UnityEngine;

namespace Merge.GameProcess.Logic
{
	public sealed class PlayingField : Field
	{
		[SerializeField] private Slot _inventorySlot;
		[SerializeField] private Slot _binSlot;

		private void Awake()
		{
			_playerActions.SlotClicked += HandleSlotClicked;
			_playerActions.SlotDragStarted += HandleSlotDragStarted;
			_playerActions.SlotDragFinished += HandleSlotDragFinished;
		}

		private void Start()
		{
			PlaceItem(new SourceItem(GameHandler.Instance.ItemsLibrary.Items[1] as SourceItemSO), _slots[0]); // temp
			PlaceItem(new SourceItem(GameHandler.Instance.ItemsLibrary.Items[2] as SourceItemSO), _slots[1]); // temp
			PlaceItem(new ConsumerItem(GameHandler.Instance.ItemsLibrary.Items[3] as ConsumerItemSO), _slots[9]); // temp

			PlaceItem(new InventoryItem(GameHandler.Instance.ItemsLibrary.InventoryItemSO), _inventorySlot);
			PlaceItem(new BinItem(GameHandler.Instance.ItemsLibrary.BinItemSO), _binSlot);

			OrdersManager.Instance.AppropriateItemPlaced += HandleAppropriateItemPlaced;
			OrdersManager.Instance.AppropriateItemRemoved += HandleAppropriateItemRemoved;
			OrdersManager.Instance.AppropriateItemReplaced += HandleAppropriateItemReplaced;
		}

		private void HandleSlotClicked(Slot slot)
		{
			if (slot != _selectedSlot)
			{
				if (slot != null && slot.Item != null)
				{
					SwitchSelectedSlot(slot);
				}
			}
			else
			{
				switch (slot.Item)
				{
					case SourceItem sourceItem:
						if (Gameplay.TryProduceItem(sourceItem, out var product))
						{
							PlaceItem(product, GetFreeSlot());
						}
						break;

					case InventoryItem:
						break;

					case BinItem:
						break;
				}
			}
		}

		private void HandleSlotDragStarted(Slot slot)
		{
			if (slot == _selectedSlot)
			{
				_selectedSlot.IsSelected = false;
				_selectedSlot = null;
			}
		}

		private void HandleSlotDragFinished(Slot sourceSlot, Slot targetSlot)
		{
			var draggingItem = sourceSlot.Item;
			var targetItem = targetSlot.Item;

			if (targetItem == null)
			{
				ReplaceItem(sourceSlot, targetSlot);
			}
			else
			{
				if (targetItem is AcceptorItem acceptor)
				{
					Gameplay.AcceptItem(draggingItem, acceptor);
				}
				else
				{
					if (Gameplay.TryMergeItems(draggingItem, targetItem, out var result))
					{
						RemoveItem(sourceSlot);
						RemoveItem(targetSlot);

						PlaceItem(result, targetSlot);
					}
					else
					{
						SwitchItems(sourceSlot, targetSlot);
					}
				}
			}
		}

		private void HandleAppropriateItemPlaced(Slot slot)
		{
			slot.IsOrderRequisite = true;
		}

		private void HandleAppropriateItemRemoved(Slot slot)
		{
			slot.IsOrderRequisite = false;
		}

		private void HandleAppropriateItemReplaced(Slot slot1, Slot slot2)
		{
			slot1.IsOrderRequisite = false;
			slot2.IsOrderRequisite = true;
		}
	}
}
