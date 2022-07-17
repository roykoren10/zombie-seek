using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _title;

    public static float health = 100;
    // Start is called before the first frame update
    void Start()
    {
    }


    public static void HealPlayer(int healNum)
    {
        health += healNum;
    }

    public static void damagePlayer(int damage)
    {
        health -= damage;
        Debug.Log("Health " + health);
    }
    // Update is called once per frame
    void Update()
    {
        _title.text = health.ToString();


        if (health <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }
}
