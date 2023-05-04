using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("______PLAYER DATA______")]
    public GameObject Player;
    public GameObject playerSpawnPoint;

    [Header("______UI______")]
    public GameObject pauseMenu;
    public GameObject loseMenu;
    GameObject activeMenu;

    public bool isPaused = false;

    float timeScaleOrig;
    // Start is called before the first frame update
    void Awake()
    {
        timeScaleOrig= Time.timeScale;
        instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
        Debug.Log("paused");
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            activeMenu.SetActive(isPaused);
            if (isPaused )
            {
                pauseState();
            }
            else
            {
                playState();
            }
        }

    }

    public void pauseState()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void playState()
    {
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void healPlayer(int ammount)
    {
        Player.GetComponent<playerManager>().heal(ammount);
    }

    public void lose()
    {
        pauseState();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
    }

    public void respawnPlayer()
    {
        Player.GetComponent<playerManager>().SpawnPlayer();
        playState();
    }
}
