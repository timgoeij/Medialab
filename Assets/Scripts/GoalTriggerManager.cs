using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTriggerManager : MonoBehaviour
{

    private GoalTrigger[] goalTriggers;

    private float switchTime = 10.0f;
    private float timer = 0;
    private float time = 0;
    
    // Use this for initialization
	void Start ()
    {
        goalTriggers = GameObject.FindObjectsOfType<GoalTrigger>();
        switchTriggerSpot();
    }
	
	// Update is called once per frame
	void Update () {

        if(timer >= time)
        {
            switchTriggerSpot();

            time += switchTime;
        }
        else
        {
            timer += Time.deltaTime;
        } 
	}

    void switchTriggerSpot()
    {
        int randomIndex = Random.Range(0, goalTriggers.Length);

        for (int i = 0; i < goalTriggers.Length; i++)
        {
            if (i == randomIndex)
            {
                goalTriggers[i].OpenTriggerSpot();
            }
            else
            {
                goalTriggers[i].BlockTriggerSpot();
            }
        }
    }
}
