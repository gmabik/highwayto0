using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using FMODUnity;

public class StandingEnemyScript : MonoBehaviour
{

    [SerializeField] private EventReference EnemyShoot;

    public Transform target;

    [SerializeField] private float timer = 5;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float bulletForce;

    public float RotationSpeed;
    private Quaternion _lookRotation;
    private Vector3 _direction;

    void Update()
    {
        bulletTime -= Time.deltaTime;
        if (target == null) return;

        _direction = (target.position - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        _lookRotation.x = 0f;
        _lookRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        Shoot();
    }

    void Shoot()
    {
        if (bulletTime > 0) return;
        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * bulletForce);
        Destroy(bulletObj, 5f);
        AudioManager.instance.PlayOneShot(EnemyShoot, this.transform.position);

    }
}
