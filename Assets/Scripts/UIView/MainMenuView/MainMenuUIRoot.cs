using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QUIFrameWork; 
using XLua;

public class MainMenuUIRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//create QUIManager Instance
		QUIManager.Instance.CreateUIManager();

		//create QUIManager Instance
		QResourceManager.Instance.Create();

		object[] _params = {};
		QUIManager.Instance.PushUI (new UIInfoData (EnumUIClass.TestView, "", _params));

		//LuaEnv luaEnv = new LuaEnv();
		//luaEnv.DoString ("require 'LuaScript.main'");
	}
}
