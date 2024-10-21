using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public GameObject Music1;
    private AudioSource audioSrc1;
    public GameObject[] objs11;

    void Awake()
    {
        objs11 = GameObject.FindGameObjectsWithTag("sound");
        if (objs11.Length == 0)
        {
            Music1 = Instantiate(Music1);
            Music1.name = "Music1";
            DontDestroyOnLoad(Music1.gameObject);
        }
        else
        {
            Music1 = GameObject.Find("Music1");
        }
    }
    void Start()
    {
        audioSrc1 = Music1.GetComponent<AudioSource>();
    }
}
