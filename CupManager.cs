using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CupManager : MonoBehaviour
{
    public GameObject[] cups;
    static int currentCup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentCup);
        for (int i = 0; i < cups.Length; i++) {
            if (i == currentCup) {
                cups[i].SetActive(true);
            }
            else {
                cups[i].SetActive(false);
            }

        }

        if ((currentCup + 1) > cups.Length) {
            SceneManager.LoadScene(3);
        }
    }

    public static void increaseCurrentCup()
    {
        currentCup++;
    }

}