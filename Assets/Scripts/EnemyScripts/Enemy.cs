using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Enemy : MonoBehaviour
{
    public float hp;
    public string enemyName;
    public float attack;
    public float speed;
    public GridLayout gl;
    private Animator animator;
    private MoveBetweenPoints movement;
    // Start is called before the first frame update 
    void Start()
    {
        EnemyEventsManager.Instance.onTakeDamage += TakeDamage;
        GlobalEventManager.Instance.onMapChanged += UpdateGrid;
        gl = MapController.currentMap.GetComponent<GridLayout>();
        animator = GetComponentInParent<Animator>();
        
        movement = new MoveBetweenPoints(this.gameObject,
        new List<Vector3>()
        {
            transform.position,
            new Vector3(13.91f, 10.37f,0),
            new Vector3(7.3f, 7.48f,0)
        },
        speed,
        gl);
        StartCoroutine(movement.StartMoving());
    }

    public void TakeDamage(Vector2 playerOrientation, Vector3 attackedGlobalPosition)
    {
        // TODO: Deberia poder preguntarle al map manager que posicion global tengo
        // Mientras tenga la GL puedo sacar yo mismo la posición, pero es bueno? WHO KNOWS
        // Si coincide con la que ataca el jugador tengo que pillar dmg bruh

        if (gl != null && gl != default) { 
            Vector3 myGlobalPosition = gl.WorldToCell(this.gameObject.transform.position + new Vector3(0, -0.5f, 0));
        
            if (myGlobalPosition.Equals(attackedGlobalPosition) && !animator.GetBool("Death"))
            {
                loseHp();
                gameObject.transform.Translate(playerOrientation);
                movement.updateMovement(transform.position);
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