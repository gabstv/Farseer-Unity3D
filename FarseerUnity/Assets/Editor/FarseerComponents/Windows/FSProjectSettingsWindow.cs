using UnityEngine;
using UnityEditor;
using System.Collections;
using Category = FarseerPhysics.Dynamics.Category;

public class FSProjectSettingsWindow : EditorWindow
{
	private static FSProjectSettingsWindow window = null;
	
	private Vector2 scrollPos = Vector2.zero;
	
	private bool showFSCategorySettings = false;
	private FSCategorySettings loadedFSCategorySettings;
	
	[MenuItem("Edit/Project Settings/FarseerUnity")]
	public static FSProjectSettingsWindow OpenWindow()
	{
		if(window != null)
		{
			window.Close();
			window = null;
		}
		window = CreateInstance<FSProjectSettingsWindow>();
		window.Setup();
		window.Show();
		return window;
	}
	
	public void Setup()
	{
		FSSettings.Load();
		loadedFSCategorySettings = FSSettings.CategorySettings;
	}
	
	private void OnGUI()
	{
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		EditorGUILayout.BeginVertical();
		showFSCategorySettings = EditorGUILayout.Foldout(showFSCategorySettings, "Collision Categories");
		if(showFSCategorySettings)
			GUI_FSCategorySettings();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
	}
	
	private void GUI_FSCategorySettings()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);
		loadedFSCategorySettings.CatAll = EditorGUILayout.TextField("All", loadedFSCategorySettings.CatAll);
		loadedFSCategorySettings.CatNone = EditorGUILayout.TextField("None", loadedFSCategorySettings.CatNone);
		for(int i = 0; i < loadedFSCategorySettings.Cat131.Length; i++)
		{
			loadedFSCategorySettings.Cat131[i] = EditorGUILayout.TextField("Cat" + (i + 1).ToString(), loadedFSCategorySettings.Cat131[i]);
		}
		EditorGUILayout.EndVertical();
	}
	
	private void OnDestroy()
	{
		Save ();
	}
	
	public void Save()
	{
		FSSettings.CategorySettings = loadedFSCategorySettings;
		FSSettings.Save();
		EditorApplication.RepaintProjectWindow();
	}
}
