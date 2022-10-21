using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt;
    public Text nme;
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.SetActive(false);
    }
    public void DisplayDialogue(string dialogue, string name){
        gameObject.SetActive(true);
        txt.text = dialogue;
        nme.text = name;
    }
    public void EndDialogue(){
        gameObject.SetActive(false);
    }
}
