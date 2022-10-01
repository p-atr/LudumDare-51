using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject shot;
    [SerializeField]
    private Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject piu = GameObject.Instantiate(shot,spawnpoint.position, transform.rotation);
            piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 12;
        }
    }
}
