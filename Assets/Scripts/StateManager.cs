using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static bool checkState;
    public static bool winState;
    public static bool loseState;
    public static bool retryState;
    public static bool emptyState;

    public static bool onGoal;

    private void Update()
    {        
        /*
         * Check if checkState is trigger(true)
         * If its triggered, it will check whether the train's head is on the red line (goal).
         * If it's on the goal, it goes to winState, else it goes to loseState.
         * winState will continue its action at GenerateGoal.
         * loseState will continue its action at PlayerControl.
         */

        if (checkState == true)
        {
            if (onGoal == true)
            {
                winState = true;
            }
            else
            {
                loseState = true;
            }
        }

        /*
         * If either loseState or winState is triggered (true),
         * checkState will reverse back to false.
         */

        if (loseState == true || winState == true)
        {
            checkState = false;
        }
    }

    /*
     * If train's head is on staying on top of the goal,
     * It triggers onGoal and leads to winState.
     * Else it leads to loseState.
     */

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            onGoal = true;
        } else
        {
            onGoal = false;
        }
    }

    /*
     * When player went pass the goal without braking or failed.
     * It goes to loseState instantly.
     */

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal") && winState == false)
        {
            // Tested and Working
            onGoal = false;
            loseState = true;
        }
    }

}
