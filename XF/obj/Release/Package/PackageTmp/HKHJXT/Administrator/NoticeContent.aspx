<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="NoticeContent.aspx.cs" Inherits="XF.HKHJXT.Administrator.NoticeContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
    <style type="text/css">
        span {
            word-break: break-all;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table border="0" cellspacing="5" cellpadding="0" runat="server" id="userlogin">
                    <tr>
                        <td colspan="2">欢迎进入上海市杨浦区人民政府台湾事务办公室工作信息管理系统：
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">今天：</td>
                        <td>
                            <asp:Label ID="lblWeek" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">时间：</td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                    <asp:Timer runat="server" OnTick="Unnamed1_Tick" ID="timer1">
                                    </asp:Timer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="left" valign="middle">天气：</td>
                        <td>
                            <iframe width="120" scrolling="no" height="50" frameborder="0" allowtransparency="true" src="http://i.tianqi.com/index.php?c=code&id=17&icon=1&num=3"></iframe>
                        </td>
                    </tr>--%>

                </table>

                <ul id="navlist" runat="server">
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <div class="content2">
        <h2 class="title2">通知公告详情</h2>
        <div class="maincontent">
            <table style="width: 100%; margin-top: 10px">
                <tr>
                    <td style="width: 100%; text-align: center">
                        <h3>
                            <asp:Label runat="server" ID="label_Title">
                            </asp:Label>
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center">发布时间：
                        <asp:Label ID="label_ShowTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="-ms-word-break: break-all; word-break: break-all">
                    <td style="width: 100%; text-align: left; padding: 10px 10px 10px 10px; -ms-word-break: break-all; word-break: break-all">
                        <div>
                            <asp:Label ID="label_Content" runat="server"></asp:Label>
                            <%-- --%>
                        </div>
                        <%--                        <div style="text-align: right">
                            上海市科普作家协会<br />
                            2014年12月19日
                        </div>--%>
                    </td>
                </tr>
            </table>
            <table style="margin-bottom: 10px">
                <tr>
                    <td style="text-align: center; width: 20%"></td>
                    <td style="text-align: center; width: 20%"></td>
                    <td style="text-align: center; width: 20%"></td>
                    <td style="text-align: center; width: 20%"></td>
                    <td style="text-align: right; width: 20%">
                        <%--<asp:Button ID="Output" runat="server" Text="导出" OnClick="btn_Output_Click" CssClass="button button-rounded button-flat-primary button-tiny" />--%>

                    </td>

                    <td style="text-align: right; width: 20%">
                        <asp:Button ID="Cancle" runat="server" Text="返回" OnClick="btn_Cancle_Click" CssClass="button button-rounded button-flat-primary button-tiny" />

                    </td>

                </tr>
            </table>
        </div>
    </div>
</asp:Content>
