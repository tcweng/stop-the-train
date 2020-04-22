using System.Collections;
using UnityEngine;

public class GenerateGoal : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject goal;
    public GameObject winAftermath;
    public GameObject screenUI;
    public GameObject countdown;

    [Header("Variables")]
    public static GameObject summonedGoal;
    public static float minTime;
    public static float maxTime;
    public static float randomSec;
    public static int station;
    public static Coroutine summonCoroutine;

    // At the start of the game, summon the first goal.
    void Start()
    {
        SummonGoal();
        minTime = 1;
        maxTime = 3;
    }

    void Update()
    {
        // If winState is triggered, these are the actions that will be performed.
        // Code out the activities here.
        if (StateManager.winState == true)
        {
            Time.timeScale = 0;
            winAftermath.SetActive(true);
            screenUI.SetActive(false);
        }

        if (summonedGoal != null)
        {
            StateManager.emptyState = false;
        } else
        {
            StateManager.emptyState = true;
        }
    }

    // Random number ranging from minTime to maxTime for seconds.
    void RandomTime(float minTime, float maxTime)
    {
        randomSec = Random.Range(minTime, maxTime);   
    }

    // Instantiate goal at 15y above player.
    public void SummonGoal()
    {
        summonedGoal = Instantiate(goal, new Vector2(transform.position.x, transform.position.y + 15), Quaternion.identity);
    }

    // Instantiate Next Level upon winning
    public void NextLevel()
    {
        StartCoroutine(SummonProcedure());
        station += 1;
        PlayerControl.maxSpeed += 1;
        PlayerControl.turbo += 0.01f;
        TextManager.time = TextManager.defaultTime + 2;
    }

    // Instantiate Advertisement and goes to retry state.
    public void AdsContinue()
    {
        StartCoroutine(SummonProcedure());
        StateManager.retryState = true;
        TextManager.time = TextManager.defaultTime + 2 * station;
    }

    // Fixed procedure of summoning subsequent goals.
    IEnumerator SummonProcedure()
    {
        Destroy(summonedGoal);
        StateManager.onGoal = false;
        StateManager.winState = false;
        StateManager.loseState = false;
        Time.timeScale = 1;
        yield return new WaitForSeconds(2);
        countdown.SetActive(true);
        RandomTime(minTime + 1f, maxTime + 1f);
        yield return new WaitForSeconds(randomSec);
        if (StateManager.emptyState == true)
        {
            countdown.SetActive(false);
            SummonGoal();
        } else
        {
            countdown.SetActive(false);
        }
    }
}
