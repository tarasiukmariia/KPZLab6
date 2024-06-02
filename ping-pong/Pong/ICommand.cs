using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD_MAN_Ping_Pong
{
    internal interface ICommand
    {
        void Execute(int mouseY);
    }
}
