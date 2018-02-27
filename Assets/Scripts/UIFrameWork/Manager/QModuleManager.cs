using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QUIFrameWork
{
	public class QModuleManager : QSingleton<QModuleManager>
	{
		private Dictionary<string, QBaseModule> dicModules = null;

		/// <summary>
		/// Create this instance.
		/// </summary>
		public override void Create()
		{
			dicModules = new Dictionary<string, QBaseModule> ();
		}

		/// <summary>
		/// Get the specified _name.
		/// </summary>
		/// <param name="_name">Name.</param>
		public QBaseModule Get(string _name)
		{
			if (dicModules.ContainsKey (_name)) 
			{
				return dicModules [_name];
			}
			return null;
		}

		/// <summary>
		/// Get this instance.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Get<T>() where T : QBaseModule
		{
			Type t = typeof(T);
			if (dicModules.ContainsKey (t.ToString ()))
				return dicModules[t.ToString ()] as T;
			return null;
		}

		#region Register

		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <param name="_moudle">Moudle.</param>
		public void RegisterModule(QBaseModule _moudle)
		{
			Type t = _moudle.GetType ();
			Register (t.ToString (), _moudle);
		}

		/// <summary>
		/// Register the specified _name and _module.
		/// </summary>
		/// <param name="_name">Name.</param>
		/// <param name="_module">Module.</param>
		public void Register(string _name, QBaseModule _module)
		{
			if (! dicModules.ContainsKey (_name))
				dicModules.Add(_name, _module);
		}

		#endregion

		#region  UnRegister

		/// <summary>
		/// Uns the register module.
		/// </summary>
		/// <param name="_moudle">Moudle.</param>
		public void UnRegisterModule(QBaseModule _moudle)
		{
			Type t = _moudle.GetType ();
			UnRegister (t.ToString ());
		}

		/// <summary>
		/// Uns the register.
		/// </summary>
		/// <param name="_name">Name.</param>
		public void UnRegister(string _name)
		{
			if (dicModules.ContainsKey (_name))
			{
				QBaseModule module = dicModules [_name];
				module.Release ();
				dicModules.Remove(_name);
				module = null;
			}
		}

		/// <summary>
		/// Uns the register all.
		/// </summary>
		public void UnRegisterAll()
		{
			List<string> _moduleList = new List<string> (dicModules.Keys);
			for (int i = 0; i < _moduleList.Count; i++) 
			{
				UnRegister(_moduleList[i]);
			}
			_moduleList.Clear ();
			dicModules.Clear();
		}

		#endregion
	}
}
