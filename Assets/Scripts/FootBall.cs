using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBall : MonoBehaviour {

    private Vector3 startPos;
    private Rigidbody body;
    private GuiManager guiManager;
    
    // Use this for initialization
	void Start () {

        body  = transform.GetComponent<Rigidbody>();
        startPos = body.position;
        guiManager = GameObject.FindObjectOfType<GuiManager>();
		
	}

    private void FixedUpdate()
    {
        if(body.position.z > 50 || body.position.x < -50 || body.position.x > 50 || body.position.y < 0)
        {
            //guiManager.SetMessage(Color.red, "You Missed, Try to aim at the green box");
            ResetBall();
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        body.AddForce(velocity, ForceMode.Impulse);
    }

    public void ResetBall()
    {
        body.position = startPos;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.rotation = Quaternion.identity;
    }
}
