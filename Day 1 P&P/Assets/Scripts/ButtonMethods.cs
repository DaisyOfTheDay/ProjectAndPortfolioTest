using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour
{
    public void resume()
    {
        GameManager.instance.playState();
    }

    public void ResetScene()
    {
        GameManager.instance.playState();
        SceneManager.LoadScene(0);
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void healPlayer()
    {
        GameManager.instance.healPlayer(3);
    }

    public void Respawn()
    {
        GameManager.instance.respawnPlayer();
    }
}
