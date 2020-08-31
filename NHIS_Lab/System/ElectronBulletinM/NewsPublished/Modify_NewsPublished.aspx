<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify_NewsPublished.aspx.cs" Inherits="System_ElectronBulletinM_NewsPublished_Modify_NewsPublished" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
    <div class="path"></div>
    <div id="NewsPublishedApp" ng-controller="ModifyNewsPublishedController" ng-cloak>
        <form name="form1" id="form1" runat="server">
            <div class="formBtn formBtnleft">
                <%if(ModifyPower.HasPower){ %>
                <asp:Button ID="Save" CssClass="btn" runat="server" Text="儲存" OnClick="Save_Click" />
                <%} %>
                <asp:Button ID="Return" CssClass="btn" runat="server" Text="回上一頁" OnClick="Return_Click" />
                <%if(DeletePower.HasPower){ %>
                <asp:Button ID="Delete" CssClass="btn button_floatright" runat="server" Text="刪除" OnClick="Delete_Click" />
                <%} %>
            </div>
            <div class="formTb">
                <table>
                  <tr>
                    <th scope="row">發佈日期: </th>
                    <td>
                        <asp:Label ID="ReleaseDate" runat="server" ></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">發佈單位: </th>
                    <td>
                        <asp:Label ID="ReleaseOrg" ClientIDMode="Static" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>主旨: </th>
                    <td>
                        <asp:TextBox ID="Subject" ClientIDMode="Static" CssClass="text01" runat="server"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row"><span class="must">*</span>內容：</th>
                    <td colspan="3">
                        <asp:TextBox ID="Contents" ClientIDMode="Static" TextMode="MultiLine" Columns="138" Rows="15" runat="server"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">附件：</th>
                    <td colspan="3">
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
                        <asp:FileUpload ID="tbFile" CssClass="text01" Font-Names="tbFile" ClientIDMode="Static" runat="server" multiple/>
                        <%} %>
                    </td>
                  </tr>
                  <tr>
                      <th scope="row"><span class="must">*</span>上架日期：</th>
                      <td>
                        <asp:TextBox ID="PublishedStarDate" ClientIDMode="Static" CssClass="text02" runat="server" ></asp:TextBox>
                        <a href="#"><asp:Image ID="PublishedStarDateImg" ImageUrl="/images/icon_calendar.png" runat="server"></asp:Image></a>
                        <asp:TextBox ID="PublishedEndDate" ClientIDMode="Static" CssClass="text02" runat="server" ></asp:TextBox>
                        <a href="#"><asp:Image ID="PublishedEndDateImg" ImageUrl="/images/icon_calendar.png" runat="server"></asp:Image></a>
                      </td>
                  </tr>
                  <tr>
                    <th scope="row">電子郵件：</th>
                    <td colspan="3">
                        <asp:CheckBox ID="EmailCheck" ClientIDMode="Static" Text="發送電子郵件" runat="server" AutoPostBack="True" OnCheckedChanged="EmailCheck_CheckedChanged" /><br/>
                        <asp:TextBox ID="OrgName" ClientIDMode="Static" TextMode="MultiLine" Columns="70" Rows="5" Visible="false" ng-click="openOrgs()" runat="server" ReadOnly="true"></asp:TextBox> 
                        <asp:HiddenField ID="OrgID" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="OrgType" ClientIDMode="Static" runat="server" />
                        <a href="#"><asp:Image ID="OrgImg" ImageUrl="/images/location.png" ng-click="openOrgs()" Visible="false" runat="server" ></asp:Image></a>
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
     <script src="/js/date/WdatePicker.js"></script>
     <%:Scripts.Render("~/bundles/NewsPublished_JS")%>
</asp:Content>
