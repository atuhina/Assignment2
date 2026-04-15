using UnityEngine;

public class TotemPuzzle : MonoBehaviour
{
    public Transform targetDirection;
    public Transform secretPanel;
    public Vector3 panelClosedPosition;
    public Vector3 panelOpenPosition;

    public Renderer totemRenderer;
    public Renderer topRenderer;
    public Renderer orbRenderer;

    public ParticleSystem doorParticles;

    public AudioSource winAudio;
    private bool hasPlayedWinAudio = false;

    public float rotateSpeed = 90f;
    public float doorSpeed = 3f;
    public float alignmentThreshold = 0.96f;

    private bool playerNearby = false;
    private bool puzzleSolved = false;
    private bool hasPlayedParticles = false;

    private Vector3 orbInactiveScale;
    private Vector3 orbActiveScale = new Vector3(0.5f, 0.5f, 0.5f);

    void Start()
    {
        if (orbRenderer != null)
        {
            orbInactiveScale = orbRenderer.transform.localScale;
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKey(KeyCode.E) && !puzzleSolved)
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        }

        if (!puzzleSolved)
        {
            Vector3 toTarget = (targetDirection.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, toTarget);

            if (dot >= alignmentThreshold)
            {
                puzzleSolved = true;

                if (!hasPlayedParticles && doorParticles != null)
                {
                    doorParticles.Play();
                    hasPlayedParticles = true;
                }
                if (!hasPlayedWinAudio && winAudio != null)
                {
                    winAudio.Play();
                    hasPlayedWinAudio = true;
                }
            }
        }

        if (puzzleSolved)
        {
            if (topRenderer != null)
                topRenderer.material.color = new Color(1f, 0.8f, 0.2f);

            if (orbRenderer != null)
            {
                orbRenderer.material.color = Color.cyan;
                orbRenderer.transform.localScale = Vector3.Lerp(
                    orbRenderer.transform.localScale,
                    orbActiveScale,
                    Time.deltaTime * 5f
                );
            }

            secretPanel.position = Vector3.Lerp(
                secretPanel.position,
                panelOpenPosition,
                Time.deltaTime * doorSpeed
            );
        }
        else
        {
            if (topRenderer != null)
                topRenderer.material.color = Color.gray;

            if (orbRenderer != null)
            {
                orbRenderer.material.color = new Color(0.5f, 0.8f, 1f);
                orbRenderer.transform.localScale = Vector3.Lerp(
                    orbRenderer.transform.localScale,
                    orbInactiveScale,
                    Time.deltaTime * 5f
                );
            }

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