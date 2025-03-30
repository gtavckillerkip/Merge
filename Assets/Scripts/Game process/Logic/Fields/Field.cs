using Merge.GameProcess.Logic.InteractionWithGame;
using System;
using UnityEngine;

namespace Merge.GameProcess.Logic
{
	public abstract class Field : MonoBehaviour
	{
		[SerializeField] protected Slot[] _slots;

		protected Slot _selectedSlot;

		[SerializeField] protected PlayerActions _playerActions;

		public event Action<Item, Slot> ItemPlaced;
		public event Action<Item, Slot> ItemRemoved;
		public event Action<Item, Slot, Slot> ItemReplaced;
		public event Action<Item, Item, Slot, Slot> ItemsSwitched;

		protected void OnItemPlaced(Item item, Slot slot) => ItemPlaced?.Invoke(item, slot);
		protected void OnItemRemoved(Item item, Slot slot) => ItemRemoved?.Invoke(item, slot);
		protected void OnItemReplaced(Item item, Slot slot1, Slot slot2) => ItemReplaced?.Invoke(item, slot1, slot2);
		protected void OnItemSwitched(Item item1, Item item2, Slot slot1, Slot slot2) => ItemsSwitched?.Invoke(item1, item2, slot1, slot2);

		public int FreeSlotsAmount { get; private set; } = 70;

		protected void PlaceItem(Item item, Slot slot)
		{
			slot.Item = item;
			item.GotReadyToBeRemoved += () => RemoveItem(slot);
			FreeSlotsAmount--;
			ItemPlaced?.Invoke(item, slot);
		}

		protected void RemoveItem(Slot slot)
		{
			var removingItem = slot.Item;
			slot.Item = null;
			FreeSlotsAmount++;
			ItemRemoved?.Invoke(removingItem, slot);
		}

		protected void ReplaceItem(Slot from, Slot to)
		{
			to.Item = from.Item;
			from.Item = null;
			ItemReplaced?.Invoke(to.Item, from, to);
		}

		protected void SwitchItems(Slot slot1, Slot slot2)
		{
			(slot1.Item, slot2.Item) = (slot2.Item, slot1.Item);

			ItemsSwitched?.Invoke(slot2.Item, slot1.Item, slot1, slot2);
		}

		protected Slot GetFreeSlot()
		{
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (_slots[i * 10 + j].Item == null)
					{
						return _slots[i * 10 + j];
					}
				}
			}

			return null;
		}

		protected void SwitchSelectedSlot(Slot newSelectedSlot)
		{
			if (_selectedSlot != null)
			{
				_selectedSlot.IsSelected = false;
			}

			_selectedSlot = newSelectedSlot;

			_selectedSlot.IsSelected = true;
		}
	}
}
