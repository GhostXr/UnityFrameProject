using UnityEngine;
using System.Collections;
using XLua;
using System;

namespace QUIFrameWork
{
	[LuaCallCSharp]
	public class QUILuaScriptView : QUIBaseVIew
	{
		public TextAsset luaScript;
		public Injection[] injections;

		internal static float lastGCTime = 0;
		internal const float GCInterval = 1;//1 second 

		private Action luaStart;
		private Action luaUpdate;
		private Action luaOnDestroy;

		private LuaTable scriptEnv;

		public QUILuaScriptView()
		{
		}

		public QUILuaScriptView(string _luaScript)
		{
			SetLuaScript (_luaScript);
		}

		public void SetLuaScript(string _luaScript)
		{	
			if (null != _luaScript) 
			{
				luaScript = QResourceManager.Instance.ResourcesLoad (_luaScript) as TextAsset;
			}
		}

		public override void OnAwake ()
		{
			if (null == luaScript) 
			{
				Debug.LogError (" luaScript is null !");
				return;
			}
			scriptEnv = QLuaEnv.Instance.NewTable();

			LuaTable meta = QLuaEnv.Instance.NewTable();
			meta.Set("__index", QLuaEnv.Instance.Global);
			scriptEnv.SetMetaTable(meta);
			meta.Dispose();

			scriptEnv.Set("self", this);
			foreach (var injection in injections)
			{
				scriptEnv.Set(injection.name, injection.value);
			}

			QLuaEnv.Instance.DoString(luaScript.text, "LuaScript", scriptEnv);

			Action luaAwake = scriptEnv.Get<Action>("awake");
			scriptEnv.Get("OnStart", out luaStart);
			scriptEnv.Get("OnUpdate", out luaUpdate);
			scriptEnv.Get("OnDestory", out luaOnDestroy);

			if (luaAwake != null)
			{
				luaAwake();
			}
		}
		
		public override void OnStart ()
		{
			if (luaStart != null)
			{
				luaStart();
			}
		}

		public override void OnUpdate ()
		{ 
			if (luaUpdate != null)
			{
				luaUpdate();
			}
			if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
			{
				QLuaEnv.Instance.Tick();
				LuaBehaviour.lastGCTime = Time.time;
			}
		}

		public override void OnDestory ()
		{
			if (luaOnDestroy != null)
			{
				luaOnDestroy();
			}
			luaOnDestroy = null;
			luaUpdate = null;
			luaStart = null;
			scriptEnv.Dispose();
			injections = null;
		}
	}
}

