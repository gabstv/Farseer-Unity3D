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
	public class TheoJansenTest : Test
	{
		private float tScale;
		private FVector2 m_offset = new FVector2(0f, 0f);
		private Body m_chassis;
		private Body m_wheel;
		private RevoluteJoint m_motorJoint;
		private bool m_motorOn = true;
		private float m_motorSpeed;
		
		public TheoJansenTest(Transform parent) : base(parent)
		{
			this.title = "Theo Jansen Walker";
		}
		
		public override void Start()
		{
			base.Start();
			
			Body body;
			
			tScale = physScale * 2f;
			
			// St position in world space
			m_offset = new FVector2(180f/physScale, -200f/physScale);
			m_motorSpeed = 2f;
			m_motorOn = true;
			
			FVector2 pivot = new FVector2(0f, 24f/tScale);
			
			for(int i = 0; i < 50; i++)
			{
				body = BodyFactory.CreateCircle(FSWorldComponent.PhysicsWorld, 3.75f /  physScale, 1f, new FVector2((Random.value * 620f + 10f)/physScale, -340f/physScale));
				body.BodyType = BodyType.Dynamic;
			}
			
			// chassis
			{
				m_chassis = BodyFactory.CreateBody(FSWorldComponent.PhysicsWorld, FVector2.Add(pivot, m_offset));
				m_chassis.BodyType = BodyType.Dynamic;
				Fixture m_chassis_f = FixtureFactory.AttachRectangle(150f / tScale, 60f / tScale, 1f, FVector2.Zero, m_chassis);
				//m_chassis_f.CollisionGroup = -1;
				m_chassis_f.CollisionCategories = Category.Cat10;
				m_chassis_f.CollidesWith = Category.Cat1;
			}
			// wheel
			{
				m_wheel = BodyFactory.CreateBody(FSWorldComponent.PhysicsWorld, FVector2.Add(pivot, m_offset));
				m_wheel.BodyType = BodyType.Dynamic;
				Fixture m_wheel_f = FixtureFactory.AttachCircle(48f / tScale, 1f, m_wheel);
				//m_wheel_f.CollisionGroup = -1;
				m_wheel_f.CollisionCategories = Category.Cat10;
				m_wheel_f.CollidesWith = Category.Cat1;
			}
			// glue chassis & wheel
			{
				m_motorJoint = JointFactory.CreateRevoluteJoint(FSWorldComponent.PhysicsWorld, m_wheel, m_chassis, FVector2.Zero);
				m_motorJoint.MotorSpeed = m_motorSpeed;
				m_motorJoint.MaxMotorTorque = 400f;
				m_motorJoint.CollideConnected = false;
				m_motorJoint.MotorEnabled = m_motorOn;
			}
			
			FVector2 wheelAnchor;
			
			wheelAnchor = new FVector2(0f, -24f/tScale) + pivot;
			
			CreateLeg(-1f, wheelAnchor);
			CreateLeg(1f, wheelAnchor);
			
			m_wheel.Rotation = 120f * Mathf.Deg2Rad;
			CreateLeg(-1f, wheelAnchor);
			CreateLeg(1f, wheelAnchor);
			
			m_wheel.Rotation = -120f * Mathf.Deg2Rad;
			CreateLeg(-1f, wheelAnchor);
			CreateLeg(1f, wheelAnchor);
		}
		
		private void CreateLeg(float s, FVector2 wheelAnchor)
		{
			FVector2 p1, p2, p3, p4, p5, p6;
			p1 = new FVector2((162f * s)/tScale, -183f / tScale);
			p2 = new FVector2((216f * s)/tScale, -36f / tScale);
			p3 = new FVector2((129f * s)/tScale, -57f / tScale);
			p4 = new FVector2((093f * s)/tScale, 24f / tScale);
			p5 = new FVector2((180f * s)/tScale, 45f / tScale);
			p6 = new FVector2((075f * s)/tScale, 111f / tScale);
			
			Body body1;
			Body body2;
			
			body1 = BodyFactory.CreateBody(FSWorldComponent.PhysicsWorld, m_offset);
			body1.BodyType = BodyType.Dynamic;
			body2 = BodyFactory.CreateBody(FSWorldComponent.PhysicsWorld, FVector2.Add(p4, m_offset));
			body2.BodyType = BodyType.Dynamic;
			
			Fixture b1fix;
			Fixture b2fix;
			
			List<Vertices> vl1 = new List<Vertices>();
			List<Vertices> vl2 = new List<Vertices>();
			vl1.Add(new Vertices());
			vl2.Add(new Vertices());
			
			if(s > 0f)
			{
				vl1[0].Add(p1);
				vl1[0].Add(p2);
				vl1[0].Add(p3);
				vl2[0].Add(FVector2.Zero);
				vl2[0].Add(FVector2.Subtract(p5, p4));
				vl2[0].Add(FVector2.Subtract(p6, p4));
			}
			else
			{
				vl1[0].Add(p1);
				vl1[0].Add(p3);
				vl1[0].Add(p2);
				vl2[0].Add(FVector2.Zero);
				vl2[0].Add(FVector2.Subtract(p6, p4));
				vl2[0].Add(FVector2.Subtract(p5, p4));
			}
			
			b1fix = FixtureFactory.AttachCompoundPolygon(vl1, 1f, body1)[0];
			b2fix = FixtureFactory.AttachCompoundPolygon(vl2, 1f, body2)[0];
			b1fix.CollisionCategories = Category.Cat10;
			b1fix.CollidesWith = Category.Cat1;
			b2fix.CollisionCategories = Category.Cat10;
			b2fix.CollidesWith = Category.Cat1;
			
			body1.AngularDamping = 10f;
			body2.AngularDamping = 10f;
			
			DistanceJoint dj;
			float dampingRatio = 0.5f;
			float freqHz = 10f;
			// Using a soft distance constraint can reduce some jitter.
			// It also makes the structure seem a bit more fluid by
			// acting like a suspension system.
			
			dj = JointFactory.CreateDistanceJoint(FSWorldComponent.PhysicsWorld, body1, body2, body1.GetLocalPoint(FVector2.Add(p2, m_offset)), body2.GetLocalPoint(FVector2.Add(p5, m_offset)));
			dj.DampingRatio = dampingRatio;
			dj.Frequency = freqHz;
			
			dj = JointFactory.CreateDistanceJoint(FSWorldComponent.PhysicsWorld, body1, body2, body1.GetLocalPoint(FVector2.Add(p3, m_offset)), body2.GetLocalPoint(FVector2.Add(p4, m_offset)));
			dj.DampingRatio = dampingRatio;
			dj.Frequency = freqHz;
			
			dj = JointFactory.CreateDistanceJoint(FSWorldComponent.PhysicsWorld, body1, m_wheel, body1.GetLocalPoint(FVector2.Add(p3, m_offset)), m_wheel.GetLocalPoint(FVector2.Add(wheelAnchor, m_offset)));
			dj.DampingRatio = dampingRatio;
			dj.Frequency = freqHz;
			
			dj = JointFactory.CreateDistanceJoint(FSWorldComponent.PhysicsWorld, body2, m_wheel, body2.GetLocalPoint(FVector2.Add(p6, m_offset)), m_wheel.GetLocalPoint(FVector2.Add(wheelAnchor, m_offset)));
			dj.DampingRatio = dampingRatio;
			dj.Frequency = freqHz;
			
			JointFactory.CreateRevoluteJoint(FSWorldComponent.PhysicsWorld, body2, m_chassis, m_chassis.GetLocalPoint(FVector2.Add(p4, m_offset)));
			
		}
		
		public override void Update ()
		{
			if(m_chassis.Position.X > 500f / physScale && m_motorSpeed > 0f)
			{
				m_motorSpeed = -2f;
				m_motorJoint.MotorSpeed = m_motorSpeed;
			}
			if(m_chassis.Position.X < 120f / physScale && m_motorSpeed < 0f)
			{
				m_motorSpeed = 2f;
				m_motorJoint.MotorSpeed = m_motorSpeed;
			}
			
			base.Update ();
		}
	}
}

