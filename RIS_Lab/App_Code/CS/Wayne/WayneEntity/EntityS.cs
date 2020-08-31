using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WayneEntity
{
    public class EntityS
    {

        public EntityS()
        {
        }

        public static void FillModel(object model)
        {
            Page handler = HttpContext.Current.Handler as Page;
            FillModel(model, handler.Form);
        }

        public static void FillModel(object model, Control contain)
        {
            if (model == null)
            {
                throw new NullReferenceException();
            }
            ControlCollection controls = contain.Controls;
            Type type = model.GetType();
            foreach (Control control in controls)
            {
                string iD = control.ID;
                if ((iD != null) && iD.StartsWith("S_"))
                {
                    string name = iD.Substring(2);
                    PropertyInfo property = type.GetProperty(name);
                    Type propertyType = type.GetProperty(name).PropertyType;
                    TextBox box = control as TextBox;
                    Label label = control as Label;
                    if ((box != null) || (label != null))
                    {
                        property.SetValue(model, Convert.ChangeType(((ITextControl) control).Text, propertyType), null);
                    }
                    else
                    {
                        DropDownList list = control as DropDownList;
                        if (list != null)
                        {
                            property.SetValue(model, Convert.ChangeType(list.SelectedValue, propertyType), null);
                        }
                        else
                        {
                            CheckBox box2 = control as CheckBox;
                            if (box2 != null)
                            {
                                property.SetValue(model, Convert.ChangeType(box2.Checked, propertyType), null);
                            }
                            else
                            {
                                HiddenField field = control as HiddenField;
                                if (field != null)
                                {
                                    property.SetValue(model, Convert.ChangeType(field.Value, propertyType), null);
                                }
                                else
                                {
                                    Literal literal = control as Literal;
                                    if (literal != null)
                                    {
                                        property.SetValue(model, Convert.ChangeType(literal.Text, propertyType), null);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void FillModel<T>(List<T> list, DataTable dt)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            foreach (DataRow row in dt.Rows)
            {
                T model = (T) typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null);
                FillModel(model, row);
                list.Add(model);
            }
        }



        static void BaseFillModel<T>(T model, DataRow dr)
        {
            if (model == null)
            {
                throw new NullReferenceException();
            }
            Type type = model.GetType();
            if (dr != null)
            {
                foreach (DataColumn column in dr.Table.Columns)
                {
                    string columnName = column.ColumnName;

                    if (dr[columnName] != null)
                    {
                        PropertyInfo pi = type.GetProperty(columnName);
                        if (pi != null)
                        {
                            Type propertyType = type.GetProperty(columnName).PropertyType;

                            if (dr[columnName] is DBNull)
                            {
                                pi.SetValue(model, null, null);
                            }
                            else
                            {
                                pi.SetValue(model, Convert.ChangeType(dr[columnName], propertyType), null);
                            }
                        }
                    }
                }
            }
        }

        public static void FillModel<T>(T model, DataRow dr)
        {
            BaseFillModel<T>(model,dr);
        }

        
        public static void FillModel<T>(T model, DataTable dt)
        {
            if(dt.Rows.Count > 0)
                FillModel(model, dt.Rows[0]);
        }
    }
}