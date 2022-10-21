using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Dialogue : MonoBehaviour
{
    Dialogue di;
    // Start is called before the first frame update
    void Start()
    {
        di = FindObjectOfType<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){ // Dave talks to person
        if(col.name == "Dave"){
            di.DisplayDialogue("Test Man");
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.name == "Dave"){
            di.EndDialogue();
        }
    }
}
