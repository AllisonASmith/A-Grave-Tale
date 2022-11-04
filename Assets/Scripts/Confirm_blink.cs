using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirm_blink : MonoBehaviour
{
    private GameObject DialConf;
    public GameObject txtBox;
    // Start is called before the first frame update
    public float interval;

    void Start()
    {
        DialConf = GameObject.Find("Dialogue Confirm");
        txtBox = GameObject.Find("TextBox");
        interval = 1;
        InvokeRepeating("FlashLabel", 0, interval);
    }

    void FlashLabel()
    {
        if(txtBox.activeSelf){
            if(DialConf.activeSelf)
                DialConf.SetActive(false);
            else
                DialConf.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
