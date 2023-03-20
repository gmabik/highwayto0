using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private bool isWalking()
    => gameObject.transform.parent.GetComponent<PlayerMovement>().state == PlayerMovement.MovementState.sprinting;

    private void SetAnimParams()
    {
        if(Input.GetMouseButton(0)) animator.SetBool("attacking", true);
        animator.SetBool("walking", isWalking());
    }

    public void StopAttacking()
    {
        animator.SetBool("attacking", false);
    }
}
