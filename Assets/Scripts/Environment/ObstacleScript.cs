using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ObstacleScript : MonoBehaviour
{


[SerializeField] private EventReference PlayerDeath;


    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.Die();
            AudioManager.instance.PlayOneShot(PlayerDeath, this.transform.position);

        }
    }
}
