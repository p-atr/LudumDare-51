using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIt_Manager_Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("HIT");
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Hiteroo");
            //Debug.Log("Hiteroo");
            collision.gameObject.GetComponent<Player_Manager>().Dmage(1);
        }
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 50 || transform.position.z > 50 || transform.position.z < -50 || transform.position.x < -50)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
