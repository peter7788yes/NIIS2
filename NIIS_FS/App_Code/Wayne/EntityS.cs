

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WayneEntity
{
    /// <summary>
    /// EncryptT 的摘要描述
    /// </summary>
    public class EntityS
    {

        public EntityS()
        {
            //
            // TODO: 在這裡新增建構函式邏輯
            //
        }

        /// <summary>
        /// 意圖
        /// </summary>
        /// <param name="model">模組</param>
        public static void FillModel(object model)
        {
            Page handler = HttpContext.Current.Handler as Page;
            FillModel(model, handler.Form);
        }

        /// <summary>
        /// 很少用到，要用.Net 控制項
        /// </summary>
        /// <param name="model"></param>
        /// <param name="contain"></param>
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

        /// <summary>
        /// SQL To Entity,
        /// 記住查詢的SQL語法要和Model欄位的大小寫一樣,
        /// model型別:string,int,double,decimal,DateTime?,bool?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="dt"></param>
        public static void FillModel<T>(List<T> list, DataTable dt)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            //輪巡每一筆資料
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
                    //欄位名稱
                    string columnName = column.ColumnName;

                    //判斷是否有這個欄位
                    if (dr[columnName] != null)
                    {
                        //取得屬性資訊物件
                        PropertyInfo pi = type.GetProperty(columnName);
                        if (pi != null)
                        {
                            //獲得型別
                            Type propertyType = type.GetProperty(columnName).PropertyType;

                            if (dr[columnName] is DBNull)
                            {
                                //設定null
                                pi.SetValue(model, null, null);
                            }
                            else
                            {
                                //設定屬性
                                pi.SetValue(model, Convert.ChangeType(dr[columnName], propertyType), null);
                            }
                        }
                    }
                }
            }
        }
       
        //public static void FillModel(object model, DataRow dr)
        //{
        //    BaseFillModel<object>(model, dr);
        //}

       
        //public static void FillModel(object model, DataTable dt)
        //{
        //    if (dt.Rows.Count > 0)
        //        FillModel(model, dt.Rows[0]);
        //}


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