using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    private Transform player;
    private LineRenderer line;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float turnSpeed = 35.0f;
    private float fireElapsedTime;
    [SerializeField] private float fireTimeDelay = 1.0f;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
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

            //Enables Line Renderer and adjusts according to new rotation
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.forward * 10 + transform.position);

            Fire();
        }
        else
        {
            //Disables Line Renderer when player outside range
            line.enabled = false;
        }
    }

    /// <summary>
    /// Instantiates bullet prefab, plays firing audio, restets firing timer
    /// </summary>
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
