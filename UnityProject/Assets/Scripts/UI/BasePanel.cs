using UnityEngine;
using System.Collections;

public abstract class BasePanel : MonoBehaviour {
	public abstract void OnShow ();
	public abstract void OnHide ();
	public virtual void Hide(){
		UIManager.Instance.HidePanel (this);
	}
}
