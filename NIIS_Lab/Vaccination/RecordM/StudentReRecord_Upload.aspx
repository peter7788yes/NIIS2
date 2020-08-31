<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentReRecord_Upload.aspx.cs" Inherits="Vaccination_RecordM_StudentReRecord_Upload" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
  <section class="Content" ng-app="MyApp" ng-controller="MyController">
  <div class="path"></div>
   <form id="MyForm" runat="server" ClientIDMode="Static" autocomplete="off">
    <div class="formBtn formBtnleft">
      <input type="button" id="lastBtn" value="回上一頁" class="btn" />
    </div>
    <div class="formTb">
      <table>
        <tr>
          <td >說明: 請下載<span class="wordred"><a href="StudentRecord_Upload.xlsx">此檔案</a></span>修改數據後再行上傳 <span class="wordred replaceA" ng-click="goDownload()">學校代碼名稱請由此下載：</span></td>
        </tr>
        <tr>
          <td>
              <asp:FileUpload ID="tbFile"  CssClass="text01" ClientIDMode="Static"   runat="server"   />
              <asp:Button CssClass="btn" ID="btnSave" Text="上傳匯入" runat="server"  OnClick="btnSave_Click" />
        </tr>
      </table>
    </div>
  </form>
    </section>
</asp:Content>

