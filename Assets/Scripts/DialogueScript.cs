using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt;
    public Text nme;
    public bool isTalking;
    private GameObject textBox;
    private GameObject dialogue;
    public string[] dial;
    public string nam;
    public int dialNum;
    void Start()
    {
        // Sets the textbox to not appear
        isTalking = false;
        dialogue = GameObject.Find("Dialogue");
        textBox = dialogue.transform.GetChild(0).gameObject;
        textBox.SetActive(false);
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Loop through dialogue array
        if(isTalking == true){
            //Debug.Log(dialNum);
            if(Input.GetButtonDown("Submit") && dialNum < dial.Length){
                //Debug.Log("print out here");
                txt.text = dial[dialNum];
                dialNum++;
            }
            else if(Input.GetButtonDown("Submit") && dialNum == dial.Length){
                EndDialogue();
            }
        }
        //gameObject.SetActive(false);
    }
    public void DisplayDialogue(string[] dialogue, string name){
        //gameObject.SetActive(true);
        dial = dialogue;
        nam = name;
        textBox.SetActive(true);
        //Debug.Log("print out here");
        //Debug.Log(dialogue.Length);
        //if(dialogue.Length > 1){
        isTalking = true;
        //}
        //txt.text = dialogue[0];
        nme.text = name;
        dialNum = 0;
    }
    public void EndDialogue(){
        textBox.SetActive(false);
        nme.text = "";
        txt.text = "";
        isTalking = false;
        //gameObject.SetActive(false);
    }

    //public bool getTalking(){
    //    return isTalking;
    //}
}
