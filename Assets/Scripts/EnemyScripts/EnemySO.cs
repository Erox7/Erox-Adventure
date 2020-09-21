using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public float hp = default;
    public string enemyName = default;
    public float speed = default;
    public float impactDamage = default;
    public float skillDamage = default;
    public int movementPattern = default;
    public int attackPattern = default;
    public bool isInvulnerable = default;
    public float attackDistance = default;
}
