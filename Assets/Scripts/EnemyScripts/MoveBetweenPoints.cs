using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints
{
    public GameObject _go;
    public List<Vector3> _wayPoints;
    public float _speed;
    public GridLayout _gl;

    public MoveBetweenPoints(GameObject go, List<Vector3> points, float speed, GridLayout gl)
    {
        _go = go;
        _wayPoints = points;
        _speed = speed;
        _gl = gl;
    }

    public void decideWhereToGo()
    {
        //moveToPoint(_wayPoints[0]);
    }
    public IEnumerator moveToPoint(Vector3 point)
    {
        Vector3 myGlobalPosition = _gl.WorldToCell(_gl.transform.position);
        while (myGlobalPosition != point)
        {
            _go.transform.Translate(new Vector3(point.x * _speed * Time.deltaTime, 0, 0));
            myGlobalPosition = _gl.WorldToCell(_gl.transform.position);
            yield return new WaitForEndOfFrame();
        }
    }

}
