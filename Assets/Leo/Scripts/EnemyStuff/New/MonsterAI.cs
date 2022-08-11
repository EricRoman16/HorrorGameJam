using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private float currentSpeed;

    public static bool playerSpotted, chasingPlayer;

    public static GameObject currentEnemyRoom;
    public static GameObject currentTarget;
    public static GameObject currentTargetRoom;
    public static GameObject currentTargetDoor;
    public static GameObject currentTargetRoaming;

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

        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, currentTarget.transform.position, OnPathComplete);
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
        Vector2 force = direction * Enemy.speed * Time.deltaTime;

        rb.velocity = force;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDist)
        {
            currentWaypoint++;
        }
    }

    private void Update()
    {
        SetPlayerSpotted();
    }

    private void SetPlayerSpotted()
    {
        if (currentEnemyRoom == Player.currentPlayerRoom && Player.playerHidden == false)
        {
            playerSpotted = true;
        }
        else
        {
            playerSpotted = false;
        }
    }
}
