<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="SentEmailContent.aspx.cs" Inherits="XF.HKHJXT.Administrator.SentEmailContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <ul id="navlist" runat="server">
                    <li id="Li1">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="SendMessage.aspx">短信通知</asp:HyperLink>
                    </li>
                    <li id="Li2">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="SendEmail.aspx">电子邮件通知</asp:HyperLink>
                    </li>
                    <li id="Li3">
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="OutputEnvelope.aspx">信封贴下载</asp:HyperLink>
                    </li>
                    <li id="Li4">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="BasicInfoManage.aspx">基本信息维护</asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">

    <style type="text/css">
        span {
            -ms-word-wrap: break-word;
            word-wrap: break-word;
            -ms-word-break: break-all;
            word-break: break-all;
        }
    </style>
    <div class="content2">
        <h2 class="title2">查看邮件</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%; height: 100%">
                <tr>

                    <td style="border: 1px solid gray; width: 80%">
                        <table style="width: 100%">

                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="panel1" Width="100%">
                                        <table style="width: 100%">

                                            <tr>
                                                <td style="width: 20%; text-align: center">收件人：
                                                </td>
                                                <td style="border-bottom: solid; border-bottom-width: 1px; border-bottom-color: black; width: 80%">
                                                    <%--<asp:TextBox ID="tb_MessageTo" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>--%>
                                                    <asp:Label ID="Label_MessageTo" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 20%; text-align: center">主&nbsp&nbsp题：</td>
                                                <td style="border-bottom: solid; border-bottom-width: 1px; border-bottom-color: black; width: 80%">
                                                    <%--<asp:TextBox runat="server" ID="tb_Subject" Width="100%" BorderStyle="None"></asp:TextBox>--%>
                                                    <asp:Label ID="Label_Subject" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>

                                                <td style="width: 20%; text-align: center">内&nbsp&nbsp容：</td>
                                                <td style="width: 60%" rowspan="5" onscroll="true" colspan="3">
                                                    <%--<asp:TextBox runat="server" ID="tb_Body" Width="100%" Height="180px" TextMode="MultiLine"></asp:TextBox>--%>
                                                    <asp:Label ID="Label_Body" runat="server" Text=""></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: right; width: 50%"></td>
                                                <td style="text-align: right; width: 10%">
                                                    <%--<asp:Button runat="server" ID="Button2" CssClass="button button-rounded button-flat-primary button-tiny" Text="发送" OnClick="btn_Send_Click" />--%>
                                                </td>
                                                <td style="text-align: right; width: 10%">
                                                    <asp:Button runat="server" ID="btn_Cancel" CssClass="button button-rounded button-flat-primary button-tiny" Text="返回" OnClick="btn_Cancel_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                    </asp:Panel>

                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>

            </table>
        </div>
    </div>
</asp:Content>
