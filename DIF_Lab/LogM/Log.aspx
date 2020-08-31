<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeFile="Log.aspx.cs" Inherits="Log" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ MasterType virtualpath="~/MasterPage/MasterPage.master" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
        <%=HeadScript %>
</asp:Content> 

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">




    <div  ng-app="MyApp" ng-controller="MyController" ng-cloak>
            <h2 ng-bind="::x.h2"></h2>
         <form name="form1" id="form1" >
                 <span ng-bind="1435558988140 | date:'{yyyy-1}-MM-dd HH:mm:ss Z'"></span><br/>
                 <span ng-bind="1435558988140 | LongTaiwanDate"></span><br/>
                 <span ng-bind="1435558988140 | LongTaiwanDate:true"></span><br/>
                 <span ng-bind="1435558988140 | ShortTaiwanDate"></span><br/>
                <%--<input type="text" name="rocId"  ng-model="A123456789" check-roc-id/>
                <span ng-show="form1.rocId.$error.checkRocId">XXXXXX</span>--%>

                <br/><br/><br />


                1.<input type="number"   ng-model="x.numberF" value="17" /><br/>
                2.<input type="number"   ng-model="x.numberF" /><br/>
                3.<input type="number"   ng-model="x.numberF" /><br/>
                4.<span ng-bind="::x.numberF"></span><br/>
                5.<span ng-bind="x.numberF"></span><br/>

               <input ng-model="x.numberF2"></input><br/>
                {{numberF2}}<br/>
                <input ng-model="numberF2"></input><br/>
                <span ng-bind="x.numberF2" ng-click="onclick(x.numberF2)" ></span><br/>

                7.<span ng-bind="x.nowDate | date:'HH:mm:ss' "></span><br/>
             

                 8.  <span ng-bind="x.nowDate | LongTaiwanDate"></span><br/>
                 9.  <span ng-bind="x.nowDate | LongTaiwanDate:true"></span><br/>
                 10. <span ng-bind="x.nowDate | ShortTaiwanDate"></span><br/>
                 11. <span ng-bind="x.nowDate | LongTaiwanTime"></span><br/>


               <%-- 6.{{::x.numberF  }}<br/>
                7.{{::x.numberF  }}--%>

             <br/><br/><br/>


                <input-rocid  vm="x.rocidF" name="rocidF" err="xxx" form="form1" required></input-rocid><br/>
              <span> </span>

                <input-number  vm="x.numberF" name="numberF" err="數字錯誤" form="form1" min="20" max="30"></input-number><br/>

                <input-submit value="送出" name="submitBtn" err="OOO" form="form1" ></input-submit><br/>
                <a ng-click="changePage(1)">click</a><br/>
                <page-nav ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav3.html"></page-nav>
             <table-grid ng-model="TM" template-url="/html/ang_template/TableGrid1.html"></table-grid>
             <%--<table  border="1" ng-cloak>
                <thead>
                    <tr>
                        <th ng-repeat="item in TM.tbHeadArray  track by $index" ng-bind="::item"></th>
                    </tr>
                </thead>
                <tfoot></tfoot>
                <tbody>
                    <tr ng-repeat="record in TM.tbData track by $index" data-ng-init="tbHeadArray=TM.tbHeadArray;tbFieldArray=TM.tbFieldArray;isHtml=TM.isHtml;IsDate=IsDate">
                        <!--<td ng-repeat="field in tbHeadArray" ng-if="isHtml==true"  ng-bind-html="record[field]"></td>
                        <td ng-repeat="field in tbHeadArray" ng-if="isHtml==false"  ng-bind="record[field] | LongTaiwanDate:true"></td>-->
                        <td ng-repeat="field in tbFieldArray"   ng-bind-html="IsDate(record[field]) ?(record[field] | LongTaiwanDate:true) : record[field]"></td>
                    </tr>
                </tbody>
            </table>--%>


         </form>
    </div>
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <%:Scripts.Render("~/bundles/LogM_JS")%>
</asp:Content> 
    


