<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentRecord_Upload.aspx.cs" Inherits="Vaccination_RecordM_StudentRecord_Upload" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
  <form style="display:none;" id='formid' method='post' target='_blank' action='/Ashx/JsonToExcel.ashx'>
            <input type="hidden" id="json" name="json"/>
  </form>
  <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
  <div class="path"></div>

   <form id="form1" runat="server" defaultbutton="btnSave">
      
    <div class="formBtn formBtnleft">
      <input type="button" id="lastBtn" value="回上一頁" class="btn" />
    </div>


    <div class="formTb">
      <table>
        <tr>
          <td >說明: 請下載<span class="wordred"><a href="StudentRecord_Upload.xlsx">此檔案</a></span>修改數據後再行上傳 <span class="wordred"><a href="javascript:void(0);">學校代碼名稱請由此下載：</a></span></td>
        </tr>
        <tr>
          <td>
              <asp:FileUpload ID="tbFile"  CssClass="text01" ClientIDMode="Static"   runat="server"   />
              <asp:Button CssClass="btn" ID="btnSave" Text="上傳匯入" runat="server"  OnClick="btnSave_Click" />
        </tr>
      </table>
    </div>
     
      <%-- <iframe src="../../SelectOrgs.aspx" ></iframe>--%>
       
  </form>
  
       
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/StudentRecord_Upload_JS")%>
    <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>
