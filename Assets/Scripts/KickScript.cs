using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickScript : MonoBehaviour {

    Vector3 angularVelocity;
    KinectInterop.JointType rightKnee = KinectInterop.JointType.KneeRight;
    KinectInterop.JointType rightFoot = KinectInterop.JointType.FootRight;

    KinectManager manager;

    Quaternion lastKneeRotation;

    Ray footRay;
    Vector3 rayPos;

    // Use this for initialization
    void Start () {
        manager = KinectManager.Instance;

        rayPos = new Vector3(transform.position.x + 0.025f, transform.position.y - 0.075f, transform.position.z + 0.19f);
        footRay = new Ray(rayPos, transform.TransformDirection(Vector3.down));
        
    }

    private void FixedUpdate()
    {
       
        if (manager && manager.IsUserDetected())
        {
            long userId = manager.GetPrimaryUserID();

            if(manager.IsJointTracked(userId,(int)rightKnee))
            {
                Quaternion deltaRot = manager.GetJointOrientation(userId, (int)rightKnee, true) * Quaternion.Inverse(lastKneeRotation);
                Vector3 eulerRot = new Vector3(Mathf.DeltaAngle(0, deltaRot.eulerAngles.x),
                    Mathf.DeltaAngle(0, deltaRot.eulerAngles.y), Mathf.DeltaAngle(0, deltaRot.eulerAngles.z));

                angularVelocity = eulerRot / Time.fixedDeltaTime;
                lastKneeRotation = manager.GetJointOrientation(userId, (int)rightKnee, false);
            }

            rayPos = new Vector3(transform.position.x + 0.025f, transform.position.y - 0.075f, transform.position.z + 0.19f);
            footRay.origin = rayPos;

            RaycastHit hit;

            if (Physics.Raycast(footRay, out hit, 0.06f))
            {
               
                if (hit.transform.CompareTag("Ball"))
                {
                    hit.rigidbody.rotation = manager.GetJointOrientation(userId, (int)rightFoot, false);

                    Vector3 direction = hit.rigidbody.transform.TransformDirection(manager.GetJointDirection(userId, (int)rightFoot, false, true));
                    Vector3 balVelocity = new Vector3(direction.x * angularVelocity.x, direction.y * angularVelocity.y, direction.z * angularVelocity.z);

                    if (balVelocity.y < 0)
                        balVelocity.y *= -1;

                    if (balVelocity.z < 0)
                        balVelocity.z *= -1;

                    Debug.Log(balVelocity);

                    hit.transform.GetComponent<FootBall>().SetVelocity(balVelocity);

                    /*if (balVelocity.y > 50)
                        balVelocity.y = 50;

                    if (balVelocity.z > 50)
                        balVelocity.z = 50;

                    balVelocity.y = Mathf.Clamp(balVelocity.y, 10f, 50f);
                    balVelocity.z = Mathf.Clamp(balVelocity.z, 10f, 50f);
                    Debug.Log(balVelocity);*/
                }
            }
        }
    }
}
