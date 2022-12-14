using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Dialogue : MonoBehaviour
{
    DialogueScript di;
    Movement2DSide ms; // Reference to Dave object
    bool canTalk; // Stores if dave can talk to the NPC
    // Start is called before the first frame update
    void Start()
    {
        di = FindObjectOfType<DialogueScript>();
        ms = FindObjectOfType<Movement2DSide>();
        canTalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canTalk == true){
            if(Input.GetButtonDown("Submit")){
                string[] testDial = {"Testing time", "test again"};
                di.DisplayDialogue(testDial, "Test Man");
                canTalk = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col){ // Dave talks to person
        if(col.name == "Dave"){
            canTalk = true;
            //ms.enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.name == "Dave"){
            canTalk = false;
        }
    }
}
