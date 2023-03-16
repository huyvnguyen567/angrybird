using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AngryBirdView : MonoBehaviour
{
    [SerializeField] private AngryBirdController controller;
    [SerializeField] private Text scoreText;

    [SerializeField] private Image setting;
    public BirdController Bird { get; set; }


    private int scoreValue;
    void Start()
    {
        scoreText.text = $"Score: {scoreValue}";
    }
    public void ShowScore(int score)
    {
        scoreValue += score;
        scoreText.text = $"Score: {scoreValue}";
    }

    public void StateSetting(bool state)
    {
        setting.gameObject.SetActive(state);
    }

    public void ShowPredictionLine(bool state)
    {
        Bird.ActiveLine(state);
    }

}
