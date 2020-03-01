<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="BasicInfoManage.aspx.cs" Inherits="XF.HKHJXT.Administrator.BasicInfoManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css" />
    <script type="text/javascript">
        function btn_Add_jbxx_Click(title, url) {
            $.layer({
                type: 2,
                shade: [0],
                fix: false,
                title: [title, true],
                iframe: { src: url },
                area: ['600px', '200px'],
                offset: ['150px', ''],
                end: function () {
                    document.getElementById("CPHMainContent_btn_Search").click();
                }
            });
        }
        function ImportFile(file_upload) {
            if (file_upload.files.length >= 1) {
                document.getElementById("CPHMainContent_btn_importEvent").click();
            }
        }
    </script>
    <style>
        .hide {
            display: none;
        }
    </style>
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
    <div class="content2">
        <h2 class="title2">基本信息维护</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <table style="width: 100%">
                <tr>
                    <td style="width: 8%; text-align: right">姓名：
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="tb_Name" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 10%; text-align: right">手机号：
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="tb_phone" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 9%; text-align: right">邮箱：
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="tb_email" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 10%; text-align: right">
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" OnClick="btn_Search_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td style="width: 20%; text-align: right"></td>
                    <td style="width: 20%; text-align: right">
                        <asp:Button ID="btn_Add" runat="server" Text="新增" OnClick="btn_Add_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td style="width: 20%; text-align: right">
                        <asp:Button ID="btn_template" runat="server" Text="模板下载" OnClick="btn_template_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td style="width: 20%; text-align: right">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_import" runat="server" Text="导入数据" OnClientClick="CPHMainContent_import_upload.click()" CssClass="button button-rounded button-flat-primary button-tiny" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button runat="server" ID="btn_importEvent" OnClick="btn_importEvent_Click" CssClass="hide" />
                        <asp:FileUpload runat="server" ID="import_upload" CssClass="hide" onchange="ImportFile(this)" />
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
            <asp:GridView ID="gv_basicInfo" runat="server" class="tableclass" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnRowCommand="gv_basicInfo_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" Visible="false" HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="姓名" ItemStyle-Wrap="true">
                        <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="true" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Phone" HeaderText="手机号">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="邮箱地址">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DepartmentAddress" HeaderText="单位地址">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ZipCode" HeaderText="单位邮编">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Update" runat="server" CommandName="Upd" CommandArgument='<%#Eval("Id") %>'>修改</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Delete" runat="server" CommandName="Del" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm ('确定删除？')">删除</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="LightGray" BorderColor="White" />
                <RowStyle Font-Strikeout="False" />
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
