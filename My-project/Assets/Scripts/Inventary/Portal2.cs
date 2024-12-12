using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal2 : MonoBehaviour
{
    private Inventory inventory;
    public int requiredRune_red = 3; 
    public int requiredRune_blue = 2; 

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
            if (inventory.HasEnoughRuneRed(requiredRune_red) && inventory.HasEnoughRuneBlue(requiredRune_blue))
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
