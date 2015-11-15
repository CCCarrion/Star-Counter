using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Data;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Excel;
public class ImportExcelEditorWIndow : EditorWindow{
	[MenuItem ("Star Counter/ImportExcel")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		ImportExcelEditorWIndow window = (ImportExcelEditorWIndow)EditorWindow.GetWindow (typeof (ImportExcelEditorWIndow));
	}
	void OnEnable(){
	}
	void OnGUI(){
		if (GUILayout.Button ("Import Excels")) {
			DirectoryInfo directoryInfo=new DirectoryInfo(Application.dataPath);
			string folderPath=string.Format("{0}/Excel",directoryInfo.Parent.Parent.FullName);
			string[] excelPaths=Directory.GetFiles(folderPath);
			foreach (string excelPath in excelPaths) {
				ImportExcel(excelPath);
			}
		}
	}
	void ImportExcel(string filePath){
		FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
		IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
		
		DataSet result = excelReader.AsDataSet();
		
		int columns = result.Tables[0].Columns.Count;
		int rows = result.Tables[0].Rows.Count;
		
		for(int i = 0;  i< rows; i++)
		{
			for(int j =0; j < columns; j++)
			{
				string  nvalue  = result.Tables[0].Rows[i][j].ToString();
				Debug.Log(nvalue);
			}
		}
		excelReader.Close ();
		/*
		FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
		
		//1. Reading from a binary Excel file ('97-2003 format; *.xls)
		//IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
		//...
		//2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
		IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
		//...
		//3. DataSet - The result of each spreadsheet will be created in the result.Tables
		DataSet result = excelReader.AsDataSet();
		//...
		//4. DataSet - Create column names from first row
		excelReader.IsFirstRowAsColumnNames = true;
		//DataSet result = excelReader.AsDataSet();
		
		//5. Data Reader methods
		while (excelReader.Read())
		{
			//excelReader.GetInt32(0);
		}
		
		//6. Free resources (IExcelDataReader is IDisposable)
		excelReader.Close();*/
	}
	
	
	void XLSX()
	{
	}
}
