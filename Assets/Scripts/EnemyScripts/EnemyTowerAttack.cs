using System;
using System.Collections;
using UnityEngine;

public class EnemyTowerAttack
{
    public float _attack;
    private float _attackDistance;
    private Animator animator;
    private GameObject _enemy;
    private GameObject _player;
    private bool _isInCooldown = false;
    private DateTime lastAttackDone;
    private UnityEngine.Object prefab = Resources.Load("EnemyBullet");

    public EnemyTowerAttack() { }
    public EnemyTowerAttack(GameObject enemy, GameObject player, EnemySO enemySO)
    {
        _attack = enemySO.skillDamage;
        _enemy = enemy;
        _player = player;
        _attackDistance = enemySO.attackDistance;
        animator = _enemy.GetComponent<Animator>();
    }

    public IEnumerator StartAttacking()
    {
        while (true)
        {
            if (calculateDistance(_enemy,_player) <= _attackDistance)
            {
                animator.SetBool("Activated",true);
            } else if (animator.GetBool("Activated"))
            {
                animator.SetBool("Activated", false);
            }

            if (animator.GetBool("Activated") && !_isInCooldown)
            {
                InstantiatePrefab();
                lastAttackDone = DateTime.Now;
                _isInCooldown = true;
            }
            TimeSpan ts = (DateTime.Now - lastAttackDone);
            if (ts.Seconds >= 3)
            {
                _isInCooldown = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private float calculateDistance(GameObject one, GameObject two)
    {
        float dist = Vector3.Distance(one.transform.position, two.transform.position);
        return dist;
    }
    public void InstantiatePrefab()
    {
        GameObject newObject = prefab as GameObject;
        EnemyBulletAttack yourObject = newObject.GetComponent<EnemyBulletAttack>();
        yourObject.bulletObjective = _player.transform.position;
        yourObject.damage = _attack;
        GameObject.Instantiate(prefab, _enemy.transform.position, Quaternion.identity);
    }

}
