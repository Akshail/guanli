<%@ Page Title="" Language="C#" MasterPageFile="~/LogIn.Master" AutoEventWireup="true" CodeBehind="LogInNoticeContent.aspx.cs" Inherits="XF.LogInNoticeContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMainContent" runat="server">
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
