using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject target; // the focus for the camera
    public bool isSmoothed; // true if the camera isn't directly following a player or enemy -- works best on a static target
 
    void Update()
    {
        // if smooth, camera will lerp to target position
        // otherwise camera snaps to target
        transform.position = isSmoothed ? Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, -10), .025f) : new Vector3(target.transform.position.x, target.transform.position.y, -10);
    }
}
