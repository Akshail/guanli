<%@ Page Title="" Language="C#" MasterPageFile="~/LogIn.Master" AutoEventWireup="true" CodeBehind="LogInPage.aspx.cs" Inherits="XF.HKHJXT.LogInPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--     <script type="text/javascript">
         $(function () {
             if ($("#CPHMainContent_hfCount").val() > 0) {
                 var i = 0;
                 $.layer({
                     dialog: { type: 0, msg: '还有未处理的问题校服,请尽快处理！' }
                 });
             }
         })
    </script>--%>    <%--<asp:HiddenField ID="hfCount" runat="server" />--%>
    <style type="text/css">
        a {
            font-size: 12px;
            color: #6F6F6F;
        }

            a:hover {
                cursor: pointer;
                color: #6F6F6F;
                text-decoration: underline;
            }

        tr {
            border-bottom: 1px dotted rgb(204, 204, 204);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMainContent" runat="server">
    <div class="content2">
        <h2 class="title2">通知公告</h2>
        <div class="maincontent">
            <%--<asp:HiddenField ID="hfCount" runat="server" />--%>

            <asp:GridView ID="gv_Notice" runat="server" class="tableclass" AutoGenerateColumns="False"
                Width="100%"
                OnPageIndexChanging="gv_Notice_PageIndexChanging"
                AllowPaging="true" PageSize="10" OnRowDataBound="gv_Notice_RowDataBound">
                <Columns>
                    <asp:BoundField Visible="false" DataField="Id" HeaderText="流水号Id">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <%-- <asp:BoundField DataField="NoticeHead" HeaderText="公告标题" ItemStyle-Width="50%">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>--%>
                    <asp:HyperLinkField DataTextField="Title" HeaderText="通知标题" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="LogInNoticeContent.aspx?Id={0}">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="ShowTime" HeaderText="发布时间" ItemStyle-Width="30%">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <%--   <asp:TemplateField HeaderText="操作" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_Update" runat="server" CommandName="Update" CommandArgument='<%#Eval("Id") %>'>修改</asp:LinkButton>
                            <asp:LinkButton ID="lb_Delete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("Id") %>'>删除</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                </Columns>
                <AlternatingRowStyle BackColor="LightGray" BorderColor="White" />
                <RowStyle Font-Strikeout="False" />
                <PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="前一页" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
