using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiedMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

    }
    public void backMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
