using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelBlockInstance : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //only exectue OnPlayerEnter if the player collides with this level block.
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) OnPlayerEnter(player);
        }

        void OnPlayerEnter(PlayerController player)
        {
            var ev = Schedule<PlayerEnteredLevelBlock>();
            ev.levelBlock = this;
            //ev.player = player;
        }
    }
}
