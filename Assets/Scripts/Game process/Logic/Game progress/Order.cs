using Merge.ScriptableObjectsDeclarations;
using Merge.ScriptableObjectsDeclarations.GameProcess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Merge.GameProcess.Logic.GameProgress
{
	public sealed class Order
	{
		private readonly Dictionary<ItemSO, List<Slot>> _itemsSlots = new();

        public event Action<Order> Passed;

        public Order(OrderSO orderSO)
        {
            OrderSO = orderSO;

            foreach (var itemSO in orderSO.Items)
            {
                _itemsSlots[itemSO] = new();
            }
        }

        public OrderSO OrderSO { get; private set; }

        public bool IsReadyToPass
        {
            get => _itemsSlots.All(itemSlots => itemSlots.Value.Count > 0);
        }

        public bool IsPassed { get; private set; } = false;

        public bool HasItemSO(ItemSO itemSO) => _itemsSlots.ContainsKey(itemSO);

        public IEnumerable<Slot> GetSlots(ItemSO itemSO)
        {
            if (HasItemSO(itemSO))
            {
                return _itemsSlots[itemSO];
            }

            return null;
        }

        public bool TryAddSlot(ItemSO itemSO, Slot slot)
        {
            if (_itemsSlots.ContainsKey(itemSO))
            {
                _itemsSlots[itemSO].Add(slot);

                return true;
            }

            return false;
        }

        public bool TryRemoveSlot(ItemSO itemSO, Slot slot)
        {
            if (_itemsSlots.ContainsKey(itemSO))
            {
                _itemsSlots[itemSO].Remove(slot);
                return true;
            }

            return false;
        }

        public bool TryPass()
        {
            if (IsReadyToPass)
            {
                _itemsSlots.Clear();
                IsPassed = true;
                Passed?.Invoke(this);
                return true;
            }

            return false;
        }
    }
}
