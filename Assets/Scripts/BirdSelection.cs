using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdSelection : MonoBehaviour
{
    [SerializeField] GameObject[] birds;
    [SerializeField] int birdIndex = 0;

    [SerializeField] Image selectionBackround;

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
        PlayerPrefs.SetInt(Constants.BIRDINDEX, birdIndex);
        SceneManager.LoadScene(scene);
    }
}
