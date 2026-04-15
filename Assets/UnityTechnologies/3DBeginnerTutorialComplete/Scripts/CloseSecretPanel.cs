using UnityEngine;

public class CloseSecretPanel : MonoBehaviour
{
    public Transform secretPanel; // the door/panel to close
    public Vector3 closedPosition; // where the panel should end up
    public float closeSpeed = 3f; // how fast it closes

    private bool shouldClose = false;

    void Update()
    {
        if (shouldClose)
        {
            // smoothly move panel to closed position
            secretPanel.position = Vector3.Lerp(
                secretPanel.position,
                closedPosition,
                Time.deltaTime * closeSpeed
            );

            // snap exactly closed when very close
            if (Vector3.Distance(secretPanel.position, closedPosition) < 0.02f)
            {
                secretPanel.position = closedPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // when player enters trigger, start closing the panel
        if (other.CompareTag("Player"))
        {
            shouldClose = true;
        }
    }
}