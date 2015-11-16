using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
/// <summary>
/// 保存数据的类，其他的类需要继承这个DataBase类。
/// DataBase,the class to save data.The other class should inherit the DataBase class.
/// 继承自DataBase的类，必须有无参的构造函数，用于第一次保存Data时设置一些默认值。
/// The other class inherited from DataBase should contain a no-argument constructor,in order to set some default value when save the Data at the first time.
/// </summary>
public abstract class DataBase
{
	public string key;
	public DataBase(){
		key = SystemInfo.deviceUniqueIdentifier;
	}
}

public class GameDataManager
{
	public static GameDataManager Instance {
		get;
		set;
	}
	private XmlSaver xmlSaver;
	Dictionary<Type,DataBase> dataDictionary; 
	public GameDataManager(){
		dataDictionary = new Dictionary<Type, DataBase> ();
		xmlSaver =new XmlSaver();
	}
	public T Get<T>() where T:DataBase,new(){
		DataBase data;
		if (dataDictionary.TryGetValue (typeof(T), out data)) {
			return data as T;
		}
		else {
			data=Load<T>();
			dataDictionary[typeof(T)] = data;
			return data as T;
		}
	}
	private void Set<T>(T t) where T:DataBase,new(){
		dataDictionary [typeof(T)] = t;
	}
	public void Save<T>(T t) where T:DataBase,new()
	{
		Set<T> (t);
		string dataFileName = typeof(T).Name + ".dat";
		string gameDataFile = Path.Combine (Application.persistentDataPath, dataFileName);
		string dataString= xmlSaver.SerializeObject(t,typeof(T));
		xmlSaver.CreateXML(gameDataFile,dataString);
	}
	/// <summary>
	/// Load Data by T,if file not found or key not mapped,save and return a new one.
	/// </summary>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	private T Load<T>() where T:DataBase,new()
	{
		string dataFileName = typeof(T).Name + ".dat";
		string gameDataFile = Path.Combine( Application.persistentDataPath,dataFileName);
		if (File.Exists (gameDataFile)) {
			string dataString = xmlSaver.LoadXML (gameDataFile);
			T gameDataFromXML = xmlSaver.DeserializeObject (dataString, typeof(T)) as T;

			if (gameDataFromXML.key == SystemInfo.deviceUniqueIdentifier) {
				return gameDataFromXML;
			}
			else{//if the key not mapped,save a new file
				T t=new T ();
				Save<T> (t);
				return t;
			}
		} 
		else {//if the file not found,save a new file
			T t=new T ();
			Save<T> (t);
			return t;
		}
	}
}