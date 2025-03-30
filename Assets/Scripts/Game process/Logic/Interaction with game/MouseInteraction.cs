using System;
using UnityEngine;

namespace Merge.GameProcess.Logic.InteractionWithGame
{
	public sealed class MouseInteraction : MonoBehaviour
	{
		private readonly float _sqrMaxMouseIdleDrag = 100f;
		private bool _isMouseDragging = false;
		private Vector3 _mouse0DownPosition;
		private Slot _hitSlot;

		public event Action<Slot> SlotDragStarted;
		public event Action<Slot> SlotDragFinished;
		public event Action<Slot> SlotClicked;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				_mouse0DownPosition = Input.mousePosition;
				_hitSlot = GetHitSlot();
			}

			if (Input.GetKey(KeyCode.Mouse0))
			{
				if (_isMouseDragging == false)
				{
					if (_hitSlot != null)
					{
						if ((Input.mousePosition - _mouse0DownPosition).sqrMagnitude > _sqrMaxMouseIdleDrag)
						{
							_isMouseDragging = true;

							SlotDragStarted?.Invoke(_hitSlot);
						}
					}
				}
			}

			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				if (_hitSlot != null)
				{
					if (_isMouseDragging == false)
					{
						SlotClicked?.Invoke(_hitSlot);
					}
					else
					{
						_isMouseDragging = false;
						SlotDragFinished?.Invoke(_hitSlot);
					}
				}
			}
		}

		private Slot GetHitSlot()
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			var hit = Physics2D.Raycast(ray.origin, ray.direction);

			if (hit && hit.collider != null)
			{
				return hit.collider.gameObject.GetComponent<Slot>();
			}

			return null;
		}
	}
}
