using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public int scoreValue;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int score)
    {
        scoreValue += score;
        scoreText.text = "Score: " + scoreValue.ToString();
    }
}
