using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _title;

    [SerializeField]
    private WeaponSwitching weaponSwitching;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(weaponSwitching.selectedWeapon);
        var gun = weaponSwitching.guns[weaponSwitching.selectedWeapon];
        _title.text = gun.CurrentAmmo().ToString() + "/" + gun.MagSize().ToString();
    }
}
