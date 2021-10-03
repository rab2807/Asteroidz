using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Particle2 : Particle
{
    private float speed = .05f;
    private Vector3 initialScale;

    public Particle2(GameObject[] o, GameObject source, ObjectPool p) : base(o, source, p)
    {
        initialScale = source.transform.localScale;
    }

    public override void Set()
    {
        fadeSpeed = .9f;
        SetColor();
        Transform t;
        foreach (var p in particles)
        {
            t = p.transform;
            Color c = p.GetComponent<SpriteRenderer>().color;
            c.a = 1;
            p.GetComponent<SpriteRenderer>().color = c;
            t.localScale = initialScale;
            t.position = position;
        }
    }

    public override void Move()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            Vector3 v = particles[i].transform.localScale;
            v.x = v.y = v.z = initialScale.x + Mathf.Pow(i, 5) * speed;
            initialScale.x += speed;
            particles[i].transform.localScale = v;
        }
        // Debug.Log(particles[0].transform.localScale.x);
    }
}