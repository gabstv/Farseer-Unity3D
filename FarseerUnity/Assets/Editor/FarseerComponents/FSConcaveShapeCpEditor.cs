using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Category = FarseerPhysics.Dynamics.Category;

[CustomEditor(typeof(FSConcaveShapeComponent))]
public class FSConcaveShapeCpEditor : Editor
{
	public SerializedObject EditTarget;
	protected FSConcaveShapeComponent target0;
	protected FSCategorySettings categorySettings;
	
	public virtual void OnEnable()
	{
		target0 = target as FSConcaveShapeComponent;
		EditTarget = new SerializedObject(target0);
		FSSettings.Load();
		categorySettings = FSSettings.CategorySettings;
	}
	
	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls();
		EditTarget.Update();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.Separator();
		target0.PointInput = (FSShapePointInput)EditorGUILayout.EnumPopup("Define points by", target0.PointInput);
		EditorGUIUtility.LookLikeInspector();
		EditorGUILayout.Separator();
		if(target0.PointInput == FSShapePointInput.Transform)
		{
			if(target0.TransformPoints == null)
				target0.TransformPoints = new Transform[0];
			
			EditorGUILayout.BeginVertical(GUI.skin.box);
			SerializedProperty tp0 = EditTarget.FindProperty("TransformPoints"); // name
			EditorGUILayout.PropertyField(tp0);
			EditorGUILayout.BeginVertical(GUI.skin.box);
			tp0.isExpanded = EditorGUILayout.Toggle("show", tp0.isExpanded);
			if(tp0.isExpanded)
			{
				tp0.Next(true);
				//EditorGUILayout.PropertyField(tp0);
				tp0.Next(true);
				EditorGUILayout.PropertyField(tp0);
				EditorGUILayout.Separator();
				while(tp0.Next(true))
				{
					//EditorGUILayout.LabelField(tp0.name);// m_FileID m_PathID data
					if(!tp0.propertyPath.StartsWith("TransformPoints"))
						break;
					if(tp0.name == "data")
						EditorGUILayout.PropertyField(tp0);
				}
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			//
			EditorGUILayout.Separator();
		}
		else
		{
			if(target0.ConcavePoints == null)
				target0.ConcavePoints = new Vector2[0];
			
			EditorGUILayout.BeginVertical(GUI.skin.box);
			SerializedProperty cp0 = EditTarget.FindProperty("ConcavePoints");
			EditorGUILayout.PropertyField(cp0);
			EditorGUILayout.BeginVertical(GUI.skin.box);
			cp0.isExpanded = EditorGUILayout.Toggle("show", cp0.isExpanded);
			if(cp0.isExpanded)
			{
				if(cp0.Next(true))
				{
					//EditorGUILayout.PropertyField(cp0);
					if(cp0.Next(true))
						EditorGUILayout.PropertyField(cp0);
					EditorGUILayout.Separator();
				}
				while(cp0.Next(true))
				{
					//EditorGUILayout.LabelField(cp0.name); m_FileID m_PathID data
					if(!cp0.propertyPath.StartsWith("ConcavePoints"))
						break;
					//if(cp0.name == "data")
						EditorGUILayout.PropertyField(cp0);
				}
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			EditorGUILayout.Separator();
		}
		target0.Density = EditorGUILayout.FloatField("Density", target0.Density);
		target0.Friction = EditorGUILayout.FloatField("Friction", target0.Friction);
		target0.Restitution = EditorGUILayout.FloatField("Restitution", target0.Restitution);
		
		EditorGUILayout.Separator();
		//target0.UseCollisionGroups = EditorGUILayout.Toggle("Use Collision Groups", target0.UseCollisionGroups);
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
			/*SerializedProperty colcats = EditTarget.FindProperty("CollisionCategories");
			EditorGUILayout.PropertyField(colcats);
			if(colcats.isExpanded)
			{
				if(colcats.Next(true))
				{
					if(colcats.Next(true))
						EditorGUILayout.PropertyField(colcats);
				}
				while(colcats.Next(true))
				{
					if(!colcats.propertyPath.StartsWith("CollisionCategories"))
						break;
					EditorGUILayout.PropertyField(colcats);
				}
			}
			//
			SerializedProperty colwith = EditTarget.FindProperty("CollidesWith");
			EditorGUILayout.PropertyField(colwith);
			if(colwith.isExpanded)
			{
				if(colwith.Next(true))
				{
					if(colwith.Next(true))
						EditorGUILayout.PropertyField(colwith);
				}
				while(colwith.Next(true))
				{
					if(!colwith.propertyPath.StartsWith("CollidesWith"))
						break;
					EditorGUILayout.PropertyField(colwith);
				}
			}*/
		}
		else if(target0.CollisionFilter == CollisionGroupDef.PresetFile)
		{
			target0.CollisionGroup = (FSCollisionGroup)EditorGUILayout.ObjectField("Group Preset File", target0.CollisionGroup, typeof(FSCollisionGroup), true);
		}
		EditorGUILayout.Separator();
		bool convert = GUILayout.Button("Generate convex shapes");
		
		EditorGUILayout.EndVertical();
		//base.OnInspectorGUI ();
		EditTarget.ApplyModifiedProperties();
		
		if(convert)
			ConvertToConvex(target0);
		Rect r = EditorGUILayout.BeginVertical();
		Vector3 offs = new Vector3(r.x + 50f, r.y + 50f, 0f);
		//Handles.BeginGUI(r);
		Transform prev;
		if(target0.TransformPoints != null)
		{
			if(target0.TransformPoints.Length > 2)
			{
				prev = target0.TransformPoints[target0.TransformPoints.Length - 1];
				for(int i = 0; i < target0.TransformPoints.Length; i++)
				{
					if(target0.TransformPoints[i] != null && prev != null)
					{
						Handles.DrawLine(offs + prev.localPosition * 10f, offs + target0.TransformPoints[i].localPosition * 10f);
					}
					prev = target0.TransformPoints[i];
				}
			}
		}
		//Handles.EndGUI();
		EditorGUILayout.EndVertical();
	}
	
	protected virtual void ConvertToConvex(FSConcaveShapeComponent targetCSC)
	{
		FSShapeComponent[] childcomps = targetCSC.GetComponentsInChildren<FSShapeComponent>();
		if(childcomps != null)
		{
			if(childcomps.Length > 0)
			{
				for(int i = 0; i < childcomps.Length; i++)
				{
					if(childcomps[i] == null)
						continue;
					if(childcomps[i].gameObject == null)
						continue;
					DestroyImmediate(childcomps[i].gameObject);
				}
			}
		}
		// convert vertices
		FarseerPhysics.Common.Vertices concaveVertices = new FarseerPhysics.Common.Vertices();
		
		if(targetCSC.PointInput == FSShapePointInput.Transform)
		{
			for(int i = 0; i < targetCSC.TransformPoints.Length; i++)
			{
				concaveVertices.Add(FSHelper.Vector3ToFVector2(targetCSC.TransformPoints[i].localPosition));
			}
		}
		List<FarseerPhysics.Common.Vertices> convexShapeVs = FarseerPhysics.Common.Decomposition.BayazitDecomposer.ConvexPartition(concaveVertices);
		
		for(int i = 0; i < convexShapeVs.Count; i++)
		{
			GameObject newConvShape = new GameObject("convexShape"+i.ToString());
			newConvShape.transform.parent = targetCSC.transform;
			newConvShape.transform.localPosition = Vector3.zero;
			newConvShape.transform.localRotation = Quaternion.Euler(Vector3.zero);
			newConvShape.transform.localScale = Vector3.one;
			FSShapeComponent shape0 = newConvShape.AddComponent<FSShapeComponent>();
			shape0.CollidesWith = targetCSC.CollidesWith;
			shape0.CollisionFilter = targetCSC.CollisionFilter;
			shape0.BelongsTo = targetCSC.BelongsTo;
			shape0.CollisionGroup = targetCSC.CollisionGroup;
			shape0.Friction = targetCSC.Friction;
			shape0.Restitution = targetCSC.Restitution;
			shape0.Density = targetCSC.Density;
			shape0.UseUnityCollider = false;
			shape0.PolygonPoints = new Transform[convexShapeVs[i].Count];
			for(int j = 0; j < convexShapeVs[i].Count; j++)
			{
				GameObject pnew = new GameObject("p"+j.ToString());
				pnew.transform.parent = shape0.transform;
				pnew.transform.localPosition = FSHelper.FVector2ToVector3(convexShapeVs[i][j]);
				shape0.PolygonPoints[j] = pnew.transform;
			}
		}
	}
}
