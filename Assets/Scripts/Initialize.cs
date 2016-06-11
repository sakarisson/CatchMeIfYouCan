﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Initialize : NetworkBehaviour
{
    public int mapSize = 8;
    public GameObject player;
    private Statistics statistics;

<<<<<<< HEAD
    public GameObject side;
=======
	[SyncVar]
	private int seed;

    public GameObject door;
    public GameObject wall;
>>>>>>> Network

    void Awake()
    {
		if (isServer) {
			seed = Random.Range (0, 1000000);
			Debug.Log ("Server seed, " + seed);
		} else {
			Debug.Log (seed);
		}
		Random.seed = seed;
		statistics = GetComponent<Statistics>();
        statistics.SetSize(mapSize);
        int[] playerSpawnPosition = new int[] { Random.Range(0, mapSize), Random.Range(0, mapSize) };
        GameObject parent = new GameObject("rooms");

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                GameObject newRoom = new GameObject("room " + i + ", " + j);
                newRoom.AddComponent<Room>();
                Room room = newRoom.GetComponent<Room>();
                room.GenerateRoom(i, j);
                room.SetSide(side);
                room.BuildRoom();
                room.DrawSides();

                newRoom.transform.parent = parent.transform;

                statistics.AddRoom(room, i, j);

                if (playerSpawnPosition[0] == i && playerSpawnPosition[1] == j)
                {
                    GameObject playerSpawned = Instantiate(player, room.GetPosition(), Quaternion.identity) as GameObject;

                    playerSpawned.name = "player";
                }
            }
        }

        FixRooms();
    }

    void FixRooms()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                Room thisRoom = statistics.GetRoom(x, y);
                
                if(x == 0)
                {
                    thisRoom.sides[3] = Room.Side.wall;
                }
                if(x == mapSize - 1)
                {
                    thisRoom.sides[1] = Room.Side.wall;
                }
                if (y == 0)
                {
                    thisRoom.sides[2] = Room.Side.wall;
                }
                if (y == mapSize - 1)
                {
                    thisRoom.sides[0] = Room.Side.wall;
                }

                Room topNeighbor = thisRoom.GetNeighbors()[1];
                Room rightNeighbor = thisRoom.GetNeighbors()[0];
                Room bottomNeighbor = thisRoom.GetNeighbors()[2];
                Room leftNeighbor = thisRoom.GetNeighbors()[3];

<<<<<<< HEAD
                if (topNeighbor != null)
                    thisRoom.sides[0] = topNeighbor.sides[2];

                if (rightNeighbor != null)
                    thisRoom.sides[1] = rightNeighbor.sides[3];

                thisRoom.DrawSides();
=======
                statistics.AddRoom(newRoom, i, j);
>>>>>>> Network
            }
        }
    }

	void Update() {

	}

	public override void OnStartServer() {
		Debug.LogError ("Server start!");
	}
	public override void OnStartClient() {
		Debug.LogError ("Client start!");
	}
	public override void OnStartLocalPlayer() {
		Debug.LogError ("LocalPlayer start!");
	}
}