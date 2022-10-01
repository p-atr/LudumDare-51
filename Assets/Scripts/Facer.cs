using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facer : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).direction * 10, Color.yellow);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(transform.position, GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).direction, out hit))
        {
            //Debug.Log("HITEROO");

            player.transform.LookAt(new Vector3(hit.point.x, player.position.y, hit.point.z));
        }
    }
}
