using Merge.ScriptableObjectsDeclarations;

namespace Merge.GameProcess.Logic
{
	public sealed class SourceItem : Item
	{
		private int _capacity;

		public SourceItem(SourceItemSO itemSO) : base(itemSO)
		{
			_capacity = itemSO.Capacity;
		}

		public new SourceItemSO ItemSO
		{
			get => _itemSO as SourceItemSO;
		}

		public Item Produce()
		{
			var productSO = TakeRandomItemSO();

			Item product = null;
			switch (productSO)
			{
				case SourceItemSO sourceSO:
					product = new SourceItem(sourceSO);
					break;

				case ConsumerItemSO consSO:
					product = new ConsumerItem(consSO);
					break;

				case SimpleItemSO simpleSO:
					product = new SimpleItem(simpleSO);
					break;
			}

			if (ItemSO.IsFinite)
			{
				_capacity--;

				if (_capacity == 0)
				{
					OnGotReadyToBeRemoved();
				}
			}

			return product;
		}

		private ItemSO TakeRandomItemSO()
		{
			var randomValue = UnityEngine.Random.Range(1, 100);
			ItemSO productItemSO = null;

			for (int i = 0; i < ItemSO.Products.Length; i++)
			{
				if (randomValue >= ItemSO.Products[i].Range.x && randomValue <= ItemSO.Products[i].Range.y)
				{
					productItemSO = ItemSO.Products[i].Item;
					break;
				}
			}

			return productItemSO;
		}
	}
}
