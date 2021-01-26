using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CarGo.UI.Button
{
    [RequireComponent(typeof(Image))]
	public class Button : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
	{
		public event Action ButtonDown;
		public event Action ButtonUp;
		public event Action ButtonExit;
		
		public Image _Image { get; protected set; }
		
		private bool _active = true;
		private bool _pressed = false;

		private void Awake()
		{
			_Image = GetComponent<Image>();
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (!_active) return;

			_pressed = true;
			ButtonDown?.Invoke();
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!_active || !_pressed) return;
			if (eventData.dragging) return;

			_pressed = false;
			ButtonUp?.Invoke();
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (!_active) return;

			_pressed = false;
			ButtonExit?.Invoke();
		}


		protected virtual void OnDestroy()
		{
			ButtonDown = null;
			ButtonUp = null;
			ButtonExit = null;
		}
	}
}
