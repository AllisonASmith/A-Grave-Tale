using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt;
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.SetActive(false);
    }
    public void DisplayDialogue(string dialogue){
        gameObject.SetActive(true);
        txt.text = dialogue;
    }
    public void EndDialogue(){
        gameObject.SetActive(false);
    }
}
