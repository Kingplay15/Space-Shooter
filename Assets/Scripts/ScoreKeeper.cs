using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
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
