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
    void Start()
    {

        // need to add way to track player - probably new script
        // CameraControls.target = temp;

        // test components. generates a box and random branch with walls
        t = GetComponent<Tilemap>();
        Vector3Int[] positions = new Vector3Int[3];
        Vector3Int position = Vector3Int.zero;
        positions = generateBox(10, 10, position);
        for (int i = 0; i < positions.Length; i++) {
            if (positions[i] != null) {
                position = positions[i];
                //position = 
                generateBlob(30, new Vector3Int(position.x, position.y), 0, 7);
            }
        }
        //position = generateBlob(30, new Vector3Int(position.x - 1, position.y - 1), 0, 7);
    }
    Vector3Int[] generateBox(int width, int height, Vector3Int start){
        // creates a room box of size (width, height) with the anchor position (bottom left corner) start and doorway position door
        // returns height/width corner
        Vector3Int[] doors = new Vector3Int[3];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                // sets grass/path (temp)
                Vector3Int tile = new Vector3Int(start.x + i, start.y + j);
                if (i == 0 || j == 0 || i + 1 == width || j + 1 == height) t.SetTile(tile, refSet[7]);
                else t.SetTile(tile, refSet[0]);
            }
        }
        // sets door (temp sand)
        // left door
        if (Random.Range(0, 10) % 2 == 0) {
            doors[0] = new Vector3Int(start.x, start.y + Random.Range(1, height - 1));
            t.SetTile(doors[0], refSet[6]);
        }       
        // top door
        if(Random.Range(0, 10) % 2 == 0) {
            doors[1] = new Vector3Int(start.x + Random.Range(1, width - 1), start.y + height - 1);
            t.SetTile(doors[1], refSet[6]);
        }
        // right door
        if (Random.Range(0, 10) % 2 == 0) {
            doors[2] = new Vector3Int(start.x + width - 1, start.y + Random.Range(1, height - 1));
            t.SetTile(doors[2], refSet[6]);
        }   
        return doors;
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
