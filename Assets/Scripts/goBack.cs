using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goBack : MonoBehaviour
{
    [SerializeField]
    private GameObject main_menu;

    [SerializeField]
    private GameObject tutorial_ui;

    private GameObject storageG;
    private Storage storage;
    // Start is called before the first frame update
    void Start()
    {
        storageG = GameObject.Find("StorageFab(Clone)");
        storage = storageG.GetComponent<Storage>();
        this.gameObject.SetActive(false);
        storage.isTutorial = false;
        main_menu.SetActive(true);
        tutorial_ui.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
