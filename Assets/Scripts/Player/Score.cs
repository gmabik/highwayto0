using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float score;
    PlayerMovement playerMov;

    private void Start()
    {
        playerMov = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float newScore = gameObject.transform.position.z * 10;
        if (score < newScore) score = newScore;
        playerMov.walkSpeed = 10 + score / 10000;
        playerMov.crouchSpeed = 5 + score / 20000;
        playerMov.UpdateMoveSpeed();
    }
}
