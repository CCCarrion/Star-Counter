using UnityEngine;
using System.Collections;

public class OptionsPanel : UIPanelEntity {
	
	public override void OnShow ()
	{
		EventDelegate.Add (transform.FindChild ("Options").GetComponent<UIEventTrigger> ().onClick, OnClickClose);
		EventDelegate.Add (transform.FindChild ("Back").GetComponent<UIEventTrigger> ().onClick, OnClickClose);
	}
	public override void OnHide ()
	{
	}
	public void OnClickClose(){
		Debug.Log ("OnClickClose");
		UIManager.Instance.HidePanel<OptionsPanel> ();
	}
}
