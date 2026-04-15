using UnityEngine;

public class PlayParticles : MonoBehaviour
{
    public ParticleSystem particles; // particles to play
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        // when player enters, play particles once
        if (!hasPlayed && other.CompareTag("Player"))
        {
            particles.Play();
            hasPlayed = true;
        }
    }
}