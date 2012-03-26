using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

using Transform = UnityEngine.Transform;

namespace CatsintheSky.FarseerDebug
{
	public class BridgeTest : Test
	{
		public BridgeTest(Transform parent) : base(parent)
		{
			this.title = "Bridge";
		}
		
		public override void Start()
		{
			base.Start();
			
			Body body;
			RevoluteJoint rj;
			
			// BRIDGE
			{
				int numPlanks = 10;
				
				Body prevBody = BodyFactory.CreateCircle(FSWorldComponent.PhysicsWorld, 0.1f, 20f, new FVector2(100f / physScale, -250f / physScale));
				for(int i = 0; i < numPlanks; i++)
				{
					body = BodyFactory.CreateRectangle(FSWorldComponent.PhysicsWorld, 
						48f / physScale, //width
						10f / physScale, //height
						20f, //density
						new FVector2((100f + 22f + 44f * (float)i) / physScale, -250f / physScale)); // position
					body.BodyType = BodyType.Dynamic;
					rj = JointFactory.CreateRevoluteJoint(FSWorldComponent.PhysicsWorld, prevBody, body, new FVector2(-22f / physScale, 0f));
					rj.LowerLimit = -15f * Mathf.Deg2Rad;
					rj.UpperLimit = 15f * Mathf.Deg2Rad;
					rj.LimitEnabled = true;
					
					prevBody = body;
				}
				
				body = BodyFactory.CreateCircle(FSWorldComponent.PhysicsWorld, 0.1f, 20f, new FVector2((100f + 44f * (float)numPlanks) / physScale, -250f / physScale));
				rj = JointFactory.CreateRevoluteJoint(FSWorldComponent.PhysicsWorld, prevBody, body, new FVector2(0f, 0f));
				rj.LowerLimit = -15f * Mathf.Deg2Rad;
				rj.UpperLimit = 15f * Mathf.Deg2Rad;
				rj.LimitEnabled = true;
				
			}
			
			// Spawn in a bunch of crap
			for(int i = 0; i < 5; i++)
			{
				body = BodyFactory.CreateRectangle(FSWorldComponent.PhysicsWorld, 
					(Random.value * 10f + 20f) / physScale, //width
					(Random.value * 10f + 20f) / physScale, //height
					1f, //density
					new FVector2((Random.value * 400f + 120f) / physScale, (Random.value * -150f -50f) / physScale)); // position
				body.Rotation = Random.value * Mathf.PI;
				body.BodyType = BodyType.Dynamic;
			}
			for(int i = 0; i < 5; i++)
			{
				body = BodyFactory.CreateCircle(FSWorldComponent.PhysicsWorld, 
					(Random.value * 20f + 10f) / physScale, //radius
					1f, //density
					new FVector2((Random.value * 400f + 120f) / physScale, (Random.value * -150f -50f) / physScale)); // position
				body.Rotation = Random.value * Mathf.PI;
				body.BodyType = BodyType.Dynamic;
			}
			for(int i = 0; i < 15; i++)
			{
				Vertices vlist = new Vertices();
				if(Random.value > 0.66f)
				{
					vlist.Add(new FVector2((10f + Random.value * 10f) / physScale, (-10f - Random.value * 10f) / physScale));
					vlist.Add(new FVector2((5f + Random.value * 10f) / physScale, (10f + Random.value * 10f) / physScale));
					vlist.Add(new FVector2((-5f - Random.value * 10f) / physScale, (10f + Random.value * 10f) / physScale));
					vlist.Add(new FVector2((-10f - Random.value * 10f) / physScale, (-10f - Random.value * 10f) / physScale));
				}
				else if(Random.value > 0.5f)
				{
					FVector2 v00 = new FVector2(0f, (-10f - Random.value * 10f) / physScale);
					FVector2 v02 = new FVector2((-5f - Random.value * 10f) / physScale, (10f + Random.value * 10f) / physScale);
					FVector2 v03 = new FVector2((5f + Random.value * 10f) / physScale, (10f + Random.value * 10f) / physScale);
					FVector2 v01 = new FVector2(v00.X + v02.X, v00.Y + v02.Y);
					v01 *= Random.value / 2f + 0.8f;
					FVector2 v04 = new FVector2(v03.X + v00.X, v03.Y + v00.Y);
					v04 *= Random.value / 2f + 0.8f;
					vlist.Add(v04); vlist.Add(v03); vlist.Add(v02); vlist.Add(v01); vlist.Add(v00);
				}
				else
				{
					vlist.Add(new FVector2((5f + Random.value * 10f) / physScale, (10f + Random.value * 10f) / physScale));
					vlist.Add(new FVector2((-5f - Random.value * 10f) / physScale, (10f + Random.value * 10f) / physScale));
					vlist.Add(new FVector2(0f, (-10f - Random.value * 10f) / physScale));
				}
				body = BodyFactory.CreateCompoundPolygon(FSWorldComponent.PhysicsWorld, new List<Vertices>{vlist}, 1f, new FVector2((Random.value * 400f + 120f) / physScale, (Random.value * -150f - 50f) / physScale));
				body.Rotation = Random.value * Mathf.PI;
				body.BodyType = BodyType.Dynamic;
			}
		}
	}
}

