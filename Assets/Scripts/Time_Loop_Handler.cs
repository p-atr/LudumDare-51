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
    private GameObject tile;

    [SerializeField]
    private static int numberOfSpawnables = 3;

    [SerializeField]
    private List<GameObject> spawnableEnemies;

    [SerializeField]
    private List<float> spawnablesPower;

    [SerializeField]
    private List<GameObject> spawnableItems;

    [SerializeField]
    private float dropChance = 0.5f;

    [SerializeField]
    private Enemy_Parent Enemies;

    [SerializeField]
    private Item_Parent Items;

    [SerializeField]
    private TextMeshProUGUI m_TimerText;

    [SerializeField]
    private GameObject g_hint_text;

    private float timer = 10;

    private Playstate state = 0;
    public int numberOfPhase = 0;

    private GameObject[][] tiles = new GameObject[12][];

    private GameLogic logic;

    private List<Monster_Type> spawnQ = new List<Monster_Type>();

    private List<GameObject> tilers = new List<GameObject>();

    private void Start()
    {
        logic = gameObject.GetComponent<GameLogic>();
        for (int i = 0; i < 12; i++)
        {
            tiles[i] = new GameObject[9];
            for (int j = 0; j < 9; j++)
            {
                int rand = Random.Range(0, 4);
                tiles[i][j] = GameObject.Instantiate<GameObject>(tile, new Vector3(-35.1f + (i * 5), -0.75f, 18.6f + (j * -5)), Quaternion.Euler(0, 90 * rand, 0), GameObject.Find("Ground").transform);
            }
        }
        m_TimerText.text = "";
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case 0:
                if (Input.GetKey(KeyCode.Space))
                {
                    GetComponent<Audio_Manager>().PlayGame();
                    //logic.fightPhase();
                    g_hint_text.SetActive(false);
                    state = Playstate.Fighting;
                    SpawnEnemies();
                }
                break;
            case Playstate.Fighting:
                if (timer <= 0)
                {
                    logic.lootPhase();
                    Enemies.PhaseChange();
                    //Items.PhaseChange();
                    timer = 10;
                    state = Playstate.Planing;
                    SpawnItems();
                    PrepareSpawn();
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
                    logic.fightPhase();
                    numberOfPhase++;
                    SpawnEnemies();
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
        }
        // Debug.Log(timer);
        if (timer <= 3.5f)
        {
            m_TimerText.text = timer.ToString("F1");
        }
        else
        {
            m_TimerText.text = timer.ToString("F0");
        }

    }

    private void SpawnItems()
    {
        foreach (Vector3 itemSpawnpoint in Enemies.itemSpawnPoints)
        {
            Debug.Log("Spawned Item");
            float rand = Random.Range(0f, 1f);
            if (rand < 0.05f)
            {
                //Spawn Sword
                GameObject.Instantiate(spawnableItems[0], new Vector3(itemSpawnpoint.x, 2, itemSpawnpoint.z), Quaternion.identity, Items.transform);
                //Enemies.itemSpawnPoints.Remove(itemSpawnpoint);
            }
            else if (rand < 0.15f)
            {
                //Bow
                GameObject.Instantiate(spawnableItems[3], new Vector3(itemSpawnpoint.x, 2, itemSpawnpoint.z), Quaternion.identity, Items.transform);
            }
            else if (rand < 0.25f)
            {
                //Sling
                GameObject.Instantiate(spawnableItems[2], new Vector3(itemSpawnpoint.x, 2, itemSpawnpoint.z), Quaternion.identity, Items.transform);
            }
            else if (rand < 0.45f)
            {
                //Kunai
                GameObject.Instantiate(spawnableItems[4], new Vector3(itemSpawnpoint.x, 2, itemSpawnpoint.z), Quaternion.identity, Items.transform);
            }
            else if (rand < 0.95f)
            {
                //Coin
                GameObject.Instantiate(spawnableItems[1], new Vector3(itemSpawnpoint.x, 2, itemSpawnpoint.z), Quaternion.identity, Items.transform);
            }


        }
        Enemies.itemSpawnPoints.Clear();
    }

    public void PrepareSpawn()
    {
        float maxAmountOfEnemies = Mathf.RoundToInt(numberOfPhase * 1.6f + 2);
        float amountOfSpawnedEnemies = 0f;


        while (amountOfSpawnedEnemies <= maxAmountOfEnemies - 2)
        {
            int enemyType = Random.Range(0, spawnableEnemies.Count);
            while (amountOfSpawnedEnemies + spawnablesPower[enemyType] > maxAmountOfEnemies)
            {
                enemyType = Random.Range(0, spawnableEnemies.Count);
            }
            int i = Random.Range(0, 12);
            int j = Random.Range(0, 9);
            if (tiles[i][j].tag == "Point")
            {
                //tiles[i][j].tag = "Untagged";
                amountOfSpawnedEnemies += spawnablesPower[enemyType];
                spawnQ.Add((Monster_Type)enemyType);
                tilers.Add(tiles[i][j]);
                tiles[i][j].GetComponent<Highlight>().HightlightTile();
            }

        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnQ.Count; i++)
        {
            //Debug.Log(spawnQ[i]);
            GameObject temp = GameObject.Instantiate(spawnableEnemies[(int)spawnQ[i]], new Vector3(tilers[i].transform.position.x, 10, tilers[i].transform.position.z), Quaternion.identity, Enemies.transform);
            Enemy_Behaviour tempo = temp.transform.GetChild(0).GetComponent<Enemy_Behaviour>();
            //Debug.Log(i + "  " + j + temp.name);
            tempo.Point = tilers[i];
            tempo.Tiles = tiles;
        }
        spawnQ.Clear();
        tilers.Clear();
    }
}
