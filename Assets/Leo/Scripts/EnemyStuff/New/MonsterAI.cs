using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public static float currentSpeed;
    public float chasingSpeed, roamingSpeed;

    public static bool playerSpotted, chasingPlayer;

    public static GameObject currentEnemyRoom;
    public static GameObject currentTarget;
    public static GameObject currentTargetRoom;
    public static GameObject currentTargetDoor;
    public static GameObject currentTargetRoaming;

    private float nextWaypointDist = 3;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    public GameObject player;

    Selector topSelector;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        ConstructBehaviourTree();
        InvokeRepeating("UpdatePath", 0f, 0.1f);
        InvokeRepeating("RunTree", 0f, 0.01f);
    }

    private void ConstructBehaviourTree()
    {
        //Nodes
        IsHidingNode isHidingNode = new IsHidingNode();
        KillPlayer killPlayer = new KillPlayer(player);
        SpottedNode spottedNode = new SpottedNode();
        TargetHidingSpot targetHidingSpot = new TargetHidingSpot(transform);
        TargetPlayer targetPlayer = new TargetPlayer(chasingSpeed, player, transform);
        CheckIfChasingNode checkIfChasingNode = new CheckIfChasingNode();
        CheckIfSameRoomNode checkIfSameRoomNode = new CheckIfSameRoomNode();
        ChooseHidingSpotNode chooseHidingSpotNode = new ChooseHidingSpotNode(currentEnemyRoom, transform);
        FindHidingPlayerNode findHidingPlayerNode = new FindHidingPlayerNode();
        NotSpottedNode notSpottedNode = new NotSpottedNode();
        StopForSecondsNode stopForSecondsNode = new StopForSecondsNode(roamingSpeed);
        TargetBestDoorNode targetBestDoorNode = new TargetBestDoorNode();
        TargetLastSeenDoorNode targetLastSeenDoorNode = new TargetLastSeenDoorNode(transform);
        TargetRoamingPointNode targetRoamingPointNode = new TargetRoamingPointNode(transform);
        TargetRoomNode targetRoomNode = new TargetRoomNode(roamingSpeed);
        KillHidingNode killHidingNode = new KillHidingNode();

        //Right Branch
        Sequence roamSequence = new Sequence(new List<Node> {targetRoomNode, targetBestDoorNode, targetRoamingPointNode, stopForSecondsNode});
        Selector isLastSeenDoorNullSelector = new Selector(new List<Node> {targetLastSeenDoorNode, roamSequence});
        Sequence wasChasingSequence = new Sequence(new List<Node> {checkIfChasingNode, isLastSeenDoorNullSelector});
        Selector wasChasingOrNotSelector = new Selector(new List<Node> {wasChasingSequence, roamSequence});
        Sequence notSpottedSequence = new Sequence(new List<Node> {notSpottedNode, wasChasingOrNotSelector});

        //Left Branch
        Sequence isNotHidingSequence = new Sequence(new List<Node> {targetPlayer, killPlayer});
        Sequence isHidingSequence = new Sequence(new List<Node> {isHidingNode, targetHidingSpot, killHidingNode});
        Selector hidingOrNotSelector = new Selector(new List<Node> {isHidingSequence, isNotHidingSequence});
        Sequence spottedSequence = new Sequence(new List<Node> {spottedNode, hidingOrNotSelector});

        //Top Node
        topSelector = new Selector(new List<Node> {spottedSequence, notSpottedSequence});
    }

    private void Update()
    {
        //Debug.Log(chasingPlayer);
        SetPlayerSpotted();
        SetColors();
    }

    private void RunTree()
    {
        topSelector.Evaluate();
    }

    private void UpdatePath()
    {
        if (currentTarget != null && seeker.IsDone())
        {
            currentSpeed = chasingSpeed;

            if (currentTargetRoaming != null)
            {
                currentTargetRoaming.GetComponent<SpriteRenderer>().color = Color.red;
            }

            seeker.StartPath(rb.position, currentTarget.transform.position, OnPathComplete);
        }
        else if (currentTarget == null)
        {
            currentSpeed = 0;
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

    private void SetColors()
    {
        if (playerSpotted)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (playerSpotted == false && chasingPlayer)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if (chasingPlayer == false)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
