<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="XF.HKHJXT.Administrator.SendEmail" %>

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
    <div class="content2">
        <h2 class="title2">历史邮件</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width:100%">
                <tr>
                    <td>邮箱服务器：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_EmailServer" Width="80px">
                            <asp:ListItem Text="163邮箱" Value="smtp.163.com" Selected="True" />
                            <asp:ListItem Text="QQ邮箱" Value="smtp.qq.com" />
                        </asp:DropDownList>
                    </td>
                    <td>邮箱账号：</td>
                    <td>
                        <asp:TextBox runat="server" ID="tb_Account" />
                    </td>
                    <td>邮箱密码：</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="tb_Password" TextMode="Password" Width="150" />--%>
                        <input type="password" id="tb_Password" runat="server" style="width:150px" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btn_SaveAccount" Text="更新保存" OnClick="btn_SaveAccount_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td style="width:10%"></td>
                </tr>

            </table>
            <table style="width: 100%;margin-top:10px">
                <tr>
                    <td style="width: 10%; text-align: right">起始日期：
                    </td>
                    <td style="width: 10%">
                        <%--<asp:TextBox ID="tb_AssociationName" runat="server" Width="100%"></asp:TextBox>--%>
                        <input id="input_StartDate" runat="server" type="text" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy年M月d日',vel:'d244_2'})" />
                    </td>
                    <td style="width: 10%; text-align: right">截止日期：
                    </td>
                    <td style="width: 10%">
                        <%--<asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>--%>
                        <input id="input_EndDate" runat="server" type="text" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy年M月d日',vel:'d244_2'})" />
                    </td>
                    <td style="width: 5%; text-align: right">
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Search_Click" />
                    </td>
                    <td style="width: 30%; text-align: right"></td>

                    <%-- <td style="width: 5%; text-align: right">
                        <asp:Button ID="btn_Output" runat="server" Text="导出" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_OutPut_Click" />
                    </td>--%>
                    <td style="width: 10%; text-align: right">
                        <asp:Button ID="Button2" runat="server" Text="新建邮件" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Add_Click" />
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
                AllowPaging="True" OnPageIndexChanging="gv_Email_PageIndexChanging" OnRowDataBound="gv_Email_RowDataBound" OnRowCommand="gv_Email_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" Visible="false" HeaderText="序号" />
                    <%--<asp:TemplateField HeaderText="注册时间">
                        <ItemTemplate>
                           
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="Title" HeaderText="主题">
                        <ItemStyle HorizontalAlign="Center" Width="70%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateTime" HeaderText="发送时间">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundField>
                    <asp:HyperLinkField Text="查看" HeaderText="操作" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="SentEmailContent.aspx?Id={0}">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:HyperLinkField>
                    <%-- <asp:TemplateField HeaderText="详细">
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Details" runat="server" CommandName="details" CommandArgument='<%#Eval("Id") %>'>查看</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
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

            <%--<asp:Label ID="lbCount" runat="server"></asp:Label>
            <asp:HiddenField ID="hfCount" runat="server" />--%>
        </div>
    </div>
</asp:Content>
