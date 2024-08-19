using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzazasBot.Services
{
    public interface IMessageHandler
    {
        string SProcess(string inMessage);
        int IProcess(string inMessage);
    }
}
