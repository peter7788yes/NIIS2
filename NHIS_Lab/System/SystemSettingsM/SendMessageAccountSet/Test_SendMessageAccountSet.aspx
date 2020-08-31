<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_SendMessageAccountSet.aspx.cs" Inherits="System_SystemSettingsM_SendMessageAccountSet_Test_SendMessageAccountSet" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<section class="Content2" id="SendMessageAccountSetApp" ng-controller="TestSendMessageAccountSetController" ng-cloak>
<h2 ng-bind="VM.Title"></h2>
    <form id="form1" name="form1">        
        <div class="formTb formTb2">
            <table>
                <tr>    
                <th scope="row">通知內容: </th>
                    <td>
                        <textarea id="Content" ng-model="VM.Content" name="Content" type="text" value="" cols="60" rows="15" >&nbsp;</textarea>
                    </td>
                </tr> 
                <tr>     
                <th scope="row">通知人員: </th>
                    <td>
                        <input id="Staff" ng-model="VM.Staff" name="Staff" type="text" value="" class="text02" ng-pattern="CheckPhone" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <th scope="row">帳號: </th>
                    <td>
                        <input id="MsgAccount" ng-model="VM.MsgAccount" name="MsgAccount" type="text" value="" class="text02" />
                    </td>
                </tr>
                <tr>
                    <th scope="row">密碼: </th>
                    <td>
                        <input id="MsgPassWord" ng-model="VM.MsgPassWord" name="MsgPassWord" type="password" value="" class="text02" style="border: 1px solid #e3e1e1; padding: 2px 6px 4px; margin: 0 5px 5px 0" />
                    </td>
                </tr>
            </table>
        </div>
        <%if(ModifyPower.HasPower){ %>
        <div class="formBtn">
            <input class="btn" type="submit" name="send" ng-click="SendMessageData()" value="發送簡訊" />
            <input class="btn" type="submit" name="send" ng-click="CloseWin()" value="取消"/>
        </div>
        <%} %>
    </form>
</section>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/SendMessageAccountSet_JS")%>
</asp:Content> 