using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float currentSpeed;

    public static bool playerSpotted, chasingPlayer;

    public static GameObject currentEnemyRoom;
    public static GameObject currentTarget;
    public static GameObject currentTargetRoom;
    public static GameObject currentTargetDoor;
    public static GameObject currentTargetRoaming;

    private float nextWaypointDist;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    public GameObject player;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        ConstructBehaviourTree();
        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    private void ConstructBehaviourTree()
    {
        IsHidingNode isHidingNode = new IsHidingNode();
        KillPlayer killPlayer = new KillPlayer();
        SpottedNode spottedNode = new SpottedNode();
        TargetHidingSpot targetHidingSpot = new TargetHidingSpot(transform);
        TargetPlayer targetPlayer = new TargetPlayer(player, transform);
        CheckIfChasingNode checkIfChasingNode = new CheckIfChasingNode();
        CheckIfSameRoomNode checkIfSameRoomNode = new CheckIfSameRoomNode();
        ChooseHidingSpotNode chooseHidingSpotNode = new ChooseHidingSpotNode(currentEnemyRoom, transform);
        FindHidingPlayerNode findHidingPlayerNode = new FindHidingPlayerNode();
        NotSpottedNode notSpottedNode = new NotSpottedNode();
        StopForSecondsNode stopForSecondsNode = new StopForSecondsNode(2);
        TargetBestDoorNode targetBestDoorNode = new TargetBestDoorNode();
        TargetLastSeenDoorNode targetLastSeenDoorNode = new TargetLastSeenDoorNode(transform);
        TargetRoamingPointNode targetRoamingPointNode = new TargetRoamingPointNode(transform);
        TargetRoomNode targetRoomNode = new TargetRoomNode();
        KillHidingNode killHidingNode = new KillHidingNode();

        Sequence roamSequence = new Sequence(new List<Node> {targetRoomNode, targetBestDoorNode, targetRoamingPointNode, stopForSecondsNode});
        //Sequence isPlayerInHidingSpotSequence = new Sequence(new List<Node> {})
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.transform.position, OnPathComplete);
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
        Vector2 force = direction * currentSpeed * Time.deltaTime;

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
