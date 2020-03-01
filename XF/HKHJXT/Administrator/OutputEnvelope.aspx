<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="OutputEnvelope.aspx.cs" Inherits="XF.HKHJXT.Administrator.OutputEnvelope" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
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
    <div class="content2">
        <h2 class="title2">人员列表</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; width: 7%">姓名：
                    </td>
                    <td style="width: 15%; text-align: left">
                        <asp:TextBox ID="tb_PersonName" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="text-align: center; width: 10%">
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Search_Click" />
                    </td>
                    <td style="width: 20%; text-align: left">
                    </td>
                    <td style="text-align: left; width: 10%">
                        
                    </td>

                    <td style="text-align: right; width: 15%">
                        <%--<asp:Button ID="btn_Add" runat="server" Text="新增个人会员" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Add_Click" />--%>
                    </td>
                    <td style="text-align: right; width: 10%">
                        <asp:Button ID="btn_Output" runat="server" Text="导出信封贴" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_OutPut_Click" />
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
            <asp:GridView ID="gv_Member" runat="server" class="tableclass" AutoGenerateColumns="False"
                Width="100%" DataKeyNames="Id"
                OnPageIndexChanging="gv_Member_PageIndexChanging"
                AllowPaging="True" OnRowDataBound="gv_Member_RowDataBound" OnRowCommand="gv_Member_RowCommand">
                <Columns>

                    <asp:BoundField DataField="Name" HeaderText="姓名">
                        <ItemStyle HorizontalAlign="Center" Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DepartmentAddress" HeaderText="单位地址">
                        <ItemStyle HorizontalAlign="Center" Width="50%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ZipCode" HeaderText="单位邮编">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundField>

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
