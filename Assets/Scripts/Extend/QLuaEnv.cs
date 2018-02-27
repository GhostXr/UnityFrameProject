using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QUIFrameWork;
using XLua;

public class QLuaEnv
{
	private static LuaEnv _instance;

	public static LuaEnv Instance 
	{
		get {
			if (null == _instance) {
				_instance = new LuaEnv ();
			}
			return _instance;
		}
	}

	public void Destory()
	{
		if (null != _instance) 
		{
			_instance.Dispose ();
		}
	}
}
