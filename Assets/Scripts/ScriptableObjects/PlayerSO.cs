using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Erox", menuName = "Player")]
public class PlayerSO : ScriptableObject
{
    public float hp;
    public int attackDamage;
    public float speed;
}
