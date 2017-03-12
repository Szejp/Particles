using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrail : MonoBehaviour {

    public TrailRenderer trail;

    private new ParticleSystem particleSystem;
    private TrailRenderer[] trails;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem.MainModule particleMain;
    private Vector3 tPos;
    private Vector3 pPos;
    private TrailRenderer t;
    private TrailRenderer tr;
    private int maxParticlesCount;

    // Use this for initialization
    void Start () {

        particleSystem = GetComponent<ParticleSystem>();
        particleMain = particleSystem.main;

        if (particles == null || particles.Length < maxParticlesCount) particles = new ParticleSystem.Particle[10000];
        if (trails == null || trails.Length < maxParticlesCount) trails = new TrailRenderer[10000];
    }
	
	// Update is called once per frame
	void Update () {

        maxParticlesCount = particleMain.maxParticles;
        particleSystem.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            if (trails.Length - 1 < i || trails[i] == null)
            {
                t = Instantiate(trail);
                trails[i] = t;
            }

            tPos = trails[i].transform.position;
            pPos = particles[i].position;
            if (Vector3.Distance(tPos, pPos) > 1)
            {
                tr = trails[i];             
                trails[i] = null;
                //Destroy(tr);
            }else
            {
                trails[i].transform.position = particles[i].position;
            }
        }
    }

    private void OnDisable()
    {
        //for(int i = 0;i< trails.Length; i++)
        //{
        //    Destroy(trails[i].gameObject);
        //}
        //trails = null;
    }
}
