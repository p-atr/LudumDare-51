using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Playstate
{
    Waiting = 0,
    Fighting = 1,
    Planing = 2
}
public class Time_Loop_Handler : MonoBehaviour
{

    [SerializeField]
    private GameObject[] spawnables;


    [SerializeField]
    private Enemy_Parent Enemies;

    [SerializeField]
    private Item_Parent Items;

    [SerializeField]
    private TextMeshProUGUI m_TimerText;

    private float timer = 10;

    private Playstate state = 0;

    private ArrayList points = new ArrayList();

    private void Start()
    {
        points.AddRange(GameObject.FindGameObjectsWithTag("Point"));
        m_TimerText.text = "";
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case 0:
                if(Input.GetKey(KeyCode.Space))
                {
                    state = Playstate.Fighting;
                    Spawn();
                }
                break;
            case Playstate.Fighting:
                if (timer <= 0)
                {
                    Enemies.PhaseChange();
                    //Items.PhaseChange();
                    timer = 10;
                    state = Playstate.Planing;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
            case Playstate.Planing:
                if (timer <= 0)
                {
                    //Enemies.PhaseChange();
                    Items.PhaseChange();
                    timer = 10;
                    state = Playstate.Fighting;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
        }
    // Debug.Log(timer);
    m_TimerText.text = timer.ToString("F2");
    }

    private void Spawn()
    {
        foreach (GameObject g in points)
        {
            if(Random.Range(0f,1f) < 0.1f)
            {
                GameObject.Instantiate(spawnables[0], g.transform.position, Quaternion.identity);
            }
        }
    }
}
