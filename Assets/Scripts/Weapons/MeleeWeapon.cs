using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using FMODUnity;

public class MeleeWeapon : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private EventReference EnemyDeath;

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
        if (!collided.GetComponent<StandingEnemyScript>()) return;
        Destroy(collided.GetComponent<StandingEnemyScript>());
        collided.transform.DOMoveY(-0.3f, 0.5f).SetRelative(true);
        collided.transform.DORotate(new Vector3(-90f, 0f, 0f), 0.5f);
        Destroy(collided, 5f);
        killCount++;
        AudioManager.instance.PlayOneShot(EnemyDeath, this.transform.position);

    }
}
