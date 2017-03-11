using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrail : MonoBehaviour {

    public TrailRenderer trail;

    private new ParticleSystem particleSystem;
    private TrailRenderer[] trails;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem.MainModule particleMain;

	// Use this for initialization
	void Start () {

        particleSystem = GetComponent<ParticleSystem>();
        particleMain = particleSystem.main;
	}
	
	// Update is called once per frame
	void Update () {

        int maxParticlesCount = particleMain.maxParticles;

        if (particles == null || particles.Length < maxParticlesCount) particles = new ParticleSystem.Particle[maxParticlesCount];
        if (trails == null || trails.Length < maxParticlesCount) trails = new TrailRenderer[maxParticlesCount];

        particleSystem.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            if (trails.Length - 1 < i || trails[i] == null)
            {
                TrailRenderer t = Instantiate(trail);
                trails[i] = t;
            }

            Vector3 tPos = trails[i].transform.position;
            Vector3 pPos = particles[i].position;
            if (Vector3.Distance(tPos, pPos) > 1)
            {
                TrailRenderer tr = trails[i];             
                trails[i] = null;
                Destroy(tr);
            }else
            {
                trails[i].transform.position = particles[i].position;
            }
        }
    }
}
