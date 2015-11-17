using UnityEngine;
using System;

public class MessageBoxPanel : BasePanel {
	public Action OnClickOKCallback;
	public Action OnClickCancelCallback;
	public override void OnShow ()
	{
	
	}
	public override void OnHide ()
	{
		
	}
	public void SetMessageAndType(string message,MessageBoxType messageBoxType){
		transform.FindChild ("Message").GetComponent<UILabel> ().text = message;
		Transform okButton = transform.FindChild ("OKBtn");
		Transform cancelButton = transform.FindChild ("CancelBtn");
		EventDelegate.Add(okButton.GetComponent<UIEventTrigger> ().onClick, OnClickOK);
		switch (messageBoxType) {
		case MessageBoxType.OK:
			okButton.localPosition = new Vector3 (0, okButton.localPosition.y);
			cancelButton.gameObject.SetActive (false);
			break;
		case MessageBoxType.OKCancel:
			EventDelegate.Add(cancelButton.GetComponent<UIEventTrigger> ().onClick, OnClickCancel);
			cancelButton.gameObject.SetActive (true);
			break;
		default:
			break;
		}
	}
	void OnClickOK(){
		Hide ();
		if (OnClickOKCallback==null) {
		}
		else {
			OnClickOKCallback ();		
		}
	}
	void OnClickCancel(){
		Hide ();
		if (OnClickCancelCallback==null) {
		}
		else {
			OnClickCancelCallback ();
		}
	}
}
