using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraetorianC.Commands
{
    abstract class AppComands
    {
        public enum ClientCommands { LOAD, REG, AUTH, CONNECT, PUSH };
        public enum ServerAnswers { OK, NOPE};
    }
}
