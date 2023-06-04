using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float score;
    [SerializeField] private PlayerMovement playerMov;
    [SerializeField] private MeleeWeapon weapon;
    private void Start()
    {
        playerMov = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float newScore = gameObject.transform.position.z * 10;
        float killScore = weapon.killCount * 1000;
        if (score < newScore) score = newScore * killScore;
        playerMov.walkSpeed = 10 + score / 10000;
        playerMov.crouchSpeed = 5 + score / 20000;
        playerMov.UpdateMoveSpeed();
    }
}
