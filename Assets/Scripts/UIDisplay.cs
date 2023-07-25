using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    private string maxHealthDisplay;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        maxHealthDisplay = "/" + healthSlider.maxValue.ToString();
        healthSlider.value = healthSlider.maxValue;
        healthText.text = healthSlider.value.ToString() + maxHealthDisplay;
        playerHealth.OnHealthChangeEvent += Health_Change;

        ScoreKeeper.instance.OnScoreChangeEvent += Score_Change;
    }

    private void Health_Change(object sender, EventArgs e)
    {
        int currentHealth = playerHealth.GetHealth();
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString() + maxHealthDisplay;
    }

    private void Score_Change(object sender, EventArgs e)
    {
        scoreText.text = ScoreKeeper.instance.score.ToString("000000");
    }
}
