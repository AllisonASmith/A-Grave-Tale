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
            if(Input.GetKeyDown("space") && dialNum < dial.Length){
                txt.text = dial[dialNum];
                dialNum++;
            }
            else if(Input.GetKeyDown("space") && dialNum == dial.Length){
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
        Debug.Log("print out here");
        Debug.Log(dialogue.Length);
        if(dialogue.Length > 1){
            isTalking = true;
        }
        txt.text = dialogue[0];
        nme.text = name;
        dialNum = 1;
    }
    public void EndDialogue(){
        isTalking = false;
        textBox.SetActive(false);
        nme.text = "";
        txt.text = "";
        //gameObject.SetActive(false);
    }

    //public bool getTalking(){
    //    return isTalking;
    //}
}
