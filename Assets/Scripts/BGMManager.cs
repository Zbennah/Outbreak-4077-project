using UnityEngine;


public class BGMManager : MonoBehaviour
{
    public AudioClip bgmClip;   
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = bgmClip;
        audioSource.loop = true;        
        audioSource.playOnAwake = true; 

        audioSource.volume = 0.5f;
    }

    void Start()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
