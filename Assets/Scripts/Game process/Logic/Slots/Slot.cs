using Merge.Common.Enums;
using System;
using UnityEngine;

namespace Merge.GameProcess.Logic
{
	public sealed class Slot : MonoBehaviour
	{
		private Item _item;
		private bool _isSelected;
		private bool _isOrderRequisite;
		private bool _isDragging;

		public event Action<Item> ItemChanged;
		public event Action<bool> SelectionChanged;
		public event Action<bool> OrderRequisitionChanged;
		public event Action DraggingStarted;
		public event Action DraggingEnded;

		public Item Item
		{
			get => _item;
			set
			{
				_item = value;
				ItemChanged?.Invoke(value);
			}
		}

		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				SelectionChanged?.Invoke(value);
			}
		}

		public bool IsOrderRequisite
		{
			get => _isOrderRequisite;
			set
			{
				_isOrderRequisite = value;
				OrderRequisitionChanged?.Invoke(value);
			}
		}

		public bool IsDragging
		{
			get => _isDragging;
			set
			{
				_isDragging = value;
				if (value == true)
				{
					DraggingStarted?.Invoke();
				}
				else
				{
					DraggingEnded?.Invoke();
				}
			}
		}

		[field: SerializeField] public SlotType SlotType { get; private set; }
	}
}
