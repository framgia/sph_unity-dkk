using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    public AudioSource Walking, Jumping, Climbing, Win;

    public void PlayWalking()
    {
        if(!Walking.isPlaying)
        {
            Walking.Play();
        }
    }

    public void PlayClimbing()
    {
        if(!Climbing.isPlaying)
        {
            Climbing.Play();
        }
    }

    public void StopClimbing()
    {
        Climbing.Stop();
    }

    public void StopWalking()
    {
        Walking.Stop();
    }

    public void PlayJumping()
    {
        Jumping.Play();
    }
}
