using System.Collections;
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
        Enemy.currentFinalTarget = temp;
        Enemy.currentTarget = temp;

        InvokeRepeating("UpdatePath", 0f, 0.2f);
    }


    private void UpdatePath()
    {
        if (seeker.IsDone() && Enemy.state == Enemy.State.alerted || Enemy.state == Enemy.State.chasing)
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
        if (Enemy.state == Enemy.State.chasing)
        {
            Enemy.currentTarget = player; 
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
}
