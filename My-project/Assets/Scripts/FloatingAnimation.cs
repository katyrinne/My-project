using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    public float floatHeight = 0.5f; // Высота подъема предмета
    public float floatSpeed = 0.5f; // Скорость анимации

    private Vector3 startPos;
    private bool floatingUp = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = transform.position.y;

        if (floatingUp)
        {
            newY += floatSpeed * Time.deltaTime;
            if (newY >= startPos.y + floatHeight)
            {
                floatingUp = false;
            }
        }
        else
        {
            newY -= floatSpeed * Time.deltaTime;
            if (newY <= startPos.y)
            {
                floatingUp = true;
            }
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
