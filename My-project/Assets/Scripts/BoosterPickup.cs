using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickup : MonoBehaviour
{
    public float boostAmount = 3f; // ��������� �������� ����� ������� 
    public float boostDuration = 10f; // ����� ������������ ����� 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                StartCoroutine(BoostPlayer(player));
                Destroy(gameObject); // ����������� ������� ������� ����� ������� 
            }
        }
    }

    IEnumerator BoostPlayer(PlayerController player)
    {
        player.ApplyBoost(boostDuration); // ���������� ��������� 
        yield return new WaitForSeconds(boostDuration); // ���������� �������� �� ��������� ����� 
        player.RemoveBoost(boostAmount); // ������� � ������� �������� ����� ��������� ��������� 
        player.ResetSpeedCoroutine(boostDuration); // ������� �������� � �������� �������� 
    }

}