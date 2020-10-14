using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAttack 
{
    private GameObject _player;
    private GameObject _enemy;
    private EnemySO _enemySO;
    private GridLayout _gl;
    public ExplodeAttack() { }
    public ExplodeAttack(GameObject enemy, GameObject player, EnemySO enemySO, GridLayout gl)
    {
        _enemy = enemy;
        _enemySO = enemySO;
        _player = player;
        _gl = gl;
    }

    public IEnumerator StartAttacking()
    {
        while (true)
        {
            if (_gl.WorldToCell(_enemy.transform.position).Equals(_gl.WorldToCell(_player.transform.position)))
            {
                EnemyEventsManager.Instance.MakeDamageNoKnockBack(_enemy.transform.position,_enemySO.skillDamage);
                GameObject.Destroy(_enemy);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
