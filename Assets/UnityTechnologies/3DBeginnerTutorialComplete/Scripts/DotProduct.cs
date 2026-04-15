using UnityEngine;

public class DotProduct : MonoBehaviour
{
    public Transform player;
    public Renderer objectRenderer;

    private Color originalColor;

    void Start()
    {
        originalColor = objectRenderer.material.color;
    }

    void Update()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, toPlayer);

        if (dot > 0f)
        {
            objectRenderer.material.color = Color.red;
        }
        else
        {
            objectRenderer.material.color = originalColor;
        }
    }
}