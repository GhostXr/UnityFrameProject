using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QUIFrameWork;

public class TestView2 : QUIBaseVIew {
	
	public override void OnStart()
	{
		Text text = OwnerGameObject.GetComponentInChildren<Text> ();
		text.text = "TestView2";

		Button btn = OwnerGameObject.GetComponentInChildren<Button> ();
		//btn.onClick.AddListener (OnClickButton);
		QEventTriggerListener btnListener = QEventTriggerListener.Get(btn.gameObject);
		object[] _params = {};
		btnListener.SetTouchEventHandle (EnumEventTouchType.OnClick, OnClickButton, _params);
	}

//	public void OnClickButton()
//	{
//		CloseSelf ();
//
//		object[] _params = {};
//		QUIManager.Instance.PushUI (new UIInfoData (EnumUIClass.TestView, "", _params));
//		return;
//	}

	public void OnClickButton(QEventTriggerListener _listener, object _args, params object[] _params)
	{
		CloseSelf ();

		QUIManager.Instance.PushUI (new UIInfoData (EnumUIClass.TestView, "", _params));
		return;
	}
}
