using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float turnSpeed = 35.0f;
    private float fireElapsedTime;
    [SerializeField] private float fireTimeDelay = 1.0f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        fireElapsedTime += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.position) <= 10)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, turnSpeed * Time.deltaTime);

            Fire();
        }
    }

    private void Fire()
    {
        if (fireElapsedTime >= fireTimeDelay)
        {
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            GetComponent<AudioSource>().Play();
            fireElapsedTime = 0;
        }
    }
}
