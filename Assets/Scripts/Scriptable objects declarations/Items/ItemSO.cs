using UnityEngine;

namespace Merge.ScriptableObjectsDeclarations
{
	public abstract class ItemSO : ScriptableObject
	{
		public string Name;

		public int Level;

		public ItemPackSO ItemPack;

		public Sprite Sprite;
	}
}
