<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountCheck2_Upload.aspx.cs" Inherits="System_AccountM_AccountCheck2_Upload" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
     <section class="Content2" ng-app="MyApp" ng-controller="MyController">
        <h2>查看結果</h2>
         <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off">
              <div class="formTb formTb2  ">
                <table>
                    <tr>
                          <th scope="row" colspan="3" id="on_cy">
                              <%=OrgName %> - <%=year %>年
                          </th>
                    </tr>
                     <tr>
                          <th scope="row">清查檔案：</th>
                          <td colspan="2">
                               <asp:FileUpload ID="tbFile" Multiple="Multiple"  CssClass="text01" ClientIDMode="Static"   runat="server"  />
                              <span class="wordred">(大小限3M以內，請勿傳空白檔案)</span>
                          </td>
                      </tr>
                 </table>
               </div>
               <div class="formBtn formBtncenter">
                      <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server"  OnClick="btnSave_Click" />
                      <input type="button" id="closeBtn" value="關閉視窗" class="btn" />
               </div>
         </form>
    </section>
</asp:Content>

