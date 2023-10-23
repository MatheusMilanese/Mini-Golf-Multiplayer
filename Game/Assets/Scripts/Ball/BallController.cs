using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rBody;

    private float stopVelocity = 0.05f;
    private bool _isIdle;

    public bool isIdle { get => _isIdle; }

    private void Awake() {
        rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(rBody.velocity.magnitude < stopVelocity){
            StopBall();
        }
    }

    public void MoveBall(Vector3 direction, float force){
        rBody.AddForce(new Vector3(direction.x, 0, direction.z)*force);
    }

    public void StopBall(){
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        _isIdle = true;
    }
}
