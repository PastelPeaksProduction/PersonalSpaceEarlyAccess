﻿ using UnityEngine;
    using UnityEngine.AI;
    using System.Collections;


    public class PatrolObjectiveActivate : MonoBehaviour {

        public Transform[] points;
        private int destPoint = 0;
        private NavMeshAgent agent;
        private float agentSpeed;

        public float waitTime;
        public GameObject zone;
        private ZoneScript zoneControl;

        private bool waiting = false;
        private bool waitingForKid = false;
        private PlayerController playerController;
        private GameObject player;
        private ObjectivesManager playerObjectives;
        void Start () 
        {
            agent = GetComponent<NavMeshAgent>();
            agentSpeed = agent.speed;
            agent.autoBraking = false;
            zoneControl = zone.GetComponent<ZoneScript>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            playerObjectives = player.GetComponent<ObjectivesManager>();
            //GotoNextPoint();
        }


        void GotoNextPoint()
        {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }


        void Update () 
        {
            if(playerController.isMoving || playerController.isBreathing)
            {
                agent.speed = agentSpeed;
            }
            else
            {
                agent.speed = 0;
            }
            if(zoneControl.getPlayerInZone() && !waiting)
            {
                waiting = true;
                StartCoroutine(Loiter());
            }
        }

        private IEnumerator Loiter()
        {
            yield return new WaitForSeconds(waitTime);
            agent.speed = agentSpeed;
            GotoNextPoint();
            waiting = false;
            waitingForKid = false;
        }
    }