using UnityEngine;

public class SidekickController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the sidekick follows the player
    public float followDistance = 2f; // Minimum distance between the player and sidekick

    private void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude; // Calculate the distance between the player and sidekick

            // Check if the distance is greater than the follow distance
            if (distance > followDistance)
            {
                // Normalize the direction vector
                direction.Normalize();

                // Move the sidekick towards the player
                transform.position += direction * followSpeed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the sidekick collides with the player, ignore the collision
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}