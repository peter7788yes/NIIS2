<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify_QnAData.aspx.cs" Inherits="System_FrequentlyAskedQuestionM_QnAData_Modify_QnAData" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
<section ng-app="QnADataApp" ng-controller="ModifyQnADataController" ng-cloak">
    <form id="form1" name="form1" runat="server">
        <div class="formBtn formBtnleft">
            <%if(ModifyPower.HasPower){ %>
            <asp:Button ID="Save" runat="server" CssClass="btn" Text="儲存" OnClick="Save_Click"  />
            <%} %>
            <asp:Button ID="Cancel" runat="server" CssClass="btn" Text="回上一頁" OnClick="Cancel_Click"  />
            <%if(DeletePower.HasPower){ %>
            <asp:Button ID="Delete" runat="server" CssClass="btn button_floatright" Text="刪除" OnClick="Delete_Click" />
            <%} %>
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
                <tr>
                    <th scope="row">附件：</th>
                    <td>
                        <div ng-repeat="item in VM.filelist track by $index">
                            <span class="blank"ng-bind='item["c4"]'></span>
                            <span class="blank"></span>
                            <%if(DeletePower.HasPower){ %>
                            <span class="blank">
                                <a href="javascript:void(0);" ng-click="DeleteFile(item,$index)"><img src="/images/icon_del01.gif"  alt="刪除"/></a>
                            </span>
                            <%} %>
                            <br/>
                        </div>
                        <%if(UploadPower.HasPower){ %>
                        <asp:FileUpload ID="tbFile" CssClass="text01" ClientIDMode="Static" runat="server" multiple/>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <th scope="row"><span class="must">*</span>上架狀態：</th>
                    <td>
                        <asp:RadioButton GroupName="PublishedStatus" ID="PublishedStatus1" ClientIDMode="Static" Text="上架" runat="server" CssClass="radio01" />
                        <asp:RadioButton GroupName="PublishedStatus" ID="PublishedStatus2" ClientIDMode="Static" Text="下架" runat="server" CssClass="radio01" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="list01">
            <ul>
            <li><span>建立者：</span><label ng-bind="VM.CreateAccount"></label>-<label ng-bind="VM.CreateRole"></label>-<label ng-bind="VM.CreateDate"></label></li>
            <li><span>異動者：</span><label ng-bind="VM.ModifyAccount"></label>-<label ng-bind="VM.ModifyRole"></label>-<label ng-bind="VM.ModifyDate"></label></li>
            </ul>
        </div>  
    </form>
</section>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/QnAData_JS")%>
</asp:Content>