using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source1;
    public AudioSource source2;
    public AudioSource source3;
    public AudioSource source4;
    public AudioSource source5;
    public AudioSource source6;

    public void ThrowAudio()
    {
        source1.Play();
    }

    public void KillAudio()
    {
        source2.Play();
    }

    public void WinAudio()
    {
        source3.Play();
    }

    public void GameOverAudio()
    {
        source4.Play();
    }

    public void TrackSound()
    {
        source5.Stop();
    }

    public void HurtAudio()
    {
        source6.Play();
    }
}

