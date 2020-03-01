<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="SendEmail_NewEmail.aspx.cs" Inherits="XF.HKHJXT.Administrator.SendEmail_NewEmail" %>

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
            word-break: break-all;
        }
    </style>
    <div class="content2">
        <h2 class="title2">编写邮件</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%; height: 100%">
                <tr>
                    <%-- <td style="border: 1px solid gray; width: 20%; height: 100%; margin: 0 0 0 0">
                        <table style="height: 100%; width: 20%">
                            <tr>
                                <td style="text-align: center; height: 15px">
                                    <label>会员列表</label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-bottom: 1px solid black; padding-bottom: 2px; height: 15px">
                                    <input id="Input_Search" runat="server" type="text" value="查询会员" style="color: gray" onclick="if (value == '查询会员') { value = '' }" onblur="if(value==''){value='查询会员'}" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="panel2" Height="100%">
                                        <ul style="margin: 0 auto; padding: 0 0 0 0 0; height: 100%; min-height: 300px; list-style-type: none">
                                            <li class="button-flat" data-id="1" title="123456@outlook.com" onclick="AddAdress(this,element)">
                                                <a href="javascript:void(0)" style="color: black">张三</a>
                                            </li>
                                            <li class="button-flat" data-id="2" title="123456@outlook.com">
                                                <a href="javascript:void(0)" style="color: black">123456@outlook.com</a>
                                            </li>
                                            <li class="button-flat">2</li>
                                            <li class="button-flat">3</li>
                                            <li class="button-flat">4</li>
                                            <li class="button-flat">1</li>
                                        </ul>
                                    </asp:Panel>
                                </td>

                            </tr>
                        </table>
                    </td>--%>

                    <td style="border: 1px solid gray; width: 80%">
                        <table style="width: 100%">

                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="panel1" Width="100%">
                                        <table style="width: 100%">

                                            <tr>
                                                <td style="width: 20%; text-align: center">收件人：
                                                </td>
                                                <td style="border-bottom: solid; border-bottom-width: 1px; border-bottom-color: black; width: 80%;height:auto">
                                                    <asp:TextBox ID="Label_MessageTo" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                                   <%-- <asp:Label ID="Label_MessageTo" runat="server" Text="Label"></asp:Label>--%>
                                                </td>


                                            </tr>

                                            <tr>
                                                <td style="width: 20%; text-align: center">主&nbsp&nbsp题：</td>
                                                <td style="border-bottom: solid; border-bottom-width: 1px; border-bottom-color: black; width: 80%">
                                                    <asp:TextBox runat="server" ID="tb_Subject" Width="100%" BorderStyle="None"></asp:TextBox>
                                                </td>

                                            </tr>

                                            <tr>

                                                <td style="width: 20%; text-align: center">内&nbsp&nbsp容：</td>
                                                <td style="width: 60%" rowspan="5" onscroll="true" colspan="3">
                                                    <asp:TextBox runat="server" ID="tb_Body" Width="100%" Height="180px" TextMode="MultiLine"></asp:TextBox>
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
                                                    <asp:Button runat="server" ID="Button2" CssClass="button button-rounded button-flat-primary button-tiny" Text="发送" OnClick="btn_Send_Click" />
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
