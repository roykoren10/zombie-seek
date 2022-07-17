using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(popUpIndex);
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }

        }

        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}