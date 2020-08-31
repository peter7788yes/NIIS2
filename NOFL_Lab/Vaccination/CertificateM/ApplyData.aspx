<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyData.aspx.cs" Inherits="Vaccination_CertificateM_ApplyData"  MasterPageFile="~/MasterPage/MasterPage.master" %>
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
            <h2>接種證明書</h2>

         <form id="form1" runat="server" defaultbutton="btnSave">
            <div class="formTb formTb2">
                        <table>
                            <tr>
                                <th scope="row">個案姓名:</th>
                                <td>
                                     <asp:TextBox ID="tbName" Enabled="false" CssClass="text02" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">個案英文姓名:</th>
                                <td>
                                     <asp:TextBox ID="tbE" CssClass="text02" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"><hr/></td>
                                </tr>
                            <tr>
                                <th scope="row">申請人姓名:</th>
                                <td>
                                   <asp:TextBox ID="tbA" CssClass="text02" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">關係:</th>
                                <td>
                                   <asp:TextBox ID="tbR" CssClass="text02" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">檢附文件:</th>
                                <td>
                                     <asp:FileUpload ID="tbFile" Multiple="Multiple"  CssClass="text01" ClientIDMode="Static"   runat="server"  />
                                    <span class="wordblue">(限3M)</span>
                                </td>
                            </tr>
                            </table>
                        
            </div>

            <div class="formBtn formBtncenter">
                  <asp:Button CssClass="btn" ID="btnSave" Text="確定" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
            </div>
             </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/ApplyData_JS")%>
    <%--<%:Scripts.Render("~/bundles/Date_JS")%>--%>
    <script src="../../js/date/calendar.js"></script>
    <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>