using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public int scene;

    public void beginGame() 
    {
        SceneManager.LoadScene(scene);
    }
}
