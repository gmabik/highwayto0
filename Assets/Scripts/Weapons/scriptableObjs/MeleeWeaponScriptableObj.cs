using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "ScriptableObjects/MeleeWeaponScriptableObject", order = 1)]
public class MeleeWeaponScriptableObj : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
}
