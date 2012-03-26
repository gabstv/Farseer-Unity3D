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
using Microsoft.Xna.Framework;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision;

public class FSBuoyancyComponent : MonoBehaviour
{
	public Vector2 StartPoint;
	public Vector2 EndPoint;
	protected AABB aabb;
	public float Density = 2f;
	public float LinearDragCoef = 0.987f;
	public float RotationalDragCoef =  0.987f;
	
	protected BuoyancyController buoyancyController;

	// Use this for initialization
	void Start ()
	{
		Vector3 p = transform.position;
		FVector2 aa = new FVector2(p.x + StartPoint.x, p.y + StartPoint.y);
		FVector2 bb = new FVector2(p.x + EndPoint.x, p.y + EndPoint.y);
		aabb = new AABB(aa, bb);
		buoyancyController = new BuoyancyController(aabb, Density, LinearDragCoef, RotationalDragCoef, FSWorldComponent.PhysicsWorld.Gravity);
		FSWorldComponent.PhysicsWorld.AddController(buoyancyController);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrawGizmos()
	{
		Vector3 p = transform.position;
		Vector3 a,b,c,d;
		a = p;
		b = p;
		c = p;
		d = p;
		Gizmos.color = Color.red;
		a.x += StartPoint.x;
		a.y += StartPoint.y;
		b.x += StartPoint.x;
		b.y += EndPoint.y;
		c.x += EndPoint.x;
		c.y += EndPoint.y;
		d.x += EndPoint.x;
		d.y += StartPoint.y;
		Gizmos.DrawLine(a, b);
		Gizmos.DrawLine(b, c);
		Gizmos.DrawLine(c, d);
		Gizmos.DrawLine(d, a);
	}
}
