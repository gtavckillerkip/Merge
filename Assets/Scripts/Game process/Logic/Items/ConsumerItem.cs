using Merge.ScriptableObjectsDeclarations;
using System.Linq;

namespace Merge.GameProcess.Logic
{
	public sealed class ConsumerItem : AcceptorItem
	{
		private int _currentCapacity;

		public ConsumerItem(ConsumerItemSO itemSO) : base(itemSO)
		{
			_currentCapacity = 0;
		}

		public new ConsumerItemSO ItemSO
		{
			get => _itemSO as ConsumerItemSO;
		}

		public override void Accept(Item item) => Consume(item);

		private void Consume(Item item)
		{
			if (TryConsumeItem(item))
			{
				_currentCapacity += ItemSO.Items.First((ic) => item.ItemSO == ic.Item).Cost;

				if (_currentCapacity >= ItemSO.MaxCapacity)
				{
					if (ItemSO.FullCapacityHandler != null)
					{
						ItemSO.FullCapacityHandler.Handle();
					}
				}
			}
		}

		private bool TryConsumeItem(Item consumimg)
		{
			if (ItemSO.Items.Any((ic) => ic.Item == consumimg.ItemSO))
			{
				consumimg.GetReadyToBeRemoved();

				return true;
			}

			return false;
		}
	}
}
