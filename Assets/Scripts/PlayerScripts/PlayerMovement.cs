﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class PlayerMovement
{
    private bool upClickEnd;
    private bool downClickEnd;
    private bool rightClickEnd;
    private bool leftClickEnd;
    public Transform playerTransform;
    private int playerSpeed,runningSpeed,actualSpeed;
    private GridLayout gl;
    private Vector3 movement;
    public enum MovementDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public PlayerMovement() { }
    public PlayerMovement(Transform pTransform)
    {
        upClickEnd = true;
        downClickEnd = true;
        rightClickEnd = true;
        leftClickEnd = true;
        movement = new Vector3();
        gl = GameObject.Find("TestMap_1").GetComponentInChildren<Tilemap>().layoutGrid;
        playerTransform = pTransform;
        PressedKeyEventManager.Instance.onUpKeyPress += MoveUp;
        PressedKeyEventManager.Instance.onDownKeyPress += MoveDown;
        PressedKeyEventManager.Instance.onLeftKeyPress += MoveLeft;
        PressedKeyEventManager.Instance.onRightKeyPress += MoveRight;
        PressedKeyEventManager.Instance.onSprintKeyPress += Sprint;

        PressedKeyEventManager.Instance.onUpKeyUnPress += StopMovingUp;
        PressedKeyEventManager.Instance.onDownKeyUnPress += StopMovingDown;
        PressedKeyEventManager.Instance.onLeftKeyUnPress += StopMovingLeft;
        PressedKeyEventManager.Instance.onRightKeyUnPress += StopMovingRight;
        PressedKeyEventManager.Instance.onSprintKeyUnPress += StopSprint;
    }

    public IEnumerator Move() {
        Vector3 vel = Vector3.zero;
        while (true)
        {
            if (!upClickEnd)
{
                vel += new Vector3(0, 1, 0);
            }
            else if (!downClickEnd)
{
                vel += new Vector3(0, -1, 0);
            }
            else if (!leftClickEnd)
            {
                vel += new Vector3(-1, 0, 0);
            }
            else if (!rightClickEnd)
            {
                vel += new Vector3(1, 0, 0);
            }
            movement = ((vel == Vector3.zero) ? vel : vel.normalized * Time.deltaTime * actualSpeed);
            Vector3Int cellPosition = gl.WorldToCell(playerTransform.position + movement + new Vector3(0, -0.5f, 0));
            if (!MapController.invalidPositions.Contains(cellPosition))
            {
                //playerTransform.position = newPosition;
                playerTransform.Translate(movement);
            }
            if (MapController.portals.Contains(cellPosition))
            {
                GlobalEventManager.Instance.MapChange(1);
            }

            vel = Vector3.zero;
            yield return new WaitForEndOfFrame();
        }
    }
    public void SetPlayerSpeed(int newSpeed)
    {
        playerSpeed = newSpeed;
    }
    public int GetPlayerSpeed()
    {
        return playerSpeed;
    }
    public void SetActualSpeed(int newSpeed)
    {
        actualSpeed = newSpeed;
    }
    public int GetActualSpeed()
    {
        return actualSpeed;
    }
    public void SetRunningSpeed(int newSpeed)
    {
        runningSpeed = newSpeed;
    }
    public int GetRunningSpeed()
    {
        return runningSpeed;
    }
    private void MoveUp() {
        upClickEnd = false;
    }
    private void MoveDown() {
        downClickEnd = false;
    }
    private void MoveLeft() {
        leftClickEnd = false;
    }
    private void MoveRight() {
        rightClickEnd = false;
    }

    private void StopMovingUp () { upClickEnd = true; }
    private void StopMovingDown() { downClickEnd = true; }
    private void StopMovingLeft() { leftClickEnd = true; }
    private void StopMovingRight() { rightClickEnd = true; }
    private void Sprint() { actualSpeed = runningSpeed; }
    private void StopSprint() { actualSpeed = playerSpeed; }


    ~PlayerMovement()
    {
        PressedKeyEventManager.Instance.onUpKeyPress -= MoveUp;
        PressedKeyEventManager.Instance.onDownKeyPress -= MoveDown;
        PressedKeyEventManager.Instance.onLeftKeyPress -= MoveLeft;
        PressedKeyEventManager.Instance.onRightKeyPress -= MoveRight;
        PressedKeyEventManager.Instance.onSprintKeyPress -= Sprint;

        PressedKeyEventManager.Instance.onUpKeyUnPress -= StopMovingUp;
        PressedKeyEventManager.Instance.onDownKeyUnPress -= StopMovingDown;
        PressedKeyEventManager.Instance.onLeftKeyUnPress -= StopMovingLeft;
        PressedKeyEventManager.Instance.onRightKeyUnPress -= StopMovingRight;
        PressedKeyEventManager.Instance.onSprintKeyUnPress -= StopSprint;

    }
}
