<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="SendEmail_SelectPerson.aspx.cs" Inherits="XF.HKHJXT.Administrator.SendEmail_SelectPerson" %>

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
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css" />

    <div class="content2">
        <h2 class="title2">选择收件人</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%;display:none;">
                        <tr>
                            <%-- <td style="width: 10%">会员姓名：
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="tb_PersonName" runat="server" Width="100%"></asp:TextBox>
                            </td>
                           
                            <td style="width: 10%">学会名称：
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="tb_AssociationName" runat="server" Width="100%"></asp:TextBox>
                            </td>--%>
                            <td style="width: 10%">范围：
                            </td>
                            <td style="width: 20%">
                                <%--<asp:TextBox ID="tb_AssociationJob" runat="server" Width="100%"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddl_MemberJob" runat="server" Width="100%">
                                    <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                   <%-- <asp:ListItem Text="理事(会)长" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="常务副理事(会)长" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="副理事(会)长" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="秘书长" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="副秘书长" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="常务理事" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="理事" Value="7"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>

                            <td style="text-align: left; width: 10%">
                                <asp:Button ID="btn_Search" runat="server" Text="搜索" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Search_Click" />
                            </td>
                            <td style="width: 10%"></td>
                            <td style="width: 20%"></td>
                            <td style="width: 10%"></td>
                            <td style="width: 20%"></td>
                        </tr>

                    </table>

                    <table style="width: 100%; border: 1px solid black">
                        <tr>
                            <td>
                                <asp:CheckBox ID="cb_SelectAll" Text="全选" runat="server" OnCheckedChanged="cb_SelectAll_CheckedChanged" AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" AutoPostBack="True" RepeatColumns="5"></asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right; width: 50%"></td>
                            <td style="text-align: right; width: 10%">
                                <asp:Button runat="server" ID="btn_Next" CssClass="button button-rounded button-flat-primary button-tiny" Text="确定收件人" OnClick="btn_Next_Click" />
                            </td>
                            <td style="text-align: right; width: 10%">
                                <asp:Button runat="server" ID="btn_Cancle" CssClass="button button-rounded button-flat-primary button-tiny" Text="取消" OnClick="btn_Cancel_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
