<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkloadStatistics.aspx.cs" Inherits="Report_WorkloadM_WorkloadStatistics" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/UC_OpenSelectSingleOrg.ascx" TagPrefix="uc1" TagName="UC_OpenSelectSingleOrg" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>

<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <link href="/css/design.min.css" rel="stylesheet"/>
     <link href="/css/tab.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server"><%:BodyClass %></asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
   <section class="Content" ng-app="MyApp" ng-controller="MyController">
     <div class="path"></div>
        <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
        <%--<div class="tab">
          <ul class="tabul">
            <li><span class="tabA tabHereA">工作量統計表</span> </li>
            <li><span class="tabA" id="tab2">公務報表</span></li>
          </ul>
        </div>--%>
        <div class="formTb">
                         <table>
                             <tr>
                                 <th scope="row">所屬單位:</th>
                                 <td>
                                     <uc1:UC_OpenSelectSingleOrg runat="server" ID="UC_OpenSelectSingleOrg" />
                                 </td>
                             </tr>
                             <tr>
                                <th scope="row"><span class="must">*</span>接種期間:</th>
                                <td> 
                                    <asp:TextBox required="required" ID="tbDateStart" type="text"  ClientIDMode="Static" runat="server" />
                                    <img style="cursor:pointer" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'tbDateEnd\')}',el:'tbDateStart',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                                    <span class='h10'>~</span> 
                                    <asp:TextBox required="required" ID="tbDateEnd"  type="text" ClientIDMode="Static" runat="server" />
                                    <img style="cursor:pointer" onclick="WdatePicker({minDate:'#F{$dp.$D(\'tbDateStart\')}',el:'tbDateEnd',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                                </td>
                              </tr>
                              <tr>
                                <th scope="row"><span class="must">*</span>接種疫苗:</th>
                                <td>
                                    <div style="float:left">
                                         <asp:DropDownList required="required" ID="ddlVaccSelect" multiple="multiple" size="5" style="width:250px;"  ClientIDMode="Static"  runat="server" />
                                    </div>
                                    <div>
                                        <br/>
                                         <span style="color:crimson">複選</span><br/>
                                         <input type="button" id="btnAll" value="全選" class="btn" /><br/>
                                         <input type="button" id="btnClean" value="取消選取" class="btn"  />
                                    </div>
                                 </td>
                              </tr>
                             <tr>
                                <th scope="row"><span class="must">*</span>統計方式:</th>
                                <td>
                                    <asp:RadioButton GroupName="rbA" ID="rb1" Text="個案戶籍地" ClientIDMode="Static" runat="server" CssClass="radio01" />
                                    <asp:RadioButton GroupName="rbA" ID="rb2" Text="預注地點" ClientIDMode="Static" runat="server" CssClass="radio01" />
                                </td>
                              </tr>
                         </table>
                 </div>
        <div class="formBtn">
             <input type="button" class="btn" value="預覽列印"  ng-click="goPrint()" />
             <asp:Button Text="匯出xls" ID="btnDownload" CssClass="btn" ClientIDMode="Static" runat="server" OnClick="btnDownload_Click" />
        </div>
      </form>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <%:Scripts.Render("~/bundles/WorkloadStatistics_JS")%>
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>