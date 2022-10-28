using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Dialogue : MonoBehaviour
{
    DialogueScript di;
    Movement2DSide ms; // Reference to Dave object
    // Start is called before the first frame update
    void Start()
    {
        di = FindObjectOfType<DialogueScript>();
        ms = FindObjectOfType<Movement2DSide>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){ // Dave talks to person
        if(col.name == "Dave"){
            //ms.enabled = false;
            string[] testDial = {"Testing time", "test again"};
            di.DisplayDialogue(testDial, "Test Man");
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.name == "Dave"){
            di.EndDialogue();
        }
    }
}
