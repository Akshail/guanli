<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="LogManage.aspx.cs" Inherits="XF.HKHJXT.Administrator.LogManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">

    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>--%><%--<ul id="navlist" runat="server">
                    <li id="Li1">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="LogManage.aspx">日志查看</asp:HyperLink>
                    </li>
                </ul>--%>
                <asp:Label ID="lbDescription" runat="server">系统日志说明：<br />&nbsp&nbsp&nbsp&nbsp系统日志记录所有登录本系统的用户操作，包括登录用户信息及用户操作信息。</asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <div class="content2">
        <h2 class="title2">日志查看</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="Result">共找到</asp:Label>
                        <asp:Label runat="server" ID="Label_ResultCount"></asp:Label>
                        <asp:Label runat="server" ID="Label2">条记录</asp:Label>

                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv_Result" runat="server" class="tableclass" AutoGenerateColumns="False"
                Width="100%"
                AllowPaging="True" OnRowDataBound="gv_Result_RowDataBound" OnPageIndexChanging="gv_Result_PageIndexChanging" OnRowCommand="gv_Result_RowCommand">
                <Columns>

                    <asp:BoundField DataField="Account" HeaderText="用户账号">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateUserType" HeaderText="用户类别">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateTime" HeaderText="操作时间">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OperateType" HeaderText="操作类别">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OperateObject" HeaderText="操作对象">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Ip" HeaderText="用户IP地址">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <%--<asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="ok" CommandArgument='<%#Eval("ProblemAreaID") %>'>批准</asp:LinkButton>
                            <asp:LinkButton ID="lb_Details" runat="server" CommandName="no" CommandArgument='<%#Eval("ProblemAreaID") %>'>拒绝</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <AlternatingRowStyle BackColor="LightGray" BorderColor="White" />
                <RowStyle Font-Strikeout="False" />
                <%--<PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />--%>
                <PagerTemplate>
                    <br />
                    <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
                    <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
                    <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                    <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                    <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                    到第<asp:TextBox runat="server" ID="inPageNum" Width="5%"></asp:TextBox>页
                                        <asp:Button ID="Button1" CommandName="go" runat="server" Text="GO" CssClass="button button-rounded button-flat-primary button-tiny" />
                    <br />
                </PagerTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
