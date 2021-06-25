using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public Text text;

    public void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        text.text = "Score: " + marioScore.Value.ToString();
        Debug.Log("updated");
    }
    private void Update()
    {

        Debug.Log(marioScore.Value.ToString());
    }
}