using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{

    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    [SerializeField]
    AudioSource dam;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void Damage(int dmg)
    {
        dam.Play();
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Debug.Log("He Dead");
            transform.parent.parent.GetComponent<Enemy_Parent>().monsterDrop(transform.position);
            Destroy(this.gameObject);
        }
    }
}
