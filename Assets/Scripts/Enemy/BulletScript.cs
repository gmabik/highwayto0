using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BulletScript : MonoBehaviour
{

    [SerializeField] private EventReference PlayerDeath;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().Die();
            AudioManager.instance.PlayOneShot(PlayerDeath, this.transform.position);

        }
    }
}