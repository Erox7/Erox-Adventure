using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private bool upClickEnd;
    private bool downClickEnd;
    private bool rightClickEnd;
    private bool leftClickEnd;
    public int speed;

    public enum MovementDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    // Update is called once per frame
    private void Start()
    {
        upClickEnd = true;
        downClickEnd = true;
        rightClickEnd = true;
        leftClickEnd = true;

        PressedKeyEventManager.current.onUpKeyPress += MoveUp;
        PressedKeyEventManager.current.onDownKeyPress += MoveDown;
        PressedKeyEventManager.current.onLeftKeyPress += MoveLeft;
        PressedKeyEventManager.current.onRightKeyPress += MoveRight;

        PressedKeyEventManager.current.onUpKeyUnPress += StopMovingUp;
        PressedKeyEventManager.current.onDownKeyUnPress += StopMovingDown;
        PressedKeyEventManager.current.onLeftKeyUnPress += StopMovingLeft;
        PressedKeyEventManager.current.onRightKeyUnPress += StopMovingRight;

        StartCoroutine(Move());
    }

    private IEnumerator Move() {
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

            transform.position += (vel == Vector3.zero) ? vel : vel.normalized * Time.deltaTime * speed; //(aixi la velocitat sempre sera constant)
            vel = Vector3.zero;
            yield return new WaitForEndOfFrame();
        }
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

    public void OnDestroy()
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
