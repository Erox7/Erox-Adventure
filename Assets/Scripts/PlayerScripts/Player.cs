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
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            playerMovement = new PlayerMovement(transform);
            playerMovement.SetSpeed(player.speed);
            playerMovement.SetRunningSpeed(player.runningSpeed);
            StartCoroutine(playerMovement.Move());
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        
    }
}
