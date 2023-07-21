using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        healthSlider.value = healthSlider.maxValue;
        playerHealth.OnHealthChangeEvent += Health_Change;

        scoreKeeper.OnScoreChangeEvent += Score_Change;
    }

    private void Health_Change(object sender, EventArgs e)
    {
        healthSlider.value = playerHealth.GetHealth();
    }

    private void Score_Change(object sender, EventArgs e)
    {
        scoreText.text = scoreKeeper.score.ToString("000000");
    }
}
