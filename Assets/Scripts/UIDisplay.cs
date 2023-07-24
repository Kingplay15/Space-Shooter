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
    [SerializeField] private TextMeshProUGUI healthText;
    string maxHealthDisplay;

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
        maxHealthDisplay = "/" + healthSlider.maxValue.ToString();
        healthSlider.value = healthSlider.maxValue;
        healthText.text = healthSlider.value.ToString() + maxHealthDisplay;
        playerHealth.OnHealthChangeEvent += Health_Change;

        scoreKeeper.OnScoreChangeEvent += Score_Change;
    }

    private void Health_Change(object sender, EventArgs e)
    {
        int currentHealth = playerHealth.GetHealth();
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString() + maxHealthDisplay;
    }

    private void Score_Change(object sender, EventArgs e)
    {
        scoreText.text = scoreKeeper.score.ToString("000000");
    }
}
