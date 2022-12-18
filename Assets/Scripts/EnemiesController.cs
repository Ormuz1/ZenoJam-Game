using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    public int positionOfPatrol;
    public Transform point;
    private bool moveRight;

    Transform player;
    public float stoppingDistance;

    bool idle;
    bool angry;
    bool goBack;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            idle = true;
        }

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            idle = false;
            goBack = false;
        }

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;

        }


        if (idle)
        {
            Chill();
        }
        else if (angry)
        {
            Angry();
        }
        else if (goBack)
        {
            Goback();
        }
    }

    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            moveRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            moveRight = true;
        }

        transform.position = moveRight 
            ? new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y) 
            : new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
    }


    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void Goback()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
    }
}