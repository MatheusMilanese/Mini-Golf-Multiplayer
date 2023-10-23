using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;

    private void OnCollisionEnter(Collision other) {
        other.transform.position = startPosition;
    }
}
