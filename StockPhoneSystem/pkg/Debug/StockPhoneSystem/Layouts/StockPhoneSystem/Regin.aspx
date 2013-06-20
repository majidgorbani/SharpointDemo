<%@ Assembly Name="StockPhoneSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2a478750747b2bb3" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regin.aspx.cs" Inherits="StockPhoneSystem.Layouts.StockPhoneSystem.Regin" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
<SharePoint:ScriptLink ID="ScriptLink1" Name="SP.js" runat="server" OnDemand="true" Localizable="false" />

<SharePoint:FormDigest ID="FormDigest1" runat="server" />
<script type="text/javascript">
    nextfield = "txtCode"; // name of first box on page
    netscape = "";

    function keyDown(DnEvents) { // handles keypress
        // determines whether Netscape or Internet Explorer
        k = (netscape) ? DnEvents.which : window.event.keyCode;
        if (k == 13) { // enter key pressed2

            if (document.getElementById("txtCode").value == "out" || document.getElementById("txtImei").value == "out") {
                alert('out?')
                // window.location = 'main.php?action=out'
            }
            if (document.getElementById("txtCode").value == "utlev" || document.getElementById("txtImei").value == "utlev") {
                alert('utlev')
                //var user = "<?php echo $user ?>";
                //var supplier = "<?php echo $supplier ?>";
                //window.location = 'utleverans.php?action=utlev&usr=' + user + '&supl=' + supplier;
            }

            if (nextfield == 'done') {

                //eval("document.getElementById('txtCode').focus()");
                return true; // submit, we finished all fields

            }
            else { // we're not done yet, send focus to next box
                eval("document.getElementById('" + nextfield + "').focus()");

                return false;
            }
        }
    }
    document.onkeydown = keyDown; // work together to analyze keystrokes
    if (netscape) document.captureEvents(Event.KEYDOWN | Event.KEYUP);
    //  End -->




//
    function getWebProperties() {
        var ctx = new SP.ClientContext.get_current();
        this.web = ctx.get_web();
        ctx.load(this.web);
        ctx.executeQueryAsync(Function.createDelegate(this, this.onSuccess), Function.createDelegate(this, this.onFail));
    }
    function onSuccess(sender, args) {
        alert(this.name);
        //alert('web title:' + this.web.get_title() + '\n ID:' + this.web.get_id() + '\n Created Date:' + this.web.get_created());
    }
    function onFail(sender, args) {
        alert('failed to get list. Error:' + args.get_message());
    }
</script>


</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
this is the test <br />
================================
<br />
<input type="button" value="this is a test" onclick="getWebProperties();return false;" />

<br />
<br />
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
                   <td ><b>Model Code:</b> </td>
                   <td><input type="text" name="txtCode" id="txtCode" autofocus="autofocus"  onfocus="nextfield ='txtImei';" /></td>
                </tr>
                <tr>
                    <td ><b>IMEI-nr:</b> </td>
                    <td ><input type="text" id="txtImei" name="txtImei" onfocus="nextfield ='done';" /></td>            
                 </tr>

                 <tr>
                
                 
                 <td ><b>action</b> </td>
                    <td > <asp:Button ID="insert" runat="server" Text="Insert" OnClick="insert_Click" /></td>         
                 </tr>
            </tbody>
        </table>

        
        <input type="submit" name="done" value="submit" style="visibility:hidden"  />

<asp:Label ID="lblMessage" runat="server" ></asp:Label>
<br />
<br />
012021007168406 1PB661-5657<br />
012020000683585 B661-6250<br />
012024008071330 1PZM661-4780<br />
012838006074263 1PZM661-4782<br />
<br />
<br />


<asp:GridView runat="server" ID="gvMyView"></asp:GridView>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Application Page
</asp:Content>
