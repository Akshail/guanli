<%@ Page Title="通知公告" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="NoticeManage.aspx.cs" Inherits="XF.HKHJXT.Administrator.NoticeManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#CPHMainContent_btn_Search").click(function () {
                if ($("#input_StartDate").val() == "") {
                    alert("请选择起始日期");
                    return false;
                }
                if ($("#input_EndDate").val() == "") {
                    alert("请选择截至日期");
                    return false;
                }
            })
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table border="0" cellspacing="5" cellpadding="0" runat="server" id="userlogin">
                    <tr>
                        <td colspan="2">欢迎进入高校管理系统：
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">今天：</td>
                        <td>
                            <asp:Label ID="lblWeek" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">时间：</td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                    <asp:Timer runat="server" OnTick="Unnamed1_Tick" ID="timer1">
                                    </asp:Timer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">

    <div class="content2">
        <h2 class="title2">通知公告列表</h2>
        <div class="maincontent">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server"></asp:UpdatePanel>
            <table style="width: 100%" class="table_search">

                <tr>
                    <td style="width: 60px; text-align: center">起始日期：
                    </td>
                    <td style="width: 130px">
                        <%--<asp:TextBox ID="input_CreateTime" runat="server" Width="100%"></asp:TextBox>--%>
                        <input id="input_StartDate" runat="server" type="text" style="width:120px" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy年M月d日',vel:'d244_2'})" />
                    </td>
                    <td style="width: 60px; text-align: center">截至日期：
                    </td>
                    <td style="width: 130px">
                        <%--<asp:TextBox ID="input_CreateTime" runat="server" Width="100%"></asp:TextBox>--%>
                        <input id="input_EndDate" runat="server" type="text" style="width:120px" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy年M月d日',vel:'d244_2'})" />
                    </td>

                    <td style="text-align: left">

                        <asp:Button ID="btn_Search" runat="server" Text="搜索" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Search_Click" />

                    </td>

                    <td style="text-align: right">
                        <%--<asp:Button ID="btn_Output" runat="server" Text="导出" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_OutPut_Click" />--%>
                    </td>

                    <td style="text-align: right">
                        <asp:Button ID="btn_Add" runat="server" Text="新增通知公告" CssClass="button button-rounded button-flat-action button-tiny" OnClick="btn_Add_Click" />
                        <%--<asp:Button ID="btn_Add" runat="server" Text="新增通知公告" CssClass="btn btn-success button-tiny" OnClick="btn_Add_Click" />--%>
                    </td>

                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="Label1">共找到</asp:Label>
                        <asp:Label runat="server" ID="ResultCount"></asp:Label>
                        <asp:Label runat="server" ID="Label2">条记录</asp:Label>

                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv_Notice" runat="server" class="tableclass" AutoGenerateColumns="False"
                Width="100%" DataKeyNames="Id"
                OnPageIndexChanging="gv_Notice_PageIndexChanging"
                AllowPaging="true" PageSize="10" OnRowDataBound="gv_Notice_RowDataBound" OnRowDeleting="gv_Notice_RowDeleting" OnRowCommand="gv_Notice_RowCommand">
                <Columns>
                    <asp:BoundField Visible="false" DataField="Id" HeaderText="流水号Id">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <%-- <asp:BoundField DataField="NoticeHead" HeaderText="公告标题" ItemStyle-Width="50%">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>--%>
                    <asp:HyperLinkField DataTextField="Title" HeaderText="通知标题" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="NoticeContent.aspx?Id={0}">
                        <ItemStyle HorizontalAlign="Center" Width="50%" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="ShowTime" HeaderText="发布时间" ItemStyle-Width="30%">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:HyperLinkField HeaderText="修改" Text="修改" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="NoticeManage_Update.aspx?Id={0}">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:HyperLinkField>
                    <asp:TemplateField HeaderText="删除" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Delete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm ('确定删除？')">删除</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
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
                    到第<asp:TextBox runat="server" ID="inPageNum"></asp:TextBox>页
                                        <asp:Button ID="Button1" CommandName="go" runat="server" Text="GO" CssClass="button button-rounded button-flat-primary button-tiny" />
                    <br />
                </PagerTemplate>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
