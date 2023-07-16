using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    public float MaxHealth;
    public GameObject Alive,
        BrokenTop,
        BrokenBottom;
    public Rigidbody2D RigidBodyAlive,
        RigidiBodyBrokenTop,
        RigidBodyBrokenBottom;
    private float _currentHealth;

    private void Start() { }

    private void Update() { }

    public void OnAttack(PlayerCombatController player)
    {
        float dotProduct = Vector2.Dot(player.transform.right, transform.right);
        if (dotProduct > 0f)
        {
            Debug.Log("Is hit from the left");
        }
        else if (dotProduct < 0f)
        {
            Debug.Log("Is hit from the right");
        }
    }
}
