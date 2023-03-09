using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public int scoreValue;

    public Image setting;

    public Slider sliderSound;

    void Start()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        AudioListener.volume = sliderSound.value;


        if (!PlayerPrefs.HasKey("volumePref"))
        {
            PlayerPrefs.SetFloat("volumePref", 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }
    private void Update()
    {
      
    }
    public void UpdateScore(int score)
    {
        scoreValue += score;
        scoreText.text = "Score: " + scoreValue.ToString();
    }
    public void ChangeVolume()
    {
        AudioListener.volume = sliderSound.value;
        SaveVolume();
    }
    public void LoadVolume()
    {
        sliderSound.value = PlayerPrefs.GetFloat("volumePref");
    }   
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("volumePref", sliderSound.value);
    }    
   
    public void StateSetting(bool state)
    {
        setting.gameObject.SetActive(state);
    }    
  

}
