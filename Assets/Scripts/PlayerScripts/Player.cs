using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player { 
    public class Player : MonoBehaviour
    {
        public PlayerSO player;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
