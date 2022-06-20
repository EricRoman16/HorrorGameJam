using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomPathfinder : MonoBehaviour
{
    public List<Node> nodeList;
    public int nodeI = 0;
    public class Node
    {
        public List<Node> connectedNodes;
        public Transform transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        nodeList = CreateLinkedList(GameObject.FindGameObjectsWithTag("Door").ToList().ConvertAll<Transform>(x => x.transform));


        nodeList[nodeI].transform.GetComponent<SpriteRenderer>().color = Color.blue;
        foreach (Node node in nodeList[nodeI].connectedNodes)
        {
            node.transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
