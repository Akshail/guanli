﻿<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="CheckAffairList_lahy.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_lahy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ImportFile(file_upload) {
            if (file_upload.files.length >= 1) {
                document.getElementById("CPHMainContent_btn_importEvent").click();
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>--%>
                <ul id="navlist" runat="server">
                    <%--<li id="Li1">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="CheckAffairList_PersonMemberInfo.aspx">个人会员信息变动</asp:HyperLink>
                    </li>
                    <li id="Li2">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="CheckAffairList_GroupMemberInfo.aspx">团体会员信息变动</asp:HyperLink>
                    </li>
                    <li id="Li9">
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="CheckAffairList_AssociationInfo.aspx">学会信息变动</asp:HyperLink>
                    </li>--%>
                    <li id="Li3">
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="CheckAffairList_zdtsqk.aspx">在沪台生信息</asp:HyperLink>
                    </li>
                    <li id="Li4">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="CheckAffairList_lahy.aspx">两岸婚姻信息</asp:HyperLink>
                    </li>
                    <li id="Li5">
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="CheckAffairList_cztb.aspx">常驻台胞信息</asp:HyperLink>
                    </li>
                    <%--<li id="Li6">
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="CheckAffairList_tzzl.aspx">为虹口发展添智助力</asp:HyperLink>
                    </li>--%>
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
                    <%--<li id="Li1">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="CheckAffairList_Paper.aspx">年度论文备案</asp:HyperLink>
                    </li>
                    <li id="Li2">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="CheckAffairList_UserDefined.aspx">自定义事务申报</asp:HyperLink>
                    </li>--%>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
    <div class="content2">
        <h2 class="title2">两岸婚姻信息</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%" class="table_search">
                <tr>
                    <td style="width: 60px">沪籍姓名：
                    </td>
                    <td style="width: 120px">
                        <asp:TextBox ID="tb_Name" runat="server" Width="100"></asp:TextBox>
                    </td>
                    <td style="width: 40px">性别：
                    </td>
                    <td style="width: 80px">
                        <asp:DropDownList runat="server" ID="ddl_sex" Width="60">
                            <asp:ListItem Value="99" Text="不限" Selected="True" />
                            <asp:ListItem Value="0" Text="男" />
                            <asp:ListItem Value="1" Text="女" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 85px">是否台湾户籍：
                    </td>
                    <td style="width: 100px">
                        <asp:DropDownList runat="server" ID="ddl_GetTWHJ" Width="80">
                            <asp:ListItem Value="99" Text="不限" Selected="True" />
                            <asp:ListItem Value="1" Text="是" />
                            <asp:ListItem Value="0" Text="否" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <%--<asp:Button ID="btn_Search" runat="server" Text="搜索" OnClick="btn_Search_Click" CssClass="button button-rounded button-flat-primary button-tiny" />--%>
                    </td>
                    <td style="width: 1px"></td>
                    <td style="width: 190px">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_AddSimple" runat="server" Text="新增" OnClick="btn_AddSimple_Click" CssClass="button button-rounded button-flat-action button-tiny" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_Download" runat="server" Text="模板" OnClick="btn_Download_Click" CssClass="button button-rounded button-flat-action button-tiny" />
                                </td>
                                <td>
                                    <%--<asp:Button ID="btn_Import" runat="server" Text="导入" OnClick="btn_Import_Click" CssClass="button button-rounded button-flat-action button-tiny" />--%>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_Import" runat="server" Text="导入" OnClientClick="CPHMainContent_import_upload.click()" CssClass="button button-rounded button-flat-action button-tiny" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Button runat="server" ID="btn_importEvent" OnClick="btn_importEvent_Click" CssClass="hide" />
                                    <asp:FileUpload runat="server" ID="import_upload" CssClass="hide" onchange="ImportFile(this)" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>出生年份：
                    </td>
                    <td colspan="3">
                        <input id="input_start" runat="server" type="text" style="width: 100px" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy'})" />
                        -
                        <input id="input_end" runat="server" type="text" style="width: 100px" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy'})" />
                    </td>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td style="width: 60px">台籍姓名：
                    </td>
                    <td style="width: 120px">
                        <asp:TextBox ID="tb_TaiName" runat="server" Width="100"></asp:TextBox>
                    </td>
                    <td style="width: 40px">性别：
                    </td>
                    <td style="width: 80px">
                        <asp:DropDownList runat="server" ID="ddl_Tai_Sex" Width="60">
                            <asp:ListItem Value="99" Text="不限" Selected="True" />
                            <asp:ListItem Value="0" Text="男" />
                            <asp:ListItem Value="1" Text="女" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 85px">是否大陆工作：
                    </td>
                    <td style="width: 100px">
                        <asp:DropDownList runat="server" ID="ddl_WorkInDL" Width="80">
                            <asp:ListItem Value="99" Text="不限" Selected="True" />
                            <asp:ListItem Value="1" Text="是" />
                            <asp:ListItem Value="0" Text="否" />
                        </asp:DropDownList>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td>结婚年份：
                    </td>
                    <td colspan="3">
                        <input id="tb_marrage_start" runat="server" type="text" style="width: 100px" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy'})" />
                        -
                        <input id="tb_marrage_end" runat="server" type="text" style="width: 100px" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy'})" />
                    </td>
                    <td style="width: 85px">婚姻是否存续：
                    </td>
                    <td style="width: 100px">
                        <asp:DropDownList runat="server" ID="ddl_IsCouple" Width="80">
                            <asp:ListItem Value="99" Text="不限" Selected="True" />
                            <asp:ListItem Value="1" Text="是" />
                            <asp:ListItem Value="0" Text="否" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" OnClick="btn_Search_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td colspan="2"><asp:Button ID="btn_Output" runat="server" style="float:right" Text="导出" CssClass="button button-rounded button-flat-action button-tiny" OnClick="btn_Output_Click"/></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="Label1">两岸婚姻信息 共找到</asp:Label>
                        <asp:Label runat="server" ID="Label_Result"></asp:Label>
                        <asp:Label runat="server" ID="Label2">条记录</asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gv_Result" runat="server" class="tableclass" AutoGenerateColumns="False"
                            Width="100%" DataKeyNames="Id" AllowPaging="True" OnPageIndexChanging="gv_Result_PageIndexChanging" OnRowDataBound="gv_Result_RowDataBound" OnRowCommand="gv_Result_RowCommand">
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
                                <asp:BoundField DataField="IdentityType" HeaderText="证件类型">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IdentityNo" HeaderText="证件号码">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AddressOfH" HeaderText="在沪居住地">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:HyperLinkField HeaderText="操作" Text="查看/修改" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="CheckAffairList_lahy_look.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:HyperLinkField>
                                <%--<asp:HyperLinkField HeaderText="修改" Text="修改" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="CheckAffairList_lahy_look.aspx?Id={0}">
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
