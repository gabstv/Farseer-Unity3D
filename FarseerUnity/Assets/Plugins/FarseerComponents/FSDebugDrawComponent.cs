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
using FarseerPhysics.DebugViews;

[AddComponentMenu("FarseerUnity/Camera/GL DebugDraw Component")]
public class FSDebugDrawComponent : MonoBehaviour
{
	public Material GLMaterial;
	
	protected FarseerDebugViewUnity debugView;
	protected Camera cam;
	protected Matrix4x4 projection;

	// Use this for initialization
	void Start ()
	{
		cam = GetComponent<Camera>();
		if(cam == null)
			return;
		projection = cam.projectionMatrix;
		debugView = new FarseerDebugViewUnity(FSWorldComponent.PhysicsWorld);
		debugView.AppendFlags(FarseerPhysics.DebugViewFlags.ContactPoints);
		//debugView.AppendFlags(FarseerPhysics.DebugViewFlags.ContactNormals);
		debugView.AppendFlags(FarseerPhysics.DebugViewFlags.CenterOfMass);
		//debugView.AppendFlags(FarseerPhysics.DebugViewFlags.PerformanceGraph);
		debugView.AppendFlags(FarseerPhysics.DebugViewFlags.DebugPanel);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnPostRender()
	{
		projection = cam.projectionMatrix;
		debugView.RenderDebugData(ref projection, GLMaterial);
	}
	
	void OnGUI()
	{
		debugView.OnGUI(cam);
	}
}
