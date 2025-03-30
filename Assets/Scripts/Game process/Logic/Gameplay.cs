using Merge.Common.Enums;
using Merge.Management;
using Merge.ScriptableObjectsDeclarations;
using System;

namespace Merge.GameProcess.Logic
{
	public static class Gameplay
	{
		public static event Action<Item> ItemProduced;
		public static event Action<Item, AcceptorItem> ItemAccepted;
		public static event Action<Item, Item, Item> ItemsMerged;
		public static event Action<Item> ItemRemoved;
		public static event Action<Item> ItemRestored;
		public static event Action<ItemProductionResult> ItemProductionFailed;

		public static bool TryProduceItem(SourceItem source, out Item product)
		{
			if (EconomicsManager.Instance.GetEnergyAmount() <= 0)
			{
				product = null;
				ItemProductionFailed?.Invoke(ItemProductionResult.InsufficientEnergy);
				return false;
			}

			if (GameHandler.Instance.PlayingField.FreeSlotsAmount <= 0)
			{
				product = null;
				ItemProductionFailed?.Invoke(ItemProductionResult.InsufficientRoom);
				return false;
			}

			if (source.ItemSO.Products.Length == 0)
			{
				product = null;
				return false;
			}

			product = ProduceItem(source);
			if (product == null)
			{
				return false;
			}

			return true;
		}

		private static Item ProduceItem(SourceItem source)
		{
			var product = source.Produce();

			ItemProduced?.Invoke(product);

			return product;
		}

		public static void AcceptItem(Item item, AcceptorItem acceptor)
		{
			acceptor.Accept(item);

			ItemAccepted?.Invoke(item, acceptor);
		}

		public static bool TryMergeItems(Item item1, Item item2, out Item result)
		{
			if (item1.ItemSO.ItemPack == item2.ItemSO.ItemPack &&
				item1.ItemSO.Level == item2.ItemSO.Level &&
				item1.ItemSO.Level != item1.ItemSO.ItemPack.Items.Length)
			{
				result = MergeItems(item1, item2);
				return true;
			}
			else
			{
				result = null;
				return false;
			}
		}

		private static Item MergeItems(Item item1, Item item2)
		{
			Item result = null;
			switch (item1.ItemSO)
			{
				case SourceItemSO sourceSO:
					result = new SourceItem(sourceSO.ItemPack.Items[sourceSO.Level] as SourceItemSO);
					break;

				case ConsumerItemSO consSO:
					result = new ConsumerItem(consSO.ItemPack.Items[consSO.Level] as ConsumerItemSO);
					break;

				case SimpleItemSO simpleSO:
					result = new SimpleItem(simpleSO.ItemPack.Items[simpleSO.Level] as SimpleItemSO);
					break;
			}

			ItemsMerged?.Invoke(item1, item2, result);

			return result;
		}

		public static void RemoveItem(Item item)
		{


			ItemRemoved?.Invoke(item);
		}

		public static void RestoreItem(Item item)
		{


			ItemRestored?.Invoke(item);
		}
	}
}
