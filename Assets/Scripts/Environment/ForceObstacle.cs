using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObstacle : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float risingMultiplier;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;
        rb.AddForce(transform.up * risingMultiplier);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;
        rb.AddForce(-transform.up * risingMultiplier);
    }
}
