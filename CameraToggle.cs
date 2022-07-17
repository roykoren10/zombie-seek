using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public GameObject firstCam;
    public GameObject thirdCam;
    public int camMode;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            camMode = (camMode + 1) % 2;
            StartCoroutine(toggleCam());
        }
    }

    // Toggle camera between first person and third person
    IEnumerator toggleCam() 
    {
        yield return new WaitForSeconds(0.01f);

        if (camMode == 0) {
            firstCam.SetActive(true);
            thirdCam.SetActive(false);
        } else if (camMode == 1) {
            thirdCam.SetActive(true);
            //firstCam.SetActive(false);
        }
    }
}