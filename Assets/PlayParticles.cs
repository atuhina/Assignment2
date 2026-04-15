using UnityEngine;

public class PlayParticles : MonoBehaviour
{
    public ParticleSystem particles;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            particles.Play();
            hasPlayed = true;
        }
    }
}