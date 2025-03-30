using Merge.ScriptableObjectsDeclarations;
using System;

namespace Merge.GameProcess.Logic
{
	public abstract class Item
	{
		protected ItemSO _itemSO;

		public event Action GotReadyToBeRemoved;

		protected void OnGotReadyToBeRemoved() => GotReadyToBeRemoved?.Invoke();

		public Item(ItemSO itemSO)
		{
			_itemSO = itemSO;
		}

		public ItemSO ItemSO
		{
			get => _itemSO;
		}

		public void GetReadyToBeRemoved()
		{
			GotReadyToBeRemoved?.Invoke();
		}
	}
}
