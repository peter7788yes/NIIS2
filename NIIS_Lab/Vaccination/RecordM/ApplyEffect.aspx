<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyEffect.aspx.cs" Inherits="Vaccination_RecordM_ApplyEffect" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content3" ng-app="MyApp" ng-controller="MyController">
    <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
            <input type="hidden" id="c" name="c"  value="<%:CaseUserID %>"/>
            <input type="hidden" id="i" name="i"  value="<%:RecordDataID %>"/>
            <input type="hidden" id="r" name="r"  value="<%:SystemRecordVaccineCode %>"/>
            <input type="hidden" id="a" name="a"  value="<%:AppointmentDate %>"/>
            <input type="hidden" id="uu" name="uu"  value="<%:UpdateUID %>"/>
     <div class="formTb formTb2 formTb3">
                  <table>
                     <tr>
                          <th scope="col">劑別代號</th>
                          <td>
                              <asp:Literal  ID="lblVC" ClientIDMode="Static" runat="server" /> 
                          </td>

                          <th scope="col">預定日期</th>
                          <td>
                            <asp:Literal  ID="lblAD" ClientIDMode="Static" runat="server" /> 
                          </td>
                  </table>
    </div>
    <div class="formTb">
      <table>
        <tr>
          <td>
               <label>接種前3日內，是否曾經就醫吃藥？</label>
               <asp:RadioButton GroupName="rb3" ID="rb3a" Text="不曾就醫" value="1" ClientIDMode="Static"   runat="server" />
               <asp:RadioButton GroupName="rb3" ID="rb3b" Text="曾就醫吃藥" value="2" ClientIDMode="Static"   runat="server" />
               <asp:CheckBox style="display:none" ID="cb3" Text="接種前3日內，曾就醫吃藥" value="1" ClientIDMode="Static"   runat="server" />
          </td>
        </tr>
        <tr>
          <td> 
              <label>疫苗接種後7日內，發生感染性疾病：</label>
              <asp:RadioButton GroupName="rb7" ID="rb7a" Text="不曾就醫" value="1" ClientIDMode="Static"   runat="server" />
              <asp:RadioButton GroupName="rb7" ID="rb7b" Text="曾就醫吃藥" value="2" ClientIDMode="Static"   runat="server" />
              <asp:CheckBox style="display:none" ID="cb7" Text="疫苗接種後7日內，發生感染性疾病" value="1" ClientIDMode="Static"   runat="server" />
          </td>
        </tr>
        <tr style="display:none">
          <td>疫苗接種後疑似症狀最早出現日期
           <asp:TextBox ID="tbDate"   CssClass="text02" ClientIDMode="Static"  runat="server" />
           <img style="cursor:pointer" onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
          </td>
        </tr>
      </table>
    </div>
      <div style="display:none;" class="tmBlock listTb">
              <table>
                <tr class="pinkcolor">
                  <td colspan="4" class="aCenter">接種部位局部症狀</td>
                </tr>
                <tr>
                  <th scope="col">症狀</th>
                  <th scope="col">反應範圍</th>
                  <th scope="col">疑似症狀最早出現日期</th>
                  <th scope="col">持續天數</th>
                </tr>
                <tr ng-repeat="record in TM.data1 track by $index" repeat-callback="repeatCallback()">
                              <td ng-bind="record['EN']"></td>
                              <td>
                                  <span ng-repeat="item in record.DA track by $index">
                                      <label><input name="rb{{record.EN}}" type="radio" ng-model="record['ESV']" ng-value="item['EV']"/><span ng-bind="item['EN']"></span></label>
                                  </span>
                              </td>
                              <td class="aCenter" style="width:200px">
                                    <input id="tbDateStart{{record.EN}}" ng-et="1" class="text04 esdate right10" onchange="goChange()" ng-index="{{$index}}" ng-model="record['ES']" />
                                    <img style="cursor:pointer" id="{{record.EN}}" onclick="goDate(this)" src="/images/icon_calendar.png"/>
                              </td>
                              <td class="aCenter"><input  style="width:80px" type="number" ng-model="record['ED']"  min="0" /> 天</td>
                </tr>
              </table>
     </div>
     <div style="display:none;" class="tmBlock listTb">
          <table>
        <tr class="pinkcolor">
          <td colspan="4" class="aCenter">全身反應</td>
        </tr>
        <tr>
          <th scope="col">症狀</th>
          <th scope="col">反應範圍</th>
          <th scope="col">疑似症狀最早出現日期</th>
          <th scope="col">持續天數</th>
        </tr>
            <tr ng-repeat="record in TM.data2 track by $index" repeat-callback="repeatCallback()">
                                <td ng-bind="record['EN']"></td>
                              <td>
                                  <span ng-repeat="item in record.DA track by $index">
                                      <label><input name="rb{{record.EN}}" type="radio" ng-model="record['ESV']" ng-value="item['EV']"/><span ng-bind="item['EN']"></span></label>
                                  </span>
                              </td>
                              <td class="aCenter" style="width:200px">
                                    <input id="tbDateStart{{record.EN}}" ng-et="1" class="text04 esdate right10" onchange="goChange()" ng-index="{{$index}}" ng-model="record['ES']" />
                                    <img style="cursor:pointer" id="{{record.EN}}" onclick="goDate(this)" src="/images/icon_calendar.png"/>
                              </td>
                              <td class="aCenter"><input  style="width:80px" type="number" ng-model="record['ED']"  min="0" /> 天</td>
                </tr>
      </table>
    </div>

         <input type="hidden" name="et"  id="et"/>
         <input type="hidden" name="ev"  id="ev"/>
         <input type="hidden" name="esv" id="esv"/>
         <input type="hidden" name="es"  id="es"/>
         <input type="hidden" name="ed"  id="ed"/>
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
     <div class="formBtn">
        <%if (AddPower.HasPower) {%>
            <asp:Button CssClass="btn" ID="btnSave" Text="儲存" runat="server" OnClick="btnSave_Click" ClientIDMode="Static" runat="server"  />
        <%} %>
        <input type="button" id="closeBtn" value="取消" class="btn" />
    </div>
  </form>
    </section>
    <script>
        var CC=<%:CaseUserID%>;
        var II=<%:RecordDataID%>;
        var RR="<%:SystemRecordVaccineCode%>";
        var RRI="<%:SystemRecordVaccineID%>";
        var AA="<%:AppointmentDate%>";
        var data1 = <%=MyPartialData%>;
        var data2 = <%=MyBodyData%>;
        var UU="<%:UpdateUID%>";
        var UUdata = <%=UpdateUserData%>;
        var UUdataList = <%=UpdateUserDataList%>;
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="jsCP" runat="Server">
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>