using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;

    public Color startColor, endColor;

    public int howManyRooms;

    public Transform genertorPoint;

    public enum Direction { up, right, down, left};
    public Direction selectedDirection;

    public float xOffset = 18;
    public float yOffset = 10;

    public LayerMask whereIsRoom;

    private GameObject endRoom;

    private List<GameObject> layoutRoomObjects = new List<GameObject>();

    public RoomPrefabs rooms;

    private List<GameObject> generatedOutlines = new List<GameObject>();

    public RoomCenter centerStart, centerEnd;
    public RoomCenter[] centersDefault;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, genertorPoint.position, genertorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;

        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();

        for(int i = 0; i < howManyRooms; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, genertorPoint.position, genertorPoint.rotation);

            layoutRoomObjects.Add(newRoom);

            if(i + 1 == howManyRooms)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                endRoom = newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(genertorPoint.position, .2f, whereIsRoom))
            {
                MoveGenerationPoint();
            }

        }

        // create room outline
        CreateRoomOutline(Vector3.zero);

        foreach(GameObject room in layoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }

        CreateRoomOutline(endRoom.transform.position);

        foreach(GameObject outline in generatedOutlines)
        {
            if(outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, new Vector3(outline.transform.position.x, outline.transform.position.y - 2, 0f), transform.rotation).room = outline.GetComponent<Room>();
            }
            else if(outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, new Vector3(outline.transform.position.x, outline.transform.position.y - 2, 0f), transform.rotation).room = outline.GetComponent<Room>();
            }
            else
            {
                int centerRand = Random.Range(0, centersDefault.Length);

                Instantiate(centersDefault[centerRand], new Vector3(outline.transform.position.x, outline.transform.position.y - 2, 0f), transform.rotation).room = outline.GetComponent<Room>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                genertorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.right:
                genertorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.down:
                genertorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.left:
                genertorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
        }
    }
    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, whereIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, whereIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, whereIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, whereIsRoom);

        int directionCount = 0;

        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }

        switch (directionCount)
        {
            case 0:
                Debug.LogError("Room has not conection!");
                break;

            case 1:
                if (roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                }
                if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                }
                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                }
                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                }
                break;

            case 2:
                if (roomAbove && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                }
                if (roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleRightLeft, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                }
                if (roomAbove && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpLeft, roomPosition, transform.rotation));
                }
                if (roomBelow && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleDownRight, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
                }
                break;

            case 3:
                if (roomAbove && roomBelow && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpDownRight, roomPosition, transform.rotation));
                }
                if (roomBelow && roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleDownRightLeft, roomPosition, transform.rotation));
                }
                if (roomAbove && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpDownLeft, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRightLeft, roomPosition, transform.rotation));
                }
                break;

            case 4:
                generatedOutlines.Add(Instantiate(rooms.quadUpRightRightLeft, roomPosition, transform.rotation));
                break;
        }
    }
}


[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
        doubleUpDown, doubleRightLeft, doubleUpRight, doubleUpLeft,
        doubleDownRight, doubleDownLeft, tripleUpDownRight, tripleDownRightLeft,
        tripleUpDownLeft, tripleUpRightLeft, quadUpRightRightLeft;
}