using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
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
        StartCoroutine(WaitToStart());
    }


    private void UpdatePath()
    {
        if (reachedEndOfPath && Enemy.state == Enemy.State.foundTarget)
        {
            Enemy.state = Enemy.State.headingToTarget;
            StartCoroutine(WaitToRoam());
        }
        else if (seeker.IsDone() && Enemy.state != Enemy.State.idle)
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
        if (Enemy.currentRoom == TempMove.currentPlayerRoom)
        {
            Enemy.ChaseTarget();
        }
        else if (Enemy.mode == Enemy.Mode.chasing && Enemy.currentRoom != TempMove.currentPlayerRoom)
        {
            if (Enemy.sight == Enemy.DirectSight.hadSight)
            {
                Debug.Log("has direct sight");
                Enemy.currentTarget = DoorScript.lastSeenDoor;
            }
            else
            {
                //Debug.Log("back to roaming");
                Enemy.currentTarget = Enemy.currentRoom.GetComponent<RoomScript>().roamingTarget;
                StartCoroutine(WaitToRoam());
            }
        }

        if (Enemy.mode == Enemy.Mode.chasing && Enemy.currentRoom == TempMove.currentPlayerRoom)
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
        Vector2 force = direction * Enemy.speed * Time.deltaTime;

        rb.velocity = force;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDist)
        {
            currentWaypoint++;
        }
    }

    /// <summary>
    /// Pauses for a moment after reaching a roaming point before roaming again
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitToRoam()
    {
        while (RoamingTarget.reachedRoamingTarget == false)
        {
            //Debug.Log("waiting");
            yield return null;
        }
        //Debug.Log("done waiting");
        Enemy.speed = 0;
        for (float timer = 2; timer >= 0; timer -= Time.deltaTime)
        {
            if (Enemy.mode == Enemy.Mode.chasing)
            {
                Enemy.speed = Enemy.roamSpeed;
                yield break;
            }
            yield return null;
        }
        Enemy.SetRoamingTarget();
        Enemy.speed = Enemy.roamSpeed;
    }

    /// <summary>
    /// Pauses at the start before starting to roam
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitToStart()
    {
        //Debug.Log("waiting to start");
        Enemy.speed = 0;
        yield return new WaitForSeconds(2);
        Enemy.SetRoamingTarget();
        Enemy.speed = Enemy.roamSpeed;
    }
}
