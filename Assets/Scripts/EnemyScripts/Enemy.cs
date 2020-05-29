using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public string enemyName;
    public float attack;
    public float speed;
    public Grid gl;
    // Start is called before the first frame update 
    void Start()
    {
        EnemyEventsManager.Instance.onTakeDamage += TakeDamage;
        GlobalEventManager.Instance.onMapChanged += UpdateGrid;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(Vector2Int playerOrientation, int attackedGlobalPosition)
    {
        // TODO: Deberia poder preguntarle al map manager que posicion global tengo
        // Mientras tenga la GL puedo sacar yo mismo la posición, pero es bueno? WHO KNOWS
        // Si coincide con la que ataca el jugador tengo que pillar dmg bruh
        /*
         Vector3Int cellPosition = gl.WorldToCell(playerTransform.position + movement + new Vector3(0, -0.5f, 0));
         */
         Vector3Int myGlobalPosition = gl.WorldToCell(this.gameObject.transform.position + new Vector3(0, -0.5f, 0));
        if (myGlobalPosition.Equals(attackedGlobalPosition))
        {
            this.hp -= 0.5f;
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