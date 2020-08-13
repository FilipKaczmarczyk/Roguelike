using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenCleared;

    public List<GameObject> enemies = new List<GameObject>();

    public Room room;

    // Start is called before the first frame update
    void Start()
    {
        if (openWhenCleared)
        {
            room.closeWhenEntered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && room.roomActive && openWhenCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                room.OpenDoors();
            }
        }
    }
}
