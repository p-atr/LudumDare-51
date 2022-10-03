    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField]
    private AudioSource menu;
    [SerializeField]
    private AudioSource game;


    public void PlayMenu()
    {
        menu.Play();
        game.Pause();
    }
    public void PlayGame()
    {
        game.Play();
        menu.Pause();
    }
}
