using System;
using System.Text;
using StackExchange.Redis;
using System.Collections.Generic;

namespace PSStackExchange.Redis
{
    public class MyMultiplexer
    {
        private static ConnectionMultiplexer _connectionMultiplexer;

        public static ConnectionMultiplexer Connect(string configuration, bool async = false)
        {
            if(_connectionMultiplexer == null)
            {
                if(async)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
                }
                
            }
            return _connectionMultiplexer;
        }               
    }
}