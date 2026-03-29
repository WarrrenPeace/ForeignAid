using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    void Awake()
    {
        if(!instance) instance = this;
        else
        {Destroy(gameObject);}
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
