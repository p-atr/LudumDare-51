using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Weapon_type
{

    Throwing_Knive = 0,
    Bow = 1,
    Sling_Shot = 2,
    Big_Ass_Club_bzw_Gigaschwert = 3,
    Nothing = 4,
}
public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject Sword_it;
    [SerializeField]
    private GameObject Bow_it;
    [SerializeField]
    private GameObject Knive_it;
    [SerializeField]
    private GameObject Sling_it;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject[] shots = new GameObject[4];
    [SerializeField]
    private Transform spawnpoint;

    [SerializeField]
    AudioSource Bow;
    [SerializeField]
    AudioSource Sling;
    [SerializeField]
    AudioSource Sword_Sound;
    [SerializeField]
    AudioSource Kunai;

    private Weapon_type type;

    private float shotCooldown = 0;

    private void Start()
    {
        type = Weapon_type.Nothing;
    }
    void Update()
    {
        if (shotCooldown <= 0)
        {
            hasDamaged = false;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject piu = null;
                switch (type)
                {
                    case Weapon_type.Throwing_Knive:
                        piu = GameObject.Instantiate(shots[0], spawnpoint.position, transform.rotation);
                        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 30;
                        piu.GetComponent<Hit_Manager>().Damage = 17;
                        shotCooldown = 0.25f;
                        Kunai.Play();
                        break;
                    case Weapon_type.Bow:
                        piu = GameObject.Instantiate(shots[1], spawnpoint.position, transform.rotation);
                        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;
                        piu.GetComponent<Hit_Manager>().Damage = 50;
                        shotCooldown = 0.6f;
                        Bow.Play();
                        break;
                    case Weapon_type.Sling_Shot:
                        //MID
                        Quaternion uWu = new Quaternion();
                        uWu.SetLookRotation(transform.forward);
                        piu = GameObject.Instantiate(shots[2], spawnpoint.position, uWu);
                        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;
                        piu.GetComponent<Hit_Manager>().Damage = 20;

                        //RIGHT
                        uWu.SetLookRotation(transform.forward * 5 + transform.right);
                        piu = GameObject.Instantiate(shots[2], spawnpoint.position + transform.right * 0.5f, uWu);
                        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;
                        piu.GetComponent<Hit_Manager>().Damage = 20;

                        uWu.SetLookRotation(transform.forward * 10 + transform.right);
                        piu = GameObject.Instantiate(shots[2], spawnpoint.position + transform.right * 0.25f, uWu);
                        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;
                        piu.GetComponent<Hit_Manager>().Damage = 20;


                        //LEFT
                        uWu.SetLookRotation(transform.forward * 5 - transform.right);
                        piu = GameObject.Instantiate(shots[2], spawnpoint.position - transform.right * 0.5f, uWu);
                        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;
                        piu.GetComponent<Hit_Manager>().Damage = 20;

                        uWu.SetLookRotation(transform.forward * 10 - transform.right);
                        piu = GameObject.Instantiate(shots[2], spawnpoint.position - transform.right * 0.25f, uWu);
                        piu.GetComponent<Rigidbody>().velocity = piu.transform.forward * 20;
                        piu.GetComponent<Hit_Manager>().Damage = 20;

                        shotCooldown = 0.7f;

                        Sling.Play();
                        break;
                    case Weapon_type.Big_Ass_Club_bzw_Gigaschwert:
                        anim.SetTrigger("Melee");
                        shotCooldown = 0.3f;
                        Sword_Sound.Play();
                        break;
                    case Weapon_type.Nothing:
                        break;
                }
            }
        }
        else
        {
            shotCooldown -= Time.deltaTime;
        }
    }

    public void Switch_weapon(Weapon_type type)
    {
        this.type = type;
        switch (type)
        {
            case Weapon_type.Big_Ass_Club_bzw_Gigaschwert:
                Sword_it.SetActive(true);
                Bow_it.SetActive(false);
                Sling_it.SetActive(false);
                Knive_it.SetActive(false);
                break;
            case Weapon_type.Throwing_Knive:
                Sword_it.SetActive(false);
                Bow_it.SetActive(false);
                Sling_it.SetActive(false);
                Knive_it.SetActive(true);
                break;
            // case bow
            case Weapon_type.Sling_Shot:
                Sword_it.SetActive(false);
                Bow_it.SetActive(false);
                Sling_it.SetActive(true);
                Knive_it.SetActive(false);
                break;
            case Weapon_type.Bow:
                Sword_it.SetActive(false);
                Bow_it.SetActive(true);
                Sling_it.SetActive(false);
                Knive_it.SetActive(false);
                break;
            case Weapon_type.Nothing:
                Sword_it.SetActive(false);
                Bow_it.SetActive(false);
                Sling_it.SetActive(false);
                Knive_it.SetActive(false);
                break;
            default:
                Debug.Log("oh ohw...");
                break;
        }
        if (type == Weapon_type.Big_Ass_Club_bzw_Gigaschwert)
        {
            Sword_it.SetActive(true);
        }
        else
        {
            Sword_it.SetActive(false);
        }
    }

    private bool hasDamaged;

    public bool ShouldDamage(Collider col)
    {
        if (hasDamaged || col.tag != "Enemy" || !anim.GetCurrentAnimatorStateInfo(1).IsTag("UwU"))
        {
            return false;
        }
        hasDamaged = true;
        return true;
    }
}