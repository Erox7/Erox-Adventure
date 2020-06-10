using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player { 
    public class Player : MonoBehaviour
    {
        public PlayerSO player;
        public PlayerMovement playerMovement;
        public PlayerAttack playerAttack;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, transform.position.z);
            playerMovement = new PlayerMovement(transform, GetComponent<Animator>());
            playerMovement.SetPlayerSpeed(player.speed);
            playerMovement.SetActualSpeed(player.speed);
            playerMovement.SetRunningSpeed(player.runningSpeed);
            playerAttack = new PlayerAttack(transform, GetComponent<Animator>());
            new WaitForEndOfFrame();
            StartCoroutine(playerMovement.Move());
            StartCoroutine(playerAttack.AttackAnimation());
        }        
    }
}
