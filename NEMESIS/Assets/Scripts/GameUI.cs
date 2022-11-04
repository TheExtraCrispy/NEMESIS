using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private void Awake()
    {
        PlayerEvents.PlayerKilled += OnPlayerDeath;
    }

    void OnPlayerDeath(object sender, EventArgs args)
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    private void OnDisable()
    {
        PlayerEvents.PlayerKilled -= OnPlayerDeath;
    }
}
