using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;
    private bool inventoryOn;
    private int[] itemCounts;
    private GameObject[] itemPrefabs;
    private int strawberryCount = 0;
    private bool canActivatePortal = false;

    private void Start()
    {
        inventoryOn = false;
        itemCounts = new int[slots.Length];
        itemPrefabs = new GameObject[slots.Length];
    }

    public void Chest()
    {
        if (inventoryOn == false)
        {
            inventoryOn = true;
            inventory.SetActive(true);
        }
        else
        {
            inventoryOn = false;
            inventory.SetActive(false);
        }
    }

    public bool CanActivatePortal()
    {
        return canActivatePortal;
    }

    public bool HasEnoughStrawberries(int requiredCount)
    {
        return strawberryCount >= requiredCount;
    }


    public void AddItem(GameObject item, AudioSource pickupSound)
{
    if (item.CompareTag("strawberry"))
    {
        strawberryCount++;
        if (strawberryCount >= 8)
        {
            canActivatePortal = true;
        }
    }

    bool itemAlreadyInInventory = false;

    for (int i = 0; i < slots.Length; i++)
    {
        if (itemPrefabs[i] == item)
        {
            itemCounts[i]++;
            UpdateItemCountText(i);
            itemAlreadyInInventory = true;
            break;
        }
    }

    if (!itemAlreadyInInventory)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!isFull[i])
            {
                isFull[i] = true;
                GameObject newItem = Instantiate(item, slots[i].transform);
                itemPrefabs[i] = item;
                itemCounts[i] = 1;
                UpdateItemCountText(i);

                // Воспроизведение звука при добавлении предмета
                if (pickupSound != null)
                {
                    pickupSound.Play();
                }

                break;
            }
        }
    }
    if (pickupSound != null)
    {
        pickupSound.Play();
    }
}

    private void UpdateItemCountText(int index)
    {
        Text countText = slots[index].transform.GetChild(0).GetComponentInChildren<Text>();
        countText.text = itemCounts[index].ToString();
    }
}
