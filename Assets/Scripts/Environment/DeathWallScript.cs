using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeathWallScript : MonoBehaviour
{
    public float time = 0f;
    public float startSpeed = 5f;
    public float speedCoef = 15f;
    public float currentSpeed;

    private void Update()
    {
        time += Time.deltaTime;
        currentSpeed = startSpeed + time/speedCoef;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(0, 0, transform.position.z + currentSpeed / 60);
    }
}
