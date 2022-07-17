using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField]
    ZombieCharacterControl zb;
    [SerializeField]
    private TMP_Text _title;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _title.text = "Final Boss - " + zb.health.ToString();
    }
}
