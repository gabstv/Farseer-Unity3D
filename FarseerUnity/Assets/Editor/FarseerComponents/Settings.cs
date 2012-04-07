using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

using Category = FarseerPhysics.Dynamics.Category;

[System.Serializable]
public class FSCategorySettings
{
	public string CatAll = "All";
	public string CatNone = "None";
	public string[] Cat131;
	
	public FSCategorySettings()
	{
		Cat131 = new string[31];
		for(int i = 0; i < Cat131.Length; i++)
		{
			Cat131[i] = "Cat"+(i+1).ToString();
		}
	}
}

public static class FSSettings
{
	private static FSCategorySettings categorySettings;
	
	public static void Load()
	{
		// path setup
		string path = Application.dataPath + "/Editor/FarseerComponents/SerializedSettings";
		if(!Directory.Exists(path))
			Directory.CreateDirectory(path);
		
		//setup vars
		FileStream fs;
		XmlSerializer xmls;
		
		//FSCategorySettings
		if(File.Exists(path + "/FSCategorySettings.cfg"))
		{
			xmls = new XmlSerializer(typeof(FSCategorySettings));
			fs = new FileStream(path + "/FSCategorySettings.cfg", FileMode.Open);
			categorySettings = xmls.Deserialize(fs) as FSCategorySettings;
			fs.Close();
		}
		else
		{
			categorySettings = new FSCategorySettings();
		}
		
	}
	
	public static void Save()
	{
		// path setup
		string path = Application.dataPath + "/Editor/FarseerComponents/SerializedSettings";
		if(!Directory.Exists(path))
			Directory.CreateDirectory(path);
		
		//setup vars
		FileStream fs;
		XmlSerializer xmls;
		StreamWriter sw;
		
		//FSCategorySettings
		xmls = new XmlSerializer(typeof(FSCategorySettings));
		if(File.Exists(path + "/FSCategorySettings.cfg"))
			fs = File.Open(path + "/FSCategorySettings.cfg", FileMode.Truncate);
		else
			fs = File.Create(path + "/FSCategorySettings.cfg");
		sw = new StreamWriter(fs);
		xmls.Serialize(sw, categorySettings);
		sw.Close();
	}
	
	//
	
	public static FSCategorySettings CategorySettings
	{
		get
		{
			if(categorySettings == null)
				Load();
			return categorySettings;
		}
		set
		{
			categorySettings = value;
			//Save();
		}
	}
}