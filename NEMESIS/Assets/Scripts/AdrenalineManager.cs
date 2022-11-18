using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Combo
{
    public string name;
    public string colorHexCode;
    public float lifespan;
    public Combo(string name, string colorCode, float life)
    {
        this.name = name;
        colorHexCode = colorCode;
        lifespan = life;
    }
}

public class AdrenalineManager : MonoBehaviour
{
    public static AdrenalineManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI comboLogText;
    [SerializeField] Image adrenalineBar;
    private float timeOfLastKill;
    public float multikillPeriod;
    [SerializeField] float maxMultiplier;
    public int score = 0;
    public int kills = 0;
    public int waves = 1;
    float multiplier;
    List<Combo> comboLog = new List<Combo>();

    [SerializeField] float scorePerAdrenalinePoint;
    [SerializeField] float adrenalineDrainRate;
    [SerializeField] float adrenalineDrainDelay;
    public float adrenaline;
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
        int scoreAdd = Mathf.CeilToInt(context.rawScore * Mathf.Clamp(multiplier, 1, maxMultiplier));
        UpdateScore(scoreAdd);
        CreateCombo(context.causeOfDeath);
        if(Time.time <= timeOfLastKill + multikillPeriod)
        {
            comboLog.Add(new Combo("MULTIKILL", "#11FF00", 6));
            UpdateScore(10);
        }
        timeOfLastKill = Time.time;
    }

    private void UpdateScore(int scoreAdd)
    {
        score += scoreAdd;
        adrenaline += Mathf.FloorToInt(scoreAdd / scorePerAdrenalinePoint);
        string scoreString = "Score: " + score;
        scoreText.text = scoreString;
    }

    private void Update()
    {
        UpdateComboLog();
        UpdateRank();

        adrenaline = Mathf.Clamp(adrenaline - adrenalineDrainRate * Time.deltaTime, 0, 100);
        adrenalineBar.fillAmount = adrenaline / 100;
    }

    private void CreateCombo(string causeOfDeath)
    {
        if(causeOfDeath == "Heatsink")
        {
            comboLog.Add(new Combo("EXPLODED", "#FFA500", 5));
            UpdateScore(5);
        }
    }

    private void UpdateRank()
    {
        string rank = "";
        if (adrenaline <= 20)
        {
            rank = "<color=#42AEFF><size=40>D</size>ISMAL";
        }
        else if (adrenaline >= 20 && adrenaline < 40)
        {
            rank = "<color=#51FF42><size=40>C</size>APTAIN";
        }
        else if (adrenaline >= 40 && adrenaline < 60)
        {
            rank = "<color=#FFC642><size=40>B</size>ARON";
        }
        else if (adrenaline >= 60 && adrenaline < 80)
        {
            rank = "<color=#FF6100><size=40>A</size>CE";
        }
        else
        {
            rank = "<color=red><size=30><mspace=20>NEMESIS";
        }

        rankText.text = rank;
    }

    private void UpdateComboLog()
    {
        string logText = "";
        foreach(Combo combo in comboLog)
        {
            combo.lifespan -= Time.deltaTime;
            if (combo.lifespan <= 0)
            {
                comboLog.Remove(combo);
            }
            else
            {
                logText += String.Format("<color={0}>{1}</color>\n", combo.colorHexCode, combo.name);
            }
        }
        comboLogText.text = logText;
    }

    private void OnDisable()
    {
        ScoreEvents.EnemyDeath -= OnEnemyDeath;
        ScoreEvents.WaveComplete -= OnWaveComplete;
    }
}
