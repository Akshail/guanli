<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="VipMemberManage.aspx.cs" Inherits="XF.HKHJXT.Administrator.VipMemberManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#CPHMainContent_btn_Add_Vip").click(function () {
                if ($("#CPHMainContent_tb_Account").val() == "") {
                    alert("请输入登录名");
                    return false;
                }
            })
        })
        function btn_addUser_click(userid) {
            $.layer({
                type: 2,
                //closeBtn:[1,true],
                shade: [0],
                fix: false,
                title: ['添加用户', true],
                iframe: { src: 'VipMemberManage_Add.aspx?UserId=' + userid },//写一个网页
                area: ['550px', '200px'],
                offset: ['150px', ''],
                end: function () {
                    document.getElementById("CPHMainContent_btn_Search").click();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>--%>
                <asp:Label runat="server" ID="description">用户管理说明：<br />&nbsp&nbsp&nbsp&nbsp 用户列表默认展示所有用户信息。可根用户登录名进行搜索。<br />&nbsp&nbsp&nbsp&nbsp填写“登录名”及“密码”后点击“添加”可新加用户。“密码“默认为“123456”，系统管理员可自行更改。“姓名”为选填。<br />&nbsp&nbsp&nbsp&nbsp点击“重置密码”将重置用户登录密码。点击“删除”可删除用户。</asp:Label>
                <%-- <ul id="navlist" runat="server">
                                            <li id="Li1">
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="VipMemberManage.aspx">用户列表</asp:HyperLink>
                                            </li>
                                            <li id="Li2">
                                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="VipMember_Add.aspx">添加用户</asp:HyperLink>
                                            </li>                                            
                                        </ul>--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 200px;
            text-align: center;
            background-color: rgb(238, 238, 238);
            border: 1px solid #666;
        }

        .auto-style10 {
            width: 200px;
            text-align: center;
            background-color: #A2D0F2;
            border: 1px solid #666;
        }
    </style>
    <div class="content2">
        <h2 class="title2">用户列表</h2>
        <div class="maincontent">
            <table style="width: 100%" class="table_search">
                <tr>
                    <td style="width: 6%">登录名：
                    </td>
                    <td style="width: 14%">
                        <asp:TextBox ID="tb_VipName" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 10%; text-align: left">
                        <asp:Button ID="btn_Search" runat="server" Style="margin-left: 10px" Text="搜索" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Search_Click" />
                    </td>
                    <td style="width: 30%"></td>
                    <td style="width: 10%; text-align: right">
                        <asp:Button ID="btn_Output" runat="server" Visible="false" Text="导出" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_OutPut_Click" />
                    </td>
                    <td style="width: 10%; text-align: right">
                        <asp:Button ID="btn_Add" runat="server" Text="新增用户" CssClass="button button-rounded button-flat-action button-tiny" OnClick="btn_Add_Click" />
                    </td>

                </tr>
            </table>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <asp:Label runat="server" ID="Label1">共找到</asp:Label>
                                <asp:Label runat="server" ID="Label_VipCount"></asp:Label>
                                <asp:Label runat="server" ID="Label2">条记录</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gv_Vip" runat="server" class="tableclass" AutoGenerateColumns="False"
                        Width="100%"
                        AllowPaging="True" OnRowDataBound="gv_Vip_RowDataBound" OnPageIndexChanging="gv_Vip_PageIndexChanging" OnRowCommand="gv_Vip_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Account" HeaderText="登录名">
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="用户姓名">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserTypeId" HeaderText="用户类型">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IDCardNum" HeaderText="身份证号">
                                <ItemStyle HorizontalAlign="Center" Width="160px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Phone" HeaderText="手机号">
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pwd" HeaderText="密码">
                                <ItemStyle HorizontalAlign="Center" Wrap="true" Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作" ItemStyle-Width="160px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lb_ResetPwd" runat="server" CommandName="repwd" CommandArgument='<%#Eval("Id") %>'>重置密码</asp:LinkButton>
                                    <asp:LinkButton ID="lb_Update" runat="server" CommandName="upd" CommandArgument='<%#Eval("Id") %>'>修改</asp:LinkButton>
                                    <asp:LinkButton ID="lb_Delete" runat="server" CommandName="del" OnClientClick="return confirm ('确定删除？')" CommandArgument='<%#Eval("Id") %>'>删除</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="LightGray" BorderColor="White" />
                        <RowStyle Font-Strikeout="False" />
                        <%--<PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />--%>
                        <PagerTemplate>
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
                    <%-- <asp:Label ID="AddTitle_Book" runat="server" CssClass="title2" Text="添加用户"></asp:Label>
                    <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                        <tr>
                            <td class="auto-style10" colspan="2">登录名：
                            </td>
                            <td class="auto-style7">
                                <asp:TextBox ID="tb_Account" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            <td class="auto-style10" colspan="2">密码：
                            </td>
                            <td class="auto-style7">
                                <asp:TextBox ID="tb_Pwd" runat="server" Width="100%" Text="123456"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style10" colspan="2">姓名：
                            </td>
                            <td class="auto-style7">

                                <asp:TextBox ID="tb_UserName" runat="server" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%"></td>
                            <td style="width: 20%"></td>
                            <td style="width: 10%"></td>
                            <td style="width: 25%; text-align: right">
                                <asp:Button ID="btn_Add_Vip" runat="server" Text="添加" OnClick="btn_Add_Vip_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                                <asp:Button ID="btn_Clear_Vip" runat="server" Text="清空" OnClick="btn_Clear_Vip_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                            </td>
                        </tr>

                    </table>--%>
                </ContentTemplate>
                <%--  <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Add_Vip" EventName="Click" />
                </Triggers>--%>
            </asp:UpdatePanel>
            <%--<asp:Label ID="lbCount" runat="server"></asp:Label>
            <asp:HiddenField ID="hfCount" runat="server" />--%>
        </div>
    </div>
</asp:Content>
