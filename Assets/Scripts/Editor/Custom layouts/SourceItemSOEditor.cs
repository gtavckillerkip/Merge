using Merge.Common.Utils;
using Merge.ScriptableObjectsDeclarations;
using UnityEditor;
using UnityEngine;
using static Merge.ScriptableObjectsDeclarations.SourceItemSO;

namespace Merge.UnityEditor.CustomLayouts
{
	[CustomEditor(typeof(SourceItemSO))]
	public sealed class SourceItemSOEditor : Editor
	{
		private bool _productsOpened = false;

		public override void OnInspectorGUI()
		{
			var t = target as SourceItemSO;

			t.Name = EditorGUILayout.TextField("Name", t.Name);
			t.Level = EditorGUILayout.IntField("Level", t.Level);
			t.ItemPack = EditorGUILayout.ObjectField("Item pack", t.ItemPack, typeof(ItemPackSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as ItemPackSO;
			t.Sprite = EditorGUILayout.ObjectField("Sprite", t.Sprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Sprite;

			EditorGUILayout.Space();

			t.CanProduce = EditorGUILayout.Toggle("Can produce items", t.CanProduce);
			if (t.CanProduce)
			{
				t.IsFinite = EditorGUILayout.Toggle("Is finite", t.IsFinite);
				if (t.IsFinite)
				{
					t.Capacity = EditorGUILayout.IntField("Capacity", t.Capacity);
				}

				t.Products = ProductRangeArrayField("Products", t.Products, ref _productsOpened);
			}

			if (GUI.changed)
			{
				EditorUtility.SetDirty(t);
			}
		}

		private ProductChanceRange[] ProductRangeArrayField(string label, ProductChanceRange[] array, ref bool open)
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

				for (var i = 0; i < size; i++)
				{
					array[i].Item = EditorGUILayout.ObjectField($"Item {i + 1}", array[i].Item, typeof(ItemSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as ItemSO;
					array[i].Range = EditorGUILayout.Vector2IntField("Drop chance range", array[i].Range, GUILayout.Height(EditorGUIUtility.singleLineHeight));
					EditorGUILayout.Space();
				}

				EditorGUI.indentLevel--;
			}

			return array;
		}
	}
}
