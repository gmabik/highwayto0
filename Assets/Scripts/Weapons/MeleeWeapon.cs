using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class MeleeWeapon : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimParams();
    }

    private void SetAnimParams()
    {
        if (Input.GetMouseButton(0)) animator.SetBool("attacking", true);
        animator.SetBool("walking", isWalking());
    }

    private bool isWalking()
        => transform.parent.GetComponent<PlayerMovement>().state == PlayerMovement.MovementState.sprinting;

    public void StopAttacking()
    {
        animator.SetBool("attacking", false);
    }

    public float killCount;
    private void OnTriggerEnter(Collider collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.tag != "enemy") return;
        collided.GetComponent<StandingEnemyScript>().enabled = false;
        collided.transform.DOMoveY(-3f, 0.5f).SetRelative(true);
        collided.transform.DORotate(new Vector3(-90f, 0f, 0f), 0.5f);
        Destroy(collided/*, 5f*/);
    }
}
