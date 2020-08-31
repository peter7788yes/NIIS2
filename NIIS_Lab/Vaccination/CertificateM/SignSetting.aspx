<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignSetting.aspx.cs" Inherits="Vaccination_CertificateM_SignSetting" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
     <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
        <form id="MyForm" runat="server"  autocomplete="off">
       <div class="formTb">
           <table>
                  <tr>
                      <th scope="row">所屬單位：</th>
                      <td>
                            <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
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
        <div class="formBtn">
               <asp:Button CssClass="btn" ID="btnSave" Text="確定" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
               <input type="button" id="clearBtn" value="清除內容" class="btn" />
        </div>
       </form>
    </section>
    <script>
        var O = <%=DefaultOrgID %>;
    </script>
</asp:Content>
