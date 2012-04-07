using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Category = FarseerPhysics.Dynamics.Category;

[CustomEditor(typeof(FSShapeComponent))]
public class FSShapeCpEditor : Editor
{
	private FSShapeComponent target0;
	protected FSCategorySettings categorySettings;
	
	public virtual void OnEnable()
	{
		target0 = target as FSShapeComponent;
		FSSettings.Load();
		categorySettings = FSSettings.CategorySettings;
	}
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();
		
		// draw collision filtering options
		
		EditorGUILayout.BeginVertical();
		
		target0.CollisionFilter = (CollisionGroupDef)EditorGUILayout.EnumPopup("Filter Collision", target0.CollisionFilter);
		
		if(target0.CollisionFilter == CollisionGroupDef.Manually)
		{
			bool flag0;
			bool flag1;
		
			target0.BelongsToFold = EditorGUILayout.Foldout(target0.BelongsToFold, "Belongs To");
			if(target0.BelongsToFold)
			{
				flag1 = (target0.BelongsTo & Category.All) == Category.All;
				flag0 = EditorGUILayout.Toggle("All", flag1);
				if(flag0 != flag1)
				{
					if(flag0)
						target0.BelongsTo = Category.All;
					else
						target0.BelongsTo = Category.None;
				}
				//Cat1 to Cat31
				for(int i = 0; i < categorySettings.Cat131.Length; i++)
				{
					flag1 = ((int)target0.BelongsTo & (int)Mathf.Pow(2f, (float)i)) != 0;
					flag0 = EditorGUILayout.Toggle(categorySettings.Cat131[i], flag1);
					
					// something changed
					if(flag0 != flag1)
					{
						if(flag0)
							target0.BelongsTo |= (Category)((int)Mathf.Pow(2f, (float)i));
						else
							target0.BelongsTo ^= (Category)((int)Mathf.Pow(2f, (float)i));
					}
				}
			}
			
			EditorGUILayout.Space();
			
			target0.CollidesWithFold = EditorGUILayout.Foldout(target0.CollidesWithFold, "Collides With");
			if(target0.CollidesWithFold)
			{
				flag1 = (target0.CollidesWith & Category.All) == Category.All;
				flag0 = EditorGUILayout.Toggle("All", flag1);
				if(flag0 != flag1)
				{
					if(flag0)
						target0.CollidesWith = Category.All;
					else
						target0.CollidesWith = Category.None;
				}
				//Cat1 to Cat31
				for(int i = 0; i < categorySettings.Cat131.Length; i++)
				{
					flag1 = ((int)target0.CollidesWith & (int)Mathf.Pow(2f, (float)i)) != 0;
					flag0 = EditorGUILayout.Toggle(categorySettings.Cat131[i], flag1);
					
					// something changed
					if(flag0 != flag1)
					{
						if(flag0)
							target0.CollidesWith |= (Category)((int)Mathf.Pow(2f, (float)i));
						else
							target0.CollidesWith ^= (Category)((int)Mathf.Pow(2f, (float)i));
					}
				}
			}
		}
		else if(target0.CollisionFilter == CollisionGroupDef.PresetFile)
		{
			target0.CollisionGroup = (FSCollisionGroup)EditorGUILayout.ObjectField("Group Preset File", target0.CollisionGroup, typeof(FSCollisionGroup), true);
		}
		
		EditorGUILayout.EndVertical();
	}
}
