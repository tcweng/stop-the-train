using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSrc;

    [Header("Game Objects")]
    public GameObject train;
    public GameObject loseAftermath;
    public GameObject screenUI;
    public GameObject countdown;
    public Button adContinue;
    public AudioSource brakingSfx;

    [Header("Speed Adjustment")]
    public float acceleration = 0.03f;
    public float brake = 0.01f;
    public static float speed;
    public static float turbo;
    public static float maxSpeed;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        audioSrc = GetComponent <AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        maxSpeed = 10;
        speed = 1;
    }

    void Update()
    {
        // Constantly Move-Up and adjust train sound effect volume proportionate to speed.
        rb.velocity = transform.up * speed;
        audioSrc.volume = (speed / maxSpeed) / 2.5f;

        // Mouse/Touch Input to Brake/Slowdown the train
        if (Input.GetMouseButton(0) && StateManager.checkState == false)
        {
            // Braking (lower = lesser brake)
            speed -= acceleration + brake;
        } else if (StateManager.checkState == false && StateManager.winState == false && StateManager.loseState == false)
        {
            // Acceleration (higher = higher acceleration)
            speed += acceleration + turbo;
        }

        if (Input.GetMouseButtonDown(0))
        {
            brakingSfx.Play();
        } else if (Input.GetMouseButtonUp(0))
        {
            brakingSfx.Stop();
        }

        /*
         * Restrict Max Speed and enter "Check State" when train stop.
         * Go to StateManager, once Enter checkState, it will check whether it should be a win or a lose.
         */

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (speed < 0)
        {
            speed = 0;
            StateManager.checkState = true;
        }

        /*
         * When loseState is triggered from StateManager
         * It will stop the train if the train is still moving.
         * Pause the game and shows menu once the train stop.
         */

        if (StateManager.loseState == true)
        {
            if (speed > 0)
            {
                speed -= 0.2f;
            }
            else
            {
                // Things that need to be perform when lose.
                loseAftermath.SetActive(true);
                Time.timeScale = 0;
            }
        }

        /*
         * RetryState is a bonus state where players choose to watch an ads to continue their progress.
         * When retryState is trigger, it simply disable the watch ads button to prevent continuous progress.
         */

        if (StateManager.retryState == true)
        {
            adContinue.interactable = false;
        } else
        {
            adContinue.interactable = true;
        }
    }

    /*
     * Reset() function resets all variables back to their origin.
     */

    public void Reset()
    {
        Destroy(GenerateGoal.summonedGoal);
        StateManager.loseState = false;
        StateManager.retryState = false;
        GenerateGoal.minTime = 1;
        GenerateGoal.maxTime = 3;
        GenerateGoal.station = 0;
        TextManager.time = TextManager.defaultTime;
        countdown.SetActive(false);
        train.transform.position = new Vector2(0, 0);
        maxSpeed = 10;
        turbo = 0;
        Time.timeScale = 1;
    }
}
