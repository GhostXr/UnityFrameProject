using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace QUIFrameWork
{
	public class TouchHandle
	{
		private event OnTouchEventHandle eventHandle;
		private object[] handleParams;

		public TouchHandle(OnTouchEventHandle _handle, params object[] _params)
		{
			SetEventHandle (_handle, _params);
		}

		public TouchHandle()
		{

		}

		public void SetEventHandle(OnTouchEventHandle _handle, params object[] _params)
		{
			DestoryEventHandle ();

			eventHandle += _handle;
			handleParams = _params;
		}

		public void CallEventHandle(QEventTriggerListener _listener, object _args)
		{
			if (null != eventHandle) 
			{
				eventHandle (_listener, _args, handleParams);
			}
		}

		public void DestoryEventHandle()
		{
			if (null != eventHandle) 
			{
				eventHandle -= eventHandle;
				eventHandle = null;
			}
		}
	}

	public class QEventTriggerListener : MonoBehaviour, 
	IPointerUpHandler,
	IPointerDownHandler,
	IPointerClickHandler,

	IPointerEnterHandler,
	IPointerExitHandler,

	ISelectHandler,
	IUpdateSelectedHandler,
	IDeselectHandler,

	IDragHandler,
	IEndDragHandler,
	IDropHandler,
	IScrollHandler,
	IMoveHandler
	{
		public TouchHandle onUp;
		public TouchHandle onDown;
		public TouchHandle onClick;
		public TouchHandle onEnter;
		public TouchHandle onExit;
		public TouchHandle onSelect;
		public TouchHandle onUpdateSelect;
		public TouchHandle onDeSelect;
		public TouchHandle onDrag;
		public TouchHandle onEndDrag;
		public TouchHandle onDrop;
		public TouchHandle onScroll;
		public TouchHandle onMove;


		static public QEventTriggerListener Get(GameObject go)
		{
			return go.GetOrAddComponent<QEventTriggerListener> ();
		}


		#region IPointerUpHandler implementation
		public void OnPointerUp (PointerEventData eventData)
		{
			if (null != onClick)
				onClick.CallEventHandle (this, eventData);
		}
		#endregion

		#region IPointerDownHandler implementation

		public void OnPointerDown (PointerEventData eventData)
		{
			if (null != onDown)
				onDown.CallEventHandle (this, eventData);
		}

		#endregion

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			if (null != onClick)
				onClick.CallEventHandle (this, eventData);
		}

		#endregion

		#region IPointerEnterHandler implementation

		public void OnPointerEnter (PointerEventData eventData)
		{
			if (null != onEnter)
				onEnter.CallEventHandle (this, eventData);
		}

		#endregion

		#region IPointerExitHandler implementation

		public void OnPointerExit (PointerEventData eventData)
		{
			if (null != onExit)
				onExit.CallEventHandle (this, eventData);
		}

		#endregion

		#region ISelectHandler implementation

		public void OnSelect (BaseEventData eventData)
		{
			if (null != onSelect)
				onSelect.CallEventHandle (this, eventData);
		}

		#endregion

		#region IUpdateSelectedHandler implementation

		public void OnUpdateSelected (BaseEventData eventData)
		{
			if (null != onUpdateSelect)
				onUpdateSelect.CallEventHandle (this, eventData);
		}

		#endregion

		#region IDeselectHandler implementation

		public void OnDeselect (BaseEventData eventData)
		{
			if (null != onDeSelect)
				onDeSelect.CallEventHandle (this, eventData);
		}

		#endregion

		#region IDragHandler implementation

		public void OnDrag (PointerEventData eventData)
		{
			if (null != onDrag)
				onDrag.CallEventHandle (this, eventData);
		}

		#endregion

		#region IEndDragHandler implementation

		public void OnEndDrag (PointerEventData eventData)
		{
			if (null != onEndDrag)
				onEndDrag.CallEventHandle (this, eventData);
		}

		#endregion

		#region IDropHandler implementation

		public void OnDrop (PointerEventData eventData)
		{
			if (null != onDrop)
				onDrop.CallEventHandle (this, eventData);
		}

		#endregion

		#region IScrollHandler implementation

		public void OnScroll (PointerEventData eventData)
		{
			if (null != onScroll)
				onScroll.CallEventHandle (this, eventData);
		}

		#endregion

		#region IMoveHandler implementation

		public void OnMove (AxisEventData eventData)
		{
			if (null != onMove)
				onMove.CallEventHandle (this, eventData);
		}

		#endregion

		/// <summary>
		/// Sets the event handler.
		/// </summary>
		/// <param name="_touchType">Touch type.</param>
		/// <param name="_handle">Handle.</param>
		/// <param name="_params">Parameters.</param>
		public void SetTouchEventHandle(EnumEventTouchType _touchType, OnTouchEventHandle _handle, params object[] _params)
		{
			switch (_touchType) 
			{
			case EnumEventTouchType.OnClick:
				if (null == onClick)
					onClick = new TouchHandle ();
				onClick.SetEventHandle (_handle, _params);
				break;
			case EnumEventTouchType.OnDrag:
				if (null == onDrag)
					onDrag = new TouchHandle ();
				onDrag.SetEventHandle (_handle, _params);
				break;
			case EnumEventTouchType.OnDrop:
				if (null == onDrop)
					onDrop = new TouchHandle ();
				onDrop.SetEventHandle (_handle, _params);
				break;
			case EnumEventTouchType.TouchDown:
				if (null == onDown)
					onDown = new TouchHandle ();
				onDown.SetEventHandle (_handle, _params);
				break;
			case EnumEventTouchType.TouchUp:
				if (null == onUp)
					onUp = new TouchHandle ();
				onUp.SetEventHandle (_handle, _params);
				break;
			default:
				break;
			}
		}

		public void RemoveEventHandle(TouchHandle _handle)
		{
			if (null == _handle) 
			{
				_handle.DestoryEventHandle ();
				_handle = null;
			}
		}

		public void RemoveAllEventHandle()
		{
			RemoveEventHandle(onUp);
			RemoveEventHandle(onDown);
			RemoveEventHandle(onClick);
			RemoveEventHandle(onEnter);
			RemoveEventHandle(onExit);
			RemoveEventHandle(onSelect);
			RemoveEventHandle(onUpdateSelect);
			RemoveEventHandle(onDeSelect);
			RemoveEventHandle(onDrag);
			RemoveEventHandle(onEndDrag);
			RemoveEventHandle(onDrop);
			RemoveEventHandle(onScroll);
			RemoveEventHandle(onMove);
		}

		void OnDestory()
		{
			RemoveAllEventHandle ();
		}
	}
}

