using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement
{
    private bool upClickEnd;
    private bool downClickEnd;
    private bool rightClickEnd;
    private bool leftClickEnd;
    public Transform playerTransform;
    private int speed,runningSpeed;

    public enum MovementDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public PlayerMovement() { }
    // Update is called once per frame
    public PlayerMovement(Transform pTransform)
    {
        upClickEnd = true;
        downClickEnd = true;
        rightClickEnd = true;
        leftClickEnd = true;

        playerTransform = pTransform;
        PressedKeyEventManager.current.onUpKeyPress += MoveUp;
        PressedKeyEventManager.current.onDownKeyPress += MoveDown;
        PressedKeyEventManager.current.onLeftKeyPress += MoveLeft;
        PressedKeyEventManager.current.onRightKeyPress += MoveRight;

        PressedKeyEventManager.current.onUpKeyUnPress += StopMovingUp;
        PressedKeyEventManager.current.onDownKeyUnPress += StopMovingDown;
        PressedKeyEventManager.current.onLeftKeyUnPress += StopMovingLeft;
        PressedKeyEventManager.current.onRightKeyUnPress += StopMovingRight;
    }

    public IEnumerator Move() {
        Vector3 vel = Vector3.zero;
        while (true)
        {
            if (!upClickEnd)
{
                vel += new Vector3(0, 1, 0);
            }
            if (!downClickEnd)
{
                vel += new Vector3(0, -1, 0);
            }
            if (!leftClickEnd)
            {
                vel += new Vector3(-1, 0, 0);
            }
            if (!rightClickEnd)
            {
                vel += new Vector3(1, 0, 0);
            }
            playerTransform.position += (vel == Vector3.zero) ? vel : vel.normalized * Time.deltaTime * speed; //(aixi la velocitat sempre sera constant)
            vel = Vector3.zero;
            yield return new WaitForEndOfFrame();
        }
    }
    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }
    public int GetSpeed()
    {
        return speed;
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


    ~PlayerMovement()
    {
        PressedKeyEventManager.current.onUpKeyPress -= MoveUp;
        PressedKeyEventManager.current.onDownKeyPress -= MoveDown;
        PressedKeyEventManager.current.onLeftKeyPress -= MoveLeft;
        PressedKeyEventManager.current.onRightKeyPress -= MoveRight;

        PressedKeyEventManager.current.onUpKeyUnPress -= StopMovingUp;
        PressedKeyEventManager.current.onDownKeyUnPress -= StopMovingDown;
        PressedKeyEventManager.current.onLeftKeyUnPress -= StopMovingLeft;
        PressedKeyEventManager.current.onRightKeyUnPress -= StopMovingRight;
    }
}
