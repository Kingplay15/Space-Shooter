using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    public static ScoreKeeper instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }           
        else Destroy(gameObject);
    }

    public EventHandler OnScoreChangeEvent;

    public int score { get; private set; }

    public void ModifyScore(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
        OnScoreChangeEvent?.Invoke(this, EventArgs.Empty);
    }

    public void ResetScore() => score = 0;
}
