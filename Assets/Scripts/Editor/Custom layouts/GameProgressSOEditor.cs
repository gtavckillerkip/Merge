using Merge.Common.Utils;
using Merge.ScriptableObjectsDeclarations;
using Merge.ScriptableObjectsDeclarations.GameProcess;
using System;
using UnityEditor;
using UnityEngine;
using static Merge.ScriptableObjectsDeclarations.GameProcess.GameProgressSO;

namespace Merge.UnityEditor.CustomLayouts
{
	[CustomEditor(typeof(GameProgressSO))]
	public sealed class GameProgressSOEditor : Editor
	{
		private bool _playingFieldArrayOpen;
		private bool[] _playingFieldArrayRowsOpen = new bool[0];

		private bool _inventoryArrayOpen;
		private bool[] _inventoryArrayRowsOpen = new bool[0];

		private bool _binArrayOpen;

		private bool _ordersArrayOpen;

		public override void OnInspectorGUI()
		{
			var t = target as GameProgressSO;

			t.PlayingFieldItems ??= new ItemSO[0,0];
			t.InventoryItems ??= new ItemSO[0,0];
			t.BinItems ??= new ItemSO_TimePlaced[0];
			t.OrdersNotPassed ??= new OrderSO[0];

			t.PlayingFieldItems = CreateDoubleArrayField("Playing field items", t.PlayingFieldItems, ref _playingFieldArrayOpen, ref _playingFieldArrayRowsOpen);
			t.InventoryItems = CreateDoubleArrayField("Inventory items", t.InventoryItems, ref _inventoryArrayOpen, ref _inventoryArrayRowsOpen);
			t.BinItems = BinItemsArrayField("Bin items", t.BinItems, ref _binArrayOpen);

			EditorGUILayout.Space();

			t.Stage = EditorGUILayout.ObjectField("Stage", t.Stage, typeof(StageSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as StageSO;

			EditorGUILayout.Space();

			t.OrdersNotPassed = OrdersArrayField("Orders not passed", t.OrdersNotPassed, ref _ordersArrayOpen);

			EditorGUILayout.Space();

			t.Economics = EditorGUILayout.ObjectField("Economics", t.Economics, typeof(EconomicResourcesSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as EconomicResourcesSO;

			if (GUI.changed)
			{
				EditorUtility.SetDirty(t);
			}
		}

		private T[,] CreateDoubleArrayField<T>(string label, T[,] array, ref bool open, ref bool[] rowsOpen) where T : UnityEngine.Object
		{
			open = EditorGUILayout.Foldout(open, label);

			if (open)
			{
				EditorGUI.indentLevel++;

				var rows = EditorGUILayout.IntField("Rows", array.GetLength(0));
				var cols = EditorGUILayout.IntField("Columns", array.GetLength(1));

				EditorGUILayout.Space();

				if (rows != array.GetLength(0) || cols != array.GetLength(1))
				{
					array = array.Resize(rows, cols);
				}

				if (rowsOpen.Length != array.GetLength(0))
				{
					rowsOpen = rowsOpen.Resize(array.GetLength(0));
				}

				for (int i = 0; i < array.GetLength(0); i++)
				{
					rowsOpen[i] = EditorGUILayout.Foldout(rowsOpen[i], $"Row {i}");

					if (rowsOpen[i])
					{
						EditorGUI.indentLevel++;

						for (int j = 0; j < array.GetLength(1); j++)
						{
							array[i, j] = EditorGUILayout.ObjectField($"Column {j}", array[i, j], typeof(T), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as T;
						}

						EditorGUI.indentLevel--;
					}
				}

				EditorGUI.indentLevel--;
			}

			return array;
		}

		private ItemSO_TimePlaced[] BinItemsArrayField(string label, ItemSO_TimePlaced[] array, ref bool open)
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
					array[i].ItemSO = EditorGUILayout.ObjectField($"Item {i}", array[i].ItemSO, typeof(ItemSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as ItemSO;
					array[i].TimePlaced = DateTime.Parse(EditorGUILayout.TextField("Time the item was placed in the bin", array[i].TimePlaced.ToString()));
				}

				EditorGUI.indentLevel--;
			}

			return array;
		}

		private OrderSO[] OrdersArrayField(string label, OrderSO[] array, ref bool open)
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
					array[i] = EditorGUILayout.ObjectField($"Order {i}", array[i], typeof(OrderSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as OrderSO;
				}

				EditorGUI.indentLevel--;
			}

			return array;
		}
	}
}
