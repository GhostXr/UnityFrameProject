using System;
using System.Collections.Generic;
using UnityEngine;

namespace QUIFrameWork
{
	public class QUIManager : QSingleton<QUIManager> 
	{
		/// <summary>
		/// The dic open UI.
		/// </summary>
		private Dictionary<EnumUIClass, GameObject> dicOpenUI = null;

		/// <summary>
		/// The stack will open UI.
		/// </summary>
		private Stack<UIInfoData> stackOpenUI = null;

		public void CreateUIManager()
		{
			Debug.Log (" Create QUIManager instance");

			dicOpenUI = new Dictionary<EnumUIClass, GameObject> ();

			stackOpenUI = new Stack<UIInfoData> ();
		}

		public T[] GetUIComponent<T>(EnumUIClass _uiClass) where T : QUIBaseVIew
		{
			GameObject _retObj = GetUIObject(_uiClass);
			if (_retObj != null) 
			{
				return _retObj.GetComponents<T> ();
			}
			return null;
		}

		public GameObject GetUIObject(EnumUIClass _uiClass)
		{
			GameObject _retObj = null;
			if (dicOpenUI.TryGetValue(_uiClass, out _retObj)) 
			{
				if (_retObj != null) {
					return _retObj;
				} 
				else 
				{
					Debug.Log (string.Format(" Get %s Object is Failed !", _uiClass.ToString()));
				}
			}

			return null;
		}

		#region Push UI

		/// <summary>
		/// Pushs the view.
		/// </summary>
		/// <param name="_uiInfoData">UI info data.</param>
		public void PushUI(UIInfoData _viewInfoData)
		{
			UIInfoData[] _viewInfoDatas = { _viewInfoData };
			PushUI (_viewInfoDatas, false);
		}

		public void PushUI(UIInfoData[] _viewInfoDatas)
		{
			PushUI (_viewInfoDatas, false);
		}

		public void PushUIAndPopOthers(UIInfoData _viewInfoData)
		{
			UIInfoData[] _viewInfoDatas = { _viewInfoData };
			PushUI (_viewInfoDatas, true);
		}

		public void PushUIAndPopOthers(UIInfoData[] _viewInfoDatas)
		{
			PushUI (_viewInfoDatas, true);
		}

		/// <summary>
		/// Pushs the view.
		/// </summary>
		/// <param name="_uiInfoDatas">UI info datas.</param>
		/// <param name="_popOtherViews">If set to <c>true</c> pop other views.</param>
		public void PushUI(UIInfoData[] _viewInfoDatas, bool _popOthers)
		{
			if (_popOthers) 
			{
				//close current dialog
				PopAllUI();
			}

			//push ui in stack
			for (int i = 0; i < _viewInfoDatas.Length; i++) 
			{
				EnumUIClass _uiClass = _viewInfoDatas [i].UIClass;
				object[] _params = _viewInfoDatas [i].UIParams;
				string _uiPath = UIDefinePath.UI_VIEW_PREFABS_PATH + _uiClass;
				UIInfoData _uiInfoData = new UIInfoData (_uiClass, _uiPath, _params);
				if (!dicOpenUI.ContainsKey(_uiClass)) 
				{
					stackOpenUI.Push (_uiInfoData);
				}
			}

			//open ui
			if (stackOpenUI.Count > 0) 
			{
				QCoroutineController.Instance.StartCoroutine (AsyncLoadData());
			}
		}

		/// <summary>
		/// 异步加载 View
		/// </summary>
		/// <returns>The load data.</returns>
		private IEnumerator<int> AsyncLoadData()
		{
			UIInfoData _uiInfoData = null;
			UnityEngine.Object _prefab = null;
			GameObject _uiObj = null;

			if (null != stackOpenUI && stackOpenUI.Count > 0) 
			{
				do
				{
					_uiInfoData = stackOpenUI.Pop();
					QResourceManager.Instance.ResourcesLoad(_uiInfoData.UIPath, out _prefab);
					if(null != _prefab)
					{
						_uiObj = MonoBehaviour.Instantiate(_prefab) as GameObject;
						QUIBaseVIew _baseView = _uiObj.GetComponent<QUIBaseVIew>();
						if(null != _baseView)
						{
							//open UI View
							_baseView.OpenUI(_uiInfoData.UIClass, _uiInfoData.UIParams);
						}
						dicOpenUI.Add(_uiInfoData.UIClass, _uiObj);
					}
				}while(stackOpenUI.Count > 0);
			}

			yield return 0;
		}

		public void PreloadUI(EnumUIClass _uiClass)
		{
			string path = UIDefinePath.UI_VIEW_PREFABS_PATH + _uiClass;
			QResourceManager.Instance.ResourcesLoad(path);
		}

		#endregion

		#region Pop UI

		/// <summary>
		/// Closes the UI.
		/// </summary>
		/// <param name="_uiName">User interface name.</param>
		public void PopUI(EnumUIClass _uiClass)
		{
			GameObject _uiObj = GetUIObject (_uiClass);
			if (null != _uiObj) 
			{
				GameObject.Destroy (_uiObj);
				dicOpenUI.Remove (_uiClass);
			}
		}

		public void PopAllUI()
		{
			List<EnumUIClass> list = new List<EnumUIClass> (dicOpenUI.Keys);
			for (int i = 0; i < list.Count; i++) 
			{
				PopUI (list[i]);
			}
			dicOpenUI.Clear ();

		}

		public void PopUI(EnumUIClass[] _uiClass)
		{
			for (int i = 0; i < _uiClass.Length; i++) 
			{
				PopUI (_uiClass[i]);	
			}
		}

		#endregion
	}
}
