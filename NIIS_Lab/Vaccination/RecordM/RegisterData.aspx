<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterData.aspx.cs" Inherits="VaccinationM_RegisterData" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/CaseMaintain/ucUserProfileList.ascx" TagPrefix="uc1" TagName="ucUserProfileList" %>

<asp:Content ContentPlaceHolderID="ctCP" Runat="Server">
    <uc1:ucUserProfileList runat="server" ID="ucUserProfileList" />
    <script>
        $(function () {
            $.ajaxSetup({ always: function () { $('section.page').show(); $('#tmBlock').hide(); } })
            $('#MyController').prepend('<div class="path"></div>');
            $('section.page').hide();
            $('#tmBlock').hide();

             var doPOST = function (url, obj, element) {
                var form = document.createElement("form");
                if (element) {
                    form = element;
                }
                form.setAttribute("method", "post");
                form.setAttribute("action", url);
                form.setAttribute("style", "display:none;");

                for (var key in obj) {
                    addInput(key, obj[key], form);
                }

                if (!element) {
                    document.body.appendChild(form);
                }

                form.submit();
            }

             var addInput = function (key, value, element) {
                var hf = document.createElement("input");
                var hf2 = document.querySelector("input[name='" + key + "']");
                if (hf2) {
                    hf = hf2;
                }
                hf.setAttribute("type", "hidden");
                hf.setAttribute("name", key);
                hf.setAttribute("value", value);
                if (!hf2) {
                    element.appendChild(hf);
                }

            }
            angular.element($('section')[0]).scope().goDetail = function (record) {
                SetLocalHash();
                var obj = {};
                obj["i"] = record["C"];
                doPOST("/Vaccination/RecordM/RegisterData_Detail.aspx", obj);
            };
            //$(document).on("click", "a[ng-click='goDetail(record)']", function (e) {
            //    e.preventDefault();
            //    var index = $(this).closest('tr').index();
                
            //    return false;
            //});
        });
    </script>
    <script src="/js/sys/menuPath.min.js"></script>
 <%--<section class="Content" ng-app="MyApp" ng-controller="MyController">
  <%-- <div class="path"></div>--%>
  <%--<form autocomplete="off">
    <div class="formBtn formBtnright">
        <input type="submit" name="send"  id="AddBtn" value="新增個案" class="btn" />
    </div>
    <div class="formTb formTb3">
      <table>
        <tr>
          <th width="37%" class="aCenter" scope="col">個案資料</th>
          <th width="30%" scope="col" class="aCenter">地址資料</th>
          <th width="33%" scope="col" class="aCenter">母親/父親/聯絡人資料</th>
        </tr>
        <tr>
          <td width="37%"><table>
              <tr>
                <th scope="row">姓名</th>
                <td><input name="CaseName" id="CaseName" type="text" class="text01"></td>
              </tr>
              <tr>
                <th scope="row">證號</th>
                <td><input name="CaseIdNo" id="CaseIdNo" type="text"class="text01" ></td>
              </tr>
              <tr>
                <th scope="row">出生日期</th>
                <td><input name="BirthDateS"    id ="BirthDateS" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" type="text" class="text02"><a href="javascript:void(0);"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateS',dateFmt: 'yyyMMdd',lang:'zh-tw'})" style ="width:20px"></a> 至 <input name="BirthDateE"  id ="BirthDateE" type="text" class="text02" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })"><a href="javascript:void(0);"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'BirthDateE',dateFmt: 'yyyMMdd',lang:'zh-tw'})" style ="width:20px"></a></td>
              </tr>
              <tr>
                <th scope="row">戶號</th>
                <td><input name="HouseNo" id ="HouseNo" type="text" class="text01"></td>
              </tr>
              <tr>
                <th scope="row">轄區</th>
                <td>  
                        <input ng-model="VM.location" id="tbLocation" type="text" class="text03" ng-click="openOrgs()" />
                        <input ng-model="VM.locationID" id="hfLocationID" type="hidden"  />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                        <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>
                  </td>
              </tr>
            </table></td>
          <td width="30%"><table>
              <tr>
                <th scope="row">地址類別</th>
                <td><select id="SelectAddrKind" class="select" style ="width:120px"> 
                    <option  value="0" >戶籍地</option>
                    <option  value="1" >通訊地</option>
                  </select></td>
              </tr>
              <tr>
                <th scope="row">縣/市</th>
                <td> 
                  <select id="SelectCounty" ng-model="VM.SelectCounty"  ng-change="SelectCountyChange()">
                            <option value="0">全部</option>
                            <option ng-repeat="option in VM.CountyAry" value="{{option.I}}" ng-bind="option.N"></option>
                  </select>
                  </td>
              </tr>
              <tr>
                <th scope="row">鄉鎮市區</th>
                <td>
                      <select id="SelectTown" ng-model="VM.SelectTown"  >
                                <option value="0">全部</option>
                                <option ng-repeat="option in VM.TownAry" value="{{option.I}}" ng-bind="option.N"></option>
                      </select>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                </td>
              </tr>
            </table></td>
          <td width="33%"><table>
              <tr>
                <th scope="row">姓名</th>
                <td><input name="ContactName" id="ContactName" type="text"  class="text01"></td>
              </tr>
              <tr>
                <th scope="row">身分證號</th>
                <td><input name="ContactIdNo" id="ContactIdNo" type="text"  class="text01"></td>
              </tr>
              <tr>
                <th scope="row">出生日期</th>
                <td><input name="ContactBirthDate" id="ContactBirthDate" type="text" onclick="WdatePicker({ dateFmt: 'yyyMMdd', lang: 'zh-tw' })" ><a href="javascript:(0);"><img src="/images/icon_calendar.png" onclick="WdatePicker({el:'ContactBirthDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})"></a></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
       <div class="formBtn formBtncenter">
      <input type="submit" name="send" id="SearchBtn" ng-click="changePage()" value="查詢" class="btn" />
    </div>
  </form>
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb">
            <table>
                <tr>
                    <th scope="col">序號</th>
                    <th scope="col">出生日期</th>
                    <th scope="col">身分證號</th>
                    <th scope="col">姓名</th>
                    <th scope="col">母親姓名</th>
                    <th scope="col">母親生日</th>
                    <th scope="col">母親身分證號</th>
                    <th scope="col">電話(日)</th>
                    <th scope="col">電話(夜)</th>
                    <th scope="col">行動電話</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                   <td class="aCenter" ng-bind='record["S"]' ></td>
                   <td class="aCenter" >
                       <a href="javascript:void(0);" ng-bind='record["BD"]' ng-click="goDetail(record)" class="ng-binding"></a>
                   </td>
                   <td class="aCenter" ng-bind='record["I"]' ></td>
                   <td class="aCenter" ng-bind='record["N"]'></td>
                   <td class="aCenter" ng-bind='record["MN"]'></td>
                   <td class="aCenter" ng-bind='record["MBD"]'></td>
                   <td class="aCenter" ng-bind='record["MI"]'></td>
                   <td class="aCenter" ng-bind='record["TD"]'></td>
                   <td class="aCenter" ng-bind='record["TN"]'></td>
                   <td class="aCenter" ng-bind='record["MM"]'></td>
                </tr>
            </table>
        </div>
    </section>--%>
</asp:Content>


