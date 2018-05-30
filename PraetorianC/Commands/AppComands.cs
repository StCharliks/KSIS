using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraetorianC.Commands
{
    abstract class AppComands
    {
        public enum ClientCommands { LOAD, REG, SEND ,AUTH, CONNECT};
        public enum ServerAnswers { OK, NOPE};
    }
}
