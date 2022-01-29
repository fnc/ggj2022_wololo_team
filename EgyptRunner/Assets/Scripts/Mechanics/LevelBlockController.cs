using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.Mechanics
{
    public class LevelBlockController : MonoBehaviour
    {
        public LevelBlockInstance[] levelBlocks;

        public LevelBlockInstance currentLevelBlock { get; set; }
        public LevelBlockInstance nextLevelBlock { get; set; }
        private float lastXPosition = 0f;

        void Awake()
        {
            currentLevelBlock = Instantiate(levelBlocks[0], new Vector3(0, 0, 0), Quaternion.identity).GetComponent<LevelBlockInstance>();
            nextLevelBlock = currentLevelBlock;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CreateNextBlock()
        {
            Debug.Log("Next Block Creation.");
            var currentBlockWidth = currentLevelBlock.gameObject.GetComponent<Collider2D>().bounds.size.x;
            lastXPosition += currentBlockWidth;
            currentLevelBlock = nextLevelBlock;
            Debug.Log(lastXPosition);
            nextLevelBlock = Instantiate(levelBlocks[0], new Vector3(lastXPosition, 0, 0), Quaternion.identity);
        }
    }
}