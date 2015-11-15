using UnityEngine;
using System.Collections;

public class OptionData : DataBase {
	public SystemLanguage language;
	public bool isMusicOn;
	public OptionData(){
		isMusicOn = true;
		switch (Application.systemLanguage) {
			case SystemLanguage.Chinese:
			case SystemLanguage.ChineseSimplified:
			case SystemLanguage.ChineseTraditional:
			case SystemLanguage.English:
				language = Application.systemLanguage;
				break;
			default:
				language=SystemLanguage.English;
				break;
		}
	}
	public OptionData(bool isMusicOn,SystemLanguage language){
		this.isMusicOn = isMusicOn;
		this.language=language;
	}
}
