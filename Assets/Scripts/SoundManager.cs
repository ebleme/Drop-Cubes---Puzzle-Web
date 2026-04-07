using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;


    [SerializeField] private AudioClip itemTouchSFX;
    [SerializeField] private AudioClip itemDestroySFX;


    /*
     * Item çarpışma sesi
     * Item yok olurken ses
     *
     * danger zone geldiği zaman music değişsin
     */


    [ContextMenu("PlayItemTouchSFX")]
    public void PlayItemTouchSFX()
    {
        soundSource.volume = 0.15f;
        soundSource.PlayOneShot(itemTouchSFX);
    }

    [ContextMenu("PlayItemDestroySFX")]
    public void PlayItemDestroySFX()
    {
        soundSource.volume = 1f;
        soundSource.PlayOneShot(itemDestroySFX);
    }
}