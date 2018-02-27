using System;
using UnityEngine;

/// <summary>
/// 非 Monobehaviour 类的单例模板
/// </summary>
namespace QUIFrameWork
{
	public abstract class QSingleton<T> where T : class, new()
	{
		private static T _instance = null;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static T Instance
		{
			get
			{ 
				if (null == _instance) {
					_instance = new T ();
				}
				return _instance;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="QUIFrameWork.Singleton`1"/> class.
		/// </summary>
		protected QSingleton()
		{
			if (null == _instance) 
			{
				Create ();
			} 
			else 
			{
				Debug.Log ("Instance is already exist !");
			}
		}

		/// <summary>
		/// Create this instance.
		/// </summary>
		public virtual void Create()
		{
		}

		/// <summary>
		/// Destory this instance.
		/// </summary>
		public virtual void Destory()
		{
		}
	}
}
