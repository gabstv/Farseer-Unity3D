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

public class FSBodyComponent : MonoBehaviour
{
	protected Body body;
	
	public BodyType Type = BodyType.Dynamic;

	// Use this for initialization
	void Start ()
	{
		//body = BodyFactory.CreateRectangle(FSWorldComponent.PhysicsWorld, 1f, 1f, Density);
		body = new Body(FSWorldComponent.PhysicsWorld);
		FSShapeComponent[] shapecs = GetComponentsInChildren<FSShapeComponent>();
		print("shapes " + name + ": " + shapecs.Length);
		foreach(FSShapeComponent shp in shapecs)
		{
			Fixture f = body.CreateFixture(shp.GetShape());
			f.Friction = shp.Friction;
			f.Restitution = shp.Restitution;
			if(shp.UseCollisionGroups)
			{
				f.CollisionCategories = Category.None;
				for(int i = 0; i < shp.CollisionCategories.Length; i++)
				{
					f.CollisionCategories = f.CollisionCategories | shp.CollisionCategories[i];
				}
				f.CollidesWith = Category.None;
				for(int i = 0; i < shp.CollidesWith.Length; i++)
				{
					f.CollidesWith = f.CollidesWith | shp.CollidesWith[i];
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
				if(shape.UseCollisionGroups)
				{
					f.CollisionCategories = Category.None;
					for(int i = 0; i < shape.CollisionCategories.Length; i++)
					{
						f.CollisionCategories = shape.CollisionCategories[i] | f.CollisionCategories;
					}
					f.CollidesWith = Category.None;
					for(int i = 0; i < shape.CollidesWith.Length; i++)
					{
						f.CollidesWith = shape.CollidesWith[i] | f.CollidesWith;
					}
				}
			}
		}
		
		body.BodyType = Type;
		body.Position = new FVector2(transform.position.x, transform.position.y);
		body.Rotation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;
		pos.x = body.Position.X;
		pos.y = body.Position.Y;
		Vector3 rot = transform.rotation.eulerAngles;
		rot.z = body.Rotation * Mathf.Rad2Deg;
		Debug.Log("POS " + name + ": " + pos);
		transform.position = pos;
		transform.rotation = Quaternion.Euler(rot);
	}
	
	public Body PhysicsBody
	{
		get
		{
			return body;
		}
	}
}
