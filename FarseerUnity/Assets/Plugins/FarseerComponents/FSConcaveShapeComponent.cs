using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Category = FarseerPhysics.Dynamics.Category;

[AddComponentMenu("FarseerUnity/Collision/Concave Shape Component")]
public class FSConcaveShapeComponent : MonoBehaviour
{
	public float Density = 1f;
	public float Restitution = 0.5f;
	public float Friction = 0.75f;
	
	[HideInInspector]
	public CollisionGroupDef CollisionFilter = CollisionGroupDef.None;
	
	public FSCollisionGroup CollisionGroup;
	
	public Category BelongsTo = Category.Cat1;
	public bool BelongsToFold = false;
	public Category CollidesWith = Category.All;
	public bool CollidesWithFold = false;
	
	[HideInInspector]
	public Vector3[,] ConvertedVertices;
	
	[HideInInspector]
	public FSShapePointInput PointInput = FSShapePointInput.Transform;
	
	[HideInInspector]
	public Transform[] TransformPoints;
	
	[HideInInspector]
	public Vector2[] ConcavePoints;
	
	public virtual void OnDrawGizmos()
	{
		//get children
		if(PointInput == FSShapePointInput.Transform)
		{
			List<Transform> tps = new List<Transform>(TransformPoints);
			for(int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if(tps.Contains(child))
				{
					Gizmos.color = Color.magenta;
					Gizmos.DrawSphere(child.position, 0.13f);
				}
				else
				{
					Gizmos.color = Color.white;
					Gizmos.DrawWireSphere(child.position, 0.1f);
				}
				
			}
			// draw connections
			Transform last = null;
			if(TransformPoints != null)
			{
				if(TransformPoints.Length > 2)
				{
					last = TransformPoints[TransformPoints.Length - 1];
					for(int i = 0; i < TransformPoints.Length; i++)
					{
						if(last != null && TransformPoints[i] != null)
						{
							Gizmos.color = Color.magenta;
							Gizmos.DrawLine(last.position, TransformPoints[i].position);
							// draw id
							Vector3 txtp = last.position;
							txtp += (TransformPoints[i].position - last.position) / 2f;
							Gizmos.color = Color.white;
							GizmosHelper.DrawString(txtp, i.ToString());
						}
						last = TransformPoints[i];
					}
				}
			}
		}
		else
		{
			
		}
		
	}
}
