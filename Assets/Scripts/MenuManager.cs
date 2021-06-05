using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MainMenuManager
{
    [SerializeField] private GameObject pauseObj;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        pauseObj.SetActive(isPaused);
        //FindObjectOfType<PlayerInputs>().DisableEnableAll(!isPaused);
        if (isPaused) { foreach (AudioSource source in FindObjectsOfType<AudioSource>()) { source.Pause(); } Time.timeScale = 0; }
        else if (!isPaused) { Time.timeScale = 1; howToPlayMenu.SetActive(false); inMenu = false; foreach (AudioSource source in FindObjectsOfType<AudioSource>()) { source.UnPause(); } }
    }

    public override void HTPMenu()
    {
        Time.timeScale = 1;
        inMenu = !inMenu;
        pauseObj.SetActive(!inMenu);
        howToPlayMenu.SetActive(inMenu);
        Time.timeScale = 0;
    }
}
