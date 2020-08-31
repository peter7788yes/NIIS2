<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyRecord.aspx.cs" Inherits="Vaccination_RecordM_ApplyRecord" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
   <section class="Content3" ng-app="MyApp" ng-controller="MyController">
   <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off">
            <input type="hidden" id="c" name="c"  value="<%:CaseUserID %>"/>
            <input type="hidden" id="i" name="i"  value="<%:RecordDataID %>"/>
            <input type="hidden" id="r" name="r"  value="<%:SystemRecordVaccineCode %>"/>
            <input type="hidden" id="ri" name="ri"  value="<%:SystemRecordVaccineID %>"/>
            <input type="hidden" id="a" name="a"  value="<%:AppointmentDate %>"/>
            <input type="hidden" id="uu" name="uu"  value="<%:UpdateUID %>"/>
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
            <asp:TextBox   ID="tbDate"  name="tbDate"  ClientIDMode="Static" CssClass="text03"  runat="server"  />
            <img style="cursor:pointer" onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
          </td>
          <td class="aCenter">
             <asp:TextBox ClientIDMode="Static" ID="tbAgency" ng-model="VM.agency" runat="server"  name="tbAgency" class="text03" type="text" ng-click="openSelectAgency()" />
             <input ng-model="VM.agencyID" value="<%=AgencyID%>" id="hfAgencyID" name="hfAgencyID" type="hidden"  />
             <img style="cursor:pointer" ng-click="openSelectAgency()" src="/images/icon_agency.png"   />
          </td>
          <td class="aCenter">
                <select id="SelectVacc" name="SelectVacc"  ng-model="VM.SelectVacc" ng-change="chagneSelect()" >
                            <option value="0">無</option>
                            <option ng-repeat="option in VM.VaccAry track by $index" value="{{option.VI}}" ng-bind="option.BI"></option>
                </select>
                <img style="cursor:pointer" ng-click="openAddVaccine()" src="/images/icon_needle.png"  />
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
                 <tr>
                     <th scope="row" colspan="2">
                          <asp:CheckBox ID="cbSI" Text="此接種紀錄有以下狀況，請勾選：打錯針、重複接種、提前接種、施打於自費對象、施打於非計劃實施對" 
                           ClientIDMode="Static"  runat="server" />
                     </th>
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
        <div class="formBtn">
            <asp:Button CssClass="btn" ID="btnSave" Text="儲存" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
            <input type="button" id="closeBtn" value="取消" class="btn" />
        </div>
  </form>
</section>
    <script>
        var tbAry =<%=tbAry%>;
        var agency ="<%:Agency%>";
        var agencyID =<%:AgencyID%>;
        var CC=<%:CaseUserID%>;
        var II=<%:RecordDataID%>;
        var RR="<%:SystemRecordVaccineCode%>";
        var AA="<%:AppointmentDate%>";
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <script src="/js/date/WdatePicker.js"></script>
</asp:Content>
