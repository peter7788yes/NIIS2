<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YellowCard_Add.aspx.cs" Inherits="Vaccination_RecordM_YellowCard_Add" MasterPageFile="~/MasterPage/MasterPage.master" %>
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

    <section class="Content2">
        <h2>申請黃卡紀錄</h2>
       <form id="form1" runat="server" defaultbutton="btnSave">
            <div class="formTb formTb2">
                <table>
                    <tr>
                        <th scope="row">核發日期：</th>
                        <td>
                            <asp:TextBox ID="tbDate" CssClass="text02" ClientIDMode="Static"   runat="server" onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                            <a href="#"><img onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/></a>

                        </td>
                    </tr>
                    <tr>
                        <th scope="row">核發地點：</th>
                        <td>
                            <asp:TextBox ID="tbLocation" CssClass="text01" ClientIDMode="Static"  runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">承辦人：</th>
                        <td>
                            <asp:TextBox ID="tbUser" CssClass="text02" ClientIDMode="Static" Enabled="false" runat="server"  />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">核發格式：</th>
                        <td>
                            <asp:RadioButton GroupName="rbGroup" ID="rbYellowCard" Text="黃卡" ClientIDMode="Static" runat="server" CssClass="radio01" />
                            <asp:RadioButton GroupName="rbGroup" ID="rbRecord" Text="預防接種紀錄" ClientIDMode="Static" runat="server" CssClass="radio01" />
                        </td>
                    </tr>
                </table>
                <div class="wordred">(請於個案紙本黃卡加註更新日期)</div>
            </div>
            <div class="formBtn">
                <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
                <input type="button" name="closeBtn" value="取消" class="btn" onclick="window.close();" />
            </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/YellowCard_Add_JS")%>
    <%--<%:Scripts.Render("~/bundles/Date_JS")%>--%>
    <script src="../../js/date/calendar.js"></script>
    <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>

