using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAD_MAN_Ping_Pong
{
    internal class MovePlayerCommand : ICommand
    {
        private Control playerPlatform;

        public MovePlayerCommand(Control platform)
        {
            playerPlatform = platform;
        }

        public void Execute(int mouseY)
        {
            if (mouseY > 60 && mouseY < playerPlatform.Parent.Height - playerPlatform.Height - 50)
            {
                playerPlatform.Location = new Point(playerPlatform.Location.X, mouseY - 45);
            }
        }
    }
}
