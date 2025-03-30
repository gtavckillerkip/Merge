using Merge.ScriptableObjectsDeclarations;

namespace Merge.GameProcess.Logic
{
	public sealed class BinItem : AcceptorItem
	{
		public BinItem(BinItemSO itemSO) : base(itemSO)
		{

		}

		public new BinItemSO ItemSO => _itemSO as BinItemSO;

		public override void Accept(Item item) => Dispose(item);

		private void Dispose(Item item)
		{

		}
	}
}
