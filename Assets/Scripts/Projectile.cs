using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float lifeSpan = 3.0f;

    public void Direction(Vector3 dir) { direction = dir;  }

    private void Awake()
    {
        direction = transform.forward;
        Destroy(gameObject, lifeSpan);
    }
    void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<PlayerHealth>().UpdateHealth(PlayerHealth.damageSources.bullet); 
        }
        Destroy(gameObject);
    }
}
