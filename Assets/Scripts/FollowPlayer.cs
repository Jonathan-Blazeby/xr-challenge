using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform listener;
    private Transform player;
    private Vector3 startOffset;

    void Start()
    {
        listener =  transform.GetChild(0).transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + startOffset;
        listener.position = player.transform.position;
    }
}
