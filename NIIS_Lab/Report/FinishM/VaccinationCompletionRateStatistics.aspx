<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VaccinationCompletionRateStatistics.aspx.cs" Inherits="Report_FinishM_VaccinationCompletionRateStatistics" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
                     <th scope="row">所屬單位:</th>
                     <td>
                         <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                     </td>
                 </tr>
                 <tr>
                    <th scope="row">提報年度:</th>
                    <td>
                         <input  class="text02" value="" /> 年度
                         <label><input type="radio" name="rb1" value="1" />上半年報</label>
                         <label><input type="radio" name="rb1" value="2" />下半年報</label>
                         <label><input type="radio" name="rb1" value="3" />年報</label>
                    </td>
                  </tr>
                  <tr>
                  <th scope="row"><span class="must">*</span>身分別:</th>
                    <td>
                         <label><input type="checkbox" value="1" />低收入戶</label>
                         <label><input type="checkbox" value="2" />中低收入戶</label>
                         <label><input type="checkbox" value="3" />社政高風險個案</label>
                         <label><input type="checkbox" value="4" />A型肝炎疫苗實施地區個案</label>
                         <label><input type="checkbox" value="5" />IPD高危險群</label>
                         <label><input type="checkbox" value="6" />山地離島偏鄉個案</label>
                         <label><input type="checkbox" value="7" />原住民</label>
                         <label><input type="checkbox" value="8" />待領養個案</label>
                         <label><input type="checkbox" value="9" />其他</label>
                    </td>
                  </tr>
                 <tr>
                 <th scope="row"><span class="must">*</span>性別:</th>
                    <td>
                         <label><input type="radio" name="rb2" value="1" />女</label>
                         <label><input type="radio" name="rb2" value="2" />男</label>
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
                    <th scope="row"></th>
                    <td>
                         <label><input type="checkbox" value="1" />全數完成率</label>
                    </td>
                  </tr>
             </table>
        </div>
        </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var tbAry = <%=tbAry%>;
    </script>
         <%:Scripts.Render("~/bundles/InoculationRecordTable_JS")%>
     <script src="/js/date/WdatePicker.js"></script>
</asp:Content>