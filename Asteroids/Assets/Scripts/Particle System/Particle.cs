using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Particle
{
    protected ObjectPool pool;
    protected GameObject[] particles;
    protected float fadeSpeed = ConfigurationData.GetData().ParticleFadeSpeed;
    protected Vector3 position;

    protected Particle(GameObject[] o, GameObject source, ObjectPool p)
    {
        pool = p;
        particles = o;
        position = source.transform.position;
    }

    public abstract void Set();
    public abstract void Move();

    public void Fade()
    {
        foreach (var g in particles)
        {
            Color c = g.GetComponent<SpriteRenderer>().color;
            c.a *= fadeSpeed;
            g.GetComponent<SpriteRenderer>().color = c;
        }
    }

    public void Return()
    {
        foreach (var p in particles) pool.ReturnObject(p);
    }

    public bool IsInvisible()
    {
        return particles[0].GetComponent<SpriteRenderer>().color.a < .05;
    }

    protected void SetColor()
    {
        int x = Random.Range(1, 4);
        foreach (var p in particles)
            p.GetComponent<SpriteRenderer>().color = ColorPicker.GetColor(x);
    }
}