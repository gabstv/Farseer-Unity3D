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
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

[AddComponentMenu("FarseerUnity/Dynamics/Body Component")]
public class FSBodyComponent : MonoBehaviour
{
	protected Body body;
	
	public BodyType Type = BodyType.Dynamic;

	protected bool initialized = false;
	
	public virtual void Start ()
	{
		if(initialized)
			return;
		initialized = true;
		//body = BodyFactory.CreateRectangle(FSWorldComponent.PhysicsWorld, 1f, 1f, Density);
		body = new Body(FSWorldComponent.PhysicsWorld);
		FSShapeComponent[] shapecs = GetComponentsInChildren<FSShapeComponent>();
		//print("shapes " + name + ": " + shapecs.Length);
		foreach(FSShapeComponent shp in shapecs)
		{
			Fixture f = body.CreateFixture(shp.GetShape());
			f.Friction = shp.Friction;
			f.Restitution = shp.Restitution;
			if(shp.tag.Length > 0)
				f.UserTag = shp.tag;
			if(shp.CollisionFilter == CollisionGroupDef.Manually)
			{
				f.CollisionCategories = shp.BelongsTo;
				f.CollidesWith = shp.CollidesWith;
			}
			else if(shp.CollisionFilter == CollisionGroupDef.PresetFile)
			{
				if(shp.CollisionGroup != null)
				{
					f.CollisionCategories = shp.CollisionGroup.BelongsTo;
					f.CollidesWith = shp.CollisionGroup.CollidesWith;
				}
			}
		}
		// try to get a single shape at the same level
		// if theres no children
		if(shapecs.Length < 1)
		{
			FSShapeComponent shape = GetComponent<FSShapeComponent>();
			if(shape != null)
			{
				Fixture f = body.CreateFixture(shape.GetShape());
				f.Friction = shape.Friction;
				f.Restitution = shape.Restitution;
				if(shape.tag.Length > 0)
					f.UserTag = shape.tag;
				if(shape.CollisionFilter == CollisionGroupDef.Manually)
				{
					f.CollisionCategories = shape.BelongsTo;
					f.CollidesWith = shape.CollidesWith;
				}
				else if(shape.CollisionFilter == CollisionGroupDef.PresetFile)
				{
					if(shape.CollisionGroup != null)
					{
						f.CollisionCategories = shape.CollisionGroup.BelongsTo;
						f.CollidesWith = shape.CollisionGroup.CollidesWith;
					}
				}
			}
		}
		
		body.BodyType = Type;
		body.Position = new FVector2(transform.position.x, transform.position.y);
		body.Rotation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		if(this.tag.Length > 0)
			body.UserTag = this.tag;
		body.UserFSBodyComponent = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;
		pos.x = body.Position.X;
		pos.y = body.Position.Y;
		Vector3 rot = transform.rotation.eulerAngles;
		rot.z = body.Rotation * Mathf.Rad2Deg;
		transform.position = pos;
		transform.rotation = Quaternion.Euler(rot);
	}
	
	protected virtual void OnDestroy()
	{
		// destroy this body on Farseer Physics
		FSWorldComponent.PhysicsWorld.RemoveBody(PhysicsBody);
	}
	
	public Body PhysicsBody
	{
		get
		{
			if(!initialized)
				Start();
			return body;
		}
	}
}
