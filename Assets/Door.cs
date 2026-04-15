using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public Vector3 closedPosition;
    public Vector3 openPosition;
    public float speed = 4f;
    public AudioSource audioSource;

    private bool shouldOpen = false;

    void Update()
    {
        if (shouldOpen)
        {
            door.position = Vector3.Lerp(door.position, openPosition, Time.deltaTime * speed);
        }
        else
        {
            door.position = Vector3.Lerp(door.position, closedPosition, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldOpen = true;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldOpen = false;
        }
    }
}