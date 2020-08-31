<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InoculationRecordTable.aspx.cs" Inherits="Report_VaccinationM_InoculationRecordTable" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectAgency.ascx" TagPrefix="uc1" TagName="UC_OpenSelectAgency" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
  <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>
       <form id="form1" runat="server"  autocomplete="off">
        <div class="formBtn formBtnleft">
             <input type="button" class="btn" value="預覽列印"  ng-click="goPrint()" />
             <asp:Button Text="匯出xls" ID="btnDownload" CssClass="btn" ClientIDMode="Static" runat="server" OnClick="btnDownload_Click" />
        </div>
       <div class="formTb">
             <table>
                 <tr>
                     <td colspan="2">
                         <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                     </td>
                 </tr>
                   <tr>
                    <th scope="row"><span class="must">*</span>出生日期:</th>
                    <td>
                         <input id="tbDateStart" name="ds" class="text02" value="<%:DateStart %>" />
                         <img style="cursor:pointer" onclick="WdatePicker({el:'tbDateStart',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                            ~ 
                         <input id="tbDateEnd" name="de" class="text02" value="<%:DateEnd %>"/>
                         <img style="cursor:pointer" onclick="WdatePicker({el:'tbDateEnd',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                    </td>
                  </tr>
                 <tr>
                    <th scope="row"><span class="must">*</span>疫苗內容:</th>
                    <td>
                        <div style="float:left">
                        <select id="vaccSelect" multiple="multiple" size="5" style="display:none;width:150px;">
                              <option ng-repeat="option in VM.vaccAry track by $index" value="{{option.I}}" ng-bind="option.R"></option>
                        </select>
                        </div>
                        <div>
                            <br/>
                             <span style="color:crimson">複選</span><br/>
                             <input  type="button" value="全選" class="btn" /><br/>
                             <input  type="button" value="取消選取"  class="btn"  />
                        </div>
                     </td>
                  </tr>
                 <tr>
                    <th scope="row"><span class="must">*</span>狀態:</th>
                    <td>
                        <label><input  type="radio"  name="rd" />全數完成</label>
                        <label><input  type="radio"  name="rd" />未完成</label>
                    </td>
                  </tr>
             </table>
        </div>
        </form>
        <div class="list01" style="color:blue">
        <ul>
          <li><span>報表說明：</span>顯示個案的接種的疫苗劑別及接種日期</li>
        </ul>
      </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var tbAry = <%=tbAry%>;
    </script>
     <%:Scripts.Render("~/bundles/InoculationRecordTable_JS")%>
     <script src="/js/date/WdatePicker.js"></script>
</asp:Content>