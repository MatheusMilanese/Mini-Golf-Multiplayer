using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        other.gameObject.GetComponent<BallController>().StopBall();
    }
}
