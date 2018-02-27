using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QUIFrameWork
{
	#region test code

	public class AssetInfo
	{
		public Type AssetType{ get; set;}

		public string Path{ get; set;}

		private UnityEngine.Object _obj;

		public bool IsLoad{ 
			get
			{ 
				return null != _obj;
			} 
		}

		public UnityEngine.Object AssetObject
		{
			get
			{
				if (null == _obj) {
					_obj = Resources.Load (Path);
				} 
				return _obj;
			}
		}
	}

	#endregion

	public class QResourceManager : QSingleton<QResourceManager> 
	{
		private Dictionary<string, UnityEngine.Object> dicAssetInfo = null;

		public override void Create ()
		{
			dicAssetInfo = new Dictionary<string, UnityEngine.Object> ();
		}

		#region load resources

		/// <summary>
		/// Resources the load.
		/// </summary>
		/// <param name="_path">Path.</param>
		public void ResourceLoad(string _path)
		{   
			UnityEngine.Object _obj = null;
			ResourcesLoad (_path, out _obj);
		}

		/// <summary>
		/// Resourceses the load.
		/// </summary>
		/// <returns>The load.</returns>
		/// <param name="_path">Path.</param>
		public UnityEngine.Object ResourcesLoad(string _path)
		{
			UnityEngine.Object _obj = null;
			ResourcesLoad (_path, out _obj);

			return _obj;
		}

		/// <summary>
		/// Loads the instance.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="_path">Path.</param>
		public UnityEngine.Object LoadInstance(string _path)
		{
			UnityEngine.Object _obj = null;
			ResourcesLoad (_path, out _obj);

			UnityEngine.Object instance = null;
			if (null != _obj) {
				instance = MonoBehaviour.Instantiate (_obj);
			} 
			else 
			{
				Debug.LogError ("ERROR: _obj is null !");	
			}
			return instance;
		}

		/// <summary>
		/// Resourceses the load.
		/// </summary>
		/// <param name="_path">Path.</param>
		/// <param name="_obj">Object.</param>
		public void ResourcesLoad(string _path, out UnityEngine.Object _obj)
		{
			_obj = null;
			if (null != _path) 
			{
				if (dicAssetInfo.ContainsKey (_path)) {
					_obj = dicAssetInfo [_path];
				} else {
					_obj = Resources.Load (_path);
					dicAssetInfo.Add(_path, _obj);
				}
			}
			else 
			{
				Debug.LogError ("ERROR: _path is null !");	
			}
		}

		#endregion
	}
}
