using SQLite.Net;

namespace MatematikOyun.App_Data
{
    public interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }
}
