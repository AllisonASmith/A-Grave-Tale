using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Bar : MonoBehaviour
{
    public Image hpBar;
    public GameObject dave;
    private DaveStats DS;
    //int hp;
    
    // Start is called before the first frame update
    void Start()
    {
        DS = dave.GetComponent<DaveStats>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = (float)(DS.daveHealth) / (float)(DS.daveMaxHealth); // Fills the health bar based on dave's health
    }
}
