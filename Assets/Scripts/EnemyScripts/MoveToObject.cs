using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject
{
    public GameObject _go;
    public GameObject _target;
    public float _speed;

    public Vector3 movement;
    public MoveToObject(GameObject go, GameObject target, float speed)
    {
        _go = go;
        _target = target;
        _speed = speed;
    }
    public IEnumerator StartMoving()
    {
        while (true)
        {
            movement = _target.transform.position - _go.transform.position;
            _go.transform.Translate(movement.normalized * _speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
    }
    public void updateMovement(Vector3 position)
    {
        movement = _target.transform.position - position;
    }
}
