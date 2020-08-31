<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrgManagement.aspx.cs" Inherits="System_OrgManagement"  MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
    <link href="/css/tree.css" rel="stylesheet" />
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
     <style>
        ul{
	        list-style: none;
        }
        .text-field{
	        cursor: pointer;
        }
        .check-box{
	        width: 24px;
            height: 18px;
            border-radius: 8px;
        }
    </style>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">

    <section class="Content" ng-app="MyApp" ng-controller="MyController">
        <div class="path"></div>

         <div class="organizationTb">
           <table>
      <tr>
        <td  class="leftbg">
            
                 <tree-view tree-data="VM.tree" text-field="O" value-field='I' selected-Item="VM.selectedItem" item-clicked="VM.itemClicked($item)" item-checked-changed="VM.itemCheckedChanged($item)" can-checked="true" ></tree-view>
  
        </td>
        <td>
          
            <div class="formBtn formBtnleft">
                <input type="button" ng-click="goSave()" id="saveBtn" value="儲存" class="btn"   />
                <input type="button" id="addBtn" value="新增下層單位" class="btn" />
                <input type="button" id="deleteBtn" value="刪除" class="btn" />
            </div>
            <div class="formTb">
              <table>
                <tr>
                  <th scope="row"><span class="must">*</span>上層組織名稱：</th>
                  <td> 
                      <label ng-bind="VM.selectedItemParent"></label>
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>組織代碼：</th>
                  <td>
                       <input  type="text" class="text03" ng-model="VM.dataItem.AC" />
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>組織中文名稱：</th>
                  <td>
                        <input   type="text" class="text03"  ng-model="VM.dataItem.N"/>
                  </td>
                </tr>
                <tr>
                  <th scope="row">組織英文名稱：</th>
                  <td>
                       <input  type="text" class="text03" ng-model="VM.dataItem.EN" />
                  </td>
                </tr>
                <tr>
                  <th scope="row">組織簡稱：</th>
                  <td>
                        <input  type="text" class="text03" ng-model="VM.dataItem.SN" /> 
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>組織層級：</th>
                  <td>
                        <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="1" />中央</label>
                        <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="2" />區管中心</label>
                        <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="3" />局</label>
                        <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="4" />所</label>  
                        <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="5" />院</label> 
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>狀態：</th>
                  <td>
                      <label><input  type="radio" name="rb2"   ng-model="VM.dataItem.AS" value="1"/>啟用</label>
                      <label><input  type="radio" name="rb2"   ng-model="VM.dataItem.AS" value="2"/>停用</label>
                  </td>
                </tr>
                <tr>
                  <th scope="row"><span class="must">*</span>顯示順序：</th>
                  <td>
                      <input  type="number"  class="text04" ng-model="VM.dataItem.ON" />
                  </td>
                </tr>
                <tr>
                  <th scope="row">轄區設定：</th>
                  <td>
                      <input type="button" value="瀏覽"  class="btn">
                  </td>
                </tr>
                <tr>
                  <th scope="row">IP範圍：</th>
                  <td>
                    
                       <div class="row" ng-repeat="record in VM.dataItem.IP track by $index" style="margin-top:5px">
                           <input  ng-model="record['i1']" value="0" type="number" min="0" max="255" />
                           <input  ng-model="record['i2']" value="0" type="number" min="0" max="255" />
                           <input  ng-model="record['i3']" value="0" type="number" min="0" max="255" />
                           <input  ng-model="record['i4']" value="0" type="number" min="0" max="255" />
                            ~
                           <input  ng-model="record['i5']" value="0" type="number" min="0" max="255" />
                           <input  ng-model="record['i6']" value="0" type="number" min="0" max="255" />
                           <input  ng-model="record['i7']" value="0" type="number" min="0" max="255" />
                           <input  ng-model="record['i8']" value="0" type="number" min="0" max="255" />
                           
                           <span ng-show="$index==0"><a href="javascript:void(0);"><img src="/images/icon_increase.png" ></a></span>
                        </div>
                  </td>
                </tr>
              </table>
            </div>
          </td>
      </tr>
    </table>
           <%--<div class="row">
            <div class="col-sm-5" >
                <tree-view tree-data="VM.tree" text-field="O" value-field='I' selected-Item="VM.selectedItem" item-clicked="VM.itemClicked($item)" item-checked-changed="VM.itemCheckedChanged($item)" can-checked="true" ></tree-view>
            </div>
            <div class="col-sm-7" >
                    <div class="row  formBtn formBtnleft">
                        <div class="col-sm-6" >
                                <input type="button" ng-click="goSave()" id="saveBtn" value="儲存" class="btn btn-danger"   />
                                <input type="button" id="addBtn" value="新增下層單位" class="btn btn-danger" />
                        </div>
                         <div class="col-sm-6" >
                                <input type="button" id="deleteBtn" value="刪除" class="btn btn-danger" />
                         </div>
                   </div>
                    <div class="formTb">
                                <table>
                                    <tr>
                                        <th scope="row">上層組織名稱:</th>
                                        <td>
                                          <label ng-bind="VM.selectedItemParent"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row"><span class="must">*</span>組織代碼:</th>
                                        <td>
                                            <input  type="text" class="text03" ng-model="VM.dataItem.AC" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row"><span class="must">*</span>組織中文名稱:</th>
                                        <td>
                                           <input   type="text" class="text03"  ng-model="VM.dataItem.N"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">組織英文名稱:</th>
                                        <td>
                                            <input  type="text" class="text03" ng-model="VM.dataItem.EN" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">組織簡稱:</th>
                                        <td>
                                             <input  type="text" class="text03" ng-model="VM.dataItem.SN" />   
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row"><span class="must">*</span>組織層級:</th>
                                        <td>
                                              <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="1" />中央</label>
                                              <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="2" />區管中心</label>
                                              <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="3" />局</label>
                                              <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="4" />所</label>  
                                              <label class="radio-inline"><input  type="radio" name="rb1" ng-model="VM.dataItem.OL" value="5" />院</label> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row"><span class="must">*</span>狀態:</th>
                                        <td>
                                            <label class="radio-inline"><input  type="radio" name="rb2"   ng-model="VM.dataItem.AS" value="1"/>啟用</label>
                                            <label class="radio-inline"><input  type="radio" name="rb2"   ng-model="VM.dataItem.AS" value="2"/>停用</label>
                                        </td>
                                    </tr>
                                      <tr>
                                        <th scope="row"><span class="must">*</span>顯示順序:</th>
                                        <td>
                                            <input  type="number"  class="text04" ng-model="VM.dataItem.ON" />
                                        </td>
                                    </tr>
                                      <tr>
                                        <th scope="row">轄區設定:</th>
                                        <td>
                                          
                                        </td>
                                    </tr>
                                     <tr>
                                        <th scope="row">IP範圍:</th>
                                        <td>
                                           
                                            <div class="row" ng-repeat="record in VM.dataItem.IP track by $index" style="margin-top:5px">
                                                <input style="width:45px;" ng-model="record['i1']" value="0" type="number" min="0" max="255" class="text04"/>
                                                <input style="width:45px;" ng-model="record['i2']" value="0" type="number" min="0" max="255" class="text04"/>
                                                <input style="width:45px;" ng-model="record['i3']" value="0" type="number" min="0" max="255" class="text04"/>
                                                <input style="width:45px;" ng-model="record['i4']" value="0" type="number" min="0" max="255" class="text04"/>
                                                ~
                                                <input style="width:45px;" ng-model="record['i5']" value="0" type="number" min="0" max="255" class="text04"/>
                                                <input style="width:45px;" ng-model="record['i6']" value="0" type="number" min="0" max="255" class="text04"/>
                                                <input style="width:45px;" ng-model="record['i7']" value="0" type="number" min="0" max="255" class="text04"/>
                                                <input style="width:45px;" ng-model="record['i8']" value="0" type="number" min="0" max="255" class="text04"/>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
            </div>

        </div>--%>
         </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
     <script>
         var data = '<%=MyTreeData %>';
    </script>
    <%:Scripts.Render("~/bundles/OrgManagement_JS")%>
</asp:Content>




