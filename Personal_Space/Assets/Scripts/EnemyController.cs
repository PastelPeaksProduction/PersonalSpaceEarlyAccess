﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : MonoBehaviour
{
    public float followSpeed;
    public float moveBackSpeed;
    public bool movingZone;
    public float stoppingDistance = 15;
    public bool isMoving = false;

    private Vector3 startingPosition;
    private Transform playerPosition;
    private NavMeshAgent agent;
    private Color randColor;



    void Awake()
    {
        // Initialize variables
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startingPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();

        /*randColor = Color.black;
        while (randColor.r + randColor.g + randColor.b < 1.0f)
        {
            randColor = new Color(Random.value, Random.value, Random.value);
        }

        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = randColor;*/

    }


    //--------------------HELPER METHODS--------------------//

    /**
     *  Helper methods that moves the enemy to the player
     **/
    public void moveTowardsPlayer()
    {
        float distance = Mathf.Abs(Vector3.Distance(this.transform.position, playerPosition.position));
        if (movingZone && distance > stoppingDistance)
        {
           
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, playerPosition.position, followSpeed * Time.deltaTime);
            //agent.destination = playerPosition.position;
        }
        else
        {
            if(distance > stoppingDistance)
            {
                //Debug.Log(distance);
                //transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, followSpeed * Time.deltaTime);
                agent.isStopped = false; 
                agent.destination = playerPosition.position;

                
                Vector3 thisPos = this.transform.position;

                float calcSpeed = Mathf.Sqrt((agent.desiredVelocity.x * agent.desiredVelocity.x) + (agent.desiredVelocity.z * agent.desiredVelocity.z));
                if (!calcSpeed.Equals(0))
                {
                    agent.velocity = new Vector3((agent.desiredVelocity.x) * followSpeed / calcSpeed, 0, (agent.desiredVelocity.z) * followSpeed / calcSpeed);
                }
                agent.SetDestination(playerPosition.position);
                isMoving = true;
            }
            else
            {
                //moveBackToStart();
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                transform.position = this.transform.position;
                isMoving = false;
            }
        }
    }

    public void stopMovement()
    {
        this.agent.ResetPath();
        agent.isStopped = true;
        //Stopps glide to stop after player ovement stops
        Vector3 thisPos = this.transform.position;
        this.transform.position = thisPos;
        isMoving = false;

    }

    /**
     *  Helper methods that moves the enemy to the their starting location
     **/
    public void moveBackToStart()
    {

        if (!movingZone)
        {

            //transform.position = Vector3.MoveTowards(transform.position, startingPosition, moveBackSpeed * Time.deltaTime);
            if (this.transform.position != startingPosition)
            {
                agent.isStopped = false;
                agent.destination = startingPosition;
                isMoving = true;
            }
            else
            {
                stopMovement();
            }
        }

    }
    
}
