<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfileList.aspx.cs" Inherits="CaseMaintain_UserProfileList" %>
<%@ Import Namespace="System.Web.Optimization" %>
 
<%@ Register src="ucUserProfileList.ascx" tagname="ucUserProfileList" tagprefix="uc1" %>
 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
   <div class="path"></div>
     <div class="formBtn formBtnleft">
      <input type="submit" name="send"  id="AddBtn" value="新增個案" class="btn" />
    </div>
    <uc1:ucUserProfileList ID="ucUserProfileList1" runat="server" />
    
</asp:Content>
 

