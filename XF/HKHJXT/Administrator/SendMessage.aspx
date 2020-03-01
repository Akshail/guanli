<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="XF.HKHJXT.Administrator.SendMessage" %>

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
    <%-- <style>table{table-layout:fixed}</style>--%>
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">

    <div class="content2">
        <h2 class="title2">历史短信</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%; text-align: center">起始日期：
                    </td>
                    <td style="width: 15%">
                        <%--<asp:TextBox ID="tb_AssociationName" runat="server" Width="100%"></asp:TextBox>--%>
                        <input id="input_StartDate" runat="server" type="text" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy年M月d日',vel:'d244_2'})" />
                    </td>
                    <td style="width: 10%; text-align: center">截止日期：
                    </td>
                    <td style="width: 15%">
                        <%--<asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>--%>
                        <input id="input_EndDate" runat="server" type="text" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy年M月d日',vel:'d244_2'})" />
                    </td>
                    <td style="width: 10%; text-align: right">
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Search_Click" />
                    </td>
                    <td style="width: 20%; text-align: right"></td>

                    <%-- <td style="width: 5%; text-align: right">
                        <asp:Button ID="btn_Output" runat="server" Text="导出" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_OutPut_Click" />
                    </td>--%>
                    <td style="width: 15%; text-align: right">
                        <asp:Button ID="Button2" runat="server" Text="新建短信" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Add_Click" />
                    </td>

                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="Result">共找到</asp:Label>
                        <asp:Label runat="server" ID="Label_ResultCount"></asp:Label>
                        <asp:Label runat="server" ID="Label2">条记录</asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv_Email" runat="server" class="tableclass" AutoGenerateColumns="False"
                Width="100%"
                AllowPaging="True" OnPageIndexChanging="gv_Email_PageIndexChanging" OnRowCommand="gv_Email_RowCommand" OnRowDataBound="gv_Email_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id" Visible="false" HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <%--<asp:TemplateField HeaderText="注册时间">
                        <ItemTemplate>                           
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="MessageContent" HeaderText="内容" ItemStyle-Wrap="true">
                        <ItemStyle HorizontalAlign="Center" Width="70%" Wrap="true" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateTime" HeaderText="发件时间">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundField>
                    <asp:HyperLinkField Text="查看" HeaderText="操作" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="SentMessageContent.aspx?Id={0}">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:HyperLinkField>
                </Columns>
                <AlternatingRowStyle BackColor="LightGray" BorderColor="White" />
                <RowStyle Font-Strikeout="False" />
                <%--<PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />--%>
                <PagerTemplate>
                    <br />
                    <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
                    <asp:LinkButton ID="lbnFirst" runat="server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
                    <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                    <asp:LinkButton ID="lbnNext" runat="server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                    <asp:LinkButton ID="lbnLast" runat="server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                    到第<asp:TextBox runat="server" ID="inPageNum" Width="5%"></asp:TextBox>页
                                        <asp:Button ID="Button1" CommandName="go" runat="server" Text="GO" CssClass="button button-rounded button-flat-primary button-tiny" />
                    <br />
                </PagerTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
