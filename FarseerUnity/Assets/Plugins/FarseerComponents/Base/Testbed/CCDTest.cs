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
	public class CCDTest : Test
	{
		public CCDTest(Transform parent) : base(parent)
		{
			this.title = "Continuous Collision Detection";
		}
		
		public override void Start()
		{
			base.Start();
			
			Body body;
			
			// Create 'basket'
			{
				body = BodyFactory.CreateBody(FSWorldComponent.PhysicsWorld, new FVector2(150f / physScale, -100f / physScale));
				body.BodyType = BodyType.Dynamic;
				body.IsBullet = true;
				
				// bottom fixture
				Fixture f = FixtureFactory.AttachRectangle(90f / physScale, 9f / physScale, 4f, FVector2.Zero, body);
				f.Restitution = 1.4f;
				
				// left fixture
				f = FixtureFactory.AttachRectangle(9f / physScale, 90f / physScale, 4f, new FVector2(-43.5f/physScale, 50.5f/physScale), body);
				f.Restitution = 1.4f;
				
				// right fixture
				f = FixtureFactory.AttachRectangle(9f / physScale, 90f / physScale, 4f, new FVector2(43.5f/physScale, 50.5f/physScale), body);
				f.Restitution = 1.4f;
			}
			
			// add some small circles for effect
			for(int i = 0; i < 5; i++)
			{
				body = BodyFactory.CreateBody(FSWorldComponent.PhysicsWorld, new FVector2((Random.value * 300f + 250f) / physScale, (Random.value * -320f - 20f) / physScale));
				Fixture f = FixtureFactory.AttachCircle((Random.value * 10f + 5f) / physScale, 1f, body);
				f.Friction = 0.3f;
				f.Restitution = 1.1f;
				body.BodyType = BodyType.Dynamic;
				body.IsBullet = true;
			}
		}
	}
}

