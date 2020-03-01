<%@ Page Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master"  AutoEventWireup="true" CodeBehind="CheckAffairList_zxgz.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_zxgz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <ul id="navlist" runat="server">
                     <li id="Li3">
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="CheckAffairList_zdtsqk.aspx">在校本科生信息</asp:HyperLink>
                    </li>
                    <li id="Li5">
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="CheckAffairList_cztb.aspx">教职工信息</asp:HyperLink>
                    </li>
                    <li id="Li8">
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="CheckAffairList_tzqy.aspx">合作大学信息</asp:HyperLink>
                    </li>
                    <li id="Li7">
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="CheckAffairList_zxgz.aspx">专项工作信息</asp:HyperLink>
                    </li>
                     <li id="Li10">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="CheckAffairList_tzjlgz.aspx">项目交流工作信息</asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
    <div class="content2">
        <h2 class="title2">专项工作信息</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%" class="table_search">
              <tr>
                    <td style="width: 40px">姓名：
                    </td>
                    <td style="width: 100px">
                        <asp:TextBox ID="tb_Name" runat="server" Width="80"></asp:TextBox>
                    </td>
                    <td style="width:40px;">性别：
                    </td>
                    <td style="width:100px;">
                        <asp:DropDownList runat="server" ID="ddl_sex" Width="80">
                            <asp:ListItem Value="99" Text="不限"  Selected="True"/>
                            <asp:ListItem Value="False" Text="男" />
                            <asp:ListItem Value="True" Text="女" />
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" OnClick="btn_Search_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td style="width:360px"></td>
                    <td>
                        <asp:Button ID="btn_AddSimple" runat="server" Text="新增" OnClick="btn_AddSimple_Click" CssClass="button button-rounded button-flat-action button-tiny" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="Label1">专项工作信息 共找到</asp:Label>
                        <asp:Label runat="server" ID="Label_Result"></asp:Label>
                        <asp:Label runat="server" ID="Label2">条记录</asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gv_Result" runat="server" class="tableclass" AutoGenerateColumns="False"
                            Width="100%" DataKeyNames="Id" AllowPaging="True" OnPageIndexChanging="gv_Result_PageIndexChanging" OnRowCommand="gv_Result_RowCommand" OnRowDataBound="gv_Result_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="序号" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="姓名">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sex" HeaderText="性别">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Birthday" HeaderText="出生年月">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PhoneNo" HeaderText="联系电话">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Exchange" HeaderText="对口交流地区">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Type" HeaderText="交流对象类别">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Link_check" runat="server" CommandName="Chk" CommandArgument='<%#Eval("Id") %>'>查看/修改</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Link_delete" runat="server" CommandName="Del" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm('确定删除？')">删除</asp:LinkButton>
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
