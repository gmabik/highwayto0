using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    void Update()
    {
        transform.position = new Vector3(0, -70, Player.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.TryGetComponent<PlayerMovement>(out PlayerMovement playerMove))
        {
            Player.transform.position = new Vector3(Player.transform.position.x, 300, Player.transform.position.z);
        }
    }
}
