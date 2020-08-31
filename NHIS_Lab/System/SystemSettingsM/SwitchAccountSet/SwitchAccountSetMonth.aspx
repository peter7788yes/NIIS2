<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SwitchAccountSetMonth.aspx.cs" Inherits="System_SystemSettingsM_SwitchAccountSet_SwitchAccountSetMonth" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <link href="/css/tabcss/jquery.ui.all.css" rel="stylesheet" />
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="SwitchAccountSetApp" ng-controller="SwitchAccountSetMonthController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <input class="btn" type="button" name="send" ng-click="Return()" value="回上一頁"/>
            </div>
            <div class="formTb formTb6">
                <table>
                    <tr>
                        <td colspan="2">
                            <input ng-init="VM.OrgName='<%=OrgName%>'" id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" ng-disabled="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <select ng-model="VM.SelectYear"  ng-change="GetMonth(1)">
                                <option ng-selected="option.EV==VM.SelectYear" ng-repeat="option in VM.Year" value="{{option.EV}}" ng-bind="option.EN"></option>
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs">
                <ul >
                    <li >
                        <a href="#tabs-1" id="li1" ng-click="GetMonth(1)" ng-bind="VM.SetName"></a>
                    </li>
                    <li >
                        <a href="#tabs-2" id="li2" ng-click="GetFile(1)" ng-bind="VM.NewFileName"></a>
                    </li>
                    <li>
                        <input id="Newbtn" class="btn" type="button" ng-click="NewData()" name="send" value="新增資料" style="display:none;"/>
                    </li>
                </ul>
                <div id="tabs-1">
                    <div id="MonthBlock" style="display:none;" class="listTb"  ng-cloak>
                        <table>
                            <tr>
                                <th scope="col" width="10%">序號</th>
                                <th scope="col" width="80%">月份</th>
                                <th scope="col" width="10%">設定</th>
                            </tr>
                            <tr ng-repeat="record in TM.tbData track by $index">
                                <td class="aCenter" ng-bind='record["c1"]'></td>
                                <td class="aCenter" ng-bind='record["c5"]'></td>
                                <td class="aCenter">
                                    <a href="javascript:void(0);" ng-click="ChangStatus(record)" ng-switch on='record["c6"]'>
                                        <img ng-switch-when="1" ng-src="/images/icon_on.png">
                                        <img ng-switch-when="0" ng-src="/images/icon_off.png">
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="tabs-2"> 
                    <page-nav  ng-model="PM" on-change-page-d="GetFile(pgIndex)" template-url="/html/ang_template/PageNav_CommonMulit.html"></page-nav>
                    <div id="NewFileBlock" style="display:none;" class="listTb"  ng-cloak>
                        <table>
                            <tr>
                                <th scope="col" width="10%">序號</th>
                                <th scope="col" width="35%">檔案</th>
                                <th scope="col" width="35%">說明</th>
                                <th scope="col" width="10%">維護</th>
                                <th scope="col" width="10%">刪除</th>
                            </tr>
                            <tr ng-repeat="record in TM2.tbData track by $index">
                                <td class="aCenter" ng-bind='record["c1"]'></td>
                                <td class="aCenter" ><a target="_blank" ng-href="/System/SystemSettingsM/SwitchAccountSet/DownloadFileOP.aspx?i={{record.c3}}" ng-bind='record["c4"]'></a></td>
                                <td class="aCenter" ng-bind='record["c5"]'></td>
                                <td class="aCenter" ><a href="javascript:void(0);" ng-click="ModifyData(record)"><img ng-src="/images/icon_maintain.png" alt="修改"></a></td>
                                <td class="aCenter" ><a href="javascript:void(0);" ng-click="DeleteData(record)"><img ng-src="/images/icon_del01.gif" alt="刪除"></a></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </form>
    </section>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var YearData = <%=Year %>;
     </script>
     <%:Scripts.Render("~/bundles/SwitchAccountSet_JS")%>
</asp:Content>