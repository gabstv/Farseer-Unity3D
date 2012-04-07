/*
* FarseerUnity based on Farseer Physics Engine port:
* Copyright (c) 2012 Gabriel Ochsenhofer https://github.com/gabstv/Farseer-Unity3D
* 
* Original source Box2D:
* Copyright (c) 2011 Ian Qvist http://farseerphysics.codeplex.com/
* 
* This software is provided 'as-is', without any express or implied 
* warranty.  In no event will the authors be held liable for any damages 
* arising from the use of this software. 
* Permission is granted to anyone to use this software for any purpose, 
* including commercial applications, and to alter it and redistribute it 
* freely, subject to the following restrictions: 
* 1. The origin of this software must not be misrepresented; you must not 
* claim that you wrote the original software. If you use this software 
* in a product, an acknowledgment in the product documentation would be 
* appreciated but is not required. 
* 2. Altered source versions must be plainly marked as such, and must not be 
* misrepresented as being the original software. 
* 3. This notice may not be removed or altered from any source distribution. 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FSTransform = FarseerPhysics.Common.Transform;
using Transform = UnityEngine.Transform;
using Microsoft.Xna.Framework;

using Category = FarseerPhysics.Dynamics.Category;

[AddComponentMenu("FarseerUnity/Collision/Basic Shape Component")]
public class FSShapeComponent : MonoBehaviour
{
	public ShapeType SType = ShapeType.Polygon;
	public bool UseUnityCollider = true;
	
	public float Density = 1f;
	public float Restitution = 0.5f;
	public float Friction = 0.75f;
	
	/// <summary>
	/// The polygon points. MUST BE COUNTER-CLOCKWISE
	/// </summary>
	public Transform[] PolygonPoints;
	
	[HideInInspector]
	public CollisionGroupDef CollisionFilter = CollisionGroupDef.None;
	
	[HideInInspector]
	public FSCollisionGroup CollisionGroup;
	
	[HideInInspector]
	public Category BelongsTo = Category.Cat1;
	[HideInInspector]
	public bool BelongsToFold = false;
	[HideInInspector]
	public Category CollidesWith = Category.All;
	[HideInInspector]
	public bool CollidesWithFold = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrawGizmos()
	{
		// draw children
		Gizmos.color = Color.red;
		if(this.transform.childCount > 0)
		{
			for(int i = 0; i < this.transform.childCount; i++)
			{
				Gizmos.DrawWireSphere(this.transform.GetChild(i).position, 0.15f);
			}
		}
		
		if(SType == ShapeType.Circle)
		{
			SphereCollider sc = GetComponent<SphereCollider>();
			if(sc != null)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(transform.position, sc.radius*transform.lossyScale.x);
			}
		}
		
		if(PolygonPoints == null)
			return;
		if(PolygonPoints.Length < 3)
			return;
		
		Color c = Color.yellow;
		
		for(int i = 1; i < PolygonPoints.Length; i++)
		{
			Gizmos.color = c;
			Gizmos.DrawLine(PolygonPoints[i-1].position, PolygonPoints[i].position);
			c.r += 0.05f;
			c.g += 0.05f;
			c.b += 0.05f;
		}
		Gizmos.DrawLine(PolygonPoints[PolygonPoints.Length-1].position, PolygonPoints[0].position);
	}
	
	public Shape GetShape()
	{
		if(SType == ShapeType.Polygon)
			return GetPolyShape();
		else if(SType == ShapeType.Circle)
		{
			SphereCollider sc = GetComponent<SphereCollider>();
			if(sc == null)
				return null;
			return new CircleShape(sc.radius*transform.lossyScale.x, Density);
		}
		return null;
	}
	protected PolygonShape GetPolyShape()
	{
		// if this GameObject contains the FSBodyComponent
		// then there is no need to adjust the colliders!
		bool isBody = GetComponent<FSBodyComponent>() != null;
		
		List<FVector2> vs = new List<FVector2>();
		if(UseUnityCollider)
		{
			BoxCollider bc = this.GetComponent<BoxCollider>();
			if(bc == null)
				return null;
			
			Vector3 scale = transform.lossyScale;
			//Debug.Log("SCALE: " + scale);
			
			Vector3 v00l = new Vector3(-bc.size.x * 0.5f, -bc.size.y * 0.5f);
			Vector3 v01l = new Vector3(bc.size.x * 0.5f, -bc.size.y * 0.5f);
			Vector3 v02l = new Vector3(bc.size.x * 0.5f, bc.size.y * 0.5f);
			Vector3 v03l = new Vector3(-bc.size.x * 0.5f, bc.size.y * 0.5f);
			
			v00l.Scale(scale);
			v01l.Scale(scale);
			v02l.Scale(scale);
			v03l.Scale(scale);
			
			
			Vector3 v00 = isBody ? v00l : FSHelper.LocalTranslatedVec3(v00l, this.transform);
			//v00.Scale(scale);
			Vector3 v01 = isBody ? v01l : FSHelper.LocalTranslatedVec3(v01l, this.transform);
			//v01.Scale(scale);
			Vector3 v02 = isBody ? v02l : FSHelper.LocalTranslatedVec3(v02l, this.transform);
			//v02.Scale(scale);
			Vector3 v03 = isBody ? v03l : FSHelper.LocalTranslatedVec3(v03l, this.transform);
			//v03.Scale(scale);
			vs.Add(FSHelper.Vector3ToFVector2(v00));
			vs.Add(FSHelper.Vector3ToFVector2(v01));
			vs.Add(FSHelper.Vector3ToFVector2(v02));
			vs.Add(FSHelper.Vector3ToFVector2(v03));
		}
		else
		{
			if(PolygonPoints.Length < 3)
				return null;
			//vs = new Vertices();
			for(int i = 0; i < PolygonPoints.Length; i++)
			{
				if(!isBody)
				{
					// updated!
					// now the points can be anywhere
					Vector3 localp = transform.parent.InverseTransformPoint(PolygonPoints[i].position);
					vs.Add(FSHelper.Vector3ToFVector2(FSHelper.LocalTranslatedVec3(localp, this.transform.parent)));
					// not doing this way anymore because this way points couldn't be shared betweeen shapes
					//vs.Add(FSHelper.Vector3ToFVector2(FSHelper.LocalTranslatedVec3(PolygonPoints[i].localPosition, this.transform)));
				}
				else
					vs.Add(FSHelper.Vector3ToFVector2(PolygonPoints[i].localPosition));
			}
		}
		
		return new PolygonShape(new Vertices(vs.ToArray()), Density);
	}
}
