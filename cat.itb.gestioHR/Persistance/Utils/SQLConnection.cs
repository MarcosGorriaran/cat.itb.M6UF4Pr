using System;
using Npgsql;

namespace cat.itb.gestioHR.connections
{
    public class SQLConnection
    {
        private String HOST = "surus.db.elephantsql.com:5432"; // Ubicació de la BD.
        private String DB = "smxmqzjw"; // Nom de la BD.
        private String USER = "smxmqzjw";
        private String PASSWORD = "O7F6jL3dN89QfFkleiTWKjbAtlGc2gfw";

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