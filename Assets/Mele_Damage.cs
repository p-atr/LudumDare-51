using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele_Damage : MonoBehaviour
{
    [SerializeField]
    private Shoot s;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("UWUWUWUWUW");
        if(s.ShouldDamage(other))
        {
            other.GetComponent<Enemy_Manager>().Damage(100);
        }
    }
}
