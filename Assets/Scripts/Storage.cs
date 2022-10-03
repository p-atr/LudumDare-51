using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public bool isRestart = false;
    public bool isTutorial = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
