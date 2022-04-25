using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private int lives;
    private int level;
    private int score;
    public Text levelText;
    public Text livesText;
    public Text scoreText;

    void Start()
    {
        level = FindObjectOfType<GameManager>().level;
        lives = FindObjectOfType<GameManager>().lives;
        score = FindObjectOfType<GameManager>().score;
    }
    void Update()
    {
        levelText.text = "Level " + level.ToString();
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
    }
}
