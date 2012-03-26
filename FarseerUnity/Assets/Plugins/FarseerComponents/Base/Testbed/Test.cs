using UnityEngine;
using System.Collections;
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
	public class Test
	{
		protected Transform parent;
		protected const float physScale = 30f;
		
		protected FixedMouseJoint mouseJoint;
		
		protected string title = "Test";
		
		public Test(Transform parent)
		{
			this.parent = parent;
		}
		
		//private Body dynB;
		
		public virtual void Start()
		{
			//box2d/farseer uses island module to solve now, no need to set the world limits
			//AABB worldAABB = new AABB(new FVector2(-1000f, -1000f), new FVector2(1000f, 1000f));
			
			// Create border of boxes
			Body walls = new Body(FSWorldComponent.PhysicsWorld);
			FVector2 v0, v1;
			
			float x0 = 0f;
			float x1 = 640f;
			float y0 = 0f;
			float y1 = -360f;
			
			// TOP
			v0 = new FVector2(x0 / physScale, y0 / physScale);
			v1 = new FVector2(x1 / physScale, y0 / physScale);
			FixtureFactory.AttachEdge(v0, v1, walls);
			// RIGHT
			v0 = new FVector2(x1 / physScale, y0 / physScale);
			v1 = new FVector2(x1 / physScale, y1 / physScale);
			FixtureFactory.AttachEdge(v0, v1, walls);
			// BOTTOM
			v0 = new FVector2(x0 / physScale, y1 / physScale);
			v1 = new FVector2(x1 / physScale, y1 / physScale);
			FixtureFactory.AttachEdge(v0, v1, walls);
			// LEFT
			v0 = new FVector2(x0 / physScale, y0 / physScale);
			v1 = new FVector2(x0 / physScale, y1 / physScale);
			FixtureFactory.AttachEdge(v0, v1, walls);
			
			/*dynB = new Body(FSWorldComponent.PhysicsWorld);
			dynB.BodyType = BodyType.Kinematic;
			Fixture f = FixtureFactory.AttachCircle(0.3f, 1f, dynB);
			f.IsSensor = true;*/
		}
		
		public virtual void Update()
		{
			UpdateMouseWorld();
			MouseDrag();
		}
		
		public virtual void Stop()
		{
			FSWorldComponent.PhysicsWorld.Clear();
		}
		
		static public float MouseXWorldPhys = 0f;
		static public float MouseYWorldPhys = 0f;
		public virtual void UpdateMouseWorld()
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			MouseXWorldPhys = wp.x;// -parent.position.x;
			MouseYWorldPhys = wp.y;// - parent.position.y;
			//Debug.Log("MX: " + MouseXWorldPhys + " MY: " + MouseYWorldPhys);
			//dynB.Position = new FVector2(MouseXWorldPhys, MouseYWorldPhys);
		}
		
		protected FVector2 mousePVec = new FVector2();
		public virtual Body GetBodyAtMouse()
		{
			return GetBodyAtMouse(false);
		}
		public virtual Body GetBodyAtMouse(bool includeStatic)
		{
			// Make a small box
			mousePVec.X = MouseXWorldPhys;
			mousePVec.Y = MouseYWorldPhys;
			FVector2 lowerBound = new FVector2(MouseXWorldPhys - 0.001f, MouseYWorldPhys - 0.001f);
			FVector2 upperBound = new FVector2(MouseXWorldPhys + 0.001f, MouseYWorldPhys + 0.001f);
			AABB aabb = new AABB(lowerBound, upperBound);
			Body body = null;
			
			// Query the world for overlapping shapes
			System.Func<Fixture, bool> GetBodyCallback = delegate (Fixture fixture0)
			{
				Shape shape = fixture0.Shape;
				if(fixture0.Body.BodyType != BodyType.Static || includeStatic)
				{
					FarseerPhysics.Common.Transform transform0;
					fixture0.Body.GetTransform(out transform0);
					bool inside = shape.TestPoint(ref transform0, ref mousePVec);
					if(inside)
					{
						body = fixture0.Body;
						return false;
					}
				}
				return true;
			};
			FSWorldComponent.PhysicsWorld.QueryAABB(GetBodyCallback, ref aabb);
			return body;
		}
		
		public virtual void MouseDrag()
		{
			// mouse press
			if(Input.GetMouseButtonDown(0) && mouseJoint == null)
			{
				Body body = GetBodyAtMouse();
				if(body != null)
				{
					FVector2 target = new FVector2(MouseXWorldPhys, MouseYWorldPhys);
					mouseJoint = JointFactory.CreateFixedMouseJoint(FSWorldComponent.PhysicsWorld, body, target);
					mouseJoint.CollideConnected = true;
					mouseJoint.MaxForce = 300f * body.Mass;
					body.Awake = true;
				}
			}
			// mouse release
			if(Input.GetMouseButtonUp(0))
			{
				if(mouseJoint != null)
				{
					FSWorldComponent.PhysicsWorld.RemoveJoint(mouseJoint);
					mouseJoint = null;
				}
			}
			
			// mouse move
			if(mouseJoint != null)
			{
				FVector2 p2 = new FVector2(MouseXWorldPhys, MouseYWorldPhys);
				mouseJoint.WorldAnchorB = p2;
			}
		}
		
		public virtual void OnGUI()
		{
			GUI.BeginGroup(new Rect(400f, 10f, 240f, 120f), GUI.skin.box);
			GUI.Label(new Rect(10f, 5f, 220f, 20f), title);
			GUI.Label(new Rect(10f, 25f, 220f, 90f), "Left/Right Arrow: change test.\n\nClick & drag to interact.");
			GUI.EndGroup();
		}
	}
}
