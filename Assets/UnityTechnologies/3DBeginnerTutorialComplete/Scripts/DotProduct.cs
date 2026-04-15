using UnityEngine;

public class DotProduct : MonoBehaviour
{
    public Transform player; // player in the scene
    public Renderer objectRenderer; // object that changes color

    void Update()
    {
        // direction from this object to the player
        Vector3 toPlayer = (player.position - transform.position).normalized;

        // dot product checks if player is in front or behind
        float dot = Vector3.Dot(transform.forward, toPlayer);

        if (dot > 0f) // player is in front
        {
            objectRenderer.material.color = Color.red;
        }
        else // player is behind
        {
            objectRenderer.material.color = Color.white;
        }
    }
}