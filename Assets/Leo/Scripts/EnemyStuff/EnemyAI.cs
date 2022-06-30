﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float chaseSpeed = 200f;
    public float roamSpeed = 50f;
    public float nextWaypointDist = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    public GameObject temp;
    public GameObject player;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Enemy.finalTarget = temp;
        Enemy.currentTarget = temp;
        Enemy.currentRoom = temp;

        InvokeRepeating("UpdatePath", 0f, 0.1f);
        StartCoroutine(WaitToRoam());
    }


    private void UpdatePath()
    {
        if (reachedEndOfPath && Enemy.state == Enemy.State.foundTarget)
        {
            Enemy.state = Enemy.State.headingToTarget;
            StartCoroutine(WaitToRoam());
        }
        else if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, Enemy.currentTarget.transform.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (Enemy.mode == Enemy.Mode.chasing || Enemy.mode == Enemy.Mode.alerted)
        {
            Enemy.finalObjective = player;
        }
        if (Enemy.mode == Enemy.Mode.chasing)
        {
            Enemy.currentTarget = Enemy.finalObjective; 
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * chaseSpeed * Time.deltaTime;

        rb.velocity = force;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDist)
        {
            currentWaypoint++;
        }
    }

    IEnumerator WaitToRoam()
    {
        chaseSpeed = 0;
        yield return new WaitForSeconds(2);
        Enemy.SetRoamingTarget();
        chaseSpeed = 200;
    }
}
