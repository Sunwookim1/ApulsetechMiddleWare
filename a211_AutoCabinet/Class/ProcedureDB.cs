using a211_AutoCabinet.Datas;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a211_AutoCabinet.Class
{
    public class ProcedureDB
    {
        private List<ReaderInfo> m_lstReaders = null;
        private List<AntennaInfo> m_lstAntennas = null;

        public int SelectUserData(string ConnectMariaDB, string UserID, string UserPW)
        {
            int UserDataID = 0;
            using (MySqlCommand cmd = (MySqlCommand)GetProcCommand(
                    ConnectMariaDB,
                    "USP_GET_SELECT_USER",
                    new IDataParameter[] {
                        new MySqlParameter("@P_USER_ID", UserID), //UserID
                        new MySqlParameter("@P_USER_PW", UserPW), //UserPW
                    }))
            {
                if (cmd == null)
                {
                    return UserDataID;
                }

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            UserDataID = reader.GetInt32("ID");
                        }
                        catch (Exception ex)
                        {
                            return UserDataID;
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }
            return UserDataID;
        }

        public List<ReaderInfo> SelectDevData(string ConnectMariaDB, int UserDataID)
        {
            m_lstReaders = new List<ReaderInfo>();

            ReaderInfo info = null;

            using (MySqlCommand cmd = (MySqlCommand)GetProcCommand(
                    ConnectMariaDB,
                    "USP_GET_SELECT_READER",
                    new IDataParameter[] {
                        new MySqlParameter("@P_USER_ID", UserDataID), //UserID
                    }))
            {
                if (cmd == null)
                {
                    return m_lstReaders;
                }

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            info = new ReaderInfo(
                                        reader.GetInt32("ID"),
                                        reader.GetString("READER_NAME"),
                                        reader.GetString("IP_ADDRESS"),
                                        reader.GetString("COM_PORT"),
                                        reader.GetInt32("BAUDRATE"),
                                        reader.GetString("DEV_TYPE"),
                                        reader.GetInt32("ANT_COUNT"),
                                        reader.GetInt32("DWELL_TIME"),
                                        reader.GetInt32("TX_ON_TIME"),
                                        reader.GetInt32("TX_OFF_TIME"),
                                        reader.GetString("USE_YN"),
                                        reader.GetDateTime("CREATE_TIME"),
                                        reader.GetDateTime("UPDATE_TIME"),
                                        AccessType.None);

                            m_lstReaders.Add(info);
                        }
                        catch (Exception ex)
                        {
                            return m_lstReaders;
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }
            return m_lstReaders;
        }

        public List<AntennaInfo> SelectAntData(string ConnectMariaDB, int ReaderID, int UserDataID)
        {
            m_lstAntennas = new List<AntennaInfo>();
            AntennaInfo info = null;

            using (MySqlCommand cmd = (MySqlCommand)GetProcCommand(
                    ConnectMariaDB,
                    "USP_GET_SELECT_ANTENNA",
                    new IDataParameter[] {
                        new MySqlParameter("@P_READER_ID", (int)ReaderID), //a211
                        new MySqlParameter("@P_USER_ID", (int)UserDataID), //a211
                    }))
            {
                if (cmd == null)
                {
                    return m_lstAntennas;
                }

                using (MySqlDataReader antenna = cmd.ExecuteReader())
                {
                    while (antenna.Read())
                    {
                        try
                        {
                            info = new AntennaInfo(
                                        antenna.GetInt32("ID"),
                                        antenna.GetInt32("READER_ID"),
                                        antenna.GetInt32("ANT_SEQ"),
                                        antenna.GetInt32("POWER_GAIN"),
                                        antenna.GetInt32("STATE"),
                                        antenna.GetString("USE_YN"),
                                        antenna.GetDateTime("CREATE_TIME"),
                                        antenna.GetDateTime("UPDATE_TIME"),
                                        AccessType.None);

                            m_lstAntennas.Add(info);
                        }
                        catch (Exception ex)
                        {
                            return m_lstAntennas;
                        }
                    }
                    antenna.Close();
                }
                cmd.Dispose();
            }

            return m_lstAntennas;
        }

        public DbCommand GetProcCommand(string connStr, string procName, IDataParameter[] param)
        {
            DbConnection conn;
            DbCommand cmd;

            try
            {
                conn = new MySqlConnection(connStr);
                try { conn.Open(); }
                catch (Exception ex)
                {
                    return null;
                }
                cmd = new MySqlCommand(procName, (MySqlConnection)conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null)
                {
                    foreach (IDataParameter p in param)
                        cmd.Parameters.Add(p);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return cmd;
        }
    }
}
