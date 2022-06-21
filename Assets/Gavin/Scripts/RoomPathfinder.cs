using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomPathfinder : MonoBehaviour
{
    public static RoomPathfinder currentPathfinder;
    public List<Node> nodeList;
    List<Transform> path;
    public int nodeI = 0;
    public class Node
    {
        public List<Node> connectedNodes;
        public Transform transform;
        public int dist = 100000;
        public bool visited = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentPathfinder = this;
        nodeList = CreateLinkedList(GameObject.FindGameObjectsWithTag("Door").ToList().ConvertAll<Transform>(x => x.transform));
        path = GetPath(nodeList[1], nodeList[15]);

        Debug.Log(path);

        //Testing
        nodeList[nodeI].transform.GetComponent<SpriteRenderer>().color = Color.blue;
        foreach (Node node in nodeList[nodeI].connectedNodes)
        {
            //node.transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    List<Transform> GetPath(Node startNode, Node endNode)
    {
        List<Node> nodePath = new List<Node>();
        Queue<Node> queue = new Queue<Node>();

        if(startNode.connectedNodes.Count == 0 || startNode.connectedNodes.Count == 0)
        {
            return null;
        }

        startNode.dist = 0;
        startNode.visited = true;
        queue.Enqueue(startNode);
        int c1 = 0;
        int c2 = 0;
        while(queue.Count > 0 && c1 <= 1000)
        {
            c1++;
            Node node = queue.Dequeue();

            if(node == endNode)
            {
                Node node1 = node;
                while(node1.dist != 0 && c2 <= 1000)
                {
                    c2++;
                    nodePath.Add(node1);

                    //Find node with lowest distance
                    Node temp = node1;
                    foreach (Node node2 in node1.connectedNodes)
                    {
                        if(temp.dist > node2.dist)
                        {
                            temp = node2;
                        }
                    }
                    node1 = temp;
                }
                nodePath.Add(node1);
                break;
            }


            foreach (Node n in node.connectedNodes)
            {
                if (n.visited)
                {
                    continue;
                }

                n.dist = n.dist < node.dist + 1 ? n.dist : node.dist + 1;
                n.visited = true;
                queue.Enqueue(n);
            }


        }

        //Reseting nodes
        foreach (Node node3 in nodeList)
        {
            node3.dist = 1000;
            node3.visited = false;
        }

        Debug.Log("c1: " + c1 + "\nc2: " + c2);

        nodePath.Reverse();
        return nodePath.ConvertAll<Transform>(x => x.transform);
    }

    List<Node> CreateLinkedList(List<Transform> doors)
    {
        //Sorting doors by the rooms
        Dictionary<string, List<Transform>> rooms = new Dictionary<string, List<Transform>>();

        foreach (Transform door in doors)
        {
            DoorCollision doorCollision = door.GetComponent<DoorCollision>();
            if (!rooms.ContainsKey(doorCollision.room.name))
            {
                rooms.Add(doorCollision.room.name, new List<Transform> { door });
            }
            else
            {
                rooms[doorCollision.room.name].Add(door);
            }
        }

        List<Node> nodes = doors.ConvertAll<Node>(delegate(Transform t)
        {
            Node node = new Node();
            node.connectedNodes = new List<Node>();
            node.transform = t;
            return node;
        });

        foreach (Node node in nodes)
        {
            //Add sibling door to list
            node.connectedNodes.Add(nodes.Find(x => x.transform == node.transform.parent.GetChild(1 - node.transform.GetSiblingIndex())));

            //add all doors in same room to list
            List<Transform> doorsInRoom = rooms[node.transform.GetComponent<DoorCollision>().room.name].ToArray().ToList();
            doorsInRoom.Remove(node.transform);
            node.connectedNodes.AddRange(doorsInRoom.ConvertAll(delegate(Transform t)
            {
                return nodes.Find(x => x.transform == t);
            }));

        }

        return nodes;
    }

    List<Transform> GetPath(Transform startDoor, Transform endDoor)
    {
        return GetPath(nodeList.Find(x => x.transform == startDoor), nodeList.Find(x => x.transform == endDoor));
    }

    public void UpdatePath(Transform start, Transform end)
    {
        path = GetPath(nodeList[0], nodeList.Find(x => x.transform == end));
        Debug.Log(path.Count + " : " + nodeList[0].connectedNodes.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(Mathf.RoundToInt(Time.time * 3 % (path.Count-1)));
        transform.position = path[Mathf.RoundToInt(Time.time * 3 % (path.Count-1))].position;
    }
}
