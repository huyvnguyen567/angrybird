using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] Text scoreText;
    [SerializeField] int scoreValue;

    [SerializeField] Image setting;

    [SerializeField] Slider sliderSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static GameManager Instance
    {
        get { return instance; }
    }
    void Start()
    {
        scoreText.text = "Score: " + scoreValue;
        AudioListener.volume = sliderSound.value;


        if (!PlayerPrefs.HasKey(Constants.VOLUME))
        {
            PlayerPrefs.SetFloat(Constants.VOLUME, 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }
   
    public void UpdateScore(int score)
    {
        scoreValue += score;
        scoreText.text = "Score: " + scoreValue;
    }
    public void ChangeVolume()
    {
        AudioListener.volume = sliderSound.value;
        SaveVolume();
    }
    public void LoadVolume()
    {
        sliderSound.value = PlayerPrefs.GetFloat(Constants.VOLUME);
    }   
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(Constants.VOLUME, sliderSound.value);
    }    
   
    public void StateSetting(bool state)
    {
        setting.gameObject.SetActive(state);
    }    
  

}
