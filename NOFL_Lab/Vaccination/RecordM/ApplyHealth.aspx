<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyHealth.aspx.cs" Inherits="Vaccination_RecordM_ApplyHealth" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">

    <section class="Content3">

            <div class="formBtn formBtnleft">
                 <input type="button" id="saveBtn" value="儲存" class="btn" />
                 <input type="button" id="closeBtn" value="取消" class="btn" />
            </div>

            <div class="formTb formTb2 formTb3">

                        <table>
                            <tr>
                                <th scope="row">建檔者:</th>
                                <td><%=UserName %>;</td>
                                 <th scope="row">評估者：</th>
                                <td>
                                    <select id="selectUser">
                                    </select>
                                    </td>
                                 <th scope="row">評估日期：</th>
                                <td>
                                    <input id="tbDate"  type="text"  
                                        onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                                     <a href="javascript:void(0);">
                                         <img onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" 
                                             src="/images/icon_calendar.png"/>
                                     </a>
                                </td> 
                            </tr>
                            </table>
                        
            </div>
             <div class="formTb formTb2">
                <table id="tb">
                    <tr><td>其他<input id="tbOther" type="text" class="text03"/></td></tr>
                    <tr><td><label><input id="cbAllow" value="99" type="checkbox"/>可接種</label></td></tr>
                </table>   
            </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var stateAry =<%=StateListAry %>;
        var userAry =<%=Users %>;
    </script>
    <%:Scripts.Render("~/bundles/ApplyHealth_JS")%>
    <%--<%:Scripts.Render("~/bundles/Date_JS")%>--%>
    <script src="../../js/date/calendar.js"></script>
    <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>