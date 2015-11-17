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
		BasePanel basePanel;
		if (panelDictionary.TryGetValue (typeof(T), out basePanel)) {
			return basePanel;
		}
		else
			return null;
	}
	public BasePanel ShowPanel<T>() where T:BasePanel {
		BasePanel basePanel;
		if (panelDictionary.TryGetValue (typeof(T),out basePanel)) {
			Debug.LogWarning(string.Format("{0} has been loaded",typeof(T).Name));
			return basePanel;
		}
		GameObject panelObject = GameObject.Instantiate (Resources.Load (string.Format ("UI/Panels/{0}", typeof(T).Name))) as GameObject;
		panelObject.transform.SetParent (panelParent);
		panelObject.transform.localScale = Vector3.one;
		panelObject.transform.localPosition = Vector3.zero;
		panelObject.transform.localRotation = Quaternion.identity;
		panelObject.name = typeof(T).Name;
		basePanel = panelObject.GetComponent<T> ();
		panelDictionary.Add (typeof(T), basePanel);
		UIPanel uiPanel = panelObject.GetComponent<UIPanel> ();

		UIPanel[] panels = basePanel.transform.GetComponentsInChildren<UIPanel> (true);
		for(int i=0;i<panels.Length;i++){
			if(panels[i].depth<DELTA_DEPTH){
				panels[i].depth+=curPanelDepth;
			}
			else {
				Debug.LogWarning(string.Format("{0}/{1},depth out of range!reset it!",basePanel.name,panels[i].name));
			}
		}
		uiPanel.depth = curPanelDepth;
		curPanelDepth += DELTA_DEPTH;
		basePanel.OnShow ();
		return basePanel;
	}
	public void HidePanel<T>() where T:BasePanel{
		BasePanel basePanel;
		if (panelDictionary.TryGetValue (typeof(T),out basePanel)) {
			curPanelDepth -= DELTA_DEPTH;
			basePanel.OnHide ();
			panelDictionary.Remove(typeof(T));
			GameObject.Destroy (basePanel.gameObject);
		}
		else {
			Debug.LogWarning(string.Format("{0} has not been loaded",typeof(T).Name));
		}
	}	
	public void HidePanel(BasePanel basePanel) {
		if (panelDictionary.ContainsValue(basePanel)) {
			curPanelDepth -= DELTA_DEPTH;
			basePanel.OnHide ();
			foreach(Type type in panelDictionary.Keys){
				if(panelDictionary[type]==basePanel){
					panelDictionary.Remove(type);
					break;
				}
			}
			GameObject.Destroy (basePanel.gameObject);
		}
		else {
			Debug.LogWarning(string.Format("{0} has not been loaded",basePanel));
		}
	}
	public void ActivePanel<T>() where T:BasePanel{
		BasePanel basePanel;
		if (panelDictionary.TryGetValue (typeof(T),out basePanel)) {
			basePanel.gameObject.SetActive (true);
		}
		else {
			Debug.LogWarning(string.Format("{0} has not been loaded",typeof(T).Name));
		}
	}
	public void DeactivePanel<T>() where T:BasePanel{
		BasePanel basePanel;
		if (panelDictionary.TryGetValue (typeof(T),out basePanel)) {
			basePanel.gameObject.SetActive (false);
		}
		else {
			Debug.LogWarning(string.Format("{0} has not been loaded",typeof(T).Name));
		}
	}
	//TODO:The first argument should be replaced by "int wordId" in the new version.
	public MessageBoxPanel ShowOKCancel(string message,Action onClickOKCallback,Action onClickCancelCallback){
		MessageBoxPanel messageBoxPanel = ShowPanel<MessageBoxPanel> () as MessageBoxPanel;
		messageBoxPanel.OnClickOKCallback = onClickOKCallback;
		messageBoxPanel.OnClickCancelCallback = onClickCancelCallback;
		messageBoxPanel.SetMessageAndType (message, MessageBoxType.OKCancel);
		return messageBoxPanel;
	}
	public MessageBoxPanel ShowOK(string message,Action onClickOKCallback){
		MessageBoxPanel messageBoxPanel = ShowPanel<MessageBoxPanel> () as MessageBoxPanel;
		messageBoxPanel.OnClickOKCallback = onClickOKCallback;
		messageBoxPanel.SetMessageAndType (message, MessageBoxType.OK);
		return messageBoxPanel;
	}
}
