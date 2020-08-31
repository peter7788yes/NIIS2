<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchoolCode_Detail.aspx.cs" Inherits="System_CodeM_SchoolCode_Detail" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/UC/CheckLog.ascx" TagPrefix="uc1" TagName="CheckLog" %>

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

          <form id="form1" runat="server"  autocomplete="off" >
               <div class="formTb">
                <table>
                    <tr>
                       <th scope="row"><span class="must">*</span>學校代碼:</th>
                        <td>
                            <asp:TextBox ID="tbCode"  required="required" CssClass="text02" ClientIDMode="Static" runat="server" />
                            <input name="i" value="<%=ElementarySchoolID %>" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>學校名稱:</th>
                        <td>
                               <asp:TextBox ID="tbName"  required="required" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row"><span class="must">*</span>地址:</th>
                        <td>
                            <select ng-hide="VM.CountyAry.length>0">
                                <option value="<%=County %>"><%=CountyName %></option>
                            </select>
                            <select required="required" style="display:none;"  id="SelectCounty" name="SelectCounty" ng-model="VM.SelectCounty" ng-change="SelectCountyChange()">
                                 <option ng-repeat="option in VM.CountyAry" ng-selected="{{option.I==VM.SelectCounty}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                             <select ng-hide="VM.TownAry.length>0">
                                 <option value="<%=Town %>"><%=TownName %></option>
                            </select>
                            <select required="required" style="display:none;" id="SelectTown" name="SelectTown" ng-model="VM.SelectTown"   ng-change="SelectTownChange()">
                                   <option ng-repeat="option in VM.TownAry" ng-selected="{{option.I==VM.SelectTown}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>
                            <select ng-hide="VM.VillageAry.length>0">
                                    <option value="<%=Village %>"><%=VillageName %></option>
                            </select>
                            <select required="required" style="display:none;" id="SelectVillage" name="SelectVillage" ng-model="VM.SelectVillage"  >
                                   <option ng-repeat="option in VM.VillageAry" ng-selected="{{option.I==VM.SelectVillage}}" value="{{option.I}}" ng-bind="option.N"></option>
                            </select>

                            <asp:TextBox ID="tbAddress" required="required" CssClass="text02" ClientIDMode="Static" runat="server" />
                         </td>
                    </tr>
                    <tr>
                      <th scope="row">電話:</th>
                        <td>
                            <asp:TextBox ID="tbTel" pattern="\(0\d{1,2}\)\d{6,10}" title="(區號)電話號碼" CssClass="text02" ClientIDMode="Static" runat="server" /><span style="color:#F99">格式: (區號)電話號碼</span>
                         </td>
                    </tr>
                    <tr>
                       <th scope="row"><span class="must">*</span>狀態:</th>
                        <td>
                             <asp:DropDownList ID="ddlEnState" CssClass="text02"  ClientIDMode="Static"  runat="server" />
                         </td>
                    </tr>
                </table>
            </div>
               <input name="hash" type="hidden" value="<%=GetString("hash") %>"/>
               <div class="formBtn">
                <% if (UpdatePower.HasPower) { %>
                      <asp:Button CssClass="btn" ID="btnSave" Text="修改儲存" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
                <% } %>
                <input type="button" id="lastBtn" value="回上一頁" class="btn" />
            </div>
           </form>
       <uc1:CheckLog runat="server" ID="uc1" />
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
