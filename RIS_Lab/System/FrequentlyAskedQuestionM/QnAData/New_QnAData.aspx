﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New_QnAData.aspx.cs" Inherits="System_FrequentlyAskedQuestionM_QnAData_New_QnAData"  MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <form id="form1" name="form1" runat="server">
        <div class="formBtn formBtnleft">
            <%if(NewPower.HasPower){ %>
            <asp:Button ID="Save" runat="server" CssClass="btn" Text="儲存" OnClick="Save_Click"  />
            <%} %>
            <asp:Button ID="Cancel" runat="server" CssClass="btn" Text="回上一頁" OnClick="Cancel_Click"  />
        </div>
        <div class="formTb">
            <table>
                <tr>
                    <th scope="row"><span class="must">*</span>發佈日期: </th>
                    <td>
                        <asp:TextBox ID="ReleaseDate" ClientIDMode="Static" CssClass="text02" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>問題類別: </th>
                    <td>
                        <asp:DropDownList ID="QuestionType" ClientIDMode="Static" runat="server"></asp:DropDownList>
                        <asp:LinkButton ID="QnAType" ClientIDMode="Static" runat="server"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>問題: </th>
                    <td>
                        <asp:TextBox ID="Question" ClientIDMode="Static" CssClass="text01" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>回覆：</th>
                    <td>
                        <asp:TextBox ID="Reply" ClientIDMode="Static" TextMode="MultiLine" Columns="138" Rows="15" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <%if(UploadPower.HasPower){ %>
                <tr>
                    <th scope="row">附件：</th>
                    <td>
                        <asp:FileUpload ID="tbFile" CssClass="text01" ClientIDMode="Static" runat="server" multiple/>
                    </td>
                </tr>
                <%} %>
                <tr>
                    <th scope="row"><span class="must">*</span>上架狀態：</th>
                    <td>
                        <asp:RadioButton GroupName="PublishedStatus" ID="PublishedStatus1" ClientIDMode="Static" Text="上架" runat="server" CssClass="radio01" />
                        <asp:RadioButton GroupName="PublishedStatus" ID="PublishedStatus2" ClientIDMode="Static" Text="下架" runat="server" CssClass="radio01" />
                    </td>
                </tr>
            </table>
        </div>    
    </form>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/QnAData_JS")%>
</asp:Content>