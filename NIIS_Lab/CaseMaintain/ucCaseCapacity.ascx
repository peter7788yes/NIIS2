<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCaseCapacity.ascx.cs" Inherits="CaseMaintain_ucCaseCapacity" %>
   <table runat="server" id="tbCapacity" >
    <tr ><td width="150px">區域相關</td><td> 
    <asp:CheckBoxList ID="cblCapacity_1"  CssClass ="cblCapacity" RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList> </td></tr>
     <tr><td>疫苗特定實施對象</td><td><asp:CheckBoxList ID="cblCapacity_2"  CssClass ="cblCapacity"  RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList></td></tr>
       <tr><td>社經相關</td><td><asp:CheckBoxList ID="cblCapacity_3"  CssClass ="cblCapacity"  RepeatDirection="Horizontal" runat="server"  RepeatLayout="Flow"></asp:CheckBoxList></td></tr>
 <tr><td>社政相關</td><td><asp:CheckBoxList ID="cblCapacity_4"  CssClass ="cblCapacity"  RepeatDirection="Horizontal" runat="server"  RepeatColumns="3" RepeatLayout="Flow"></asp:CheckBoxList></td></tr>
            </table>