using UnityEngine;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class TestCoreCollisionEvents : MonoBehaviour
{
	public FSBodyComponent AttachedBody;
	public FSShapeComponent AttachedShape;
	
	private GUIText guiText;
	
	private bool initialized = false;
	
	private List<Contact> lastContacts;

	// Use this for initialization
	void Start ()
	{
		lastContacts = new List<Contact>();
		guiText = GetComponent<GUIText>();
		//Debug.Log(string.Format("lots of razors {0} should be here", "1337"));
		//Time.timeScale = 0.5f;
	}
	
	void Update ()
	{
		Init();
		
		if(lastContacts.Count < 1)
		{
			guiText.text = "Contact: null";
		}
		else
		{
			string guiOutput = "";
			float weight = 0f;
			foreach(Contact lastContact in lastContacts)
			{
				// get stats!
				bool isTouching = lastContact.IsTouching();
				FarseerPhysics.Common.FixedArray2<FVector2> contactPoints;
				FVector2 normal;
				
				string cps = "none";
				string cn = "none";
				string lpm = "none";
				
				float dot = 0f;
				float dot2 = 0f;
				
				if(isTouching)
				{
					lastContact.GetWorldManifold(out normal, out contactPoints);
					cps = string.Format("p0[ {0}, {1} ] p1[ {2}, {3} ]", contactPoints[0].X, contactPoints[0].Y, contactPoints[1].X, contactPoints[1].Y);
					cn = string.Format("[ {0}, {1} ]", normal.X, normal.Y);
					FarseerPhysics.Common.FixedArray2<FarseerPhysics.Collision.ManifoldPoint> lpp = lastContact.Manifold.Points;
					lpm = string.Format("nimpulse[ {0}, {1} ] tanimpulse[ {2}, {3} ]", lpp[0].NormalImpulse/Time.fixedDeltaTime, lpp[1].NormalImpulse/Time.fixedDeltaTime, lpp[0].TangentImpulse/Time.fixedDeltaTime, lpp[1].TangentImpulse/Time.fixedDeltaTime);
					dot = FVector2.Dot(FVector2.Normalize(-AttachedBody.PhysicsBody.Position+contactPoints[0]), normal);
					dot2 = FVector2.Dot(FVector2.Normalize(-AttachedBody.PhysicsBody.Position+contactPoints[1]), normal);
					weight += (1f * (lpp[0].NormalImpulse/Time.fixedDeltaTime) / 9.8f);
					weight += (1f * (lpp[1].NormalImpulse/Time.fixedDeltaTime) / 9.8f);
				}
				
				guiOutput += string.Format(contactInfoBase, 
					lastContact.Restitution,
					lastContact.TangentSpeed,
					cps,
					cn,
					lpm,
					dot);
			}
			for(int i = 0; i < lastContacts.Count; i++)
			{
				if(!lastContacts[i].IsTouching())
				{
					lastContacts.RemoveAt(i);
					i = Mathf.Max(0, i - 1);
				}
			}
			float ownmass = AttachedBody.PhysicsBody.Mass;
			weight -= ownmass;
			weight *= 0.5f;
			weight += ownmass;
			guiText.text = "TOTAL WEIGHT: "+weight.ToString()+"Kg";
			guiText.text += guiOutput;
		}
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
	
	private void Init()
	{
		if(initialized)
			return;
		
		AttachedBody.PhysicsBody.OnCollision += OnCollisionEvent;
		
		Debug.Log(AttachedBody.PhysicsBody.Mass);
		
		initialized = true;
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact)
	{
		if(!lastContacts.Contains(contact))
			lastContacts.Add(contact);
		return true;
	}
	
	private string contactInfoBase = @"
Contact Information:
restitution: {0}
tangent speed: {1}
contact points: {2}
contact normal: {3}
local points: {4}
dot: {5}
";
}
