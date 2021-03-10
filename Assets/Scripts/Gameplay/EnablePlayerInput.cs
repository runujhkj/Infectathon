using Infectathon.Core;
using Infectathon.Model;

namespace Infectathon.Gameplay
{
    /// <summary>
    /// This event is fired when user input should be enabled.
    /// </summary>
    public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
    {
        InfectathonModel model = Simulation.GetModel<InfectathonModel>();

        public override void Execute()
        {
            var player = model.player;
            player.controlEnabled = true;
        }
    }
}