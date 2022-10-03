using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Phasehandler : MonoBehaviour
{
    public void PhaseChange()
    {
        // //get parent gamepbject
        // GameObject parent = this.transform.parent.gameObject;

        // //get name of gameobject parent
        // string name = parent.name;

        // Debug.Log(transform.parent.name);
        // Debug.Log("PhaseChange");
        if (transform.parent.gameObject.name == "Item Container"){
            Destroy(this.gameObject);
        }
    }
}
