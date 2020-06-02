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
            new Vector3(5.906329f,9.879445f,0f)
        },
        speed,
        gl);
        StartCoroutine(movement.moveToPoint(gl.WorldToLocal(new Vector3(6f, 10.03f, 0f))));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(Vector2 playerOrientation, Vector3 attackedGlobalPosition)
    {
        // TODO: Deberia poder preguntarle al map manager que posicion global tengo
        // Mientras tenga la GL puedo sacar yo mismo la posición, pero es bueno? WHO KNOWS
        // Si coincide con la que ataca el jugador tengo que pillar dmg bruh
        /*
         Vector3Int cellPosition = gl.WorldToCell(playerTransform.position + movement + new Vector3(0, -0.5f, 0));
         */
        if (gl != null && gl != default) { 
            Vector3 myGlobalPosition = gl.WorldToCell(this.gameObject.transform.position + new Vector3(0, -0.5f, 0));
        
            if (myGlobalPosition.Equals(attackedGlobalPosition) && !animator.GetBool("Death"))
            {
                loseHp();
                Vector3 cellSize = gl.cellSize;
                this.gameObject.transform.Translate(playerOrientation);
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