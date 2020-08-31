using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Log : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.DisableTop(true);
        //Master.BanSingleUsed = 1;
        //using (SqlConnection connection = new SqlConnection(""))
        //{
        //    SqlCommand command = new SqlCommand("", connection);
        //    command.Parameters.Add("@ID", SqlDbType.Int);
        //    command.Parameters["@ID"].Value = customerID;

        //    // Use AddWithValue to assign Demographics.
        //    // SQL Server will implicitly convert strings into XML.
        //    command.Parameters.AddWithValue("@demographics", demoXml);

        //    try
        //    {
        //        connection.Open();
        //        Int32 rowsAffected = command.ExecuteNonQuery();
        //        Console.WriteLine("RowsAffected: {0}", rowsAffected);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}