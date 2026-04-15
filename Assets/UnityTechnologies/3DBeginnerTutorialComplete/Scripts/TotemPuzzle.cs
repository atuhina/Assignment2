using UnityEngine;

public class TotemPuzzle : MonoBehaviour
{
    public Transform targetDirection; // the direction the totem should face
    public Transform secretPanel; // the door/panel that opens
    public Vector3 panelClosedPosition; // closed position of the panel
    public Vector3 panelOpenPosition; // open position of the panel

    public Renderer topRenderer; // top part of the totem
    public Renderer orbRenderer; // glowing orb on the totem

    public ParticleSystem doorParticles; // particles by the door
    public AudioSource winAudio; // sound when puzzle is solved

    public float rotateSpeed = 90f; // how fast the totem rotates
    public float doorSpeed = 3f; // how fast the panel opens
    public float alignmentThreshold = 0.96f; // how exact the direction must be

    private bool playerNearby = false;
    private bool puzzleSolved = false;
    private bool hasPlayedParticles = false;
    private bool hasPlayedWinAudio = false;

    private Vector3 orbInactiveScale;
    private Vector3 orbActiveScale = new Vector3(0.5f, 0.5f, 0.5f);

    void Start()
    {
        // save the orb's starting size
        orbInactiveScale = orbRenderer.transform.localScale;
    }

    void Update()
    {
        // let player rotate the totem while nearby
        if (playerNearby && Input.GetKey(KeyCode.E) && !puzzleSolved)
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        }

        // use dot product to check if the totem is facing the correct direction
        if (!puzzleSolved)
        {
            Vector3 toTarget = (targetDirection.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, toTarget);

            if (dot >= alignmentThreshold)
            {
                puzzleSolved = true;

                // play particles once
                if (!hasPlayedParticles)
                {
                    doorParticles.Play();
                    hasPlayedParticles = true;
                }

                // play win sound once
                if (!hasPlayedWinAudio)
                {
                    winAudio.Play();
                    hasPlayedWinAudio = true;
                }
            }
        }

        if (puzzleSolved)
        {
            // show solved colors
            topRenderer.material.color = new Color(1f, 0.8f, 0.2f);
            orbRenderer.material.color = Color.cyan;

            // make orb slightly bigger
            orbRenderer.transform.localScale = Vector3.Lerp(
                orbRenderer.transform.localScale,
                orbActiveScale,
                Time.deltaTime * 5f
            );

            // open the panel smoothly
            secretPanel.position = Vector3.Lerp(
                secretPanel.position,
                panelOpenPosition,
                Time.deltaTime * doorSpeed
            );
        }
        else
        {
            // normal colors before solving
            topRenderer.material.color = Color.gray;
            orbRenderer.material.color = new Color(0.5f, 0.8f, 1f);

            // keep orb at normal size
            orbRenderer.transform.localScale = Vector3.Lerp(
                orbRenderer.transform.localScale,
                orbInactiveScale,
                Time.deltaTime * 5f
            );

            // keep panel closed before solving
            secretPanel.position = Vector3.Lerp(
                secretPanel.position,
                panelClosedPosition,
                Time.deltaTime * doorSpeed
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}