using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints
{
    public GameObject _go;
    public List<Vector3> _wayPoints;
    public float _speed;
    public Vector3 movement;

    private int _nextPointCounter;

    public MoveBetweenPoints(GameObject go, List<Vector3> points, float speed)
    {
        _go = go;
        _wayPoints = points;
        _speed = speed;
        _nextPointCounter = 1;
    }

    public IEnumerator StartMoving()
    {
        Vector3 myPosition = _go.transform.position;
        Vector3 nextPoint = _wayPoints[_nextPointCounter];
        movement = nextPoint - myPosition;
        while (true)
        {
            if (haveEqualValues3(myPosition, nextPoint))
            {
                if(_nextPointCounter + 1 >= _wayPoints.Count)
                {
                    _nextPointCounter = 0;
                } else
                {
                    _nextPointCounter++;
                }
                _go.transform.position = nextPoint;
                myPosition = _go.transform.position;
                nextPoint = _wayPoints[_nextPointCounter];
                movement = nextPoint - myPosition;
                _go.transform.Translate(movement.normalized * _speed * Time.deltaTime);
            } else
            {
                movement = nextPoint - myPosition;
                _go.transform.Translate(movement.normalized * _speed * Time.deltaTime);
                myPosition = _go.transform.position;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public bool haveEqualValues(Vector3 vector1, Vector3 vector2)
    {
        if (vector1.x.Equals(vector2.x) && vector1.y.Equals(vector2.y) && vector1.z.Equals(vector2.z)) {
            return true;
        }
        return false;
    }

    public bool haveEqualValues2(Vector3 vector1, Vector3 vector2)
    {
        if ((vector1.x.Equals(vector2.x) || Mathf.Abs(vector1.x).CompareTo(Mathf.Abs(vector2.x)) > 0)
            && (vector1.y.Equals(vector2.y) || Mathf.Abs(vector1.y).CompareTo(Mathf.Abs(vector2.y)) > 0)
            && (vector1.z.Equals(vector2.z) || Mathf.Abs(vector1.z).CompareTo(Mathf.Abs(vector2.z)) > 0))
            return true;
        return false;
    }

    public bool haveEqualValues3(Vector3 vector1, Vector3 vector2)
    {
        if (IsWithin(vector1.x, vector2.x-0.1f, vector2.x+0.1f)
            && IsWithin(vector1.y, vector2.y - 0.1f, vector2.y + 0.1f)
            && IsWithin(vector1.z, vector2.z - 0.1f, vector2.z + 0.1f))
            return true;
        return false;
    }

    public bool IsWithin(float value, float minimum, float maximum)
    {
        return value >= minimum && value <= maximum;
    }
}
