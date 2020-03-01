<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="CheckAffairList_tzqy.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffair_tzqy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>--%>
                <ul id="navlist" runat="server">
                    <li id="Li3">
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="CheckAffairList_zdtsqk.aspx">在沪台生信息</asp:HyperLink>
                    </li>
                    <li id="Li4">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="CheckAffairList_lahy.aspx">两岸婚姻信息</asp:HyperLink>
                    </li>
                    <li id="Li5">
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="CheckAffairList_cztb.aspx">常驻台胞信息</asp:HyperLink>
                    </li>
                    <li id="Li8">
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="CheckAffairList_tzqy.aspx">台资企业信息</asp:HyperLink>
                    </li>
                    <li id="Li7">
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="CheckAffairList_zxgz.aspx">专项工作信息</asp:HyperLink>
                    </li>
                    <li id="Li9">
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="CheckAffairList_ypqstdkjlqk.aspx">里长交流工作信息</asp:HyperLink>
                    </li>
                     <li id="Li10">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="CheckAffairList_tzjlgz.aspx">团组交流工作信息</asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
    <div class="content2">
        <h2 class="title2">台资企业信息</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%" class="table_search">
                <tr>
                    <td style="width: 60px">企业名称：
                    </td>
                    <td style="width: 100px">
                        <asp:TextBox ID="tb_Name" runat="server" Width="80"></asp:TextBox>
                    </td>
                     <td style="width: 75px">法定代表人：
                    </td>
                    <td style="width: 100px">
                        <asp:TextBox ID="tb_Referee" runat="server" Width="80"></asp:TextBox>
                    </td>
                     <td style="width: 60px">联系人：
                    </td>
                    <td style="width: 100px">
                        <asp:TextBox ID="tb_Contact" runat="server" Width="80"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btn_Search" runat="server" Text="搜索"  OnClick="btn_Search_Click"  CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td style="width:145px"></td>
                    <td>
                        <asp:Button ID="btn_AddSimple" runat="server" Text="新增"  OnClick="btn_AddSimple_Click"  CssClass="button button-rounded button-flat-action button-tiny" />

                    </td>
                   <%-- <td style="text-align: right; width: 10%">
                        <asp:Button ID="btn_Output" runat="server" Text="导出" Visible="false" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>--%>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="Label1">台资企业信息 共找到</asp:Label>
                        <asp:Label runat="server" ID="Label_Result"></asp:Label>
                        <asp:Label runat="server" ID="Label2">条记录</asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gv_Result" runat="server" class="tableclass" AutoGenerateColumns="False"
                            Width="100%" DataKeyNames="Id" AllowPaging="True" OnPageIndexChanging="gv_Result_PageIndexChanging" OnRowCommand="gv_Result_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="序号" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="企业名称">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Domicile" HeaderText="住所地">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Referee" HeaderText="法定代表人">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Contacts" HeaderText="联系人">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PhoneNo" HeaderText="联系电话">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="邮箱地址">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:HyperLinkField HeaderText="操作" Text="查看/修改" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="CheckAffairList_tzqy_Add.aspx?Id={0}" >
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:HyperLinkField>
                               <%--<asp:HyperLinkField HeaderText="修改" Text="修改" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="CheckAffairList_tzqy_Add.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:HyperLinkField>--%>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lb_Del" runat="server" CommandName="Del" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm('确定删除？')">删除</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="LightGray" BorderColor="White" />
                            <RowStyle Font-Strikeout="False" />
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
