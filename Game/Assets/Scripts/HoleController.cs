using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Ganhou");
        other.gameObject.GetComponent<BallController>().StopBall();
    }
}
