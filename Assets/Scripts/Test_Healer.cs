using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Healer : MonoBehaviour
{
    DialogueScript di;
    Movement2DSide ms; // Reference to Dave object
    DaveStats ds; // Reference to DaveStats script
    bool canTalk; // Stores if dave can talk to the NPC
    // Start is called before the first frame update
    void Start()
    {
        di = FindObjectOfType<DialogueScript>();
        ms = FindObjectOfType<Movement2DSide>();
        ds = FindObjectOfType<DaveStats>();
        canTalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canTalk == true){
            if(Input.GetButtonDown("Submit")){
                //ms.enabled = false;
                ds.DaveHeal();
                string[] testDial = {"Healing time", "come again"};
                di.DisplayDialogue(testDial, "Test Healer");
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
