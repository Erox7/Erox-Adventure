using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "GameConfiguration")]
public class GameConfigurationSO : ScriptableObject
{
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveRight;
    public KeyCode moveLeft;

    public KeyCode attack;
    public KeyCode sprint;
    public KeyCode inventory;
}
