<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignSetting.aspx.cs" Inherits="Vaccination_CertificateM_SignSetting" MasterPageFile="~/MasterPage/MasterPage.master" %>
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

     <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>

        <div class="path"></div>

        <form id="form1" runat="server" defaultbutton="btnSave" >
        <div class="formBtn formBtnleft">
               <asp:Button CssClass="btn" ID="btnSave" Text="確定" runat="server" OnClick="btnSave_Click" />
               <input type="button" id="clearBtn" value="清除內容" class="btn" />
        </div>

       <div class="formTb">
           <table>
                  <tr>
                      <td>
                        <asp:TextBox ID="tbLocation" CssClass="text03" ClientIDMode="Static" ng-click="openOrgs()" runat="server" />
                        <asp:HiddenField ID="hfLocationID" ClientIDMode="Static"  runat="server" />
                        <a href="javascript:void(0);"><img  src="/images/location.png" ng-click="openOrgs()" /></a>
                      </td>
                  </tr>
          </table>
        </div>
        <div class="formTb">
             <table>
                  <tr>
                    <th scope="row">醫師簽章:</th>
                    <td>
                         <asp:TextBox ID="tbP" CssClass="text01" ClientIDMode="Static"  runat="server" />
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">單位主任:</th>
                    <td>
                         <asp:TextBox ID="tbD" CssClass="text01" ClientIDMode="Static"  runat="server" />
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">單位關防:</th>
                    <td>
                         <asp:TextBox ID="tbS" CssClass="text01" ClientIDMode="Static"  runat="server" />
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">英文全銜:</th>
                    <td>
                         <asp:TextBox ID="tbE" CssClass="text01" ClientIDMode="Static"  runat="server" />
                    </td>
                  </tr>
                   <tr>
                    <th scope="row">中文全銜:</th>
                    <td>
                         <asp:TextBox ID="tbC" CssClass="text01" ClientIDMode="Static"  runat="server" />
                    </td>
                  </tr>
             </table>
        </div>
       </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <%:Scripts.Render("~/bundles/SignSetting_JS")%>
</asp:Content>
