using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public float score;
    [SerializeField] private PlayerMovement playerMov;
    [SerializeField] private MeleeWeapon weapon;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;

    private float starterWalkSpeed;
    private float starterCrouchSpeed;

    private void Start()
    {
        starterWalkSpeed = playerMov.walkSpeed;
        starterCrouchSpeed = playerMov.crouchSpeed;
    }

    void FixedUpdate()
    {
        CountNewScore();

        scoreText.text = Mathf.Round(score) + "";
        comboText.text = weapon.killCount + "x";
    }

    private void CountNewScore()
    {
        float killScore = weapon.killCount * 1000;
        float newScore = playerMov.transform.position.z * 10 + killScore;
        if (score < newScore) score = newScore;
        playerMov.walkSpeed = starterWalkSpeed + score / 10000;
        playerMov.crouchSpeed = starterCrouchSpeed + score / 20000;
        playerMov.UpdateMoveSpeed();
    }
}
