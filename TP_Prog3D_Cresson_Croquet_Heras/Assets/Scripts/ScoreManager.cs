using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int score = 0;

    public void AddScore(int value)
    {
        score += value;
        if (score < 0)
            score = 0;
        if (text != null)
            text.text = score.ToString();
    }
}
