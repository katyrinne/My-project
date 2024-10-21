using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFirst : MonoBehaviour
{
    // Start is called before the first frame update
    public void Load()
    {
        SceneManager.LoadScene("First_Forest");
    }
}
