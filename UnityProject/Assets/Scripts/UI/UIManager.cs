using UnityEngine;
using System.Collections.Generic;
using System;
public class UIManager{
	public static UIManager Instance {
		get;
		set;
	}
	Transform panelParent;
	Dictionary<Type,BasePanel> panelDictionary;
	int curPanelDepth;
	const int DELTA_DEPTH = 10;
	public UIManager(){
		UIRoot uiRoot = GameObject.Find ("UI Root").GetComponent<UIRoot>();
		panelParent = uiRoot.transform.FindChild ("Camera");

		panelDictionary = new Dictionary<Type, BasePanel> ();
		curPanelDepth = 0;
		//adapt the ui height and width
		int ManualWidth = uiRoot.manualWidth;
		int ManualHeight = uiRoot.manualHeight;
		if (uiRoot != null)
		{
			if (System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
				uiRoot.manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
			else
				uiRoot.manualHeight = ManualHeight;
		}
	}
	public BasePanel GetPanel<T>() where T:BasePanel{
		BasePanel panelEntity;
		if (panelDictionary.TryGetValue (typeof(T), out panelEntity)) {
			return panelEntity;
		}
		else
			return null;
	}
	public void ShowPanel<T>() where T:BasePanel {
		BasePanel panelEntity;
		if (panelDictionary.TryGetValue (typeof(T),out panelEntity)) {
			Debug.LogWarning(string.Format("{0} has been loaded",typeof(T).Name));
			return;
		}
		GameObject panelObject = GameObject.Instantiate (Resources.Load (string.Format ("UI/Panels/{0}", typeof(T).Name))) as GameObject;
		panelObject.transform.SetParent (panelParent);
		panelObject.transform.localScale = Vector3.one;
		panelObject.transform.localPosition = Vector3.zero;
		panelObject.transform.localRotation = Quaternion.identity;
		panelObject.name = typeof(T).Name;
		panelEntity = panelObject.GetComponent<T> ();
		panelDictionary.Add (typeof(T), panelEntity);
		UIPanel uiPanel = panelObject.GetComponent<UIPanel> ();

		UIPanel[] panels = panelEntity.transform.GetComponentsInChildren<UIPanel> (true);
		for(int i=0;i<panels.Length;i++){
			if(panels[i].depth<DELTA_DEPTH){
				panels[i].depth+=curPanelDepth;
			}
			else {
				Debug.LogWarning(string.Format("{0}/{1},depth out of range!reset it!",panelEntity.name,panels[i].name));
			}
		}
		uiPanel.depth = curPanelDepth;
		curPanelDepth += DELTA_DEPTH;
		panelEntity.OnShow ();
	}
	public void HidePanel<T>() where T:BasePanel{
		BasePanel panelEntity;
		if (panelDictionary.TryGetValue (typeof(T),out panelEntity)) {
			curPanelDepth -= DELTA_DEPTH;
			panelEntity.OnHide ();
			panelDictionary.Remove(typeof(T));
			GameObject.Destroy (panelEntity.gameObject);
		}
		else {
			Debug.LogWarning(string.Format("{0} has not been loaded",typeof(T).Name));
		}
	}	
	public void HidePanel(BasePanel panelEntity) {
		if (panelDictionary.ContainsValue(panelEntity)) {
			curPanelDepth -= DELTA_DEPTH;
			panelEntity.OnHide ();
			foreach(Type type in panelDictionary.Keys){
				if(panelDictionary[type]==panelEntity){
					panelDictionary.Remove(type);
					break;
				}
			}
			GameObject.Destroy (panelEntity.gameObject);
		}
		else {
			Debug.LogWarning(string.Format("{0} has not been loaded",panelEntity));
		}
	}
	public void ActivePanel<T>() where T:BasePanel{
		BasePanel panelEntity;
		if (panelDictionary.TryGetValue (typeof(T),out panelEntity)) {
			panelEntity.gameObject.SetActive (true);
		}
		else {
			Debug.LogWarning(string.Format("{0} has not been loaded",typeof(T).Name));
		}
	}
	public void DeactivePanel<T>() where T:BasePanel{
		BasePanel panelEntity;
		if (panelDictionary.TryGetValue (typeof(T),out panelEntity)) {
			panelEntity.gameObject.SetActive (false);
		}
		else {
			Debug.LogWarning(string.Format("{0} has not been loaded",typeof(T).Name));
		}
	}
}
