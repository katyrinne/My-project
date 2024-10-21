using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Slider slider;
    public float FillSpeed = 0.2f; // Скорость заполнения полосы

    private float targetProgress = 0;

    private void Awake()
    {
        slider = GetComponent<Slider>(); // Changed from gameObject.GetComponent<Slider>() to just GetComponent<Slider>()
    }

    private void Start()
    {
        if(slider != null) // Added a null check for slider
        {
            IncrementProgress(1f); // Увеличиваем прогресс на 1 перед загрузкой сцены
        }
    }

    private void Update()
    {
        if (slider != null && slider.value < targetProgress) // Added a null check for slider
        {
            slider.value += FillSpeed * Time.deltaTime;
        }
        else
        {
            StartCoroutine(LoadSceneDelay());
        }
    }

    private IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("First_Forest");
    }

    public void IncrementProgress(float newProgress)
    {
        if (slider != null) // Added a null check for slider
        {
            targetProgress = Mathf.Min(slider.value + newProgress, slider.maxValue); // Устанавливаем прогресс, ограничивая его максимальным значением
        }
    }
}