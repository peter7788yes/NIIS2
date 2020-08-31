<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New_QnAType.aspx.cs" Inherits="System_FrequentlyAskedQuestionM_QnAData_New_QnAType" MasterPageFile="~/MasterPage/MasterPage.master"%>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div> 
    <form id="form1" name="form1" runat="server" >
        <div class="formBtn formBtnleft">
            <%if(NewPower.HasPower){ %>
            <asp:Button CssClass="btn" ID="Save" runat="server" Text="儲存" OnClick="Save_Click" />
            <%} %>
            <asp:Button CssClass="btn" ID="Return" runat="server" Text="回上一頁" OnClick="Return_Click" />
        </div>        
        <div class="formTb ">
            <table>
                <tr>
                    <th scope="row"><span class="must">*</span>問題類別: </th>
                    <td>
                        <asp:TextBox CssClass="text01" ID="TypeName" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/QnAData_JS")%>
</asp:Content>