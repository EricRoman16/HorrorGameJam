using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform currentTarget;
    public Transform targetplayer;
    private Transform targetLocation;

    public float chaseSpeed = 200f;
    public float roamSpeed = 50f;
    public float nextWaypointDist = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;


    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        SetTarget();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void SetRoamingTarget()
    {
        //RoomPathfinder.currentPathfinder.GetPath();
    }

    private void SomeMethod()
    {
        //Dictionary<string, List<Transform>> rooms = new Dictionary<string, List<Transform>>();

        //foreach (Transform door in doors)
        //{
        //    DoorCollision doorCollision = door.GetComponent<DoorCollision>();
        //    if (!rooms.ContainsKey(doorCollision.room.name))
        //    {
        //        rooms.Add(doorCollision.room.name, new List<Transform> { door });
        //    }
        //    else
        //    {
        //        rooms[doorCollision.room.name].Add(door);
        //    }
        //}
    }

    private void SetAlertedTarget()
    {

    }

    private void SetTarget()
    {
        if (Enemy.state == Enemy.State.chasing)
        {
            currentTarget = targetplayer;
        }
        else
        {
            currentTarget = targetLocation;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
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
        SetTarget();
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
