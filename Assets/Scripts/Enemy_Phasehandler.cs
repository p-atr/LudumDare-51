using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Phasehandler : MonoBehaviour
{
    public void PhaseChange()
    {
        //GameObject point = transform.GetChild(0).GetComponent<Enemy_Behaviour>().Point;
        //if(point != null)
        //{
        //    point.GetComponent<Highlight>().ResetTile();
        //}
        GameObject.Destroy(this.gameObject);
    }
}
