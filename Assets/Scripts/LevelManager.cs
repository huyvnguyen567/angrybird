using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    
    public void PlayGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
