<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify_QnAType.aspx.cs" Inherits="System_FrequentlyAskedQuestionM_QnAData_Modify_QnAType" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <div id="QnADataApp" ng-controller="ModifyQnATypeController" ng-cloak>
        <form name="form1" id="form1" runat="server" >
            <div class="formBtn formBtnleft">
                <%if(ModifyPower.HasPower){ %>
                <asp:Button ID="Save" CssClass="btn" runat="server" Text="儲存" OnClick="Save_Click" />
                <%} %>
                <asp:Button ID="Return" CssClass="btn" runat="server" Text="回上一頁" OnClick="Return_Click" />
                <%if(DeletePower.HasPower){ %>
                <asp:Button ID="Delete" CssClass="btn button_floatright" runat="server" Text="刪除" OnClick="Delete_Click" />
                <%} %>
            </div>
            <div class="formTb ">
                <table>
                    <tr>
                        <th scope="row"><span class="must">*</span>問題類別: </th>
                        <td>
                            <asp:TextBox ID="TypeName" ClientIDMode="Static" CssClass="text01" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th scope="row"><span class="must">*</span>狀態：</th>
                        <td>
                            <asp:RadioButton GroupName="Status" ID="Status1" ClientIDMode="Static" Text="啓用" runat="server" CssClass="radio01" />
                            <asp:RadioButton GroupName="Status" ID="Status2" ClientIDMode="Static" Text="停用" runat="server" CssClass="radio01" />
                        </td>
                    </tr>
                    <tr>
                        <th scope="row">問答數量: </th>
                        <td>
                            <asp:Label ID="QaNum" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>    
         </form>
         <div class="list01">
            <ul>
            <li><span>建立者：</span><label ng-bind="VM.CreateAccount"></label>-<label ng-bind="VM.CreateRole"></label>-<label ng-bind="VM.CreateDate"></label></li>
            <li><span>異動者：</span><label ng-bind="VM.ModifyAccount"></label>-<label ng-bind="VM.ModifyRole"></label>-<label ng-bind="VM.ModifyDate"></label></li>
            </ul>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/QnAData_JS")%>
</asp:Content>
