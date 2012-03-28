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

[AddComponentMenu("FarseerUnity/Dynamics/Joints/Pulley Joint Component")]
public class FSPulleyJointComponent : FSJointComponent
{
	protected PulleyJoint joint;
	
	public Transform GroundAnchorA;
	public Transform GroundAnchorB;
	
	public Vector2 LocalAnchorA = Vector2.zero;
	public Vector2 LocalAnchorB = Vector2.zero;
	
	public float ForceRatio = 1.0f;
	
	public override void InitJoint ()
	{
		base.InitJoint ();
		
		Vector3 p0 = BodyA.transform.TransformPoint(new Vector3(LocalAnchorA.x, LocalAnchorA.y, -5f));
		Vector3 p1 = BodyB.transform.TransformPoint(new Vector3(LocalAnchorB.x, LocalAnchorB.y, -5f));
		
		joint = JointFactory.CreatePulleyJoint(FSWorldComponent.PhysicsWorld, 
			BodyA.PhysicsBody, 
			BodyB.PhysicsBody, 
			FSHelper.Vector3ToFVector2(GroundAnchorA.position), 
			FSHelper.Vector3ToFVector2(GroundAnchorB.position), 
			BodyA.PhysicsBody.GetLocalPoint(FSHelper.Vector3ToFVector2(p0)), 
			BodyB.PhysicsBody.GetLocalPoint(FSHelper.Vector3ToFVector2(p1)),
			ForceRatio);
		
		joint.CollideConnected = CollideConnected;
	}
	
	public override void OnDrawGizmos ()
	{
		if(BodyA != null && BodyB != null && GroundAnchorA != null && GroundAnchorB != null)
		{
			Vector3 p0 = BodyA.transform.TransformPoint(new Vector3(LocalAnchorA.x, LocalAnchorA.y, -5f));
			Vector3 p1 = BodyB.transform.TransformPoint(new Vector3(LocalAnchorB.x, LocalAnchorB.y, -5f));
			
			Gizmos.color = Color.red;
			Gizmos.DrawLine(p0, GroundAnchorA.position);
			Gizmos.DrawLine(GroundAnchorA.position, GroundAnchorB.position);
			Gizmos.DrawLine(GroundAnchorB.position, p1);
		}
		if(GroundAnchorA != null)
			Gizmos.DrawIcon(GroundAnchorA.position, "Shape");
		if(GroundAnchorB != null)
			Gizmos.DrawIcon(GroundAnchorB.position, "Shape");
		
		base.OnDrawGizmos ();
	}
}
