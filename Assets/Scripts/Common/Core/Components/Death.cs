using UnityEngine;

[System.Serializable]
public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            core.ParticleManager.StartParticles(particle);
        }
        core.transform.root.gameObject.SetActive(false);
    }

    protected void Start()
    {
        core.Stats.Health.OnCurrentValueZero += Die;
    }
    protected void OnDestroy()
    {
        core.Stats.Health.OnCurrentValueZero -= Die;
    }
}