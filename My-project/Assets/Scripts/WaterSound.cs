using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour
{
    AudioSource source;
    Collider2D soundTrigger;
    bool isExiting = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            source.Play();
            isExiting = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isExiting = true;
            StartCoroutine(FadeOutSound());
        }
    }

    IEnumerator FadeOutSound()
    {
        while (source.volume > 0.1f)
        {
            source.volume -= Time.deltaTime * 0.5f; // Уменьшаем громкость постепенно
            yield return null;
        }
        source.Stop();
        source.volume = 1f; // Возвращаем громкость к исходному значению
    }
}
