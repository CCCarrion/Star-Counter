﻿using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;
public class XmlSaver
{
	const string KEY = "We will be counting stars.......";
	public string Encrypt(string toE)
	{
		//加密和解密采用相同的key,具体自己填，但是必须为32位//
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes(KEY);
		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateEncryptor();
		
		byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
		byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray,0,toEncryptArray.Length);
		
		return Convert.ToBase64String(resultArray,0,resultArray.Length);
	}

	public string Decrypt(string toD)
	{
		//加密和解密采用相同的key,具体值自己填，但是必须为32位//
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes(KEY);
		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateDecryptor();
		
		byte[] toEncryptArray = Convert.FromBase64String(toD);
		byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray,0,toEncryptArray.Length);
		
		return UTF8Encoding.UTF8.GetString(resultArray);
	}
	
	public string SerializeObject(object pObject,System.Type ty)
	{
		string XmlizedString   = null;
		MemoryStream memoryStream  = new MemoryStream();
		XmlSerializer xs  = new XmlSerializer(ty);
		XmlTextWriter xmlTextWriter  = new XmlTextWriter(memoryStream, Encoding.UTF8);
		xs.Serialize(xmlTextWriter, pObject);
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
		return XmlizedString;
	}
	
	public object DeserializeObject(string pXmlizedString , System.Type ty)
	{
		XmlSerializer xs  = new XmlSerializer(ty);
		MemoryStream memoryStream  = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
		XmlTextWriter xmlTextWriter   = new XmlTextWriter(memoryStream, Encoding.UTF8);
		return xs.Deserialize(memoryStream);
	}

	public void CreateXML(string fileName,string thisData)
	{
        string dataString = Encrypt(thisData);
		StreamWriter writer;
		writer = File.CreateText(fileName);
        writer.Write(dataString);
		writer.Close();
	}

	public string LoadXML(string fileName)
	{
		StreamReader sReader = File.OpenText(fileName);
		string dataString = sReader.ReadToEnd();
		sReader.Close();
        dataString = Decrypt(dataString);
        return dataString;
	}
	public string UTF8ByteArrayToString(byte[] characters  )
	{    
		UTF8Encoding encoding  = new UTF8Encoding();
		string constructedString  = encoding.GetString(characters);
		return (constructedString);
	}
	
	public byte[] StringToUTF8ByteArray(String pXmlString )
	{
		UTF8Encoding encoding  = new UTF8Encoding();
		byte[] byteArray  = encoding.GetBytes(pXmlString);
		return byteArray;
	}
}