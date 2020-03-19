﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedKeyEventManager : MonoBehaviour
{
    public static PressedKeyEventManager current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action onUpKeyPress;
    public void UpKeyPressed()
    {
        if (onUpKeyPress != null)
        {
            onUpKeyPress();
        }
    }
    public event Action onDownKeyPress;
    public void DownKeyPressed()
    {
        if (onDownKeyPress != null)
        {
            onDownKeyPress();
        }
    }

    public event Action onLeftKeyPress;
    public void LeftKeyPressed()
    {
        if (onLeftKeyPress != null)
        {
            onLeftKeyPress();
        }
    }

    public event Action onRightKeyPress;
    public void RightKeyPressed()
    {
        if (onRightKeyPress != null)
        {
            onRightKeyPress();
        }
    }

    public event Action onUpKeyUnPress;
    public void UpKeyUnPressed ()
    {
        if ( onUpKeyUnPress != null)
        {
            onUpKeyUnPress();
        }
    }

    public event Action onDownKeyUnPress;
    public void DownKeyUnPressed()
    {
        if (onDownKeyUnPress != null)
        {
            onDownKeyUnPress();
        }
    }
    public event Action onLeftKeyUnPress;
    public void LeftKeyUnPressed()
    {
        if (onLeftKeyUnPress != null)
        {
            onLeftKeyUnPress();
        }
        //onLeftKeyUnPress?.Invoke();
    }
    public event Action onRightKeyUnPress;
    public void RightKeyUnPressed()
    {
        if (onRightKeyUnPress != null)
        {
            onRightKeyUnPress();
        }
    }
}