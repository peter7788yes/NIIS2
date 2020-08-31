<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentReRecord_Upload.aspx.cs" Inherits="Vaccination_RecordM_StudentReRecord_Upload" MasterPageFile="~/MasterPage/MasterPage.master" %>
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

  <form>


    <div class="formBtn formBtnleft">
      <input type="button" id="lastBtn" value="回上一頁" class="btn" />
    </div>


    <div class="formTb">
      <table>
        <tr>
          <td >說明: 請下載<span class="wordred"><a href="../../FileTemplate/範本-國小補種登錄.xlsx">此檔案</a></span>修改數據後再行上傳 <span class="wordred"><a href="#">學校代碼名稱請由此下載：</a></span></td>
        </tr>
        <tr>
          <td><input type="file" name="textfield11" id="textfield11"  /><input type="button" value="上傳匯入" class="btn"/></td>
        </tr>
      </table>
    </div>


  </form>
  
  
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/StudentReRecord_Upload_JS")%>
    <%--<%:Scripts.Render("~/bundles/Date_JS")%>--%>
    <script src="../../js/date/calendar.js"></script>
    <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>



