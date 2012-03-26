using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Controllers
{
    public enum GravityType
    {
        Linear,
        DistanceSquared
    }

    public class GravityController : Controller
    {
        public List<Body> Bodies = new List<Body>();
        public List<FVector2> Points = new List<FVector2>();

        public GravityController(float strength)
            : base(ControllerType.GravityController)
        {
            Strength = strength;
            MaxRadius = float.MaxValue;
        }

        public GravityController(float strength, float maxRadius, float minRadius)
            : base(ControllerType.GravityController)
        {
            MinRadius = minRadius;
            MaxRadius = maxRadius;
            Strength = strength;
        }

        public float MinRadius { get; set; }
        public float MaxRadius { get; set; }
        public float Strength { get; set; }
        public GravityType GravityType { get; set; }

        public override void Update(float dt)
        {
            FVector2 f = FVector2.Zero;

            foreach (Body body1 in World.BodyList)
            {
                if (!IsActiveOn(body1))
                    continue;

                foreach (Body body2 in Bodies)
                {
                    if (body1 == body2 || (body1.IsStatic && body2.IsStatic) || !body2.Enabled)
                        continue;

                    FVector2 d = body2.WorldCenter - body1.WorldCenter;
                    float r2 = d.LengthSquared();

                    if (r2 < Settings.Epsilon)
                        continue;

                    float r = d.Length();

                    if (r >= MaxRadius || r <= MinRadius)
                        continue;

                    switch (GravityType)
                    {
                        case GravityType.DistanceSquared:
                            f = Strength / r2 / (float)Math.Sqrt(r2) * body1.Mass * body2.Mass * d;
                            break;
                        case GravityType.Linear:
                            f = Strength / r2 * body1.Mass * body2.Mass * d;
                            break;
                    }

                    body1.ApplyForce(ref f);
                    FVector2.Negate(ref f, out f);
                    body2.ApplyForce(ref f);
                }

                foreach (FVector2 point in Points)
                {
                    FVector2 d = point - body1.Position;
                    float r2 = d.LengthSquared();

                    if (r2 < Settings.Epsilon)
                        continue;

                    float r = d.Length();

                    if (r >= MaxRadius || r <= MinRadius)
                        continue;

                    switch (GravityType)
                    {
                        case GravityType.DistanceSquared:
                            f = Strength / r2 / (float)Math.Sqrt(r2) * body1.Mass * d;
                            break;
                        case GravityType.Linear:
                            f = Strength / r2 * body1.Mass * d;
                            break;
                    }

                    body1.ApplyForce(ref f);
                }
            }
        }

        public void AddBody(Body body)
        {
            Bodies.Add(body);
        }

        public void AddPoint(FVector2 point)
        {
            Points.Add(point);
        }
    }
}