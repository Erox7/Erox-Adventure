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
    private Vector3Int lastPosition;
    private bool isInvulnerable;

    void Start()
    {
        EnemyEventsManager.Instance.onTakeDamage += TakeDamage;
        EnemyEventsManager.Instance.onTakeProjectileDamage += TakeProjectileDamage;
        GlobalEventManager.Instance.onMapChanged += UpdateGrid;
        gl = MapController.currentMap.GetComponent<GridLayout>();
        animator = GetComponentInParent<Animator>();
        lastPosition = gl.WorldToCell(transform.position);
        GameObject player = GameObject.FindWithTag("Player");
        StartMovement(player);
        StartAttacking(player);
        hp = enemySO.hp;
        isInvulnerable = enemySO.isInvulnerable;
    }
    void Update()
    {
        Vector3Int myGlobalPosition = gl.WorldToCell(new Vector3(transform.position.x, transform.position.y, 0));
        if (myGlobalPosition != lastPosition)
        {
            EnemyEventsManager.Instance.MakeDamage(myGlobalPosition,
                                                   enemySO.impactDamage,
                                                   CalculateRotation(myGlobalPosition));
            lastPosition = myGlobalPosition;
        }
    }
    private void StartMovement(GameObject player)
    {
        if (enemySO.movementPattern.Equals(0))
        {
            movementPoints.Insert(0,transform.position);
            MoveBetweenPoints movement = new MoveBetweenPoints(gameObject, movementPoints,
            enemySO.speed);
            StartCoroutine(movement.StartMoving());
        }
        else if (enemySO.movementPattern.Equals(1))
        {
            MoveToObject movement = new MoveToObject(gameObject,player, enemySO.speed, enemySO.attackDistance);
            StartCoroutine(movement.StartMoving());
        } else if (enemySO.movementPattern.Equals(99))
        {
            //99 code is to not move
        }
    }
    private void StartAttacking(GameObject player)
    {
        if (enemySO.attackPattern.Equals(1))
        {
            //Tower Attack pattern
            EnemyTowerAttack attack = new EnemyTowerAttack(gameObject, player, enemySO);
            StartCoroutine(attack.StartAttacking());
        } else if (enemySO.attackPattern.Equals(2))
        {
            //Exploding attack pattern

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
            Vector3 myGlobalPosition = gl.WorldToCell(gameObject.transform.position);
        
            if (myGlobalPosition.Equals(attackedGlobalPosition) && !animator.GetBool("Death"))
            {
                loseHp();
                gameObject.transform.Translate(playerOrientation);
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
            Vector3 myGlobalPosition = gl.WorldToCell(gameObject.transform.position);

            if (myGlobalPosition.Equals(attackedGlobalPosition) && !isInvulnerable && !animator.GetBool("Death"))
            {
                loseHp();
            }
        }
    }

    private void loseHp()
    {
        hp -= 0.5f;
        if (hp <= 0)
        {
            animator.SetBool("Death", true);
        }
        Debug.Log(hp);
    }
    private void UpdateGrid()
    {
        gl = MapController.currentMap.GetComponent<Grid>();
    }
    private void OnDestroy()
    {
        EnemyEventsManager.Instance.onTakeDamage -= TakeDamage;
        GlobalEventManager.Instance.onMapChanged -= UpdateGrid; 
        EnemyEventsManager.Instance.onTakeProjectileDamage -= TakeProjectileDamage;
    }
}