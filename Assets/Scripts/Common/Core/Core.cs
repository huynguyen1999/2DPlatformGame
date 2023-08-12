using UnityEngine;
[System.Serializable]
public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(movement, transform.root.name);
    }
    public CollisionDetection CollisionDetection
    {
        get => GenericNotImplementedError<CollisionDetection>.TryGet(collisionDetection, transform.root.name);
    }
    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(combat, transform.root.name);
    }
    public Stats Stats
    {
        get => GenericNotImplementedError<Stats>.TryGet(stats, transform.root.name);
    }
    public ParticleManager ParticleManager
    {
        get => GenericNotImplementedError<ParticleManager>.TryGet(particleManager, transform.root.name);
    }
    public Death Death
    {
        get => GenericNotImplementedError<Death>.TryGet(death, transform.root.name);
    }

    [SerializeField] private Movement movement;
    [SerializeField] private CollisionDetection collisionDetection;
    [SerializeField] private Combat combat;
    [SerializeField] private Stats stats;
    [SerializeField] private ParticleManager particleManager;
    [SerializeField] private Death death;
    private void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collisionDetection = GetComponentInChildren<CollisionDetection>();
        combat = GetComponentInChildren<Combat>();
        stats = GetComponentInChildren<Stats>();
        particleManager = GetComponentInChildren<ParticleManager>();
        death = GetComponentInChildren<Death>();
    }
}