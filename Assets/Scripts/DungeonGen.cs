using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGen : MonoBehaviour
{
    // Start is called before the first frame update
    Tilemap t;
    [SerializeField]
    public TileBase[] refSet;
    public GameObject temp; // prefab for room positions
    public List<Vector3Int> doors = new List<Vector3Int>(); // list of positions where other rooms can be generated
    public List<Vector2Int> directions = new List<Vector2Int>(); // what direction each door faces
    public int dungeonSize; // total rooms per dungeon
    void Start()
    {

        // need to add way to track player - probably new script
        // CameraControls.target = temp;

        // test components. generates a box and random branch with walls
        t = GetComponent<Tilemap>();
        Vector3Int position = Vector3Int.zero;
        generateBox(10, 10, position, 6, 7, 0, new Vector2Int(1, 1));
        while (doors.Count > 0) {
            position = doors[0];//new Vector3Int((int)(doors[0].x), (int)(doors[0].y - directions[0].y)); 
            Debug.Log("NEW ROOM: " + position + " door: " + doors[0] + " dir: " + directions[0] + " count: " + doors.Count);
            generateBox(5, 5, position, 6, 7, 0, directions[0]);
            //generateBlob(30, new Vector3Int(position.x, position.y), 0, 7);
            doors.RemoveAt(0);
            directions.RemoveAt(0);
        }
        //position = generateBlob(30, new Vector3Int(position.x - 1, position.y - 1), 0, 7);
    }
    void generateBox(int width, int height, Vector3Int start, int door, int wall, int floor, Vector2Int direction){
        // creates a room box of size (width, height) (towards the direction) with the anchor position (bottom left corner) start and doorway position door
        // door, wall, and floor are indicators for tiles

        // get wall directions
        int imod = direction.x;
        int jmod = direction.y;
        if (imod == 0) {
            start.x--;
            imod = 1;
        }
        else if (jmod == 0) {
            start.y--;
            jmod = 1;
        }
        //check if the box has the space to generate
        if (t.GetTile(new Vector3Int(start.x + width * imod - 1, start.y + height * jmod - 1)) == refSet[floor]) return;
        if (t.GetTile(new Vector3Int(start.x, start.y)) == refSet[floor]) return;
        if (t.GetTile(new Vector3Int(start.x, start.y + height * jmod - 1)) == refSet[floor]) return;
        if (t.GetTile(new Vector3Int(start.x + width * imod - 1, start.y)) == refSet[floor]) return;
        // generate square
        for (int i = 0; i != width*imod; i += imod) {
            for (int j = 0; j != height*jmod; j += jmod) {
                // sets floor and walls
                Vector3Int tile = new Vector3Int(start.x + i, start.y + j);
                if (t.GetTile(tile) == refSet[door]) continue;
                else if (i == 0 || j == 0 || i + imod == width*imod || j + jmod == height*jmod) t.SetTile(tile, refSet[wall]);
                else t.SetTile(tile, refSet[floor]);
            }
        }
        //fix anchor
        start.x = imod == -1 ? start.x - width + 1 : start.x;
        start.y = jmod == -1 ? start.y - height + 1 : start.y;
        // sets door
        int numDoors = Random.Range(1, 5);
        Debug.Log(numDoors);
        // left door
        if (numDoors > 0 && dungeonSize >= 0 && direction != new Vector2(1, 0)) {
            // places a new door in a random place along the wall
            doors.Add(new Vector3Int(start.x, start.y + Random.Range(1, height*jmod - 1 - start.y)));
            Debug.Log("first door " + doors[0] + " from start " + start + " with mods " + height + ", " + width + ": " + imod + ", " + jmod);
            directions.Add(new Vector2Int(-1, 0));
            t.SetTile(doors[0], refSet[door]);
            dungeonSize--;
        }
        // top door
        if(numDoors > 1 && dungeonSize >= 0 && direction != new Vector2(0, -1)) {
            doors.Add(new Vector3Int(start.x + Random.Range(1, width*imod - 1 - start.x), start.y + height*jmod - 1));
            Debug.Log("second door " + doors[1] + " from start " + start + " with mods " + height + ", " + width + ": " + imod + ", " + jmod);
            t.SetTile(doors[1], refSet[door]);
            directions.Add(new Vector2Int(0, 1));
            dungeonSize--;
        }
        // right door
        if (numDoors > 2 && dungeonSize >= 0 && direction != new Vector2(-1, 0)) {
            doors.Add(new Vector3Int(start.x + width*imod - 1, start.y + Random.Range(1, height*jmod - 1 - start.y)));
            Debug.Log("third door " + doors[2] + " from start " + start + " with mods " + height + ", " + width + ": " + imod + ", " + jmod);
            t.SetTile(doors[2], refSet[door]);
            directions.Add(new Vector2Int(1, 0));
            dungeonSize--;
        }
        // bottom door
        if (numDoors > 3 && dungeonSize >= 0 && direction != new Vector2(0, 1)) {
            doors.Add(new Vector3Int(start.x + Random.Range(1, width*imod - 1 - start.x), start.y));
            t.SetTile(doors[3], refSet[door]);
            directions.Add(new Vector2Int(0, -1));
            dungeonSize--;
        }
    }
    Vector3Int generateBlob(int distance, Vector3Int start, int type1, int type2) {
        // creates a random blob/branch
        // distance = how many tiles generate, start = starting position, type1 = material generated on the floor, type2 = material generated as walls
        Vector3Int position = start;
        int fails = 0; // backup if a different route can't be found
        
        while (distance > 0) {
            int x = 0;
            int y = 0;
            if (Random.Range(0, 2) == 1) x = Random.Range(-1, 2);
            else y = Random.Range(-1, 2);
            // generate tile, currently overrides other floor tiles and anything adjacent to this. EDIT LATER
            if (t.GetTile(new Vector3Int(position.x + x, position.y + y)) != refSet[type1])
            {
                position = new Vector3Int(position.x + x, position.y + y);
                t.SetTile(position, refSet[type1]);
                // add walls. this is a bit... much, so edit and optimize if desired
                if (t.GetTile(new Vector3Int(position.x + 1, position.y)) == null) t.SetTile(new Vector3Int(position.x + 1, position.y), refSet[type2]);
                if (t.GetTile(new Vector3Int(position.x, position.y + 1)) == null) t.SetTile(new Vector3Int(position.x, position.y + 1), refSet[type2]);
                if (t.GetTile(new Vector3Int(position.x + 1, position.y + 1)) == null) t.SetTile(new Vector3Int(position.x + 1, position.y + 1), refSet[type2]);
                if (t.GetTile(new Vector3Int(position.x - 1, position.y)) == null) t.SetTile(new Vector3Int(position.x - 1, position.y), refSet[type2]);
                if (t.GetTile(new Vector3Int(position.x, position.y - 1)) == null) t.SetTile(new Vector3Int(position.x, position.y - 1), refSet[type2]);
                if (t.GetTile(new Vector3Int(position.x - 1, position.y - 1)) == null) t.SetTile(new Vector3Int(position.x - 1, position.y - 1), refSet[type2]);
                if (t.GetTile(new Vector3Int(position.x + 1, position.y - 1)) == null) t.SetTile(new Vector3Int(position.x + 1, position.y - 1), refSet[type2]);
                if (t.GetTile(new Vector3Int(position.x - 1, position.y + 1)) == null) t.SetTile(new Vector3Int(position.x - 1, position.y + 1), refSet[type2]);
                distance--;
                fails = 0;
            }
            else fails++;
            // if enough fails on the current path, reset to a random path
            if (fails == 10)
            {
                x = Random.Range(start.x, position.x);
                y = Random.Range(start.y, position.y);
            }
            //too many fails, end generation
            else if (fails >= 100) break;
        }
        return position;
    }
}
