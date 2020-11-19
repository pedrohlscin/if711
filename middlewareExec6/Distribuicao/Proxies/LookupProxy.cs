using System;
using System.Collections.Generic;
using System.Text;

namespace middlewareExec6.Proxies
{
    class LookupProxy
    {
        public Dictionary<String, AOR> proxies;

        private LookupProxy() 
        {
            proxies = new Dictionary<String, AOR>();
        }

        private static LookupProxy _instance;

        private static readonly object _lock = new object();

        public static LookupProxy GetInstance()
        {
            if(_instance == null)
            {
                lock (_lock)
                {
                    if(_instance == null)
                    {
                        _instance = new LookupProxy();
                    }
                }
            }
            return _instance;
        }

        public AOR Lookup(String proxy)
        {
            AOR proxyDesired;
            if (!proxies.TryGetValue(proxy, out proxyDesired))
            {
                return null;
            }
            return proxyDesired;
        }

        public void Register(String proxy, AOR aor)
        {
            proxies.Add(proxy, aor);
        }
    }
}
