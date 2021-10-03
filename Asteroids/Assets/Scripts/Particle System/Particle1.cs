using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle1 : Particle
{
    private float velocity = .07f;
    private float radius;

    public Particle1(GameObject[] o, GameObject source, ObjectPool p) : base(o, source, p)
    {
        radius = source.GetComponent<CircleCollider2D>().radius * source.transform.localScale.x;
    }

    public override void Set()
    {
        float gap = 360.0f / particles.Length;
        float angle = 0;

        SetColor();
        foreach (var p in particles)
        {
            Color c = p.GetComponent<SpriteRenderer>().color;
            c.a = 1;
            p.GetComponent<SpriteRenderer>().color = c;

            Transform t = p.transform;
            Vector3 temp = t.eulerAngles;
            temp.z = angle;
            t.eulerAngles = temp;

            temp = t.position;
            temp.x = position.x + radius * Mathf.Cos(angle * Mathf.PI / 180);
            temp.y = position.y + radius * Mathf.Sin(angle * Mathf.PI / 180);
            t.position = temp;

            angle += gap;
        }

        angle = Random.Range(-60, 60.0f);
        foreach (var p in particles)
            p.transform.Rotate(Vector3.forward, angle);
    }

    public override void Move()
    {
        Transform t;
        foreach (var g in particles)
        {
            t = g.transform;
            Vector3 pos = t.position;
            pos.x += velocity * Mathf.Cos(t.eulerAngles.z * Mathf.PI / 180);
            pos.y += velocity * Mathf.Sin(t.eulerAngles.z * Mathf.PI / 180);
            t.position = pos;
        }
    }
}