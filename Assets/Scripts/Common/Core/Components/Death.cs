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
        if (core.Stats == null) return;
        core.Stats.OnZeroHealth += Die;
    }
    protected void OnDestroy(){
        core.Stats.OnZeroHealth -= Die;
    }
}