<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyRecord.aspx.cs" Inherits="Vaccination_RecordM_ApplyRecord" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">

   <section class="Content3" ng-app="MyApp" ng-controller="MyController" ng-cloak>
  
  <form id="form2" runat="server" defaultbutton="btnSave">
    <div class="formBtn  formBtnleft">
         <asp:Button CssClass="btn" ID="btnSave" Text="儲存" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
        <input type="button" id="closeBtn" value="取消" class="btn" />
    </div>

    <div class="formTb formTb2 formTb3">
      <table>
         <tr>
              <th scope="col">劑別代號</th>
              <th scope="col">預定日期</th>
              <th scope="col">接種日期</th>
              <th scope="col">接種單位</th>
              <th scope="col">批號</th>
        </tr>
        <tr>
          <td class="aCenter">
              <asp:Literal  ID="lblVC" ClientIDMode="Static" runat="server" /> 
          </td>
          <td class="aCenter">
               <asp:Literal  ID="lblAD" ClientIDMode="Static" runat="server" /> 
          </td>
          <td class="aCenter">
            <asp:TextBox   ID="tbDate" name="tbDate"  ClientIDMode="Static" CssClass="text03"  runat="server" onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})" />
            <a href="javascript:void(0);">
                <img onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
            </a>
          </td>
          <td class="aCenter">
             <asp:TextBox ClientIDMode="Static" ID="tbAgency" ng-model="VM.agency" runat="server"  name="tbAgency" class="text03" type="text" ng-click="openSelectAgency()" />
             <input ng-model="VM.agencyID" value="<%=AgencyID%>" id="hfAgencyID" name="hfAgencyID" type="hidden"  />
             <asp:HiddenField  ID="hfc" ClientIDMode="Static" runat="server" />
             <asp:HiddenField  ID="hfi" ClientIDMode="Static" runat="server" />
             <asp:HiddenField  ID="hfr" ClientIDMode="Static" runat="server"  />
             <asp:HiddenField  ID="hfa" ClientIDMode="Static" runat="server"  />
            <a href="javascript:void(0);" ng-click="openSelectAgency()">
                              <img src="/images/icon_agency.png" />
             </a>
          </td>
          <td class="aCenter">
                <select id="SelectVacc" name="SelectVacc"  ng-model="VM.SelectVacc" ng-change="chagneSelect()" >
                            <option ng-repeat="option in VM.VaccAry" value="{{option.VI}}" ng-bind="option.BI"></option>
                  </select>
                <a href="javascript:void(0);" >
                     <img src="/images/icon_needle.png" ng-click="openAddVaccine()" />
                </a>
          </td>
        </tr>
      </table>
    </div>
   


      <div class="formTb">
             <table>
                  <tr>
                    <th scope="row">補登原因:</th>
                    <td>
                         <asp:DropDownList ID="ddlReason1" style="width:200px;"  ClientIDMode="Static"  runat="server" />
                         <asp:TextBox ID="tbReason1" CssClass="text02"  ClientIDMode="Static"  runat="server" />
                         <asp:FileUpload ID="tbFile" Multiple="Multiple"  CssClass="text02" ClientIDMode="Static"   runat="server"  />
                    </td>
                  </tr>
                  <tr>
                    <th scope="row">補種原因:</th>
                    <td>
                        
                         <asp:DropDownList ID="ddlReason2" style="width:200px;"  ClientIDMode="Static"  runat="server" />
                         <asp:TextBox ID="tbReason2"   CssClass="text02"  ClientIDMode="Static"  runat="server" />

                    </td>
                  </tr>
                   <tr>
                    <th scope="row">提早/逾期原因:</th>
                    <td>
                         <asp:DropDownList ID="ddlReason3"  style="width:200px;" ClientIDMode="Static"  runat="server" />
                         <asp:TextBox ID="tbReason3"   CssClass="text02"  ClientIDMode="Static"  runat="server" />
                    </td>
                  </tr>
             </table>
                <hr />
                 疫苗批號：<span ng-bind="VM.picVM.BI"></span>
                 廠牌：<span ng-bind="VM.picVM.BB"></span>
                 包裝樣式：<span ng-bind="VM.picVM.FD"></span>
                 劑量：<span ng-bind="VM.picVM.DP"></span>
                 有效日期：<span ng-bind="VM.picVM.AD | SimpleTaiwanDate"></span>
              <br/>
              <img  src="/images/vacc.jpg" />
        </div>
     
       
  </form>
</section>
  
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var tbAry =<%=tbAry%>;
        var agency ="<%=Agency%>";
        var agencyID =<%=AgencyID%>;
        var CC=<%=CaseUserID%>;
        var II=<%=RecordDataID%>;
        var RR="<%=VaccineCode%>";
        var AA="<%=AppointmentDate%>";
    </script>
     <%:Scripts.Render("~/bundles/ApplyRecord_JS")%>
     <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>
