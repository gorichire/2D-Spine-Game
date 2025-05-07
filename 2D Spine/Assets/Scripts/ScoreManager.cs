using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }

    public int score = 0;
    public int maxScore = 72;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            transform.SetParent(null); 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddPerfect()
    {
        score += 2;
    }

    public void AddGood()
    {
        score += 1;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
