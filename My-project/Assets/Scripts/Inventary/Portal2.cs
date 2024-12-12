using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal2 : MonoBehaviour
{
    private Inventory inventory;
    public int requiredStrawberries = 8; 

    private void Start()
    {   
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory component not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inventory != null)
        {   
            if (inventory.HasEnoughStrawberries(requiredStrawberries))
            {
                Debug.Log("Player entered the portal with enough strawberries!");
                SceneManager.LoadScene("Third");
            }
            else
            {
                Debug.Log("Player entered the portal, but doesn't have enough strawberries!");
            }
        }
    }
}
