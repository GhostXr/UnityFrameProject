using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QUIFrameWork
{
	/// <summary>
	/// 全局变量
	/// </summary>
	#region Global Enum

	/// <summary>
	/// Enum object state.
	/// </summary>
	public enum EnumObjectState
	{
		None,
		Initial,
		Loading,
		Ready,
		Disabled,
		Closing
	}

	/// <summary>
	/// Enum UI Name 
	/// value: XXXView
	/// </summary>
	public enum EnumUIClass : int
	{
		None = -1,
		TestView = 1,
		TestView2 = 2,
	}

	public class UIDefinePath
	{
		/// <summary>
		/// The UI View Prefab Path.
		/// </summary>
		public const string UI_VIEW_PREFABS_PATH = "Prefabs/UIViewPrefabs/";

		/// <summary>
		/// The UI Sub Prefab Path.
		/// </summary>
		public const string UI_SUB_PREFABS_PATH = "Prefabs/UISubPrefabs/";
	}

	public class UIInfoData
	{
		public EnumUIClass UIClass{ get; private set; }

		public object[] UIParams{ get; private set; }

		public string UIPath{ get; private set; }

		public UIInfoData(EnumUIClass _uiClass, string _uiPath, params object[] _uiParams)
		{
			UIClass = _uiClass;
			UIParams = _uiParams;
			UIPath = _uiPath;
		}
	}

	public enum EnumSceneType : int
	{
		None = 0,
		StartGame = 1,
		LoginScene = 2,
		MainScene = 3,
		BattleScene = 4,
	}

	public enum EnumEventTouchType
	{
		OnClick,
		TouchDown,
		TouchUp,
		OnDrag,
		OnDrop,
	}

	#endregion

	/// <summary>
	/// Message evnet.
	/// </summary>
	#region Evnet

	public delegate void MessageEvnet(QMessage message);

	public delegate void OnTouchEventHandle(QEventTriggerListener _listener, object _args, params object[] _param);

	#endregion
}