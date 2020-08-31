<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsPublished.aspx.cs" Inherits="System_NewsPublishedM_NewsPublished" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server" >
     <%=BodyClass %>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div id="NewsPublishedApp" ng-controller="NewsPublishedController" ng-cloak>
        <form name="form1" id="form1" >
            <%if(NewPower.HasPower){ %>
            <div class="formBtn formBtnleft">
                <input class="btn" type="submit" name="send" ng-click="TransferNew()" value="新增"/>
            </div>
            <%} %>
            <%if(SearchPower.HasPower){ %>
            <div class="formTb">
                <table>
                  <tr>
                    <th scope="row">發佈單位: </th>
                    <td>
                        <input ng-init="VM.OrgName='<%=OrgName%>'" id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" ng-click="openOrgs()" readonly/>
                        <input id="OrgID" ng-model="VM.OrgID" name="OrgID" type="hidden" />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                        <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">主旨: </th>
                    <td>
                        <input ng-model="VM.KeyNote" name="name" type="text" value="" class="text03"/>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">上架狀態: </th>
                        <td>
                            <select ng-model="VM.PublishedStatus">
                            <option ng-repeat="option in VM.Status" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                        </td>
                  </tr>
                </table>
            </div>
            <div class="formBtn">
                <input class="btn" type="submit" name="send" ng-click="Search(1)" value="查詢"/>
            </div>
            <%} %>
         </form>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table>
                <tr>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="10%">發佈日期</th>
                    <th scope="col" width="20%">發佈單位</th>
                    <th scope="col" width="10%">上架狀態</th>
                    <th scope="col" width="50%">主旨</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='TransformROCDate(record.c9)' ></td>
                    <td class="aCenter" ng-bind='record["c3"]'></td>
                    <td class="aCenter" ng-bind='GetStatus(record)'></td>
                    <td class="aCenter"><a href="javascript:void(0);" ng-bind='record["c4"]' ng-click="TransferModiy(record)"></a></td>
                </tr>
            </table>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
        <script>
            var StatusData = <%=PublishedStatus %>;
        </script>
     <%:Scripts.Render("~/bundles/NewsPublished_JS")%>
</asp:Content>