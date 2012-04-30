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
using FarseerPhysics.Dynamics;

using Microsoft.Xna.Framework;

/// <summary>
/// Farseer world component.
/// SINGLETON
/// </summary>
[AddComponentMenu("FarseerUnity/Physics World Component")]
public class FSWorldComponent : MonoBehaviour
{
	protected static FSWorldComponent instance;
	
	private World world;
	
	public Vector2 Gravity = new Vector2(0f, -9.8f);
	
	protected int pwait = 2;
	
	void Awake ()
	{
		instance = this;
		world = new World(new FVector2(Gravity.x, Gravity.y));
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(pwait > 0)
		{
			pwait--;
			return;
		}
		world.Step(Time.fixedDeltaTime);
	}
	
	public static World PhysicsWorld
	{
		get
		{
			return instance.world;
		}
	}
}
