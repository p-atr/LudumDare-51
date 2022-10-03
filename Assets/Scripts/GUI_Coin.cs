using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Coin : MonoBehaviour
{
    // private int coin = 0;
    private int i=0;
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    public void Show(int coin){
        foreach (Transform child in transform) {
            if (i<coin) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
            i++;
        }
        i=0;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
