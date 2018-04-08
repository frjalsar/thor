using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;


namespace MotFRI
{
    public class MotADO
    {
        private String ConnStr;

        public string ValinnVollur;
        public string ValinnStadur;
        public string ValidUtiInni;
        public string ValinnFjoldiBrauta;
        public string ValinnFjBeinnaBrauta;

        public MotADO()
        {
//            ConnStr = "Password=Langst0kk.;User ID=FyrirspurnIFrjalsar;" +  //ÞARF AÐ BREYTA!!!
//                "Initial Catalog=Athletics;" +
//                "Data Source=GREENQL-03G8BO5\\SQLEXPRESS";

            // Within the code body set your variable    
            ConnStr = ConfigurationManager.ConnectionStrings["AthleticsConnectionString"].ConnectionString;
        }
               
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnStr;
            connection.Open();

            return connection;
        }

        public string SkilaNaestaMotanr ()
        {
            string SidastaTaknMots = "";
            string NaestaTaknMots;
            //string Tolustafahluti;
            string Tolustafahluti2;
            Int32 Tolust = 0;
            //int TolustafirByrjaI = -1;
            //int ix = 0;

            SqlConnection objConn = OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 Code FROM [Athletics].[dbo].[Athl$Competition] WHERE Code LIKE 'M-%' order by Code DESC", objConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Comp");
            DataTable dt = ds.Tables["Comp"];
            SidastaTaknMots = "";
            foreach (DataRow row in dt.Rows)
            {
                SidastaTaknMots = Convert.ToString(row[0]);
            }
            //{
            //    while ((ix < SidastaTaknMots.Length) && (TolustafirByrjaI < 0))
            //    {
            //        if (" 0123456789".IndexOf(SidastaTaknMots.Substring(ix, 1)) > 0)
            //            TolustafirByrjaI = ix;
            //        ix = ix + 1;
            //    }
            //    Tolustafahluti = SidastaTaknMots.Substring(TolustafirByrjaI);
            //    Tolust = Convert.ToInt32(Tolustafahluti);
            //    Tolust = Tolust + 1;
            //    Tolustafahluti2 = Convert.ToString(Tolust);
            //    NaestaTaknMots = SidastaTaknMots.Substring(0, TolustafirByrjaI);
            //    NaestaTaknMots = NaestaTaknMots + "0000000000".Substring(1, 10 - NaestaTaknMots.Length - Tolustafahluti2.Length) + Tolustafahluti2;
            //}

            if (SidastaTaknMots == String.Empty)
            {
                NaestaTaknMots = "M-00000001";
            }
            else
            {
                Tolust = Convert.ToInt32(SidastaTaknMots.Substring(2));
                Tolust = Tolust + 1;
                Tolustafahluti2 = Convert.ToString(Tolust);
                NaestaTaknMots = "M-" + "0000000000".Substring(1, 8 - Tolustafahluti2.Length) + Tolustafahluti2;
            }

            return NaestaTaknMots;

        }

        public void GeymaVallarupplys(String _ValinnVollur)
        {
            //[FrjalsarProject].[dbo].[Frjálsar Íþróttir$motedagotuhlaup]

            SqlConnection objConn = OpenConnection();
            //string SqlGetVollur = "SELECT * FROM [Frj].[dbo].[Frjálsar Íþróttir$Keppnisvöllur] where [Heiti] = " +
            string SqlGetVollur = "SELECT * FROM [Athletics].[dbo].[Athl$Venues] WHERE [Heiti] = '" +
              _ValinnVollur + "'";
            SqlDataAdapter da = new SqlDataAdapter(SqlGetVollur, objConn);
            ValinnStadur = "";
            ValidUtiInni = "";

            DataSet ds = new DataSet();
            da.Fill(ds, "Venues");
            DataTable dt = ds.Tables["Venues"];
            foreach (DataRow row in dt.Rows)
            {
                ValinnVollur = Convert.ToString(row[1]);
                ValinnStadur = Convert.ToString(row[2]);
                ValidUtiInni = Convert.ToString(row[3]);
                ValinnFjoldiBrauta = Convert.ToString(row[4]);
                ValinnFjBeinnaBrauta = Convert.ToString(row[5]);
            }

        }


        public DataSet InsertMot(
            String _Heiti, 
            String _EnsktHeiti, 
            DateTime _Dags, 
            String _Vollur,
            String _Stadur, 
            String _UtiInni, 
            String _Motshaldari, 
            String _Yfirdomari,
            String _SkranGjPrGrein, 
            String _SkranGjaldPrBodhl, 
            bool _ReiknaUnglStig, 
            Int16 _TegStigakeppni)

            
        {
            string DagsText;


            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            DataSet ds = null;

            string NaestaTaknMots;

            try
            {
                NaestaTaknMots = SkilaNaestaMotanr();

                //string UtanEdaInnan = "";
                //if (Convert.ToString(_UtiInni) == "0")
                //    UtanEdaInnan = "Utanhúss";
                //else
                //    UtanEdaInnan = "Innanhúss";
                Int16 UtIn = -1;
                string ReiknaUnglSt;

                if (_UtiInni == "Utanhúss")
                {
                    UtIn = 0;
                }
                else
                {
                    UtIn = 1;
                }
                if (_ReiknaUnglStig)
                {
                    ReiknaUnglSt = "1";
                }
                else
                {
                    ReiknaUnglSt = "0";
                }
                _Motshaldari = _Motshaldari.ToUpper();
                DagsText = _Dags.ToString("yyyy.MM.dd");


                connection = OpenConnection();

                string SQLIns = "INSERT INTO [Frj_AllowNull].[dbo].[Frjálsar Íþróttir$motedagotuhlaup] " +
                "(mot, heiti, ensktheitiamoti, dagsetning, keppnisvollur, stadur, " +
                "utiinni, hlaupedamot, motshaldari, yfirdomari, [Reikna unglingastig], " +
                 "skraningargjaldprgrein, [Skráningargjld f_ boðhlaup], tegundstigakeppni) " +
                "VALUES " +
                "('" + NaestaTaknMots + "','" +
                _Heiti + "','" +
                _EnsktHeiti + "','" +
                DagsText + "','" +
                _Vollur + "','" +
                _Stadur + "','" +
                Convert.ToString(UtIn) + "','" +
                "1" + "','" +  //Always 1 (Athletic Competition and not Road Race)
                _Motshaldari + "','" +
                _Yfirdomari + "','" +
                ReiknaUnglSt + "','" +
                Convert.ToString(_SkranGjPrGrein) + "','" +
                Convert.ToString(_SkranGjaldPrBodhl) + "','" +
                Convert.ToString(_TegStigakeppni) + "')";

                command = new SqlCommand(SQLIns, connection);
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "Mot");

            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                throw ex;
            }
            finally
            {
                if (adapter != null)
                    adapter.Dispose();
                if (command != null)
                    command.Dispose();
                if (connection != null)
                    connection.Dispose();
            }
            return ds;
        }

        public DataSet ListiYfirMot(string _Ar)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            DataSet ds = null;

            try
            {
                connection = OpenConnection();
                string strSQL = "SELECT heiti, ensktheitiamoti, convert(varchar, [dagsetning], 104) as Dags, keppnisvollur, stadur, " +
                " utiinni as [Úti/Inni], motshaldari " +
                  "FROM [Frj_AllowNull].[dbo].[Frjálsar Íþróttir$motedagotuhlaup] where (DATEPART(Year,dagsetning) = " + _Ar + ") order by dagsetning";
                command = new SqlCommand(strSQL, connection);
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "Mot");
                return ds;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                throw ex;
            }
            finally
            {
                if (adapter != null)
                    adapter.Dispose();
                if (command != null)
                    command.Dispose();
                if (connection != null)
                    connection.Dispose();

            }


        }
        public DataSet InsertVoll(String _HeitiVallar, String _Stadur, String _UtiInni, String _FjBeinnaBrauta, String _FjHringbrauta)
        {

            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            DataSet ds = null;
             
            try
            {
             
                connection = OpenConnection();

                string SQLIns = "INSERT INTO [Athletics].[dbo].[Athl$Venues] " +
                  "([Heiti], [Staður], [Úti_Inni], [Hringhlaup á hallandi braut], [Fjöldi beinna brauta], " +
                  "[Opnunardagsetning], [Fj_ hringbrauta spretthlaup], [Fj_ hingbrauta millivegalengd], " +
                  "[Fj_ hringbrauta langhlaup]) " +
                  "VALUES " +
                  "('" + _HeitiVallar + "','" +
                  _Stadur + "'," +
                  _UtiInni + "," +
                  "0," + 
                  _FjBeinnaBrauta + "," +
                  "'1753-01-01 00:00:00.000'" + "," +
                  _FjHringbrauta + "," + 
                  "0" + "," +
                  "0" + ")";

      //          ,[Hringhlaup á hallandi braut]
      //,[Fj_ hringbrauta spretthlaup]
      //,[Fjöldi beinna brauta]
      //,[Opnunardagsetning]
      //,[Fj_ hingbrauta millivegalengd]
      //,[Fj_ hringbrauta langhlaup]


                command = new SqlCommand(SQLIns, connection);
                adapter = new SqlDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds, "Venues");

            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                throw ex;
            }
            finally
            {
                if (adapter != null)
                    adapter.Dispose();
                if (command != null)
                    command.Dispose();
                if (connection != null)
                    connection.Dispose();
            }
            return ds;
        }
  
        
    }
}




        

    //    public DataSet UpdateNews(String _fyrirsogn, String _formali, String _meginmal, String _hofundur, String _id)
    //    {

    //        OleDbConnection connection = null;
    //        OleDbCommand command = null;
    //        OleDbDataAdapter adapter = null;
    //        DataSet ds = null;

    //        try
    //        {
    //            connection = OpenConnection();
    //            String strSQL = "Update Frettir Set fyrirsogn = '" + _fyrirsogn + "', formali = '" + _formali +
    //                "', meginmal = '" + _meginmal + "', hofundur = '" + _hofundur + "', Where Id = '" + _id + "' ";

    //            command = new OleDbCommand(strSQL, connection);
    //            adapter = new OleDbDataAdapter(command);
    //            ds = new DataSet();

    //            adapter.Fill(ds, "News");

    //        }

    //        catch (Exception ex)
    //        {
    //            Debug.Write(ex);
    //            throw ex;
    //        }
    //        finally
    //        {
    //            if (adapter != null)
    //                adapter.Dispose();
    //            if (command != null)
    //                command.Dispose();
    //            if (connection != null)
    //                connection.Dispose();

    //        }

    //        return ds;
    //    }


    //        public DataSet DeleteNews(String _id)
    //        {
                              
    //        OleDbConnection connection = null;
    //        OleDbCommand command = null;
    //        OleDbDataAdapter adapter = null;
    //        DataSet ds = null;

    //            try 
    //            {
    //                connection = OpenConnection();
    //                String strSQL = "Delete From Frettir Where ID Like '" + _id + "' ";
    //                command = new OleDbCommand(strSQL, connection); 
    //                adapter = new OleDbDataAdapter(command);
    //                ds = new DataSet();

    //                adapter.Fill(ds, "News");

    //            }
    //            catch (Exception ex)
    //            {
    //                Debug.Write(ex);
    //                throw ex;

    //            }
    //            finally
    //            {
    //                if (adapter != null)
    //                adapter.Dispose();
    //            if (command != null)
    //                command.Dispose();
    //            if (connection != null)
    //                connection.Dispose();
    //            }

    //            return ds;

    //        }

    //        public DataSet LoadDefaultNews()
    //        {


    //            OleDbConnection connection = null;
    //            OleDbCommand command = null;
    //            OleDbDataAdapter adapter = null;
    //            DataSet ds = null;


    //            try
    //            {
    //                connection = OpenConnection();
    //                String strSQL = "";
    //                strSQL = "select top 4 * from Frettir Order by dagsetning Desc, timi desc";
    //                command = new OleDbCommand(strSQL, connection);
    //                adapter = new OleDbDataAdapter(command);
    //                ds = new DataSet();

    //                adapter.Fill(ds, "Listi");
    //            }

    //            catch (Exception ex)
    //            {
    //                Debug.Write(ex);
    //                throw ex;

    //            }
    //            finally
    //            {
    //                if (adapter != null)
    //                    adapter.Dispose();
    //                if (command != null)
    //                    command.Dispose();
    //                if (connection != null)
    //                    connection.Dispose();
    //            }

    //            return ds;
    //        }

    //        public DataSet LoadOneNews(String _id)
    //        {

    //            OleDbConnection connection = null;
    //            OleDbCommand command = null;
    //            OleDbDataAdapter adapter = null;
    //            DataSet ds = null;


    //            try
    //            {
    //                connection = OpenConnection();
    //                String strSQL = "";
    //                strSQL = "select * from Frettir Where Id like '" + _id + "' ";
    //                command = new OleDbCommand(strSQL, connection);
    //                adapter = new OleDbDataAdapter(command);
    //                ds = new DataSet();

    //                adapter.Fill(ds, "Listi");
    //            }

    //            catch (Exception ex)
    //            {
    //                Debug.Write(ex);
    //                throw ex;

    //            }
    //            finally
    //            {
    //                if (adapter != null)
    //                    adapter.Dispose();
    //                if (command != null)
    //                    command.Dispose();
    //                if (connection != null)
    //                    connection.Dispose();
    //            }

    //            return ds;
    
    //        }

    //    public DataSet LoadAllNews()
    //    {

    //        OleDbConnection connection = null;
    //        OleDbCommand command = null;
    //        OleDbDataAdapter adapter = null;
    //        DataSet ds = null;


    //        try
    //        {
    //            connection = OpenConnection();
    //            String strSQL = "";
    //            strSQL = "select * from Frettir";
    //            command = new OleDbCommand(strSQL, connection);
    //            adapter = new OleDbDataAdapter(command);
    //            ds = new DataSet();

    //            adapter.Fill(ds, "FrettirList");
    //        }

    //        catch (Exception ex)
    //        {
    //            Debug.Write(ex);
    //            throw ex;

    //        }
    //        finally
    //        {
    //            if (adapter != null)
    //                adapter.Dispose();
    //            if (command != null)
    //                command.Dispose();
    //            if (connection != null)
    //                connection.Dispose();
    //        }

    //        return ds;
    

    //    }



    //    }
        

    //}
