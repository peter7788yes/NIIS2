<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_QAView.aspx.cs" Inherits="System_FrequentlyAskedQuestionM_QAView_View_QAView" MasterPageFile="~/MasterPage/MasterPage.master"%>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server" >

</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div id="QAViewApp" ng-controller="ViewQAViewController" ng-init="LoadQAViewData()" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formBtn formBtnleft">
                <input class="btn" type="submit" name="lastBtn" ng-click value="回上一頁"/>
            </div>
            <div class="formTb">
                <table>
                  <tr>
                    <th scope="row">發佈日期: </th>
                    <td>
                        <label ng-bind="VEVM.ReleaseDate"></label>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">問題: </th>
                    <td>
                        <label ng-bind="VEVM.Question"></label>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">回覆：</th>
                    <td colspan="3">
                        <textarea ng-bind="VEVM.Reply" name="Content" cols="138" rows="15" ng-disabled="true"></textarea>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">附件：</th>
                        <td colspan="3">
                            <div ng-repeat="item in VEVM.filelist track by $index">
                            <span class="blank"><a target="_blank" ng-href="/System/FrequentlyAskedQuestionM/QAView/DownloadFileOP.aspx?i={{item.c3}}" ng-bind='item["c4"]'></a></span>
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
     <%:Scripts.Render("~/bundles/QAView_JS")%>
</asp:Content>
