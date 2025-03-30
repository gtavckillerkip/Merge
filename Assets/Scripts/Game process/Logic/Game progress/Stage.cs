using System.Collections.Generic;

namespace Merge.GameProcess.Logic.GameProgress
{
	public sealed class Stage
	{
		private readonly Dictionary<Order, bool> _ordersPassed = new();

        private int _ordersPassedAmount = 0;

        public Stage(Order[] orders)
        {
            foreach (var order in orders)
            {
                _ordersPassed[order] = false;
				order.Passed += HandleOrderPassed;
            }
        }

        public bool IsCompleted { get; private set; } = false;

        public bool IsOrderPassed(Order order) => _ordersPassed[order];

        public IEnumerable<Order> GetOrders() => _ordersPassed.Keys;

		private void HandleOrderPassed(Order order)
		{
            _ordersPassed[order] = true;
            _ordersPassedAmount++;

            if (_ordersPassedAmount == _ordersPassed.Count)
            {
                IsCompleted = true;
            }
		}
    }
}
