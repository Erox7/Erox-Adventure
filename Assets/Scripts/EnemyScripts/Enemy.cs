using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;
    public List<Vector3> movementPoints;
    GridLayout gl;
    private float hp;
    private Animator animator;
    private MoveBetweenPoints movement;
    private Vector3Int lastPosition;
    // Start is called before the first frame update 
    void Start()
    {
        EnemyEventsManager.Instance.onTakeDamage += TakeDamage;
        EnemyEventsManager.Instance.onTakeProjectileDamage += TakeProjectileDamage;
        GlobalEventManager.Instance.onMapChanged += UpdateGrid;
        gl = MapController.currentMap.GetComponent<GridLayout>();
        animator = GetComponentInParent<Animator>();
        lastPosition = gl.WorldToCell(transform.position);
        hp = enemySO.hp;
        movement = new MoveBetweenPoints(gameObject,
        new List<Vector3>()
        {
            transform.position,
            movementPoints[0],
            movementPoints[1],
            movementPoints[2]
        },
        enemySO.speed,
        gl);
        
        //movement = new MoveToObject(this.gameObject, GameObject.FindWithTag("Player"), speed);
        StartCoroutine(movement.StartMoving());
    }

    private void Update()
    {
        Vector3Int myGlobalPosition = gl.WorldToCell(transform.position);
        if(myGlobalPosition != lastPosition)
        {
            EnemyEventsManager.Instance.MakeDamage(gl.WorldToCell(new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, 0)), 
                                                   enemySO.impactDamage,
                                                   CalculateRotation(myGlobalPosition));
            lastPosition = myGlobalPosition;
        }
    }

    private Vector2 CalculateRotation(Vector3Int myGlobalPosition)
    {
        if(myGlobalPosition.x > lastPosition.x)
        {
            return new Vector2(2,0);
        } else if (myGlobalPosition.x < lastPosition.x)
        {
            return new Vector2(-2, 0);
        }
        else if (myGlobalPosition.y > lastPosition.y)
        {
            return new Vector2(0, 2);
        }
        else 
        {
            return new Vector2(0, -2);
        }
    }
    public void TakeDamage(Vector2 playerOrientation, Vector3 attackedGlobalPosition)
    {
        // TODO: Deberia poder preguntarle al map manager que posicion global tengo
        // Mientras tenga la GL puedo sacar yo mismo la posición, pero es bueno? WHO KNOWS
        // Si coincide con la que ataca el jugador tengo que pillar dmg bruh

        if (gl != null && gl != default) { 
            Vector3 myGlobalPosition = gl.WorldToCell(gameObject.transform.position + new Vector3(0, -0.5f, 0));
        
            if (myGlobalPosition.Equals(attackedGlobalPosition) && !animator.GetBool("Death"))
            {
                loseHp();
                gameObject.transform.Translate(playerOrientation);
                // movement.updateMovement(transform.position);
            }
        }
    }

    public void TakeProjectileDamage(Vector3 attackedGlobalPosition)
    {
        // TODO: Deberia poder preguntarle al map manager que posicion global tengo
        // Mientras tenga la GL puedo sacar yo mismo la posición, pero es bueno? WHO KNOWS
        // Si coincide con la que ataca el jugador tengo que pillar dmg bruh

        if (gl != null && gl != default)
        {
            Vector3 myGlobalPosition = gl.WorldToCell(gameObject.transform.position + new Vector3(0, -0.5f, 0));

            if (myGlobalPosition.Equals(attackedGlobalPosition) && !animator.GetBool("Death"))
            {
                loseHp();
            }
        }
    }

    private void loseHp()
    {
        this.hp -= 0.5f;
        if (this.hp <= 0)
        {
            animator.SetBool("Death", true);
        }
    }
    private void UpdateGrid()
    {
        gl = MapController.currentMap.GetComponent<Grid>();
    }
    private void OnDestroy()
    {
        EnemyEventsManager.Instance.onTakeDamage -= TakeDamage;
        GlobalEventManager.Instance.onMapChanged -= UpdateGrid;
    }
}