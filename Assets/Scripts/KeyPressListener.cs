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
            PressedKeyEventManager.Instance.UpKeyPressed();
        }
        if (Input.GetKeyDown(gameConfig.moveDown))
        {
            PressedKeyEventManager.Instance.DownKeyPressed();
        }
        if (Input.GetKeyDown(gameConfig.moveRight))
        {
            PressedKeyEventManager.Instance.RightKeyPressed();
        }
        if (Input.GetKeyDown(gameConfig.moveLeft))
        {
            PressedKeyEventManager.Instance.LeftKeyPressed();
        }
        if (Input.GetKeyDown(gameConfig.sprint))
        {
            PressedKeyEventManager.Instance.Sprint();
        }
        if (Input.GetKeyDown(gameConfig.attack))
        {
            PressedKeyEventManager.Instance.AttackKeyPressed();
        }

        if (Input.GetKeyUp(gameConfig.moveUp))
        {
            PressedKeyEventManager.Instance.UpKeyUnPressed();
        }
        if (Input.GetKeyUp(gameConfig.moveDown))
        {
            PressedKeyEventManager.Instance.DownKeyUnPressed();
        }
        if (Input.GetKeyUp(gameConfig.moveRight))
        {
            PressedKeyEventManager.Instance.RightKeyUnPressed();
        }
        if (Input.GetKeyUp(gameConfig.moveLeft))
        {
            PressedKeyEventManager.Instance.LeftKeyUnPressed();
        }
        if (Input.GetKeyUp(gameConfig.sprint)) 
        {
            PressedKeyEventManager.Instance.StopSprint();
        }
    }
}
