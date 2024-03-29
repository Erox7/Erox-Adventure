﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Erox", menuName = "Player")]
public class PlayerSO : ScriptableObject
{
    public float hp = default;
    public int attackDamage = default;
    public int speed = default;
    public int runningSpeed = default;
    public int mana = default;
    public float fireManaCost, waterManaCost, rockManaCost, windManaCost;
}
