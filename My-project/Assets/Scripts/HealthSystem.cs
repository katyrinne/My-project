using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int health = 3;
    private bool isHurt = false;
    private bool isAnimating = false;
    private float timeInDamageZone = 0f;
    private float timeBeforeAdditionalDamage = 4f;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator animator;

    public AudioSource damageSound;
    public AudioSource GameOverSound;

    void Update()
    {
        if (isHurt)
        {
            timeInDamageZone += Time.deltaTime;
            if (timeInDamageZone >= timeBeforeAdditionalDamage)
            {
                TakeDamage(1);
                timeInDamageZone = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DamageZone")) // Проверяем столкновение с озером
        {
            if (!isAnimating)
            {
                TakeDamage(1);
                isAnimating = true;
            }
            isHurt = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DamageZone")) // Проверяем выход из зоны урона
        {
            isHurt = false;
            timeInDamageZone = 0f;
            isAnimating = false;
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        damageSound.Play();
        UpdateHeartsDisplay();
        StartCoroutine(GetHurt());
        if (health <= 0)
        {   
            SceneManager.LoadScene("GameOver"); 
            GameOverSound.Play();
        }
    }

      IEnumerator GetHurt(){ 
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);

    }
    void UpdateHeartsDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}