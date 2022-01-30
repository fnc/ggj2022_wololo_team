using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class EnemyEmitterController : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject enemy;
        private int nextUpdate = 1;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // If the next update is reached
            if (Time.time >= nextUpdate)
            {                

                nextUpdate = Mathf.FloorToInt(Time.time) + 3;
                int spawnCount = spawnPoints.Length;
                int myRandomIndex = Random.Range(0, spawnCount);

                Instantiate(enemy, spawnPoints[myRandomIndex].position, Quaternion.identity);
            }
            
        }
    }
}
