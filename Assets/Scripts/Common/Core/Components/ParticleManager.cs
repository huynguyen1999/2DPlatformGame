using UnityEngine;

public class ParticleManager : CoreComponent
{
    private Transform particleContainer;
    protected override void Awake()
    {
        base.Awake();
    }
    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
    {
        return Instantiate(particlePrefab, position, rotation);
    }
    public GameObject StartParticles(GameObject particlePrefab) // at itself
    {
        return Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }
    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab) // at itself and with random rotation
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return Instantiate(particlePrefab, transform.position, randomRotation);
    }
}