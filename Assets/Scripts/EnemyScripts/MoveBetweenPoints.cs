using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints
{
    public GameObject _go;
    public List<Vector3> _wayPoints;
    public float _speed;
    public GridLayout _gl;
    public Vector3 movement;

    private int _nextPoint;

    public MoveBetweenPoints(GameObject go, List<Vector3> points, float speed, GridLayout gl)
    {
        _go = go;
        _wayPoints = points;
        _speed = speed;
        _gl = gl;
        _nextPoint = 1;
    }

    public IEnumerator StartMoving()
    {
        Vector3Int myGlobalPosition = _gl.WorldToCell(_go.transform.position);
        Vector3Int nextPoint = _gl.WorldToCell(_wayPoints[_nextPoint]);
        movement = nextPoint - myGlobalPosition;
        while (true)
        {
            if (haveEqualValues(myGlobalPosition, nextPoint))
            {
                if(_nextPoint + 1 >= _wayPoints.Count)
                {
                    _nextPoint = 0;
                } else
                {
                    _nextPoint++;
                }
                myGlobalPosition = _gl.WorldToCell(_go.transform.position);
                nextPoint = _gl.WorldToCell(_wayPoints[_nextPoint]);
                movement = nextPoint - myGlobalPosition;
            } else
            {
                _go.transform.Translate(movement.normalized * _speed * Time.deltaTime);
                myGlobalPosition = _gl.WorldToCell(_go.transform.position);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public bool haveEqualValues(Vector3Int vector1, Vector3Int vector2)
    {
        if (vector1.x.Equals(vector2.x) && vector1.y.Equals(vector2.y) && vector1.z.Equals(vector2.z))
            return true;
        return false;
    }

    public void updateMovement(Vector3 position)
    {
        movement = _gl.WorldToCell(_wayPoints[_nextPoint]) - _gl.WorldToCell(position);
    }
}
