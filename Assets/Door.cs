using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door; // the door object that moves
    public Vector3 closedPosition; // position when closed
    public Vector3 openPosition; // position when open
    public float speed = 4f; // how fast the door moves
    public AudioSource audioSource; // sound when opening

    private bool shouldOpen = false;

    void Update()
    {
        // move door smoothly based on state
        if (shouldOpen)
        {
            door.position = Vector3.Lerp(
                door.position,
                openPosition,
                Time.deltaTime * speed
            );
        }
        else
        {
            door.position = Vector3.Lerp(
                door.position,
                closedPosition,
                Time.deltaTime * speed
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // when player enters, open door and play sound
        if (other.CompareTag("Player"))
        {
            shouldOpen = true;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // when player leaves, close the door
        if (other.CompareTag("Player"))
        {
            shouldOpen = false;
        }
    }
}