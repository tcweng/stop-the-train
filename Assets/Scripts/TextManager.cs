using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [Header("Database")]
    public static float time;
    public static float defaultTime;
    public static int highscore;

    [Header("Text Object")]
    public Text speedometer;
    public Text station;
    public Text countdown;
    public Text timeLimit;
    public Text loseText;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
        } 
    }

    void Start()
    {
        Debug.Log(highscore);
        defaultTime = 15;
        time = defaultTime;
    }

    void Update()
    {
        // Save and Update Highscore
        if (GenerateGoal.station > highscore)
        {
            highscore = GenerateGoal.station;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();

        }

        // randomSec and time reduce with time.
        GenerateGoal.randomSec -= Time.deltaTime;
        time -= Time.deltaTime;

        // Update all Text component with accurate data.
        station.text = "Station" + '\n' + GenerateGoal.station;
        speedometer.text = "Speed" + '\n' + Mathf.Round(PlayerControl.speed) * 4;
        countdown.text = Mathf.Round(GenerateGoal.randomSec) + " seconds until next station.";
        timeLimit.text = "Time Left" + '\n' + Mathf.Round(time);
        loseText.text = "Failed at" + '\n' + "Station " + GenerateGoal.station + '\n' + '\n' + "Highscore: " + highscore;

        // if timeLimit reach zero, player lost the game.
        if (time <= 0)
        {
            Time.timeScale = 0;
            StateManager.loseState = true;
        }
    }
}
