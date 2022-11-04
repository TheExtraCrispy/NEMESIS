using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PerformanceRecap : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI scoreText;

    public Canvas restartCanvas;
    private void Awake()
    {
        PlayerEvents.PlayerKilled += OnPlayerDeath;
    }

    void OnPlayerDeath(object sender, EventArgs args)
    {
        restartCanvas.enabled = true;

        waveText.text = "Wave Reached: " + AdrenalineManager.instance.waves;
        killsText.text = "Kills: " + AdrenalineManager.instance.kills;
        scoreText.text = "Score: " + AdrenalineManager.instance.score;
    }

    private void OnDisable()
    {
        PlayerEvents.PlayerKilled -= OnPlayerDeath;
    }
}
