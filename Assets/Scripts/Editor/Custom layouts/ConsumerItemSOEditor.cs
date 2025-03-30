using Merge.Common.Utils;
using Merge.ScriptableObjectsDeclarations;
using UnityEditor;
using UnityEngine;
using static Merge.ScriptableObjectsDeclarations.ConsumerItemSO;

namespace Merge.UnityEditor.CustomLayouts
{
	[CustomEditor(typeof(ConsumerItemSO))]
	public sealed class ConsumerItemSOEditor : Editor
	{
		private bool _itemsOpened = false;

		public override void OnInspectorGUI()
		{
			var t = target as ConsumerItemSO;

			t.Name = EditorGUILayout.TextField("Name", t.Name);
			t.Level = EditorGUILayout.IntField("Level", t.Level);
			t.ItemPack = EditorGUILayout.ObjectField("Item pack", t.ItemPack, typeof(ItemPackSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as ItemPackSO;
			t.Sprite = EditorGUILayout.ObjectField("Sprite", t.Sprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Sprite;

			EditorGUILayout.Space();

			t.CanConsume = EditorGUILayout.Toggle("Can consume", t.CanConsume);
			if (t.CanConsume)
			{
				t.MaxCapacity = EditorGUILayout.IntField("Max capacity", t.MaxCapacity);

				t.Items = ItemCostArrayField("Consumings", t.Items, ref _itemsOpened);

				t.FullCapacityHandler = EditorGUILayout.ObjectField("Full capacity handler", t.FullCapacityHandler, typeof(ConsumingItemFullCapacityHandlerSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as ConsumingItemFullCapacityHandlerSO;
			}

			if (GUI.changed)
			{
				EditorUtility.SetDirty(t);
			}
		}

		private ItemCapacityCost[] ItemCostArrayField(string label, ItemCapacityCost[] array, ref bool open)
		{
			open = EditorGUILayout.Foldout(open, label);

			if (open)
			{
				EditorGUI.indentLevel++;

				var size = EditorGUILayout.IntField("Amount", array.Length);
				size = size < 0 ? 0 : size;

				EditorGUILayout.Space();

				if (size != array.Length)
				{
					array = array.Resize(size);
				}

				for (int i = 0; i < size; i++)
				{
					array[i].Item = EditorGUILayout.ObjectField($"Item {i + 1}", array[i].Item, typeof(ItemSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as ItemSO;
					array[i].Cost = EditorGUILayout.IntField("Cost (impact)", array[i].Cost);
					EditorGUILayout.Space();
				}

				EditorGUI.indentLevel--;
			}

			return array;
		}
	}
}
