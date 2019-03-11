using System.Net.Http;
using Task2.Database;

namespace Task2
{
    static class App
    {
        static DataBase _db;
        static HttpClient _client;
        
        public static DataBase DB
        {
            get {
                if(_db == null) {
                    _db = new DataBase();
                }
                return _db;
            }
        }

        public static HttpClient Client
        {
            get {
                if(_client == null) {
                    _client = new HttpClient();
                }
                return _client;
            }
        }
    }
}
