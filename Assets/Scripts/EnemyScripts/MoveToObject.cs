using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject
{
    public GameObject _go;
    public GameObject _target;
    public float _speed;
    private float _attackDistance;

    public Vector3 movement;
    public MoveToObject(GameObject go, GameObject target, float speed, float attackDistance)
    {
        _go = go;
        _target = target;
        _speed = speed;
        _attackDistance = attackDistance;
    }
    public IEnumerator StartMoving()
    {
        while (true)
        {
            if (calculateDistance(_go, _target) <= _attackDistance) { 
                movement = _target.transform.position - _go.transform.position;
                _go.transform.Translate(movement.normalized * _speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            } else { 
                yield return new WaitForEndOfFrame();
            }
        }
        
    }
    private float calculateDistance(GameObject one, GameObject two)
    {
        float dist = Vector3.Distance(one.transform.position, two.transform.position);
        return dist;
    }
}
