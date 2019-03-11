using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Database;

namespace Task2
{
    static class App
    {
        static DataBase _db;
        
        public static DataBase DB
        {
            get {
                if(_db == null) {
                    _db = new DataBase();
                }
                return _db;
            }
        }
    }
}
