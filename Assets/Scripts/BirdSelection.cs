using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdSelection : MonoBehaviour
{
    public GameObject[] birds;
    public int birdIndex = 0;

    public Image selectionBackround;

    public void State(bool state)
    {
        selectionBackround.gameObject.SetActive(state);
    }
    public void NextBird()
    {
        birds[birdIndex].SetActive(false);
        birdIndex = (birdIndex + 1) % birds.Length;
        birds[birdIndex].SetActive(true);
    }    
    public void PreviousBird()
    {
        birds[birdIndex].SetActive(false);
        birdIndex--;
        if (birdIndex < 0)
        {
            birdIndex += birds.Length;
        }
        birds[birdIndex].SetActive(true);
    }    
    
    public void StartGame(string scene)
    {
        PlayerPrefs.SetInt("birdIndex", birdIndex);
        SceneManager.LoadScene(scene);
    }
}
