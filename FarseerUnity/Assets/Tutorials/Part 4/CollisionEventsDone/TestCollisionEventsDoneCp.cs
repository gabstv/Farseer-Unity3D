using UnityEngine;
using System.Collections.Generic;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class TestCollisionEventsDoneCp : MonoBehaviour
{
	public TextMesh LinkedTextMesh;
	
	private Body body;
	
	private List<Contact> lastContacts;
	
	void Start()
	{
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		lastContacts = new List<Contact>();
		body.OnCollision += OnCollisionEvent;
	}
	
	void Update()
	{
		float weight = body.Mass;
		GetWeight(ref weight);
		LinkedTextMesh.text = weight.ToString("#0.00") + "Kg";
	}
	
	void OnDrawGizmos()
	{
		if(lastContacts == null)
			return;
		foreach(Contact lastContact in lastContacts)
		{
			if(!lastContact.IsTouching())
				return;
		
			FarseerPhysics.Common.FixedArray2<FVector2> contactPoints;
			FVector2 normal;
			lastContact.GetWorldManifold(out normal, out contactPoints);
		
			Vector3 p0 = FSHelper.FVector2ToVector3(contactPoints[0]);
			Vector3 p1 = FSHelper.FVector2ToVector3(contactPoints[1]);
			Vector3 pn = FSHelper.FVector2ToVector3(normal);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(p0, 0.15f);
			Gizmos.DrawLine(p0, p0 + pn * 2f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(p1, 0.15f);
			Gizmos.DrawLine(p1, p1 + pn * 2f);
		}
	}
	
	private void GetWeight(ref float weight)
	{
		if(lastContacts.Count < 1)
			return;
		float ownWeight = weight;
		weight = 0f;
		foreach(Contact lastContact in lastContacts)
		{
			bool isTouching = lastContact.IsTouching();
			if(isTouching)
			{
				FarseerPhysics.Common.FixedArray2<FarseerPhysics.Collision.ManifoldPoint> localManifoldPoints = lastContact.Manifold.Points;
				// gravity = 9.8f (hard coded here just for testing purposes)
				// Time.fixedDeltaTime is the FPE timeStep
				weight += (1f * (localManifoldPoints[0].NormalImpulse/Time.fixedDeltaTime) / 9.8f);
				weight += (1f * (localManifoldPoints[1].NormalImpulse/Time.fixedDeltaTime) / 9.8f);
			}
		}
		// remove inactive contacts
		for(int i = 0; i < lastContacts.Count; i++)
		{
			if(!lastContacts[i].IsTouching())
			{
				lastContacts.RemoveAt(i);
				i = Mathf.Max(0, i - 1);
			}
		}
		// calc weight
		weight -= ownWeight;
		weight *= 0.5f;
		weight += ownWeight;
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact)
	{
		if(!lastContacts.Contains(contact))
			lastContacts.Add(contact);
		return true;
	}
	
}
