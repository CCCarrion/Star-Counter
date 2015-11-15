using UnityEngine;
using System.Collections;

public class GameMainLoop : MonoBehaviour {
	public static GameMainLoop Instance;
	void Awake(){
		Instance = this;
		Debug.Log ("No more counting dollars\tWe'll be counting stars");
		DontDestroyOnLoad (gameObject);
	}
	void Start () {
		//creat singleton
		UIManager.Instance = new UIManager ();
		GameDataManager.Instance = new GameDataManager ();
		UIManager.Instance.ShowPanel<LoginPanel> ();
	}
	void OnApplicationPause(bool pause){
	
	}
	void Update () {
	
	}
}
