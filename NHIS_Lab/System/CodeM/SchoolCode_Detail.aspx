<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchoolCode_Detail.aspx.cs" Inherits="System_CodeM_SchoolCode_Detail" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/CheckLog.ascx" TagPrefix="uc1" TagName="CheckLog" %>

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
     <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>

          <form id="form1" runat="server" defaultbutton="btnSave" autocomplete="off" >
            <div class="formBtn formBtnleft">
                <% if (UpdatePower.HasPower) { %>
                      <asp:Button CssClass="btn" ID="btnSave" Text="修改儲存" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
                <% } %>
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>

               <div class="formTb">
                <table>
                    <tr>
                       <th scope="row"><span class="must">*</span>學校代碼:</th>
                        <td>
                            <asp:TextBox ID="tbCode" CssClass="text02" ClientIDMode="Static" runat="server" />
                            <input name="i" value="<%=ElementarySchoolID %>" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>學校名稱:</th>
                        <td>
                               <asp:TextBox ID="tbName" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row"><span class="must">*</span>地址:</th>
                        <td>
                            <select ng-hide="VM.CountyAry.length>0">
                                <option value="<%=County %>"><%=CountyName %></option>
                            </select>
                            <select id="SelectCounty" name="SelectCounty" ng-model="VM.SelectCounty" ng-change="SelectCountyChange()">
                                 <option ng-repeat="option in VM.CountyAry" ng-selected="{{option.I==VM.SelectCounty}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                             <select ng-hide="VM.TownAry.length>0">
                                 <option value="<%=Town %>"><%=TownName %></option>
                            </select>
                            <select id="SelectTown" name="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                   <option ng-repeat="option in VM.TownAry" ng-selected="{{option.I==VM.SelectTown}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                             <select ng-hide="VM.VillageAry.length>0">
                                    <option value="<%=Village %>"><%=VillageName %></option>
                            </select>
                            <select id="SelectVillage" name="SelectVillage" ng-model="VM.SelectVillage"  >
                                   <option ng-repeat="option in VM.VillageAry" ng-selected="{{option.I==VM.SelectVillage}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>

                            <asp:TextBox ID="tbAddress" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row">電話:</th>
                        <td>
                            <asp:TextBox ID="tbTel" CssClass="text02" ClientIDMode="Static" runat="server" /><span style="color:#F99">格式: (區號)電話號碼</span>
                         </td>
                    </tr>
                    <tr>
                       <th scope="row"><span class="must">*</span>狀態:</th>
                        <td>
                             <asp:DropDownList ID="ddEnState" CssClass="text02"  ClientIDMode="Static"  runat="server" />
                         </td>
                    </tr>
                </table>

            </div>
           </form>

            <uc1:CheckLog runat="server" ID="uc1" />

        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">機構代碼</th>
                    <th scope="col">機構名稱</th>
                    <th scope="col">開業狀態</th>
                    <th scope="col">更新日期</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter" ng-bind='record["S"]'></td>
                      <td class="aCenter" ng-bind='record["AC"]'></td>
                      <td class="aCenter" ng-bind='record["AN"]'></td>
                      <td class="aCenter" ng-bind='record["BS"]'></td>
                      <td class="aCenter" ng-bind='record["CD"] | ShortTaiwanTime:-480'></td>
                </tr>
            </table>
       
</div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var i =<%=ID %>;
        var county="<%=County%>";
        var town ="<%=Town%>";
        var village ="<%=Village%>";
        var countyJson  =<%=CountyJson%>;
        var townJson  =<%=TownJson%>;
        var villageJson  =<%=VillageJson%>; 
        var UP =<%=UpdatePower.HasPower ? 1 :0%>;
        var stateAry =<%=StateListAry %>;
        var tbOther ="<%=tbOther%>";
        var tbOtherIDs ="<%=tbOtherIDs%>";
    </script>
     <%:Scripts.Render("~/bundles/SchoolCode_Detail_JS")%>
</asp:Content>
