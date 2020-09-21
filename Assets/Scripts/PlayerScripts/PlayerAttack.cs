using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack
{
    public Transform playerTransform;
    public Animator playerAnimator;
    public float _fireManaCost, _waterManaCost, _rockManaCost, _windManaCost;
    public Object prefab = Resources.Load("FireAttack");
    private GridLayout gl;
    private bool attackClick;


    public PlayerAttack() { }
    public PlayerAttack(Transform pTransform, Animator pAnimator, PlayerSO player, GridLayout map)
    {
        attackClick = false;
        playerTransform = pTransform;
        playerAnimator = pAnimator;
        _fireManaCost = player.fireManaCost;
        _waterManaCost = player.waterManaCost;
        _rockManaCost = player.windManaCost;
        _windManaCost = player.rockManaCost;
        gl = map;
        PressedKeyEventManager.Instance.onAttackKeyPress += Attack;
        PressedKeyEventManager.Instance.onFireAttackKeyPress += FireAttack;
        GlobalEventManager.Instance.onMapChanged += UpdateGrid;
    }

    public IEnumerator AttackAnimation()
    {
        while (true)
        {
            if (attackClick)
            {
                playerAnimator.SetBool("attack", true);
                yield return new WaitForEndOfFrame();
                playerAnimator.SetBool("attack", false);
                attackClick = false;
                yield return new WaitForSeconds(.2f);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public void Attack()
    {
        // La posición que tengo delante como Vector3Int, le pregunto al EnemyController que si en esa posición se encuentra algun enemigo
        // En caso positivo le hacemos trigger de la función para hacer daño y retroceder 1 casilla
        // Para saber la casilla a la que tiene que moverse hacia atrás (Dependiendo de donde le de el player)
        // Le podría pasar la dupla de X/Y del animator.
        float xRotation = playerAnimator.GetFloat("moveX");
        float yRotation = playerAnimator.GetFloat("moveY");

        Vector3 attackPosition = new Vector3(xRotation, yRotation, playerTransform.position.z);
        Vector3Int attackCell = gl.WorldToCell(playerTransform.position + attackPosition);
        EnemyEventsManager.Instance.TakeDamage(new Vector2(xRotation, yRotation), attackCell);
        attackClick = true;
    }

    public void FireAttack()
    {
        float xRotation = playerAnimator.GetFloat("moveX");
        float yRotation = playerAnimator.GetFloat("moveY");
        if (Inventory.instance.ContainsItemName("Fire Scroll") && Inventory.instance.actualMana >= _fireManaCost)
        {
            // Implement Fire attack
            GlobalEventManager.Instance.DecreaseMana(_fireManaCost);
            Inventory.instance.actualMana -= _fireManaCost;
            InstantiatePrefab(xRotation,yRotation);
        }
    }

    public void InstantiatePrefab(float xRotation, float yRotation)
    {
        GameObject newObject = prefab as GameObject;
        PlayerFireAttack yourObject = newObject.GetComponent<PlayerFireAttack>();
        yourObject.orientation = new Vector2(xRotation,yRotation);
        GameObject.Instantiate(prefab, playerTransform.position, Quaternion.identity);
    }
    private void UpdateGrid()
    {
        gl = MapController.currentMap.GetComponent<Grid>();
    }

    ~PlayerAttack()
    {
        PressedKeyEventManager.Instance.onAttackKeyPress -= Attack;
        PressedKeyEventManager.Instance.onFireAttackKeyPress -= FireAttack;
        GlobalEventManager.Instance.onMapChanged -= UpdateGrid;
    }
}
