using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class EnemyEmitterController : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject enemy;
        public GameObject enemyAlt;
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

                nextUpdate = Mathf.FloorToInt(Time.time) + Random.Range(1, 4);
                int spawnCount = spawnPoints.Length;
                int myRandomIndex = Random.Range(0, spawnCount);
                bool isAltEnemy = (Random.value > 0.5f);
                if (isAltEnemy) 
                {
                    Instantiate(enemyAlt, spawnPoints[myRandomIndex].position, Quaternion.identity);
                }
                else
                {
                    Instantiate(enemy, spawnPoints[myRandomIndex].position, Quaternion.identity);
                }
            }
            
        }
    }
}
