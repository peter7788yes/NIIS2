using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// SystemCodeControl 的摘要描述
/// </summary>
public static class SystemCodeControl
{
    public static void ServerSelect(ref DropDownList ddl, string SystemCateName)
    {
        List<SystemCodeVM> SystemCodeList = SystemCode.GetDict(SystemCateName);
        foreach (SystemCodeVM sc in SystemCodeList)
        {
            ddl.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
        }

    }
    public static void ServerCheckBox(ref CheckBoxList cbl, string SystemCateName)
    {
        List<SystemCodeVM> SystemCodeList = SystemCode.GetDict(SystemCateName);
        foreach (SystemCodeVM sc in SystemCodeList)
        {
            cbl.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));
        } 
    }

    public static string HtmlSelect(string inputid, string inputname, string SystemCodeCate, string SelectVal)
    { 
        string select = "<select   {2}  {0}  >{1}</select>";
        string options = "";
        List<SystemCodeVM> SystemCodeList = SystemCode.GetDict(SystemCodeCate);
        foreach (SystemCodeVM sc in SystemCodeList)
            options += string.Format("<option value=\"{1}\" {2} >{0}</option>", sc.EnumName, sc.EnumValue.ToString(), (sc.EnumValue.ToString() == SelectVal ? "selected" : ""));

        return string.Format(select, (inputid != "" ? " id=\"" + inputid + "\"" : ""), options, (inputname != "" ? " name=\"" + inputname + "\"" : ""));
   }


}