using UnityEngine;

public class CloseSecretPanel : MonoBehaviour
{
    public Transform secretPanel;
    public Vector3 closedPosition;
    public float closeSpeed = 3f;

    private bool shouldClose = false;

    void Update()
    {
        if (shouldClose)
        {
            secretPanel.position = Vector3.Lerp(
                secretPanel.position,
                closedPosition,
                Time.deltaTime * closeSpeed
            );

            // snap fully shut when it's close enough
            if (Vector3.Distance(secretPanel.position, closedPosition) < 0.02f)
            {
                secretPanel.position = closedPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldClose = true;
        }
    }
}