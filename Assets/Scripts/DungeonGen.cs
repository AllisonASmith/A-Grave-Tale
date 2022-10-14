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
    void Start()
    {
        // generates 10 tiles between ([1-10), [1-10))
        t = GetComponent<Tilemap>();
        for (int i = 0; i < 10; i++)
        {
            generateBox(5, 5, Vector3Int.zero, Vector3Int.right);//t.SetTile(new Vector3Int(Random.Range(1, 10), Random.Range(1, 10), 0), t.GetTile(new Vector3Int(1,0)));//Vector3Int.zero));
        }
    }
    void generateBox(int width, int height, Vector3Int start, Vector3Int door) {
        // creates a room box of size (width, height) with the anchor position (bottom left corner) start and doorway position door
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                // sets grass/path (temp)
                if(i == 0 || j == 0 || i + 1 == width || j + 1 == height) t.SetTile(new Vector3Int(start.x + i, start.y + j), refSet[7]);
                else t.SetTile(new Vector3Int(start.x + i, start.y + j), refSet[0]);
            }
        }
        // sets cobblestone (temp)
        t.SetTile(door, refSet[6]);
    }
}
