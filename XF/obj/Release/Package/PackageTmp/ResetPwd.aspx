<%@ Page Title="" Language="C#" MasterPageFile="~/LogIn.Master" AutoEventWireup="true" CodeBehind="ResetPwd.aspx.cs" Inherits="XF.ResetPwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#CPHMainContent_btnUpdate").click(function () {
                if ($("#CPHMainContent_txtOldPwd").val() == "") {
                    alert("请输入旧密码");
                    return false;
                }
                if ($("#CPHMainContent_txtNewPwd").val() == "") {
                    alert("请输入新密码");
                    return false;
                }
                if ($("#CPHMainContent_txtRegNewPwd").val() == "") {
                    alert("请输入确认密码");
                    return false;
                }
                if ($("#CPHMainContent_txtRegNewPwd").val() != $("#CPHMainContent_txtNewPwd").val()) {
                    alert("请输入确认密码与新密码不同");
                    return false;
                }
            })
        })
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMainContent" runat="server">
     <div class="content2">
        <h2 class="title2">
            修改密码</h2>
        <div class="maincontent">
           <table class="table" style="width:100%">
        <tr>
            <td style="width:15%">
                旧密码:
            </td>
            <td>
                <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                新密码:
            </td>
            <td>
                <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                确认密码:
            </td>
            <td>
                <asp:TextBox ID="txtRegNewPwd" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnUpdate" runat="server" Text="修改"  CssClass="button button-rounded button-flat-primary button-tiny" onclick="btnUpdate_Click" />
                &nbsp;
                </td>
        </tr>
    </table>
        </div>
    </div>
</asp:Content>
