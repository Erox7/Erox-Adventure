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
    public Animator playerAnimator;
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
    public PlayerMovement(Transform pTransform, Animator pAnimator)
    {
        upClickEnd = true;
        downClickEnd = true;
        rightClickEnd = true;
        leftClickEnd = true;

        movement = new Vector3();
        playerTransform = pTransform;
        playerAnimator = pAnimator;
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

        GlobalEventManager.Instance.onMapChanged += UpdateGrid;
    }

    public IEnumerator Move() {
        Vector3 vel = Vector3.zero;
        while (true)
        {
            if((gl != null || gl != default)) { 
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
                if (vel != Vector3.zero && !MapController.invalidPositions.Contains(cellPosition))
                {
                    playerAnimator.SetBool("walking", true);
                    playerAnimator.SetFloat("moveX",vel.x);
                    playerAnimator.SetFloat("moveY", vel.y);
                    playerTransform.Translate(movement);
                } 
                if (MapController.globalItemsPosition.Contains(cellPosition))
                {
                    GlobalEventManager.Instance.PickUpItem(cellPosition);
                }
                if (MapController.portals.ContainsKey(cellPosition))
                {
                    GlobalEventManager.Instance.MapChange(MapController.portals[cellPosition]);
                }
                if (vel == Vector3.zero)
                {
                    playerAnimator.SetBool("walking", false);
                } else
                {
                    vel = Vector3.zero;
                }
            }
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

    private void UpdateGrid()
    {
        gl = MapController.currentMap.GetComponent<Grid>();
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

        GlobalEventManager.Instance.onMapChanged -= UpdateGrid;

    }
}
