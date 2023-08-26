using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [System.Serializable]
    public struct ParticleParameters
    {
        public ParticleSystem Particle;
        public int ParticleCount;
    }

    [SerializeField] private List<ParticleParameters> _spawnParticles;
    private List<ParticleSystem> AllParticles = new List<ParticleSystem>();

    private void Start()
    {
        foreach (var parctileSpawn in _spawnParticles)
        {

            for (int i = 0; i < parctileSpawn.ParticleCount; i++)
            {
                var parcticle = Instantiate(parctileSpawn.Particle, transform);
                parcticle.name = parctileSpawn.Particle.name;
                AllParticles.Add(parcticle);
            }
        }
    }

    public void RunParticleAtLocation(Vector3 location,  ParticleSystem particle)
    {
        var freeParticle = AllParticles.FirstOrDefault(p => p.name == particle.name && !p.isPlaying);
        freeParticle.transform.position = location;
        freeParticle.Play();
    }


}
