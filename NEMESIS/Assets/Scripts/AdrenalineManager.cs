using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdrenalineManager : MonoBehaviour
{
    public static AdrenalineManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI comboLogText;
    [SerializeField] Image adrenalineBar;

    [SerializeField] float maxMultiplier;
    public int score = 0;
    public int kills = 0;
    public int waves = 1;
    float multiplier;
    List<string> comboLog = new List<string>();
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        ScoreEvents.EnemyDeath += OnEnemyDeath;
        ScoreEvents.WaveComplete += OnWaveComplete;
    }

    public void OnWaveComplete(object sender, EventArgs args)
    {
        ++waves;
    }
    private void OnEnemyDeath(object sender, DeathContext context)
    {
        ++kills;
        score +=  Mathf.CeilToInt(context.rawScore * Mathf.Clamp(multiplier, 1, maxMultiplier));
        //Debug.Log("Enemy killed by " + context.causeOfDeath + " at " + context.timeOfDeath + ". Score is now " + score);
        UpdateScore();
    }

    private void UpdateScore()
    {
        string scoreString = "Score: " + score;
        scoreText.text = scoreString;
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        ScoreEvents.EnemyDeath -= OnEnemyDeath;
        ScoreEvents.WaveComplete -= OnWaveComplete;
    }
}
