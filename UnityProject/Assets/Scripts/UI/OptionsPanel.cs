using UnityEngine;
using System.Collections;

public class OptionsPanel : BasePanel {
	OptionData data;
	public override void OnShow ()
	{
		data = GameDataManager.Instance.Get<OptionData> ();
		SetMusicSprite ();
		//GameData data=GameDataManager.Instance.gameData;
		Debug.Log ("isMusicOn:" + data.isMusicOn + ",language:" + data.language);
		EventDelegate.Add (transform.FindChild ("Options").GetComponent<UIEventTrigger> ().onClick, Hide);
		EventDelegate.Add (transform.FindChild ("Back").GetComponent<UIEventTrigger> ().onClick, Hide);
		EventDelegate.Add (transform.FindChild ("Music").GetComponent<UIEventTrigger> ().onClick, OnClickMusic);
	}
	public override void OnHide ()
	{
		GameDataManager.Instance.Save<OptionData> (data);
	}
	void OnClickMusic(){
		data.isMusicOn = !data.isMusicOn;
		SetMusicSprite ();
	}
	void SetMusicSprite(){
		transform.FindChild ("Music").GetComponent<UISprite> ().spriteName = data.isMusicOn ? "Buttons_Sound" : "Buttons_Mute";
	}
	void OnChooseLanguage(){
		//data.language=
	}
}
