using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.Die();
        }
    }
}
