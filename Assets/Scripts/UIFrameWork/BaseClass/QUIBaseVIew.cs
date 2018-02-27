using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QUIFrameWork
{
	[XLua.LuaCallCSharp]
	public abstract class QUIBaseVIew : MonoBehaviour {

		#region BaseView Value
		/// <summary>
		/// 缓存 gameObject.
		/// </summary>
		private GameObject _ownerGameObject;
		public GameObject OwnerGameObject
		{
			get
			{
				if (null == _ownerGameObject)
				{
					_ownerGameObject = this.gameObject;
				}
				return _ownerGameObject;
			}
		}

		/// <summary>
		/// 缓存 transform.
		/// </summary>
		private Transform _ownerTransform;
		public Transform OwnerTransform
		{
			get
			{
				if (null == _ownerTransform)
				{
					_ownerTransform = this.transform;
				}
				return _ownerTransform;
			}
		}

		/// <summary>
		/// The type of the UI View.
		/// </summary>
		private EnumUIClass _uiClass = EnumUIClass.None;
		public EnumUIClass UIClass {
			get 
			{ 
				return _uiClass;
			}

			protected set{ _uiClass = value; }
		}

		/// <summary>
		/// The parameters.
		/// </summary>
		private object[] _params;
		protected object[] Params {
			get ;
			private set;
		}

		#endregion

		#region MonoBehaviour Method

		void Awake()
		{
			OnAwake ();
		}

		public virtual void OnAwake()
		{

		}

		void Start()
		{
			OnStart ();
		}

		public virtual void OnStart()
		{
			
		}

		void Update()
		{
			OnUpdate ();
		}

		public virtual void OnUpdate()
		{

		}

		void Destory()
		{
			OnDestory ();
		}

		public virtual void OnDestory()
		{

		}

		#endregion

		#region Owner Method

		/// <summary>
		/// Opens the U.
		/// </summary>
		/// <param name="__uiClass">User interface class.</param>
		/// <param name="_uiParams">User interface parameters.</param>
		public virtual void OpenUI(EnumUIClass __uiClass, params object[] _uiParams)
		{
			Params = _uiParams;
			UIClass = __uiClass;
			//QCoroutineController.Instance.StartCoroutine (AsyncLoadData());
		}

		/// <summary>
		/// Loads the data asyn.
		/// </summary>
		/// <returns>The data asyn.</returns>
		private IEnumerator AsyncLoadData()
		{
			yield return new WaitForSeconds (0);
			OnLoadData ();
		}

		/// <summary>
		/// the load data method.
		/// </summary>
		protected virtual void OnLoadData()
		{
			
		}

		protected virtual void CloseSelf()
		{
			QUIManager.Instance.PopUI (UIClass);
		}
		#endregion
	}
}
