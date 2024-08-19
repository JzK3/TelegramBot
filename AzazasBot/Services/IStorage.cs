using AzazasBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzazasBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatID);
    }
}
