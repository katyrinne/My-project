using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Enable();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 0;

        if (other.gameObject.CompareTag("Player"))
        {
            float verticalInput = controls.Land.Jump.ReadValue<float>() - controls.Land.Down.ReadValue<float>();
            
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, verticalInput * speed);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        other.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
