using System;
using System.Collections.Generic;
using System.Text;

namespace middlewareExec7
{
    class Pool
    {
        public Queue<Server.Server> pool = new Queue<Server.Server>(10);

        private Pool() 
        {
            for(int i = 0; i < 10; i++)
            {
                pool.Enqueue(new Server.Server());
            }
        }

        private static Pool _instance;

        private static readonly object _lock = new object();

        public static Pool GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Pool();
                    }
                }
            }
            return _instance;
        }

        public Server.Server getServer()
        {
            while(pool.Count == 0) { }
            return pool.Dequeue();
        }

        public void putServer(Server.Server param)
        {
            pool.Enqueue(param);
        }
    }
}
