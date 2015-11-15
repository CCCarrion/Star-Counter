using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Excel;
public class ImportExcelEditorWindow : EditorWindow{
	[MenuItem ("Star Counter/ImportExcel")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		ImportExcelEditorWindow window = (ImportExcelEditorWindow)EditorWindow.GetWindow (typeof (ImportExcelEditorWindow));
	}
	string dstDirName;
	string[] excelPaths;
	char[] charArray= new char[]{'\t','\n'};
	void OnEnable(){
		dstDirName = string.Format("{0}/Resources/Configs/",Application.dataPath);
	}
	void OnGUI(){
		if (GUILayout.Button ("Import Excels")) {
			DirectoryInfo directoryInfo=new DirectoryInfo(Application.dataPath);
			string folderPath=string.Format("{0}/Excel",directoryInfo.Parent.Parent.FullName);
			excelPaths = Directory.GetFiles (folderPath);
			foreach (string excelPath in excelPaths) {
				ImportExcel(excelPath);
			}
		}
	}
	void ImportExcel(string excelPath){
		FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read);
		IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
		
		DataSet result = excelReader.AsDataSet();
		foreach (DataTable table in result.Tables) {
			ExportTxt(table,table.TableName);
		}
		excelReader.Close ();
		stream.Dispose ();
		AssetDatabase.Refresh ();
	}
	void ExportTxt(DataTable dt, string tabName) {
		//tabName = tabName.Replace("(p)", "");
		Debug.Log(string.Format("Export {0}.txt,targetName:{1}", tabName,dstDirName+ tabName + ".txt"));
		using (StreamWriter writer = new StreamWriter(dstDirName + tabName + ".txt")) {
			bool isFirstRow = true;
			int fieldNum = 0;
			foreach (DataRow row in dt.Rows) {
				List<string> elemList = new List<string>();
				bool haveFirstCol = true;
				for (int i = 0; i < row.ItemArray.Length; ++i) {
					if (!isFirstRow && i >= fieldNum)
						break;
					string elem = row.ItemArray[i].ToString();
					if (isFirstRow && elem == string.Empty)
						break;
					if (isFirstRow && elem == string.Empty) {
						haveFirstCol = false;
						break;
					}
					elemList.Add(elem.ToString().TrimEnd().TrimStart(charArray));
				}
				if (!haveFirstCol)
					break;
				if (isFirstRow)
					fieldNum = elemList.Count;
				isFirstRow = false;
				bool isEmpty = true;
				foreach (string item in elemList) {
					if (item != string.Empty) {
						isEmpty = false;
						break;
					}
				}
				if (isEmpty)
					continue;
				string line = string.Join("\t", elemList.ToArray());
				writer.WriteLine(line);
			}
		}
	}
}
