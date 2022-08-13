using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


public class cSQL
{
    private String Conexion()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["API_IA_GOOGLEContext"].ToString();
    }
        


    public DataSet EjecutarSQL_DS(String sSQL)
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        try
        {
            cn = new SqlConnection(Conexion());
            cmd = new SqlCommand(sSQL, cn);
            cmd.CommandType = CommandType.Text;
            //Cmnd.CommandTimeout = 800
            cn.Open();
            da.SelectCommand = cmd;
            da.Fill(ds);
            int tablas = ds.Tables.Count;
            int contador = 0;
            while (contador <= (tablas - 1))
            {
                DataTable dt = ds.Tables[contador];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.IsNull(0))
                    {
                        dr.Delete();
                    }
                }
                dt.AcceptChanges();
                contador = contador + 1;
            }
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            cmd.Dispose();
            da.Dispose();
        }
    }
    public DataTable Ejecutar_StoredProcedure(String sStoredProcedure, Hashtable ht_Parametros)
    {
            SqlConnection Cnx = new SqlConnection();
        SqlCommand Cmnd = new SqlCommand();
        SqlDataAdapter AdapterSql = new SqlDataAdapter();
        DataTable dt = new DataTable();

        try
        {
            Cnx = new SqlConnection(Conexion());
            Cmnd = new SqlCommand(sStoredProcedure, Cnx);
            Cmnd.CommandType = CommandType.StoredProcedure;
            Cmnd.CommandTimeout = 900;

            if (!(ht_Parametros == null))
            {
                foreach (DictionaryEntry Fila in ht_Parametros)
                {
                    Cmnd.Parameters.AddWithValue(Fila.Key.ToString(), Fila.Value);
                    //if (Fila.Value.ToString() == String.Empty || Fila.Value.ToString() == "Selecciona una opción")
                    //{
                    //    Cmnd.Parameters.AddWithValue(Fila.Key.ToString(), DBNull.Value);
                    //}
                    //else
                    //{
                    //    Cmnd.Parameters.AddWithValue(Fila.Key.ToString(), Fila.Value);
                    //}
                }
            }

            Cnx.Open();
            AdapterSql.SelectCommand = Cmnd;
            AdapterSql.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.IsNull(0))
                {
                    dr.Delete();
                }
            }

            dt.AcceptChanges();
        }
        catch (Exception ex)
        {
            dt.Columns.Add("error", typeof(String));
            DataRow workRow = dt.NewRow();
            dt.Columns.Add("exception", typeof(String));
            workRow["error"] = "error";
            workRow["exception"] = ex.ToString();
            dt.Rows.Add(workRow);
            //cle.Log("Funcion: " + ex.TargetSite.Name + " Error:" + ex.Message.ToString());
        }
        finally
        {
            Cnx.Close();
            Cnx.Dispose();
            Cmnd.Dispose();
            AdapterSql.Dispose();
        }
        return dt;
    }
    public DataTable EjecutarSQL_DT(String sSQL)
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cn = new SqlConnection(Conexion());
            cmd = new SqlCommand(sSQL, cn);
            cmd.CommandType = CommandType.Text;
            //Cmnd.CommandTimeout = 800
            cn.Open();
            da.SelectCommand = cmd;
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.IsNull(0))
                {
                    dr.Delete();
                }
            }
            dt.AcceptChanges();
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            cmd.Dispose();
            da.Dispose();
        }
    }
    public DropDownList Cargar_Combo(DropDownList ddl, String sSQL)
    {
        try
        {
            ddl.ClearSelection();
            ddl.Items.Clear();
            ddl.DataSource = EjecutarSQL_DT(sSQL);
            ddl.DataTextField = "descripcion";
            ddl.DataValueField = "value";
            ddl.DataBind();
            ddl.Items.Insert(0, "-");
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }
        return ddl;
    }

    public ListBox Cargar_ListBox(ListBox lst, String sSQL)
    {
        try
        {
            lst.ClearSelection();
            lst.Items.Clear();
            lst.DataSource = EjecutarSQL_DT(sSQL);
            lst.DataTextField = "descripcion";
            lst.DataValueField = "value";
            lst.DataBind();
            //ddl.Items.Insert(0, "-");
            //ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }
        return lst;
    }



}

