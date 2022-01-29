using Platformer.Core;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="PlayerStopJump"></typeparam>
    public class CreateLevelBlock : Simulation.Event<CreateLevelBlock>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var levelController = model.level;
            levelController.CreateNextBlock();
        }
    }
}