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
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

[AddComponentMenu("FarseerUnity/Dynamics/Joints/Prismatic Joint Component")]
public class FSPrismaticJointComponent : FSJointComponent
{
	protected PrismaticJoint joint;
	
	public float Frequency = 60f;
	
	public override void InitJoint ()
	{
		base.InitJoint ();
		
		//Microsoft.Xna.Framework.FVector2 angleV = new Microsoft.Xna.Framework.FVector2(BodyB.PhysicsBody.Position.X - BodyA.PhysicsBody.Position.X, BodyB.PhysicsBody.Position.Y - BodyA.PhysicsBody.Position.Y);
		float ang = Mathf.Atan2(BodyB.PhysicsBody.Position.Y - BodyA.PhysicsBody.Position.Y, BodyB.PhysicsBody.Position.X - BodyA.PhysicsBody.Position.X);
		Microsoft.Xna.Framework.FVector2 angleV = new Microsoft.Xna.Framework.FVector2(Mathf.Cos(ang), Mathf.Sin(ang));
		//angleV.Normalize();
		joint = FarseerPhysics.Factories.JointFactory.CreatePrismaticJoint(FSWorldComponent.PhysicsWorld, 
			BodyA.PhysicsBody, 
			BodyB.PhysicsBody,
			Microsoft.Xna.Framework.FVector2.Zero,
			angleV);
		joint.CollideConnected = CollideConnected;
		//joint.Frequency = Frequency;
		//joint.DampingRatio = 0.5f; d
	}
	
	public override void Update ()
	{
		base.Update();
	}
	
	public override void OnDrawGizmos()
	{
		if(BodyA == null || BodyB == null)
			return;
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(BodyA.transform.position, BodyB.transform.position);
	}
}
