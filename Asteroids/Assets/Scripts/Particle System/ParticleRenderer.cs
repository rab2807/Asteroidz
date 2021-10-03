using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleRenderer : MonoBehaviour
{
    private static ObjectPool particlePool1, particlePool2, particlePool3;
    private static List<Particle> swarms;
    private int num1 = 60, num2 = 70;
    private static int numPerGroup1 = 15, numPerGroup2 = 3;

    void Awake()
    {
        particlePool1 = gameObject.AddComponent<ObjectPool>();
        particlePool2 = gameObject.AddComponent<ObjectPool>();
        particlePool3 = gameObject.AddComponent<ObjectPool>();
        particlePool1.Initialize(num1, "Particle1");
        particlePool2.Initialize(num2, "Particle2");
        particlePool3.Initialize(num2, "Particle3");
        swarms = new List<Particle>();
    }

    public static void EmitParticles(GameObject source)
    {
        int n = Random.Range(0, 3);

        GameObject[] particles = new GameObject[n < 2 ? numPerGroup1 : numPerGroup2];
        for (int i = 0; i < particles.Length; i++)
            if (n == 0)
                particles[i] = particlePool1.GetObject();
            else if (n == 1)
                particles[i] = particlePool2.GetObject();
            else
                particles[i] = particlePool3.GetObject();

        Particle s;
        if (n == 0)
            s = new Particle1(particles, source, particlePool1);
        else if (n == 1)
            s = new Particle1(particles, source, particlePool3);
        else
            s = new Particle2(particles, source, particlePool2);
        s.Set();
        swarms.Add(s);
    }

    void Update()
    {
        if (swarms.Count != 0)
            foreach (var swarm in swarms.ToList())
            {
                swarm.Move();
                swarm.Fade();
                if (swarm.IsInvisible()) Delete(swarm);
            }
    }

    void Delete(Particle swarm)
    {
        swarm.Return();
        swarms.Remove(swarm);
    }
}