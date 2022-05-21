using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform listener;
    private Transform player;
    private Vector3 startOffset;

    // Start is called before the first frame update
    void Start()
    {
        listener =  transform.GetChild(0).transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + startOffset;
        listener.position = player.transform.position;
    }
}
