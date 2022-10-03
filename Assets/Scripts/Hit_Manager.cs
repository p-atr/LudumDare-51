using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Manager : MonoBehaviour
{
    private int damage;

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hiteroo");
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hiteroo");
            other.gameObject.GetComponent<Enemy_Manager>().Damage(damage);
        }
        GameObject.Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Hiteroo");
        if (collision.gameObject.tag == "Enemy")
        { 
            //Debug.Log("Hiteroo");
            collision.gameObject.GetComponent<Enemy_Manager>().Damage(damage);
        }
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 50 || transform.position.z > 50 || transform.position.z < -50 || transform.position.x < -50)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
