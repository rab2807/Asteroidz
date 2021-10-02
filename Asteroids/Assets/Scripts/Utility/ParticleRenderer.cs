using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleRenderer : MonoBehaviour
{
    class Swarm
    {
        private GameObject[] particles;
        private float velocity = .1f;
        private float fadeSpeed = .8f;
        private float radius;
        private Vector3 position;

        public Swarm(GameObject[] o, Vector3 pos, float r)
        {
            particles = o;
            position = pos;
            radius = r;
        }

        public void Set()
        {
            float gap = 360.0f / particles.Length;
            float angle = 0;

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
        }

        public void Move()
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
            foreach (var p in particles)
                particlePool.ReturnObject(p);
        }

        public bool IsInvisible()
        {
            return particles[0].GetComponent<SpriteRenderer>().color.a < .05;
        }
    }

    private static ObjectPool particlePool;
    private static List<Swarm> swarms;
    private int num = 60;
    private static int numPerGroup = 15;

    void Awake()
    {
        particlePool = gameObject.AddComponent<ObjectPool>();
        particlePool.Initialize(num, "Particle");

        swarms = new List<Swarm>();
    }

    public static void EmitParticles(GameObject obj)
    {
        Vector3 pos = obj.transform.position;
        float r = obj.GetComponent<CircleCollider2D>().radius * obj.transform.localScale.x;

        GameObject[] particles = new GameObject[numPerGroup];
        for (int i = 0; i < numPerGroup; i++)
            particles[i] = particlePool.GetObject();

        Swarm s = new Swarm(particles, pos, r);
        s.Set();
        swarms.Add(s);
    }

    void Update()
    {
        if (swarms.Count != 0)
        {
            foreach (var swarm in swarms.ToList())
            {
                swarm.Move();
                swarm.Fade();
                if (swarm.IsInvisible()) Delete(swarm);
            }
        }
    }

    void Delete(Swarm swarm)
    {
        swarm.Return();
        swarms.Remove(swarm);
    }
}