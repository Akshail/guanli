<%@ Page Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master"  AutoEventWireup="true" CodeBehind="SchoolManage.aspx.cs" Inherits="XF.HKHJXT.Administrator.SchoolManage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
    <script type="text/javascript">
        function btn_addUser_click(userid) {
            $.layer({
                type: 2,
                //closeBtn:[1,true],
                shade: [0],
                fix: false,
                title: ['添加学校', true],
                iframe: { src: 'SchoolManage_Add.aspx?UserId=' + userid },//写一个网页
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
                <asp:Label runat="server" ID="description">学校管理说明：<br />&nbsp&nbsp&nbsp&nbsp 学校列表默认展示所有学校信息。可根据学校名字进行搜索。<br />&nbsp&nbsp&nbsp&nbsp点击“修改”可修改学校信息。点击“删除”可删除学校。</asp:Label>
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
        <h2 class="title2">学校列表</h2>
        <div class="maincontent">
            <table style="width: 100%" class="table_search">
                <tr>
                    <td style="width: 6%">学校名：
                    </td>
                    <td style="width: 14%">
                        <asp:TextBox ID="tb_VipName" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td style="width: 10%; text-align: left">
                    <asp:Button ID="btn_Search" runat="server" Style="margin-left: 10px" Text="搜索" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Search_Click" />
                    </td>
                    <td style="width: 30%"></td>
                    <td style="width: 10%; text-align: right">
   <%--                     <asp:Button ID="btn_Output" runat="server" Visible="false" Text="导出" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_OutPut_Click" />--%>
                    </td>
                    <td style="width: 10%; text-align: right">
                    <asp:Button ID="btn_Add" runat="server" Text="新增学校" CssClass="button button-rounded button-flat-action button-tiny" OnClick="btn_Add_Click" />
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
                        AllowPaging="True"  OnPageIndexChanging="gv_Vip_PageIndexChanging" OnRowCommand="gv_Vip_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="SchoolName" HeaderText="学校名">
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SchoolCode" HeaderText="学校代码">
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作" ItemStyle-Width="170px">
                                <ItemTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

