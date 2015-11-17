using UnityEngine;
using System.Collections;

public class LoginPanel : BasePanel {
	public override void OnShow ()
	{
		EventDelegate.Add (transform.FindChild ("Options").GetComponent<UIEventTrigger> ().onClick, OnClickOptions);
		EventDelegate.Add (transform.FindChild ("Facebook").GetComponent<UIEventTrigger> ().onClick, OnClickFacebook);
		EventDelegate.Add (transform.FindChild ("Twitter").GetComponent<UIEventTrigger> ().onClick, OnClickWeibo);
		EventDelegate.Add (transform.FindChild ("New Game").GetComponent<UIEventTrigger> ().onClick, OnClickNewGame);
		EventDelegate.Add (transform.FindChild ("Continue").GetComponent<UIEventTrigger> ().onClick, OnClickContinue);
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
	
	void OnClickNewGame(){
		UIManager.Instance.ShowOKCancel ("Start New Game?\nThe old save data would be erased.", () => {
			ArchiveData data = new ArchiveData ("Fucker", 0);
			GameDataManager.Instance.Save<ArchiveData> (data);
			UIManager.Instance.ShowPanel<GamePanel> ();
			Hide ();
		}, null);
	}
	void OnClickContinue(){
		ArchiveData data = GameDataManager.Instance.Get<ArchiveData> ();
		Debug.Log ("PlayerName:" + data.playerName + ",UDID:" + data.key);
		UIManager.Instance.ShowPanel<GamePanel> ();
		Hide ();
	}
}
