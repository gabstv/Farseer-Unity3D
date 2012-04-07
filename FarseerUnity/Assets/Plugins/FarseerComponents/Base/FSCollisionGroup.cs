using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;

[System.Serializable]
public class FSCollisionGroup : ScriptableObject
{
	public Category BelongsTo = Category.Cat1;
	[HideInInspector]
	public bool BelongsToFold = true;
	public Category CollidesWith = Category.All;
	[HideInInspector]
	public bool CollidesWithFold = true;
}
