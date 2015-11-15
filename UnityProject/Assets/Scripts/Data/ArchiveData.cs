using UnityEngine;
using System.Collections;

public class ArchiveData : DataBase {
	public string playerName;
	public int progress;
	public ArchiveData(){
		playerName = "";
		progress = 0;
	}
	public ArchiveData(string playerName,int progress){
		this.playerName = playerName;
		this.progress = progress;
	}
}
