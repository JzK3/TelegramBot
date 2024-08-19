using AzazasBot.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzazasBot.Services
{
    public class MemStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }
        public Session GetSession(long chatID)
        {
            if (_sessions.ContainsKey(chatID)) return _sessions[chatID];
            var newSession = new Session { SposobCode = "lg" };
            _sessions.TryAdd(chatID, newSession);
            return newSession;
        }
    }
}
