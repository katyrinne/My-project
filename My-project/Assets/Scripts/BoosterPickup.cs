using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickup : MonoBehaviour
{
    public float boostAmount = 3f; // Изменение скорости после подбора 
    public float boostDuration = 10f; // Время длительности буста 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                StartCoroutine(BoostPlayer(player));
                Destroy(gameObject); // Уничтожение объекта бустера после подбора 
            }
        }
    }

    IEnumerator BoostPlayer(PlayerController player)
    {
        player.ApplyBoost(boostDuration); // Применение ускорения 
        yield return new WaitForSeconds(boostDuration); // Увеличение скорости на указанное время 
        player.RemoveBoost(boostAmount); // Возврат к обычной скорости после окончания ускорения 
        player.ResetSpeedCoroutine(boostDuration); // Возврат скорости к базовому значению 
    }

}