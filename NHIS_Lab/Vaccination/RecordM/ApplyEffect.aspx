<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyEffect.aspx.cs" Inherits="Vaccination_RecordM_ApplyEffect" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
  
    <form id="form1" runat="server" defaultbutton="btnSave">
            <input type="hidden" id="c" name="c"  value="<%=CaseUserID %>"/>
            <input type="hidden" id="i" name="i"  value="<%=RecordDataID %>"/>
            <input type="hidden" id="r" name="r"  value="<%=SystemRecordVaccineCode %>"/>
            <input type="hidden" id="a" name="a"  value="<%=AppointmentDate %>"/>
    <div class="formBtn  formBtnleft">
        <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" />
        <input type="button" id="closeBtn" value="取消" class="btn" />
    </div>


    <div class="formTb">
      <table>
        <tr>
          <td>
              <asp:CheckBox ID="cb3" Text="接種前3日內，曾就醫吃藥" value="1" ClientIDMode="Static"   runat="server" />
          </td>
        </tr>
        <tr>
          <td> 
              <asp:CheckBox ID="cb7" Text="疫苗接種後7日內，發生感染性疾病" value="1" ClientIDMode="Static"   runat="server" />
          </td>
        </tr>
        <tr>
          <td>疫苗接種後疑似症狀最早出現日期
           <asp:TextBox ID="tbDate"  CssClass="text02" ClientIDMode="Static" onclick="WdatePicker({ dateFmt: 'yyyMMdd',lang:'zh-tw'})"  runat="server" />
            <a href="javascript:void(0);">
                   <img onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
            </a>

          </td>
        </tr>
      </table>
    </div>


    <div class="listTb">
      <table>
        <tr>
          <th scope="col" >有無症狀</th>
          <th scope="col">疑似症狀最早出現日期</th>
          <th scope="col">持續天數</th>
        </tr>
        <tr class="pinkcolor">
          <td colspan="3" class="aCenter">局部症狀</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr class="pinkcolor">
          <td colspan="3" class="aCenter">局部症狀</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
      </table>
    </div>



    <hr/>


   <%-- <div class="button_floatleft">
      <input type="button" id="addBtn" value="新增"  class="btn" />
      <input type="button" id="deleteBtn" value="刪除" class="btn" />
    </div>--%>

    <%--<div class="listTb">
                    <table>
                  <tr>
                    <th scope="col">選取</th>
                    <th scope="col">登錄日期</th>
                    <th scope="col">登錄單位</th>
                    <th scope="col">登錄人員</th>
                    <th scope="col">副作用</th>
                  </tr>


                           <tr ng-show="TM.ApplyEffect.length>0"  ng-repeat="record in TM.ApplyEffect track by $index">
                                      <td class="aCenter"><input type="checkbox" value=""/></td>
                                      <td class="aCenter"><a href="javascript:void(0);"  ng-bind="record['SD'] | SimpleTaiwanDate"></a></td>
                                      <td class="aCenter" ng-bind="record['ON']"></td>
                                      <td class="aCenter" ng-bind="record['UN']"></td>
                                      <td class="aCenter"><a href="javascript:void(0);"><img src="/images/icon_file01.png" /></a></td>
                           </tr> 
              </table>
    </div>--%>
  </form>
</section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var CC=<%=CaseUserID%>;
        var II=<%=RecordDataID%>;
        var RR="<%=SystemRecordVaccineCode%>";
        var AA="<%=AppointmentDate%>";
    </script>
    <%--<script>
        var ApplyEffect=<%=ApplyEffectJson%>;
    </script>--%>
    <%:Scripts.Render("~/bundles/ApplyEffect_JS")%>
    <script src="../../js/date/WdatePicker.js"></script>
</asp:Content>