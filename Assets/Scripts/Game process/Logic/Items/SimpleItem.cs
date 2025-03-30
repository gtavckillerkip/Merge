using Merge.ScriptableObjectsDeclarations;

namespace Merge.GameProcess.Logic
{
	public sealed class SimpleItem : Item
	{
		public SimpleItem(SimpleItemSO itemSO) : base(itemSO) { }

		public new SimpleItemSO ItemSO
		{
			get => _itemSO as SimpleItemSO;
		}
    }
}
