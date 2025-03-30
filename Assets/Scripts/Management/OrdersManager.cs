using Merge.GameProcess.Logic;
using Merge.GameProcess.Logic.GameProgress;
using Merge.Patterns;
using Merge.ScriptableObjectsDeclarations;
using Merge.ScriptableObjectsDeclarations.GameProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Merge.Management
{
	public sealed class OrdersManager : SingletonMB<OrdersManager>
	{
		[SerializeField] private GameProgressSO _currentGameProgressSO;

		private Stage _stage;

		private int _currentOrdersAmount;

		public IEnumerable<Order> CurrentOrders { get; private set; }

		#region Events for the playing field visuals
		public event Action<Slot> AppropriateItemPlaced;
		public event Action<Slot> AppropriateItemRemoved;
		public event Action<Slot, Slot> AppropriateItemReplaced;
		#endregion

		#region Events for orders visuals
		public event Action<Order, ItemSO> FirstAppropriateItemPlaced;
		public event Action<Order, ItemSO> LastAppropriateItemRemoved;

		public event Action<Order> OrderBecameReadyToPass;
		public event Action<Order> OrderBecameUnreadyToPass;
		#endregion

		protected override void SingletonAwake() { }

		private void Start()
		{
			var orders = new List<Order>();
			orders.AddRange(_currentGameProgressSO.OrdersNotPassed.Select(orderSO => new Order(orderSO)));
			//_stage = _currentGameProgressSO.Stage;

			_currentOrdersAmount = 4;
			CurrentOrders = new List<Order>();

			for (int i = 0; i < orders.Count && (CurrentOrders as List<Order>).Count < _currentOrdersAmount; i++)
			{
				if (orders[i].IsPassed == false)
				{
					(CurrentOrders as List<Order>).Add(orders[i]);
				}
			}

			GameHandler.Instance.PlayingField.ItemPlaced += HandleItemPlaced;
			GameHandler.Instance.PlayingField.ItemRemoved += HandleItemRemoved;
			GameHandler.Instance.PlayingField.ItemReplaced += HandleItemReplaced;
			GameHandler.Instance.PlayingField.ItemsSwitched += HandleItemsSwitched;

			foreach (var order in orders)
			{
				order.Passed += HandleOrderPassed;
			}
		}

		private void HandleItemPlaced(Item item, Slot slot)
		{
			var orders = CurrentOrders.Where(order => order.HasItemSO(item.ItemSO));

			foreach (var order in orders)
			{
				order.TryAddSlot(item.ItemSO, slot);

				if (order.GetSlots(item.ItemSO).Count() == 1)
				{
					FirstAppropriateItemPlaced?.Invoke(order, item.ItemSO);
				}

				if (order.IsReadyToPass)
				{
					OrderBecameReadyToPass?.Invoke(order);
				}
			}

			if (orders.Any())
			{
				AppropriateItemPlaced?.Invoke(slot);
			}
		}

		private void HandleItemRemoved(Item item, Slot slot)
		{
			var orders = CurrentOrders.Where(order => order.HasItemSO(item.ItemSO));

			foreach (var order in orders)
			{
				order.TryRemoveSlot(item.ItemSO, slot);

				if (order.GetSlots(item.ItemSO).Count() == 0)
				{
					LastAppropriateItemRemoved?.Invoke(order, item.ItemSO);

					OrderBecameUnreadyToPass?.Invoke(order);
				}
			}

			if (orders.Any())
			{
				AppropriateItemRemoved?.Invoke(slot);
			}
		}

		private void HandleItemReplaced(Item item, Slot slot1, Slot slot2)
		{
			var orders = CurrentOrders.Where(order => order.HasItemSO(item.ItemSO));

			foreach (var order in orders)
			{
				order.TryRemoveSlot(item.ItemSO, slot1);
				order.TryAddSlot(item.ItemSO, slot2);

				AppropriateItemReplaced?.Invoke(slot1, slot2);
			}
		}

		private void HandleItemsSwitched(Item item1, Item item2, Slot slot1, Slot slot2)
		{
			HandleItemReplaced(item1, slot1, slot2);
			HandleItemReplaced(item2, slot2, slot1);
		}

		private void HandleOrderPassed(Order order)
		{
			_currentGameProgressSO.OrdersNotPassed = _currentGameProgressSO.OrdersNotPassed.Where(orderSO => orderSO != order.OrderSO).ToArray();

			(CurrentOrders as List<Order>).Remove(order);

			for (int i = 0; i < _currentGameProgressSO.OrdersNotPassed.Length && (CurrentOrders as List<Order>).Count < _currentOrdersAmount; i++)
			{
				(CurrentOrders as List<Order>).Add(new(_currentGameProgressSO.OrdersNotPassed[i]));
			}
		}
	}
}
