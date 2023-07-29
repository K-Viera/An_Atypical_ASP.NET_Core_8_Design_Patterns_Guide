using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class MySingleton
    {
        private static MySingleton? _instance;
        private static readonly object _myLock = new();
        private MySingleton() { }
        public static MySingleton Create()
        {
            lock(_myLock) { _instance ??= new MySingleton(); }
            return _instance;
        }
    }
    public class MySimpleSingleton
    {
        public static MySimpleSingleton Instance { get; } = new MySimpleSingleton();
        private MySimpleSingleton() { }
    }
}
