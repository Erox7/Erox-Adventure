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
        // Start is called before the first frame update
        void Start()
        {
            gl = MapController.currentMap.GetComponent<GridLayout>();
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, transform.position.z);
            playerMovement = new PlayerMovement(transform, GetComponent<Animator>());
            playerMovement.SetPlayerSpeed(player.speed);
            playerMovement.SetActualSpeed(player.speed);
            playerMovement.SetRunningSpeed(player.runningSpeed);
            playerAttack = new PlayerAttack(transform, GetComponent<Animator>(), player.fireManaCost, player.waterManaCost, player.windManaCost, player.rockManaCost);
            new WaitForEndOfFrame();
            hp = player.hp;
            StartCoroutine(playerMovement.Move());
            StartCoroutine(playerAttack.AttackAnimation());
            EnemyEventsManager.Instance.onMakeDamage += TakeDamage;
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
        private void Lose()
        {
            Debug.Log("You Lost");
        }
        private void UpdateGrid()
        {
            gl = MapController.currentMap.GetComponent<Grid>();
        }

        private void OnDestroy()
        {
            EnemyEventsManager.Instance.onMakeDamage -= TakeDamage;
            GlobalEventManager.Instance.onMapChanged -= UpdateGrid;
        }
    }
}
