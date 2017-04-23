using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {

    private GuiManager guiManager;

    private void Start()
    {
        guiManager = FindObjectOfType<GuiManager>();
    }

    public void BlockTriggerSpot()
    {
        transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.25f) ;
        GetComponent<BoxCollider>().isTrigger = false;
    }

    public void OpenTriggerSpot()
    {
        transform.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.25f);
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            other.GetComponent<FootBall>().ResetBall();
            guiManager.setScore();
            guiManager.SetMessage(Color.green, "Goal!, You hit the green box");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Ball"))
        {
            collision.transform.GetComponent<FootBall>().ResetBall();
            guiManager.SetMessage(Color.red, "You Missed, Try to aim at the green box");
        }
    }
}
