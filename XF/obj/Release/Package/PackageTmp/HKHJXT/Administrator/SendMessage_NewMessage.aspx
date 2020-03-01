<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="SendMessage_NewMessage.aspx.cs" Inherits="XF.HKHJXT.Administrator.SendMessage_NewMessage" %>

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
    <script type="text/javascript">

        $(function () {
            $("[data-id]").click(function () {

            });
        });
    </script>
    <style>
        .panel2 li:hover {
            background-color: #99ceff;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#CPHMainContent_Button2").click(function () {
                if ($("#CPHMainContent_tb_AddName").val() == "") {
                    alert("请输入学会名称");
                    return false;
                }
                if ($("#CPHMainContent_TB_Content").val() == "") {
                    alert("请输入短信内容");
                    return false;
                }
            })
        })
    </script>
    <div class="content2">
        <h2 class="title2">编写短信</h2>
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
                                                    <asp:TextBox ID="tb_To" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; text-align: center">内&nbsp&nbsp容：</td>
                                                <td style="width: 60%" rowspan="5" onscroll="true" colspan="3">
                                                    <asp:TextBox runat="server" ID="TB_Content" Width="100%" Height="180px" TextMode="MultiLine"></asp:TextBox>
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
                                                    <asp:Button runat="server" ID="Button2" CssClass="button button-rounded button-flat-primary button-tiny" Text="发送" OnClick="btn_Send_Click" Enabled="false" />
                                                </td>
                                                <td style="text-align: right; width: 10%">
                                                    <asp:Button runat="server" ID="Button3" CssClass="button button-rounded button-flat-primary button-tiny" Text="取消" OnClick="btn_Cancel_Click" />
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
