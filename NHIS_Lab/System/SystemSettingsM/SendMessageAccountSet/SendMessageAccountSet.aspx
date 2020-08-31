<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessageAccountSet.aspx.cs" Inherits="System_SystemSettingsM_SendMessageAccountSet_SendMessageAccountSet" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div id="SendMessageAccountSetApp" ng-controller="SendMessageAccountSetController" ng-cloak>
        <form name="form1" id="form1" >
            <div class="formTb formTb6">
                <table>
                  <%if(SearchPower.HasPower){ %>
                  <tr>
                    <td colspan="2">
                        <input ng-init="VM.OrgName='<%=OrgName%>'" id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" ng-click="openOrgs()" readonly/>
                        <%if(Check){ %> 
                        <input ng-init="VM.OrgID='<%=OrgID%>'" id="OrgID" ng-model="VM.OrgID" name="OrgID" type="hidden" />
                        <%} %>
                        <img  src="/images/location.png" ng-click="openOrgs()" />  
                    </td>
                  </tr>
                  <%} %>
                  <tr>
                    <th scope="row">帳號: </th>
                        <td>
                            <input id="MsgAccount" ng-model="VM.MsgAccount" name="MsgAccount" type="text" value="" class="text02" ng-disabled="(<%=OrgID%>!=VM.OrgID)?true:false"/>
                        </td>
                  </tr>
                  <tr>
                    <th scope="row">密碼: </th>
                        <td>
                            <input id="MsgPassWord" ng-model="VM.MsgPassWord" name="MsgPassWord" type="password" value="" class="text02" style="border: 1px solid #e3e1e1; padding: 2px 6px 4px; margin: 0 5px 5px 0" ng-disabled="(<%=OrgID%>!=VM.OrgID)?true:false"/>
                        </td>
                  </tr>
                  <tr>
                    <th scope="row">狀態: </th>
                      <td>
                        <label for="">
                            <input id="MsgStatus" ng-model="VM.MsgStatus" name="MsgStatus" type="radio" value="1" ng-disabled="(<%=OrgID%>!=VM.OrgID)?true:false"/>啓用
                        </label>
                        <label for="">
                            <input id="MsgStatus1" ng-model="VM.MsgStatus" name="MsgStatus" type="radio" value="2" ng-disabled="(<%=OrgID%>!=VM.OrgID)?true:false"/>停用
                        </label>
                      </td>
                  </tr>
                </table>
            </div>
            <%if(ModifyPower.HasPower && Check){ %> 
            <div class="formBtn">
                <input class="btn" type="submit" name="send" ng-click="SaveData()" value="儲存變更"/>
                <input class="btn" type="submit" name="send" ng-click="openTestMessage()" value="測試發送簡訊"/>
            </div>
            <%} %>
         </form>         
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
        var OrgID = <%=OrgID%>;
     </script>
     <%:Scripts.Render("~/bundles/SendMessageAccountSet_JS")%>
</asp:Content>