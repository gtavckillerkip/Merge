using Merge.ScriptableObjectsDeclarations;

namespace Merge.GameProcess.Logic
{
	public sealed class InventoryItem : AcceptorItem
	{
        public InventoryItem(InventoryItemSO itemSO) : base(itemSO)
        {
            
        }

        public new InventoryItemSO ItemSO => _itemSO as InventoryItemSO;

        public override void Accept(Item item) => PutIn(item);

        private void PutIn(Item item)
        {

        }
    }
}
