using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QUIFrameWork
{
	public class QMessageCenter : QSingleton<QMessageCenter>
	{
		private Dictionary<string, List<MessageEvnet>> dicMessageEvents = null;

		public override void Create()
		{
			dicMessageEvents = new Dictionary<string, List<MessageEvnet>> ();
		}

		#region Add & Remove EventListener

		public void AddEventListener(QMessageType _messageType, MessageEvnet _messageEvent)
		{
			AddEventListener (_messageType.ToString(), _messageEvent);
		}

		public void RemoveEventListener(QMessageType _messageType, MessageEvnet _messageEvent)
		{
			RemoveEventListener (_messageType.ToString(), _messageEvent);
		}

		public void AddEventListener(string _messageName, MessageEvnet _messageEvent)
		{
			List<MessageEvnet> list = null;
			if (dicMessageEvents.ContainsKey (_messageName)) 
			{
				list = dicMessageEvents [_messageName];		
			} 
			else 
			{
				list = new List<MessageEvnet> ();
				dicMessageEvents.Add (_messageName, list);
			}

			if (!list.Contains (_messageEvent)) 
			{
				list.Add (_messageEvent);
			}
		}

		public void RemoveEventListener(string _messageName, MessageEvnet _messageEvent)
		{
			if (dicMessageEvents.ContainsKey (_messageName)) 
			{
				List<MessageEvnet> list = dicMessageEvents [_messageName];	
				if (list.Contains (_messageEvent)) 
				{
					list.Remove (_messageEvent);
				}
				if (list.Count <= 0) 
				{
					dicMessageEvents.Remove (_messageName);
				}
			} 
		}

		public void RemoveAllEventListeners()
		{
			dicMessageEvents.Clear ();
		}

		#endregion

		#region SendMessage

		public void SendMessage(QMessage _message)
		{
			DispatchEvent (_message);
		}
			
		public void SendMessage(string _name, object _sender)
		{
			SendMessage (new QMessage(_name, _sender));
		}

		public void SendMessage(string _name, object _sender, object _content)
		{
			SendMessage (new QMessage(_name, _sender, _content));
		}
			
		public void SendMessage(string _name, object _sender, object _content, params object[] _dicParam)
		{
			SendMessage (new QMessage(_name, _sender, _content, _dicParam));
		}

		private void DispatchEvent(QMessage _message)
		{
			if (null == dicMessageEvents || ! dicMessageEvents.ContainsKey(_message.Name)) 
			{
				return;
			}
			List<MessageEvnet> list = dicMessageEvents [_message.Name];

			for (int i = 0; i < list.Count; i++) 
			{
				MessageEvnet messageEvent = list [i];
				if(null != messageEvent)
				{
					messageEvent (_message);
				}
			}
		}

		#endregion
	}
}
