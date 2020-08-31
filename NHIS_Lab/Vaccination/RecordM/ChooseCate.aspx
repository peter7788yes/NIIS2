<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseCate.aspx.cs" Inherits="Vaccination_RecordM_ChooseCate" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
    <form style="display:none;" id='formid' method='post' action='/Vaccination/RecordM/ChooseCate.aspx'>
            <input type="hidden" id="c" name="c"  value="<%=CaseUserID %>"/>
            <input type="hidden" id="i" name="i"  value="<%=RecordDataID %>"/>
            <input type="hidden" id="r" name="r"  value="<%=SystemRecordVaccineCode %>"/>
            <input type="hidden" id="a" name="a"  value="<%=AppointmentDate %>"/>
    </form>
    <section class="Content2">
            <h2>請選擇接種作業</h2>
            <div class="close">
                <input type="button" id="closeBtn" value="關閉" class="btn" />
            </div>
            <div class="formBtn">
                <input type="button" class="btn" id="btnHealth" value="健康評估" onclick="goHealth()" />
                <input type="button" class="btn" id="btnRecord" value="接種登錄"  onclick="goRecord()" />
                <input type="button" class="btn" id="btnEffect" value="副作用登入"  onclick="goEffect()" />
            </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <asp:Literal ID="lblScript" ClientIDMode="Static"  runat="Server" />
    <script>
        var CC=<%=CaseUserID%>;
        var II=<%=RecordDataID%>;
        var RR="<%=SystemRecordVaccineCode%>";
        var AA="<%=AppointmentDate%>";
    </script>
    <%:Scripts.Render("~/bundles/ChooseCate_JS")%>
</asp:Content>

