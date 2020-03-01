<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="CheckAffairList_zdtsqk_Add.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_zdtsqk_Add" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css" />
    <style type="text/css">
        .auto-style1 {
            width: 30px;
            text-align: center;
            background-color: #A2D0F2;
            border: 1px solid #666;
        }

        .auto-style2 {
            width: 70px;
            text-align: center;
            background-color: #A2D0F2;
            border: 1px solid #666;
        }

        .auto-style3 {
            width: 280px;
            text-align: center;
            background-color: rgb(238, 238, 238);
            border: 1px solid #666;
        }

        .auto-style4 {
            width: auto;
            text-align: center;
            background-color: rgb(238, 238, 238);
            border: 1px solid #666;
        }

        .auto-style5 {
            width: 105px;
            text-align: center;
            background-color: #A2D0F2;
            border: 1px solid #666;
        }

        .auto-style7 {
            width: 250px;
            text-align: center;
            background-color: rgb(238, 238, 238);
            border: 1px solid #666;
        }

        .auto-style11 {
            width: 20%;
            height: 28px;
        }
    </style>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
    <script type="text/javascript">

        function btn_Add_jlqk_Click(option, url) {
            if ((document.getElementById("CPHMainContent_label_ResultCount").innerHTML * 1) >= 5) {
                layer.alert('家庭成员最多只能5条记录！');
                return;
            }
            $.layer({
                type: 2,
                shade: [0],
                fix: false,
                title: [option, true],
                iframe: { src: url },
                area: ['600px', '200px'],
                offset: ['150px', ''],
                end: function () {
                    document.getElementById("CPHMainContent_btn_Refresh").click();
                }
            });
        }
        function btn_Add_xxjl_Click(option, url) {
            if ((document.getElementById("CPHMainContent_label6").innerHTML * 1) >= 10) {
                layer.alert('学习经历最多只能10条记录！');
                return;
            }
            $.layer({
                type: 2,
                shade: [0],
                fix: false,
                title: [option, true],
                iframe: { src: url },
                area: ['600px', '200px'],
                offset: ['150px', ''],
                end: function () {
                    document.getElementById("CPHMainContent_btn_Refresh2").click();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">

                <div id="toolbar" style="position: fixed; text-align: left">
                    <asp:Label ID="label1" runat="server" Style="-ms-word-break: break-all; word-break: break-all; -ms-word-wrap: break-word; word-wrap: break-word">
                        导出：<br />
                        &nbsp&nbsp&nbsp;若当前操作是新增，请先将信息填<br />写完成后点击上方的确定添加按钮，<br />添加成功后在进入该页面进行导出；<br />若当前操作是修改则可直接导出。导出<br />功能只导出已提交到系统的信息。</asp:Label>
                    <br />
                    <br />
                    &nbsp&nbsp
                    <%--年份：<asp:DropDownList runat="server" ID="ddl_year" />--%>
                    <asp:Button ID="Button4" runat="server" Text="导出PDF" Enabled="false" OnClick="Button4_Click" CssClass="button button-rounded button-flat-caution button-tiny" />&nbsp&nbsp&nbsp
                    <br />
                    <br />
                    <asp:Label ID="label_des" runat="server" Style="-ms-word-break: break-all; word-break: break-all; -ms-word-wrap: break-word; word-wrap: break-word">
                        说明：<br />
                        &nbsp&nbsp&nbsp系统管理员可针对在校本科生信息<br />信息进行修改只需修改信息内容，<br />点击"确认修改"即可。</asp:Label>
                    <br />
                    <br />
                    <br />
                    &nbsp&nbsp
                    <asp:Button ID="Button3" runat="server" Text="确定添加" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="Button3_Click" />&nbsp&nbsp&nbsp
                    <asp:Button ID="Button1" runat="server" Text="取消" OnClick="Button1_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    &nbsp&nbsp&nbsp
                    
                    
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <div class="content2">
        <h2 class="title2">新增在校本科生信息</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel_basic" runat="server">
                <ContentTemplate>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                <tr>
                    <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                    <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                        <b id="1">基本信息</b>
                    </td>
                    <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">

                <tr>
                    <td class="auto-style5" colspan="2">姓名：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_AssociationName" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">性别：
                    </td>
                    <td class="auto-style3">
                        <asp:RadioButtonList runat="server" ID="radio_1" RepeatColumns="2">
                            <asp:ListItem Text="男" Value="1" Selected="True" />
                            <asp:ListItem Text="女" Value="2" />
                        </asp:RadioButtonList>
                        <%-- <asp:RadioButton ID="RadioButton1" GroupName="rb_LR_Sex" runat="server" Text="男" Checked="true" />
                                <asp:RadioButton ID="RadioButton2" GroupName="rb_LR_Sex" runat="server" Text="女" />--%>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">证件号码：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_OrganizationCode" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox></td>
                    <td class="auto-style5" colspan="2">出生年月：
                    </td>
                    <td class="auto-style3">
                        <input id="input_RegisterDate" runat="server" type="text" style="width: 100%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>

                </tr>

                <tr>
                    <td class="auto-style5" colspan="2">就读学院：</td>
                    <%--   <td class="auto-style3">
                        <asp:TextBox ID="tb_RegisteredFund" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>--%>
                    <td class="auto-style3">
                        <asp:DropDownList runat="server" ID="ddl_School" Width="99%" Height="100%" />
                        <%--<asp:TextBox ID="TextBox1" runat="server" Width="170"></asp:TextBox>--%>
                    </td>
                    <td class="auto-style5" colspan="2">专业：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_ContactName" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style5" colspan="2">就读类别：</td>
                    <td class="auto-style3">
                        <asp:DropDownList runat="server" ID="ddl_StudyType" Width="99%" Height="100%" />
                    </td>
                    <td class="auto-style5" colspan="2">入学日期：</td>
                    <td class="auto-style3">
                        <input id="input_JoinSchoolDate" runat="server" type="text" style="width: 100%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">学制：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Telephone" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">联系电话：</td>
                    <td class="auto-style3">

                        <asp:TextBox ID="tb_Fax" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">户籍地址：</td>
                    <td colspan="4" class="auto-style4">

                        <asp:TextBox ID="tb_ScopeOfBusiness" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td class="auto-style5" colspan="2">备注：
                    </td>
                    <td class="auto-style3" colspan="4" style="height: 50px">
                        <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
            </table>

            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                <tr>
                    <td style="background-color: #A2D0F2; text-align: center; width: 25%"></td>
                    <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                        <b id="7">家庭主要成员</b>
                    </td>
                    <td style="background-color: #A2D0F2; text-align: center; width: 25%"></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <asp:Button ID="btn_Refresh" runat="server" Text="刷新GridView" CssClass="hidden" OnClick="btn_Refresh_Click" />
                                <div id="div_gv_Periodical">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label runat="server" ID="label2">共找到</asp:Label>
                                                <asp:Label runat="server" ID="label_ResultCount"></asp:Label>
                                                <asp:Label runat="server" ID="label3">条记录</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="gv_Periodical" runat="server" class="tableclass" AutoGenerateColumns="False"
                                        Width="100%" DataKeyNames="Id" AllowPaging="True" OnRowDataBound="gv_Periodical_RowDataBound1" OnRowCommand="gv_Periodical_RowCommand1">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="姓名">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Sex" HeaderText="性别">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Birthday" HeaderText="出生年月">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Relationship" HeaderText="与本人关系">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Company" HeaderText="工作单位">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tip" HeaderText="备注">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="修改">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Update1" runat="server" CommandName="Udp" CommandArgument='<%#Eval("Id") %>' OnClientClick="btn_update_periodical_click(this)">修改</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="删除">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Delete1" runat="server" CommandName="Del" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm ('确定删除？')">删除</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <%--<AlternatingRowStyle BackColor="LightGray" BorderColor="White" />--%>
                                        <RowStyle Font-Strikeout="False" />
                                        <PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
                                    </asp:GridView>
                                </div>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 20%"></td>
                                        <td style="width: 20%"></td>
                                        <td style="width: 15%"></td>
                                        <td style="width: 25%; text-align: right">
                                            <asp:Button runat="server" ID="btn_Add_jlqk" OnClick="btn_Add_jlqk_Click" class="button button-rounded button-flat-primary button-tiny" Text="添加家庭成员"></asp:Button>
                                            <%--           <button type="button" id="btn_Add_Periodical" onclick="btn_Add_jtcy_Click()" class="button button-rounded button-flat-primary button-tiny">添加家庭成员</button>
                                            <asp:Button ID="btn_Add" runat="server" Text="添加" AutoPostBack="true" CssClass="hidden" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                <tr>
                    <td style="background-color: #A2D0F2; text-align: center; width: 25%"></td>
                    <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                        <b id="7">学习经历（从初中起）</b>
                    </td>
                    <td style="background-color: #A2D0F2; text-align: center; width: 25%"></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_Refresh2" runat="server" Text="刷新GridView" CssClass="hidden" OnClick="btn_Refresh2_Click" />
                                <div id="div_gv_Periodical1">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label runat="server" ID="label5">共找到</asp:Label>
                                                <asp:Label runat="server" ID="label6"></asp:Label>
                                                <asp:Label runat="server" ID="label7">条记录</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GridView1" runat="server" class="tableclass" AutoGenerateColumns="False"
                                        Width="100%" DataKeyNames="Id" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="StartTime" HeaderText="开始时间">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EndTime" HeaderText="结束时间">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="School" HeaderText="学校">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="修改">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Update" runat="server" CommandName="Udp" CommandArgument='<%#Eval("Id") %>' OnClientClick="btn_update_periodical_click(this)">修改</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="删除">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Delete" runat="server" CommandName="Del" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm ('确定删除？')">删除</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />

                                            </asp:TemplateField>
                                        </Columns>
                                        <%--<AlternatingRowStyle BackColor="LightGray" BorderColor="White" />--%>
                                        <RowStyle Font-Strikeout="False" />
                                        <PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
                                    </asp:GridView>
                                </div>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 20%"></td>
                                        <td style="width: 20%"></td>
                                        <td style="width: 15%"></td>
                                        <td style="width: 25%; text-align: right">
                                            <asp:Button runat="server" ID="Button2" OnClick="btn_Add_xxjl_Click" class="button button-rounded button-flat-primary button-tiny" Text="添加学习经历"></asp:Button>
                                            <%--   <button type="button" id="btn_Add_Periodical2" onclick="btn_Add_SchoolRecord_Click()" class="button button-rounded button-flat-primary button-tiny">添加学习经历</button>
                                            <asp:Button ID="Button2" runat="server" Text="添加" AutoPostBack="true" CssClass="hidden" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
