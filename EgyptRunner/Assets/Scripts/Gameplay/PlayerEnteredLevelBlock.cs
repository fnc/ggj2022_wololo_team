using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class PlayerEnteredLevelBlock : Simulation.Event<PlayerEnteredDeathZone>
    {
        public LevelBlockInstance levelBlock;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            Debug.Log("Entro a level block");
            Simulation.Schedule<CreateLevelBlock>(0);
        }
    }
}
