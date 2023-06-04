using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{
    private GameObject enemy;
    private StandingEnemyScript enemyScript;
    private Animator enemyAnim;
    void Start()
    {
        enemy = transform.parent.gameObject;
        enemyScript = enemy.GetComponent<StandingEnemyScript>();
        enemyAnim = enemy.GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        enemyScript.target = other.transform;
        enemyAnim.SetBool("shoot", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        enemyScript.target = null;
        enemyAnim.SetBool("shoot", false);
    }
}
