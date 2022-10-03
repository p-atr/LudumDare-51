using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    [SerializeField]
    private AudioSource ManueSound;

    [SerializeField]
    private Time_Loop_Handler timeHandler;

    [SerializeField]
    private GameObject main_camera;

    [SerializeField]
    private GameObject menu_camera;

    [SerializeField]
    private GameObject main_menu;

    [SerializeField]
    private GameObject game_ui;

    [SerializeField]
    private GameObject tutorial_ui;

    [SerializeField]
    private GameObject death_menu;


    [SerializeField]
    private GameObject fight_text;

    [SerializeField]
    private GameObject loot_text;

    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private GameObject StorageFab;

    // [SerializeField]
    // private Storage storage;
    private GameObject storageG;
    private Storage storage;

    // Start is called before the first frame update
    void Start()
    {
        storageG = GameObject.Find("StorageFab(Clone)");
        if (storageG == null)
        {
            Instantiate(StorageFab);
            storageG = GameObject.Find("StorageFab(Clone)");
        }
        storage = storageG.GetComponent<Storage>();
        if (storage.isRestart)
        {
            storage.isRestart = false;
            startgame();
        }
        else
        {
            player.GetComponent<Move>().enabled = false;
            menu_camera.SetActive(true);
            main_camera.SetActive(false);
            main_menu.SetActive(true);
            game_ui.SetActive(false);
            StartCoroutine(waiter(0.3f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator waiter(float time = 0.5f)
    {
        // Debug.Log("wait start");
        yield return new WaitForSecondsRealtime(time);
        timeHandler.enabled = false;
        // Debug.Log("wait end");

    }

    public void stopgame()
    {
        ManueSound.Play();
#if UNITY_WEBGL
            mainMenu();
            return;
#endif
        Debug.Log("stop");
        Application.Quit();
    }

    public void fightPhase()
    {
        fight_text.SetActive(true);
        loot_text.SetActive(false);
        //todo disable after 1 sec
        Invoke("hidePhase", 1);
    }

    public void lootPhase()
    {
        fight_text.SetActive(false);
        loot_text.SetActive(true);
        //todo disable after 1 sec
        Invoke("hidePhase", 1);
    }
    public void hidePhase()
    {
        //fade out
        fight_text.SetActive(false);
        loot_text.SetActive(false);
    }

    public void startgame()
    {

        ManueSound.Play();
        Debug.Log("start");
        player.SetActive(true);
        gameObject.GetComponent<Time_Loop_Handler>().numberOfPhase = 0;
        gameObject.GetComponent<Time_Loop_Handler>().PrepareSpawn();
        player.GetComponent<Move>().enabled = true;
        timeHandler.enabled = true;
        main_camera.SetActive(true);
        menu_camera.SetActive(false);
        main_menu.SetActive(false);
        game_ui.SetActive(true);
        death_menu.SetActive(false);
        score.enabled = false;

    }

    public void death()
    {
        timeHandler.enabled = false;

        player.GetComponent<Move>().enabled = false;
        game_ui.SetActive(false);
        score.enabled = true;
        score.text = "You survived " + gameObject.GetComponent<Time_Loop_Handler>().numberOfPhase + " Rounds";
        death_menu.SetActive(true);

    }

    public void tutorial()
    {

        ManueSound.Play();
        Debug.Log("tutorial");
        storage.isTutorial = true;
        main_menu.SetActive(false);
        tutorial_ui.SetActive(true);
        score.enabled = false;
    }

    public void mainMenu()
    {

        ManueSound.Play();
        Debug.Log("main menu");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        // main_menu.SetActive(true);
        // tutorial_ui.SetActive(false);
        // menu_camera.SetActive(true);
        // main_camera.SetActive(false);
        // timeHandler.enabled = false;
        // game_ui.SetActive(false);
        // death_menu.SetActive(false);
    }

    public void restartGame()
    {

        ManueSound.Play();
        storage.isRestart = true;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        score.enabled = false;
        // startgame();
    }
}
