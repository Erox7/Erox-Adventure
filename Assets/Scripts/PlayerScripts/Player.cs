using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player { 
    public class Player : MonoBehaviour
    {
        public PlayerSO player;
        public PlayerMovement playerMovement;
        public PlayerAttack playerAttack;
        GridLayout gl;
        float hp;

        void Start()
        {
            gl = MapController.currentMap.GetComponent<GridLayout>();
            playerMovement = new PlayerMovement(transform, GetComponent<Animator>(), gl);
            playerMovement.SetPlayerSpeed(player.speed);
            playerMovement.SetActualSpeed(player.speed);
            playerMovement.SetRunningSpeed(player.runningSpeed);
            playerAttack = new PlayerAttack(transform, GetComponent<Animator>(), player ,gl);
            new WaitForEndOfFrame();
            hp = player.hp;
            StartCoroutine(playerMovement.Move());
            StartCoroutine(playerAttack.AttackAnimation());
            EnemyEventsManager.Instance.onMakeDamage += TakeDamage;
            EnemyEventsManager.Instance.onMakeDamageNoKnockBack += TakeDamageNoKnockBack;
            EnemyEventsManager.Instance.onMakeProjectileDamage += TakeBulletDamage;
            GlobalEventManager.Instance.onMapChanged += UpdateGrid;
        }
        
        public void TakeDamage(Vector3Int position, float damage, Vector2 orientation)
        {
            if (position.Equals(gl.WorldToCell(transform.position)))
            {
                hp -= damage;
                gameObject.transform.Translate(orientation);
                if(hp <= 0)
                {
                    GlobalEventManager.Instance.DecreaseHp(hp + damage);
                    Lose();
                } else
                {
                    GlobalEventManager.Instance.DecreaseHp(damage);
                }
            }
        }
        public void TakeDamageNoKnockBack(Vector3 position, float damage)
        {
            if (gl.WorldToCell(position).Equals(gl.WorldToCell(transform.position)))
            {
                hp -= damage;
                if (hp <= 0)
                {
                    GlobalEventManager.Instance.DecreaseHp(hp + damage);
                    Lose();
                }
                else
                {
                    GlobalEventManager.Instance.DecreaseHp(damage);
                }
            }
        }
        public void TakeBulletDamage(Vector3Int position, float damage)
        {
            if (position.Equals(gl.WorldToCell(transform.position)))
            {
                hp -= damage;
                if (hp <= 0)
                {
                    GlobalEventManager.Instance.DecreaseHp(hp + damage);
                    Lose();
                }
                else
                {
                    GlobalEventManager.Instance.DecreaseHp(damage);
                }
            }
        }
        private void Lose()
        {
            transform.GetComponent<Animator>().SetBool("death", true);
            Time.timeScale = 1f;
            GlobalEventManager.Instance.GameOver();
            Debug.Log("You Lost");
        }
        private void UpdateGrid()
        {
            gl = MapController.currentMap.GetComponent<Grid>();
        }

        private void OnDestroy()
        {
            EnemyEventsManager.Instance.onMakeDamage -= TakeDamage;
            EnemyEventsManager.Instance.onMakeDamageNoKnockBack -= TakeDamageNoKnockBack;
            EnemyEventsManager.Instance.onMakeProjectileDamage -= TakeBulletDamage;
            GlobalEventManager.Instance.onMapChanged -= UpdateGrid;
        }
    }
}
