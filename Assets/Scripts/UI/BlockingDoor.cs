using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public int _neededLevers;
    public bool _opened;

    private int _activatedLevers;
    void Start()
    {
        _neededLevers = 1;
        _activatedLevers = 0;
        GlobalEventManager.Instance.onLeverActivated += ActivateLever;
        GlobalEventManager.Instance.onLeverDectivated += DeactivateLever;
    }

    public void ActivateLever()
    {
        _activatedLevers += 1;
        if (_activatedLevers >= _neededLevers)
        {
            //OPEN THE DOOR
            _opened = true;
        }
    }
    public void DeactivateLever()
    {
        if (_activatedLevers != 0)
        {
            _activatedLevers -= 1;
        }
        if(_opened)
        {
            //CLOSE THE DOOR
            _opened = false;
        }
    }

    public void OnDestroy()
    {
        GlobalEventManager.Instance.onLeverActivated -= ActivateLever;
        GlobalEventManager.Instance.onLeverDectivated -= DeactivateLever;
    }
}
