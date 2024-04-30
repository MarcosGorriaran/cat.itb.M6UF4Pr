using System;
using Npgsql;

namespace cat.itb.gestioHR.connections
{
    public class SQLConnection
    {
        private String HOST = "balarama.db.elephantsql.com:5432"; // Ubicació de la BD.
        private String DB = "qylrvsaa"; // Nom de la BD.
        private String USER = "qylrvsaa";
        private String PASSWORD = "WApqM0DJGoMManfagt-fBh-8r8wrRUyI";

        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(
                "Host=" + HOST + ";" + "Username=" + USER + ";" +
                "Password=" + PASSWORD + ";" + "Database=" + DB + ";"
            );
            conn.Open();
            return conn;
        }
    }
}