using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在切换场景时不销毁的 MonoBehaviour 类的单例模板
/// </summary>
public class QDDOLSingleton<T> : MonoBehaviour where T : Component
{
	/// <summary>
	/// The instance.
	/// </summary>
	private static T _instance = null;
	public static T Instance
	{
		get
		{
			if (null == _instance) 
			{
				GameObject _ddolGoObj = GameObject.Find("QDDOLGameObject");
				if (null == _ddolGoObj) 
				{
					_ddolGoObj = new GameObject ("QDDOLGameObject");
					DontDestroyOnLoad (_ddolGoObj);
				}
				_instance = _ddolGoObj.AddComponent<T> ();
			}
			return _instance;
		}
	}

	/// <summary>
	/// Raises the application close event.
	/// </summary>
	private void OnApplicationClose()
	{
		_instance = null;
	}
}
