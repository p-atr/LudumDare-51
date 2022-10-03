using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Parent : MonoBehaviour
{
    public void PhaseChange()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Item_Phasehandler>().PhaseChange();
        }
    }
}
