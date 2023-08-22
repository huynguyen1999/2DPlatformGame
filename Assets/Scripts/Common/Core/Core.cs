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
    public DamageReceiver DamageReceiver
    {
        get => GenericNotImplementedError<DamageReceiver>.TryGet(damageReceiver, transform.root.name);
    }
    public KnockBackReceiver KnockBackReceiver
    {
        get => GenericNotImplementedError<KnockBackReceiver>.TryGet(knockBackReceiver, transform.root.name);
    }

    private Movement movement;
    private CollisionDetection collisionDetection;
    private DamageReceiver damageReceiver;
    private KnockBackReceiver knockBackReceiver;
    private Stats stats;
    private ParticleManager particleManager;
    private Death death;
    private void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collisionDetection = GetComponentInChildren<CollisionDetection>();
        damageReceiver = GetComponentInChildren<DamageReceiver>();
        knockBackReceiver = GetComponentInChildren<KnockBackReceiver>();
        stats = GetComponentInChildren<Stats>();
        particleManager = GetComponentInChildren<ParticleManager>();
        death = GetComponentInChildren<Death>();
    }

    private void Update()
    {
        movement.Update();
        collisionDetection.Update();
        damageReceiver.Update();
        knockBackReceiver.Update();
        stats.Update();
        particleManager.Update();
        death.Update();
    }
    private void FixedUpdate()
    {
        movement.FixedUpdate();
        collisionDetection.FixedUpdate();
        damageReceiver.FixedUpdate();
        knockBackReceiver.FixedUpdate();
        stats.FixedUpdate();
        particleManager.FixedUpdate();
        death.FixedUpdate();
    }
}