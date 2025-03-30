using Merge.ScriptableObjectsDeclarations;

namespace Merge.GameProcess.Logic
{
	public abstract class AcceptorItem : Item
	{
		public AcceptorItem(ItemSO itemSO) : base(itemSO) { }

		public abstract void Accept(Item item);
    }
}
