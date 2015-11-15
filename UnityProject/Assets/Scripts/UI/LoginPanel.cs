using UnityEngine;
using System.Collections;

public class LoginPanel : UIPanelEntity {
	public override void OnShow ()
	{
		EventDelegate.Add (transform.FindChild ("Options").GetComponent<UIEventTrigger> ().onClick, OnClickOptions);
		EventDelegate.Add (transform.FindChild ("Facebook").GetComponent<UIEventTrigger> ().onClick, OnClickFacebook);
		EventDelegate.Add (transform.FindChild ("Twitter").GetComponent<UIEventTrigger> ().onClick, OnClickWeibo);
	}
	public override void OnHide ()
	{
	}
	void OnClickOptions(){
		Debug.Log ("OnClickOptions");
		UIManager.Instance.ShowPanel<OptionsPanel> ();
	}
	void OnClickFacebook(){
		Application.OpenURL ("https://www.facebook.com/Warcraft");
	}
	void OnClickWeibo(){
		Application.OpenURL ("http://www.weibo.com/wow");
	}
}
