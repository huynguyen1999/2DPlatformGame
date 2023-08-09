using UnityEngine;

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

    private Movement movement;
    private CollisionDetection collisionDetection;
    private Combat combat;
    private void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collisionDetection = GetComponentInChildren<CollisionDetection>();
        combat = GetComponentInChildren<Combat>();
    }
}