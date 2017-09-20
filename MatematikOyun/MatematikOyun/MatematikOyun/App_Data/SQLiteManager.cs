using MatematikOyun.Model;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MatematikOyun.App_Data
{
    public class SQLiteManager
    {
        SQLiteConnection _sqlconnection;

        public SQLiteManager()
        {
            _sqlconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqlconnection.CreateTable<OyunDetaylari>();
        }
        public int Insert(OyunDetaylari o)
        {
            return _sqlconnection.Insert(o);
        }
        public int Delete(int ID)
        {
            return _sqlconnection.Delete<OyunDetaylari>(ID);
        }
        public IEnumerable<OyunDetaylari> GetAll()
        {
            return _sqlconnection.Table<OyunDetaylari>();
        }
        public OyunDetaylari Get(int skor)
        {
            return _sqlconnection.Table<OyunDetaylari>().Where(x => x.Skor == skor).FirstOrDefault();
        }
        public void Dispose()
        {
            _sqlconnection.Close();
        }
    }
}
