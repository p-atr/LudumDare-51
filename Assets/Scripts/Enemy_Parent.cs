using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Parent : MonoBehaviour
{

    public List<Vector3> itemSpawnPoints;
    public void PhaseChange()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Enemy_Phasehandler>().PhaseChange();
        }
    }

    public void monsterDrop(Vector3 pos)
    {
        itemSpawnPoints.Add(pos);
    }

}
