using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // TODO: El moviment ara mateix només funciona a clicks, he de mirar quan es para de pitjar la tecla.
    // Start is called before the first frame update
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
        upClickEnd = false;
        downClickEnd = false;
        rightClickEnd = false;
        leftClickEnd = false;

        PressedKeyEventManager.current.onUpKeyPress += MoveUp;
        PressedKeyEventManager.current.onDownKeyPress += MoveDown;
        PressedKeyEventManager.current.onLeftKeyPress += MoveLeft;
        PressedKeyEventManager.current.onRightKeyPress += MoveRight;

        PressedKeyEventManager.current.onUpKeyUnPress += StopMovingUp;
        PressedKeyEventManager.current.onDownKeyUnPress += StopMovingDown;
        PressedKeyEventManager.current.onLeftKeyUnPress += StopMovingLeft;
        PressedKeyEventManager.current.onRightKeyUnPress += StopMovingRight;
    }

    private IEnumerator Move(Vector3 movement, MovementDirection direction) {

        switch (direction)
        {
            case MovementDirection.Up:
                while (!upClickEnd)
                {
                    transform.Translate(movement);
                    yield return new WaitForEndOfFrame();
                }
                break;
            case MovementDirection.Down:
                while (!downClickEnd)
                {
                    transform.Translate(movement);
                    yield return new WaitForEndOfFrame();
                }
                break;
            case MovementDirection.Right:
                while (!rightClickEnd)
                {
                    transform.Translate(movement);
                    yield return new WaitForEndOfFrame();
                }
                break;
            case MovementDirection.Left:
                while (!leftClickEnd)
                {
                    transform.Translate(movement);
                    yield return new WaitForEndOfFrame();
                }
                break;
            default:
                yield return null;
                break;
        }
    }

    private void MoveUp() {
        upClickEnd = false;
        StartCoroutine(Move(new Vector3(0, 1 * Time.deltaTime * speed, 0), MovementDirection.Up));    
    }
    private void MoveDown() {
        downClickEnd = false;
        StartCoroutine(Move(new Vector3(0, -1 * Time.deltaTime * speed, 0), MovementDirection.Down));
    }
    private void MoveLeft() {
        leftClickEnd = false;
        StartCoroutine(Move(new Vector3(-1 * Time.deltaTime * speed, 0, 0), MovementDirection.Left));
    }
    private void MoveRight() {
        rightClickEnd = false;
        StartCoroutine(Move(new Vector3(1 * Time.deltaTime * speed, 0, 0), MovementDirection.Right));
    }

    private void StopMovingUp () { upClickEnd = true; }
    private void StopMovingDown() { downClickEnd = true; }
    private void StopMovingLeft() { leftClickEnd= true; }
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
