using System;
using System.Data;
using System.Data.SqlClient;

namespace PopulateDataSet
{

    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class Class1
    {
        static void Main(string[] args)
        {
            string sConnectionString;

            // Modify the following string to correctly connect to your SQL Server.
            sConnectionString = "Password=;User ID=sa;"
                + "Initial Catalog=pubs;"
                + "Data Source=(local)";

            SqlConnection objConn
                = new SqlConnection(sConnectionString);
            objConn.Open();

            // Create an instance of a DataAdapter.
            SqlDataAdapter daAuthors 
                = new SqlDataAdapter("Select * From Authors", objConn);

            // Create an instance of a DataSet, and retrieve 
            // data from the Authors table.
            DataSet dsPubs = new DataSet("Pubs");
            daAuthors.FillSchema(dsPubs,SchemaType.Source, "Authors");
            daAuthors.Fill(dsPubs,"Authors"); 
            //****************
            // BEGIN ADD CODE 
            // Create a new instance of a DataTable.
            DataTable tblAuthors;
            tblAuthors = dsPubs.Tables["Authors"];

            DataRow drCurrent;
            // Obtain a new DataRow object from the DataTable.
            drCurrent = tblAuthors.NewRow();

            // Set the DataRow field values as necessary.
            drCurrent["au_id"] = "993-21-3427";
            drCurrent["au_fname"] = "George";
            drCurrent["au_lname"] = "Johnson";
            drCurrent["phone"] = "800 226-0752";
            drCurrent["address"] = "1956 Arlington Pl.";
            drCurrent["city"] = "Winnipeg";
            drCurrent["state"] = "MB";
            drCurrent["contract"] = 1;

            // Pass that new object into the Add method of the DataTable.
            tblAuthors.Rows.Add(drCurrent);
            Console.WriteLine("Add was successful, Click any key to continue!!");
            Console.ReadLine();

            // END ADD CODE   
            //*****************
            // BEGIN EDIT CODE 

            drCurrent = tblAuthors.Rows.Find("213-46-8915");
            drCurrent.BeginEdit();
            drCurrent["phone"] = "342" + drCurrent["phone"].ToString().Substring(3);
            drCurrent.EndEdit();
            Console.WriteLine("Record edited successfully, Click any key to continue!!");
            Console.ReadLine();
			
            // END EDIT CODE   
            //*****************
            // BEGIN SEND CHANGES TO SQL SERVER 

            SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(daAuthors);
            daAuthors.Update(dsPubs, "Authors");
            Console.WriteLine("SQL Server updated successfully, Check Server explorer to see changes");
            Console.ReadLine();
			
            // END SEND CHANGES TO SQL SERVER 
            //*****************
            //BEGIN DELETE CODE 

            drCurrent = tblAuthors.Rows.Find("993-21-3427");
            drCurrent.Delete();
            Console.WriteLine("SRecord deleted successfully, Click any key to continue!!"); 
            Console.ReadLine();
       
            //END DELETE CODE  
            //*****************
            // CLEAN UP SQL SERVER
            daAuthors.Update(dsPubs, "Authors");
            Console.WriteLine("SQL Server updated successfully, Check Server explorer to see changes");
            Console.ReadLine();         	
			
        }

        internal static void Main()
        {
            throw new NotImplementedException();
        }
    }
}
