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
using FarseerPhysics.Controllers;
using Microsoft.Xna.Framework;

using Transform = UnityEngine.Transform;

namespace CatsintheSky.FarseerDebug
{
	public class BuoyancyTest : Test
	{
		private BuoyancyController controller;
		private List<Body> bodies;
		
		public BuoyancyTest(Transform parent) : base(parent)
		{
			this.title = "Buoyancy";
		}
		
		public override void Start()
		{
			base.Start();
			
			AABB waterBounds = new AABB(new FVector2(0f, -360f / physScale), new FVector2(640f / physScale, -200f / physScale));
			controller = new BuoyancyController(waterBounds, 2.0f, 5f, 2f, FSWorldComponent.PhysicsWorld.Gravity);
			
			// add the controller
			FSWorldComponent.PhysicsWorld.AddController(controller);
			
			bodies = new List<Body>();
			Body tbody;
			// Spawn in a bunch of crap
			for(int i = 0; i < 5; i++)
			{
				tbody = BodyFactory.CreateRectangle(FSWorldComponent.PhysicsWorld, (Random.value * 5f + 10f) / physScale, (Random.value * 5f + 10f) / physScale, 1f, new FVector2((Random.value * 400f + 120f) / physScale, (Random.value * -150f - 50f) / physScale));
				tbody.Rotation = Random.value * Mathf.PI;
				tbody.BodyType = BodyType.Dynamic;
				bodies.Add(tbody);
			}
			for(int i = 0; i < 5; i++)
			{
				tbody = BodyFactory.CreateCircle(FSWorldComponent.PhysicsWorld, (Random.value * 5f + 10f) / physScale, 0.5f, new FVector2((Random.value * 400f + 120f) / physScale, (Random.value * -150f - 50f) / physScale));
				tbody.Rotation = Random.value * Mathf.PI;
				tbody.BodyType = BodyType.Dynamic;
				bodies.Add(tbody);
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
				tbody = BodyFactory.CreateCompoundPolygon(FSWorldComponent.PhysicsWorld, new List<Vertices>{vlist}, 1f, new FVector2((Random.value * 400f + 120f) / physScale, (Random.value * -150f - 50f) / physScale));
				tbody.Rotation = Random.value * Mathf.PI;
				tbody.BodyType = BodyType.Dynamic;
				bodies.Add(tbody);
			}
			
			//Add some exciting bath toys
			tbody = new Body(FSWorldComponent.PhysicsWorld);
			tbody.Position = new FVector2(50f / physScale, -300f / physScale);
			tbody.BodyType = BodyType.Dynamic;
			FixtureFactory.AttachRectangle(80f / physScale, 20f / physScale, 3f, FVector2.Zero, tbody);
			bodies.Add(tbody);
			
			tbody = new Body(FSWorldComponent.PhysicsWorld);
			tbody.Position = new FVector2(300f / physScale, -300f / physScale);
			tbody.BodyType = BodyType.Dynamic;
			FixtureFactory.AttachSolidArc(2f, Mathf.PI * 2f, 8, 7f / physScale,
				new FVector2(30f / physScale, 0f), 0f, tbody);
			FixtureFactory.AttachSolidArc(2f, Mathf.PI * 2f, 8, 7f / physScale,
				new FVector2(-30f / physScale, 0f), 0f, tbody);
			FixtureFactory.AttachRectangle(60f / physScale, 4f / physScale, 2f, FVector2.Zero, tbody);
			FixtureFactory.AttachSolidArc(2f, Mathf.PI * 2f, 8, 7f / physScale,
				new FVector2(0f, 30f / physScale), 0f, tbody);
			FixtureFactory.AttachSolidArc(2f, Mathf.PI * 2f, 8, 7f / physScale,
				new FVector2(0f, -30f / physScale), 0f, tbody);
			FixtureFactory.AttachRectangle(4f / physScale, 60f / physScale, 2f, FVector2.Zero, tbody);
		}
	}
}

