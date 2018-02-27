using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QUIFrameWork
{
	public class QMessage : IEnumerable<KeyValuePair<string, object>>
	{
		private Dictionary<string, object> dicDatas = null;

		public string Name{ get; private set;}

		public object Sender{ get; private set;}

		public object Content{ get; set;}

		public object this [string key] {
			get
			{ 
				if (null != dicDatas && dicDatas.ContainsKey (key))
					return dicDatas [key];
				return null;
			}
			set
			{ 
				if (null == dicDatas) 
				{
					dicDatas = new Dictionary<string, object> ();
				}
				if (dicDatas.ContainsKey (key)) 
				{
					dicDatas [key] = value;
				}
				else
				{
					dicDatas.Add (key, value);
				}
			}
		}

		#region IEnumerable implementation

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator ()
		{
			if (null == dicDatas) 
			{
				yield break;
			}

			foreach(KeyValuePair<string, object> kvp in dicDatas)
			{
				yield return kvp;
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return dicDatas.GetEnumerator ();
		}

		#endregion

		#region Message Construction Function

		public QMessage(string _name, object _sender)
		{
			Name = _name;
			Sender = _sender;
		}

		public QMessage(string _name, object _sender, object _content)
		{
			Name = _name;
			Sender = _sender;
			Content = _content;
		}

		public QMessage(string _name, object _sender, object _content, params object[] _dicParam)
		{
			Name = _name;
			Sender = _sender;
			Content = _content;

			if(_dicParam.GetType() == typeof(Dictionary<string, object>))
			{
				foreach (object data in _dicParam) 
				{
					foreach (KeyValuePair<string, object> kvp in data as Dictionary<string, object>) 
					{
						this [kvp.Key] = kvp.Value;
					}
				}
			}
		}

		public QMessage(QMessage _message)
		{
			Name = _message.Name;
			Sender = _message.Sender;
			Content = _message.Content;

			foreach (KeyValuePair<string, object> kvp in _message.dicDatas) 
			{
				this [kvp.Key] = kvp.Value;
			}
		}

		#endregion

		#region add && remove 

		public void Add(string key, object value)
		{
			this [key] = value;
		}

		public void RemoveEvent(string key)
		{
			if (key != null && dicDatas.ContainsKey(key)) 
			{
				dicDatas.Remove (key);
			}
		}

		#endregion

		#region Send message

		public void Send()
		{
			QMessageCenter.Instance.SendMessage (this);
		}

		#endregion
	}
}
