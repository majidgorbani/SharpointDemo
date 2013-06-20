<%@ Assembly Name="StockPhoneSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2a478750747b2bb3" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dpItem.aspx.cs" Inherits="StockPhoneSystem.Layouts.StockPhoneSystem.dpItem" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
 <table border="0">

            <thead>
                <tr>
                    <th><b>inmatning</b></th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <td colspan="2"><asp:Label ID="lblInfo" runat="server"></asp:Label></td>
                </tr>
            </tfoot>
            <tbody>
               
                <tr>
                    <td ><b>IMEI-nr:</b> </td>
                    <td ><input type="text" id="txtImei" name="txtImei" onfocus="nextfield ='done';" /></td>            
                 </tr>

                 <tr>               
                 
                 <td ><b>action</b> </td>
                    <td > <asp:Button ID="btnRemove" runat="server" Text="remove" OnClick="remove_Click" /></td>         
                 </tr>
            </tbody>
        </table>

        <br />
<br />
012021007168406<br />
012020000683585<br />
012024008071330<br />
012838006074263<br />
<br />
<br />


<asp:GridView runat="server" ID="gvMyViewout"></asp:GridView>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Application Page
</asp:Content>
