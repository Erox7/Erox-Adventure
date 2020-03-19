using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressListener : MonoBehaviour
{
    public GameConfigurationSO gameConfig;

    void Update()
    {
        if (Input.GetKeyDown(gameConfig.moveUp))
        {
            PressedKeyEventManager.current.UpKeyPressed();
        }
        if (Input.GetKeyDown(gameConfig.moveDown))
        {
            PressedKeyEventManager.current.DownKeyPressed();
        }
        if (Input.GetKeyDown(gameConfig.moveRight))
        {
            PressedKeyEventManager.current.RightKeyPressed();
        }
        if (Input.GetKeyDown(gameConfig.moveLeft))
        {
            PressedKeyEventManager.current.LeftKeyPressed();
        }
        if (Input.GetKeyDown(gameConfig.sprint))
        {
            PressedKeyEventManager.current.Sprint();
        }

        if (Input.GetKeyUp(gameConfig.moveUp))
        {
            PressedKeyEventManager.current.UpKeyUnPressed();
        }
        if (Input.GetKeyUp(gameConfig.moveDown))
        {
            PressedKeyEventManager.current.DownKeyUnPressed();
        }
        if (Input.GetKeyUp(gameConfig.moveRight))
        {
            PressedKeyEventManager.current.RightKeyUnPressed();
        }
        if (Input.GetKeyUp(gameConfig.moveLeft))
        {
            PressedKeyEventManager.current.LeftKeyUnPressed();
        }
        if (Input.GetKeyUp(gameConfig.sprint)) 
        {
            PressedKeyEventManager.current.StopSprint();
        }
    }
}
