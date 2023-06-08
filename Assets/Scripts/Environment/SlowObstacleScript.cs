using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SlowObstacleScript : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] private EventReference ObstacleHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out playerMovement))
        {
            playerMovement.walkSpeed /= 2;
            playerMovement.crouchSpeed /= 2;
            playerMovement.moveSpeed /= 2;
        }
        AudioManager.instance.PlayOneShot(ObstacleHit, this.transform.position);
    }

    private void OnCollisionExit(Collision collision)
    {
        canCount = true;
    }

    bool canCount = false;
    float time = 0f;
    private void Update()
    {
        if (playerMovement == null) return;
        if (canCount) time += Time.deltaTime;
        if(time > 2f)
        {
            playerMovement.walkSpeed *= 2;
            playerMovement.crouchSpeed *= 2;
            playerMovement.moveSpeed *= 2;
            time = 0f;
            canCount = false;
        }
    }
}
