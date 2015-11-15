using UnityEngine;
using System.Collections;

public class GamePanel : BasePanel {
	UILabel progress;
	ArchiveData archiveData;
	public override void OnShow ()
	{
		progress = transform.FindChild ("Progress").GetComponent<UILabel> ();
		archiveData = GameDataManager.Instance.Get<ArchiveData> ();
		progress.text = archiveData.progress.ToString();
		EventDelegate.Add (transform.FindChild ("DoBtn").GetComponent<UIEventTrigger> ().onClick, OnClickDo);
		EventDelegate.Add (transform.FindChild ("BackBtn").GetComponent<UIEventTrigger> ().onClick, OnClickBack);

	}
	public override void OnHide ()
	{

	}
	void OnClickDo(){
		archiveData.progress += 1;
		progress.text = archiveData.progress.ToString();
		GameDataManager.Instance.Save<ArchiveData> (archiveData);
	}
	void OnClickBack(){
		Hide ();
		UIManager.Instance.ShowPanel<LoginPanel> ();
	}
}
