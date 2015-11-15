using UnityEngine;
using System.Collections;

public class NGUIThemeSwitch : MonoBehaviour {
	public UIAtlas SoilAtlas;
	public UIAtlas RockAtlas;
	public Texture2D BGDay;
	public Texture2D BGNight;
	

	public void SwitchThemeRock () {
		GameObject[] UISprites;
		UISprites = GameObject.FindGameObjectsWithTag ("NGUI");
		foreach (GameObject go in UISprites) {
			if (go.GetComponent <UISprite> () != null) {
				go.GetComponent <UISprite> ().atlas = RockAtlas;
			}
			if (go.GetComponent <UITexture> () != null) {
				go.GetComponent <UITexture> ().mainTexture = BGNight;
			}
		}
	}

	public void SwitchThemeSoil () {
		GameObject[] UISprites;
		UISprites = GameObject.FindGameObjectsWithTag ("NGUI");
		foreach (GameObject go in UISprites) {
			if (go.GetComponent <UISprite> () != null) {
				go.GetComponent <UISprite> ().atlas = SoilAtlas;
			}
			if (go.GetComponent <UITexture> () != null) {
				go.GetComponent <UITexture> ().mainTexture = BGDay;
			}
		}
	}
}
