using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    private AudioSource AS;
    [SerializeField] AudioClip PickUp;
    [SerializeField] AudioClip Spend;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void PlayPickUp()
    {
        AS.PlayOneShot(PickUp);
    }
    public void PlaySpend()
    {
        AS.PlayOneShot(Spend);
    }
}
