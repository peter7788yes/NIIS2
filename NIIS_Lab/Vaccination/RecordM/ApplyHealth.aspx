<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyHealth.aspx.cs" Inherits="Vaccination_RecordM_ApplyHealth" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
    <section class="Content3">
          <form id="MyForm" runat="server" ClientIDMode="Static"  autocomplete="off" >
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
            <div class="formTb formTb2 formTb3">
                        <table>
                            <tr>
                                <th scope="row">建檔者:</th>
                                <td><%:UserName %></td>
                                 <th scope="row">評估者：</th>
                                <td>
                                    <select id="selectUser">
                                    </select>
                                    </td>
                                 <th scope="row">評估日期：</th>
                                <td>
                                    <input id="tbDate"  type="text"  value="<%:nowDate %>" />
                                     <img style="cursor:pointer" onclick="WdatePicker({el:'tbDate',dateFmt: 'yyyMMdd',lang:'zh-tw'})" src="/images/icon_calendar.png"/>
                                </td> 
                            </tr>
                      </table>
            </div>
             <div class="formTb formTb2">
                <table id="tb">
                    <tr>
                        <%--<td>
                            <label>其他<input id="tbOther" type="text" class="text03"/></label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <span style="color:red">※警示：第七、八、九項上列狀況如經評估為〝是〞者，應依其規範之間隔時間再接種水痘疫苗或麻疹、腮腺炎、德國麻疹混合疫苗</span>
                        </td>
                   </tr>
                    <tr>
                        <td>
                            評估是否接種：
                            <label><input id="rbAllow" type="radio" name="rb1" value="1" />是</label>
                            <label><input id="rbDeny" type="radio" name="rb1" value="0" checked="checked" />否</label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <label><input id="cbAllow" value="99" type="checkbox"/>可接種</label>
                        </td>
                    </tr>--%>
                </table>   
            </div>
                <div class="list01">
                    備註：
                    <ul >
                        <li></li>
                        <li>一、以上評估結果請按「各項常規疫苗接種禁忌與注意事項｣，決定是否給予接種，並給予疫苗接種後相關衛教。</li>
                        <li>二、如無法判定，請協調家屬帶幼兒前往預注協辦醫院診所，請醫師檢查後再決定是否接種，如續由衛生所接種，請持醫師醫囑。</li>
                  
                    </ul>
                </div>
                <div class="formBtn">
                        <% if (AddPower.HasPower) { %>
                         <input type="button" id="saveBtn" value="儲存" class="btn" />
                        <% } %>
                         <input type="button" id="closeBtn" value="取消" class="btn" />
                </div>
            </form>
    </section>
    <script>
        var stateAry =<%=StateListAry %>;
        var userAry =<%=UserAry %>;
        var CC=<%:CaseUserID%>;
        var II=<%:RecordDataID%>;
        var RR="<%:SystemRecordVaccineCode%>";
        var RRI="<%:SystemRecordVaccineID%>";
        var AA="<%:AppointmentDate%>";
        var UU="<%:UpdateUID%>";
        var UUdata = <%=UpdateUserData%>;
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="jsCP" runat="Server">
    <script src="/js/date/WdatePicker.js"></script>
</asp:Content>