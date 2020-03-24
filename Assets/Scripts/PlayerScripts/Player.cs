using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player { 
    public class Player : MonoBehaviour
    {
        public PlayerSO player;
        public PlayerMovement playerMovement;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f, transform.position.z);
            playerMovement = new PlayerMovement(transform);
            playerMovement.SetPlayerSpeed(player.speed);
            playerMovement.SetActualSpeed(player.speed);
            playerMovement.SetRunningSpeed(player.runningSpeed);
            new WaitForEndOfFrame();
            StartCoroutine(playerMovement.Move());
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        
    }
}
