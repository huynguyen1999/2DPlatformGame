using UnityEngine;
public class ProjectileComponent : MonoBehaviour
{
    protected Projectile projectile;

    protected Rigidbody2D rb => projectile.RB;
    protected bool isActive = false;


    // This function is called whenever the projectile is fired, indicating the start of it's journey
    protected virtual void Init()
    {
        isActive = true;
    }

    /* Handles receiving specific data from the weapon. Implemented in any component that needs to use it. Automatically subscribed for all projectile
    components by this base class (see Awake and OnDestroy) */
    protected virtual void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
    }
    public virtual void HandleStuck()
    {
        this.enabled = false;
    }
    protected virtual void Awake()
    {
        projectile = GetComponent<Projectile>();
        isActive = false;
        projectile.OnInit += Init;
        projectile.OnReceiveDataPackage += HandleReceiveDataPackage;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnDestroy()
    {
        projectile.OnInit -= Init;
        projectile.OnReceiveDataPackage -= HandleReceiveDataPackage;
    }
}