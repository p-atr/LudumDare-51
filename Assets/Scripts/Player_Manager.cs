using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private AudioSource dam;

    [SerializeField]
    private GUI_Coin coinUI;

    public int currentHealth;

    [SerializeField]
    private ParticleSystem deathParticles;

    [SerializeField]
    private GameLogic logic;

    //storage stuff
    private GameObject storageG;
    private Storage storage;


    void Awake()
    {
        coinUI.Show(currentHealth);

    }

    void Start()
    {
        storageG = GameObject.Find("StorageFab(Clone)");
        storage = storageG.GetComponent<Storage>();
    }

    public void Dmage(int amount)
    {
        if (storage.isTutorial)
        {
            return;
        }
        currentHealth -= amount;
        coinUI.Show(currentHealth);
        Camera.main.gameObject.GetComponent<Facer>().shakeScreen(1f, 0.15f);
        if (currentHealth <= 0)
        {
            Die();
        }
        dam.Play();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        coinUI.Show(currentHealth);
    }

    public void Die()
    {
        deathParticles.transform.SetParent(null);
        deathParticles.Play();
        this.gameObject.SetActive(false);
        //fade to black
        logic.death();
        //Debug.Log("U DED HAHAHAHAHHAHHA");
    }
}
