<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_MessageView.aspx.cs" Inherits="System_MessageViewM_View_MessageView" MasterPageFile="~/MasterPage/MasterPage.master"%>
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
    <div id="MessageViewApp" ng-controller="ViewMessageViewController" ng-init="LoadMessageViewData()" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <input class="btn" type="submit" name="lastBtn" ng-click value="回上一頁"/>
            </div>
            <div class="formTb">
                <table>
                  <tr>
                    <th scope="row">發佈日期: </th>
                    <td>
                        <label ng-bind="VM.ReleaseDate"></label>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">發佈單位: </th>
                    <td>
                        <label ng-bind="VM.OrgName"></label>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">主旨: </th>
                    <td>
                        <label ng-bind="VM.Keynote"></label>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">內容：</th>
                    <td colspan="3">
                        <textarea ng-bind="VM.Content" name="Content" cols="138" rows="15" ng-disabled="true"></textarea>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">附件：</th>
                        <td colspan="3">
                            <div ng-repeat="item in VM.filelist track by $index">
                            <span class="blank"><a target="_blank" ng-href="/System/ElectronBulletinM/MessageView/DownloadFileOP.aspx?i={{item.c3}}" ng-bind='item["c4"]'></a></span>
                            <br/>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>    
         </form>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/MessageView_JS")%>
</asp:Content>