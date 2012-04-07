using UnityEngine;
using UnityEditor;
using System.Collections;

public class CollisionGroupAsset
{
	
	[MenuItem("Assets/Create/FarseerUnity Collision Group")]
	public static void CreateAsset()
	{
		CustomAssetUtility.CreateAsset<FSCollisionGroup>();
	}
}
