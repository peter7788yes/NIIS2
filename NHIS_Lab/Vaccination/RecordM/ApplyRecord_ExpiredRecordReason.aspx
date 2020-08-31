<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyRecord_ExpiredRecordReason.aspx.cs" Inherits="Vaccination_RecordM_ApplyRecord_ExpiredRecordReason" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
     <section class="Content3" ng-app="MyApp" ng-controller="MyController" ng-cloak>
  
  <form id="form2" runat="server" defaultbutton="btnSave">
    <div class="formBtn  formBtnleft">
         <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
        <input type="button" id="closeBtn" value="取消" class="btn" />
    </div>

    <div class="formTb formTb2 formTb3">
      <table>
         <tr>
              <th scope="col">劑別代號</th>
              <th scope="col">預定日期</th>
              <th scope="col">接種日期</th>
              <th scope="col">接種單位</th>
              <th scope="col">批號</th>
        </tr>
        <tr>
          <td class="aCenter">
              HBIG
          </td>
          <td class="aCenter">
               <asp:TextBox ID="tbDate"  ClientIDMode="Static" CssClass="text03" runat="server" onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
                <a href="javascript:void(0);">
                    <img onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                </a>
          </td>
          <td class="aCenter">
            <asp:TextBox ID="tbDate2"  ClientIDMode="Static" CssClass="text03"  runat="server" onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
            <a href="javascript:void(0);">
                <img onclick="WdatePicker({el:'tbDate2',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
            </a>
          </td>
          <td class="aCenter">
            <input ng-model="VM.agency" id="tbAgency"  class="text03" type="text" ng-click="openSelectAgency()"/>
            <input ng-model="VM.agencyID" id="hfAgencyID" type="hidden"  />
            <a href="javascript:void(0);" ng-click="openSelectAgency()">
                              <img src="/images/icon_agency.png" />
             </a>
          </td>
          <td class="aCenter">
            <select>
                <option>1</option>
            </select>
               <a href="javascript:void(0);" >
                     <img src="/images/icon_needle.png" />
                </a>
          </td>
        </tr>
      </table>
    </div>
   

      <div class="formTb formTb2 formTb3">
      <table>
         <tr>
              <th scope="col">補登原因</th>
              <th scope="col">補種原因</th>
              <th scope="col">提早/逾期原因</th>
       </tr>
        <tr>
          <td>
              <asp:DropDownList ID="ddlReason1"   ClientIDMode="Static"  runat="server" />
          </td>
          <td> 
              <asp:DropDownList ID="ddlReason2"   ClientIDMode="Static"  runat="server" />
          </td>
          <td>
                <asp:Button CssClass="btn" ID="Button1" Text="登錄" runat="server" />
          </td>
        </tr>
      </table>
         <hr />
         疫苗批號：廠牌：包裝樣式：劑量：有效日期
          <img  src="/images/vacc.jpg" />
    </div>
  </form>
</section>
  
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <%:Scripts.Render("~/bundles/ApplyRecord_ExpiredRecordReason_JS")%>
     <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>
