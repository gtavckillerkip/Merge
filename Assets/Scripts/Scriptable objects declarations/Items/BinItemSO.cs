using Merge.GameProcess.Logic;
using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations
{
	[CreateAssetMenu(menuName = "Scriptable objects/Items/BinItemSO")]
	public sealed class BinItemSO : ItemSO
	{
		private readonly Item[] _items = new Item[5]; 
	}
}
