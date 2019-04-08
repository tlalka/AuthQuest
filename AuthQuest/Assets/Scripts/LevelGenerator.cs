using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public enum TileType
    {
        Wall, Floor,
    }
    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject boardHolder;
    public Tilemap colorTiles;

    public int columns;
    public int rows;
    public IntRange numRooms;
    public IntRange roomWidth;
    public IntRange roomHeight;
    public IntRange corridorLength;
    public TileBase floorTile;   // make an array if you want a few types                       
    public TileBase wallTile;    // make an array if you want a few types                        
    //public GameObject[] outerWallTiles;

    void Awake()
    {
        boardHolder = new GameObject("BoardHolder");
        colorTiles = GameObject.FindWithTag("Tiles").GetComponent<Tilemap>();
        columns = 100;
        rows = 100;
        SetupTilesArray();
        numRooms = new IntRange(10, 25);
        roomWidth = new IntRange(6, 17);
        roomHeight = new IntRange(6, 17);
        corridorLength = new IntRange(5, 15);
    }

    // Update is called once per frame
    public void BuildFloor()
    {
        CreateRoomsAndCorridors();
        int tilescovered = SetTilesValuesForRooms();
        Debug.Log(tilescovered + " / " + columns * rows + " = " + (tilescovered / (columns * rows)));
        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();
        InstantiateTiles();
    }

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors()
    {
        // Create the rooms array with a random size.
        rooms = new Room[numRooms.Random];

        // There should be one less corridor than there is rooms.
        corridors = new Corridor[rooms.Length - 1];

        // Create the first room and corridor.
        rooms[0] = new Room();
        corridors[0] = new Corridor();

        // Setup the first room, there is no previous corridor so we do not use one.
        rooms[0].SetupRoom(roomWidth, roomHeight, columns - 1, rows - 3);

        // Setup the first corridor using the first room.
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns - 1, rows - 3, true);

        for (int i = 1; i < rooms.Length; i++)
        {
            // Create a room.
            rooms[i] = new Room();

            // Setup the room based on the previous corridor.
            rooms[i].SetupRoom(roomWidth, roomHeight, columns - 1, rows - 3, corridors[i - 1]);

            // If we haven't reached the end of the corridors array...
            if (i < corridors.Length)
            {
                // ... create a corridor.
                corridors[i] = new Corridor();

                // Setup the corridor based on the room that was just created.
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns - 1, rows - 3, false);
            }

            //if (i == rooms.Length * .5f)
            //{
            //Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
            //Instantiate(player, playerPos, Quaternion.identity);
            //}
        }

    }

    int SetTilesValuesForRooms()
    {
        int newtilescovered = 0;
        // Go through all the rooms...
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            // ... and for each room go through it's width.
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                // For each horizontal tile, go up vertically through the room's height.
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    if (tiles[xCoord][yCoord] != TileType.Floor)
                    {
                        newtilescovered++;
                    }

                    // The coordinates in the jagged array are based on the room's position and it's width and height.
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
        return newtilescovered;
    }

    void SetTilesValuesForCorridors()
    {
        // Go through every corridor...
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            // and go through it's length.
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.
                bool verticalCorridor = false;
                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        verticalCorridor = true;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        verticalCorridor = true;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                // Set the tile at these coordinates to Floor.
                tiles[xCoord][yCoord] = TileType.Floor;
                if (verticalCorridor)//set vertical corridor to be 2 blocks wide
                {
                    tiles[xCoord + 1][yCoord] = TileType.Floor;
                }
            }
        }
    }

    void InstantiateTiles()
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                //Debug.Log(j);
                // If the tile type is Wall...
                if (tiles[i][j] == TileType.Wall)
                {
                    // ... instantiate a wall over the top.
                    InstantiateFromArray(wallTile, i, j);
                }
                else
                {
                    InstantiateFromArray(floorTile, i, j);
                }
            }
        }
    }

    void InstantiateFromArray(TileBase tile, int xCoord, int yCoord)
    {
        // used only if you have a few types of each tile
        //int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3Int position = new Vector3Int(xCoord, yCoord, 0);

        // Create an instance of the prefab from the random index of the array.
        //GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;
        // Set the tile's parent to the board holder.
        //tileInstance.transform.parent = boardHolder.transform;
        colorTiles.SetTile(position, tile);
    }


}
