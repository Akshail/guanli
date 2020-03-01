<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="CheckAffairList_cztb_look.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_cztb_look" %>


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
            width: 250px;
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
            width: 150px;
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
        function lhmd_change(value1) {
            switch (value1) {
                case '1':
                    document.getElementById("tzjs").style.display = '';
                    document.getElementById("jd").style.display = 'none';
                    document.getElementById("gz").style.display = 'none';
                    document.getElementById("yq").style.display = 'none';
                    document.getElementById("yl").style.display = 'none';
                    document.getElementById("tq").style.display = 'none';
                    break;
                case '2':
                    document.getElementById("jd").style.display = '';
                    document.getElementById("tzjs").style.display = 'none';
                    document.getElementById("gz").style.display = 'none';
                    document.getElementById("yq").style.display = 'none';
                    document.getElementById("yl").style.display = 'none';
                    document.getElementById("tq").style.display = 'none';
                    break;
                case '3':
                    document.getElementById("gz").style.display = '';
                    document.getElementById("jd").style.display = 'none';
                    document.getElementById("tzjs").style.display = 'none';
                    document.getElementById("yq").style.display = 'none';
                    document.getElementById("yl").style.display = 'none';
                    document.getElementById("tq").style.display = 'none';
                    break;
                case '4':
                    document.getElementById("yq").style.display = '';
                    document.getElementById("gz").style.display = 'none';
                    document.getElementById("jd").style.display = 'none';
                    document.getElementById("tzjs").style.display = 'none';
                    document.getElementById("yl").style.display = 'none';
                    document.getElementById("tq").style.display = 'none';
                    break;
                case '5':
                    document.getElementById("yl").style.display = '';
                    document.getElementById("yq").style.display = 'none';
                    document.getElementById("gz").style.display = 'none';
                    document.getElementById("jd").style.display = 'none';
                    document.getElementById("tzjs").style.display = 'none';
                    document.getElementById("tq").style.display = 'none';
                    break;
                case '6':
                    document.getElementById("tq").style.display = '';
                    document.getElementById("yq").style.display = 'none';
                    document.getElementById("gz").style.display = 'none';
                    document.getElementById("jd").style.display = 'none';
                    document.getElementById("tzjs").style.display = 'none';
                    document.getElementById("yl").style.display = 'none';
                    break;
                case '7':
                    document.getElementById("tq").style.display = 'none';
                    document.getElementById("yq").style.display = 'none';
                    document.getElementById("gz").style.display = 'none';
                    document.getElementById("jd").style.display = 'none';
                    document.getElementById("tzjs").style.display = 'none';
                    document.getElementById("yl").style.display = 'none';
                    break;
                default:
                    break;
            }
        }
    </script>
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
                    <asp:Button ID="Button4" runat="server" Text="导出PDF" Enabled="false" OnClick="Button4_Click" CssClass="button button-rounded button-flat-caution button-tiny" />&nbsp&nbsp&nbsp
                    <br />
                    <br />
                    <asp:Label ID="label_des" runat="server" Style="-ms-word-break: break-all; word-break: break-all; -ms-word-wrap: break-word; word-wrap: break-word">
                        说明：<br />
                        &nbsp&nbsp&nbsp系统管理员可针对在沪台生信息<br />信息进行修改。只需修改信息内容，<br />点击"确认修改"即可。</asp:Label>
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
        <h2 class="title2">新增常驻台胞信息</h2>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="maincontent">
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
                    <td class="auto-style5" colspan="2">台胞姓名：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_AssociationName" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">性别：
                    </td>
                    <td class="auto-style3">
                        <asp:RadioButtonList runat="server" ID="radio_1" RepeatColumns="3">
                            <asp:ListItem Text="男" Value="1" Selected="True" />
                            <asp:ListItem Text="女" Value="2" />
                        </asp:RadioButtonList>
                        <%-- <asp:RadioButton ID="RadioButton1" GroupName="rb_LR_Sex" runat="server" Text="男" Checked="true" />
                                <asp:RadioButton ID="RadioButton2" GroupName="rb_LR_Sex" runat="server" Text="女" />--%>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">来沪时间：</td>
                    <td class="auto-style3">
                        <input id="Text1" runat="server" type="text" style="width: 100%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>
                    <td class="auto-style5" colspan="2">出生年月：
                    </td>
                    <td class="auto-style3">
                        <input id="input_RegisterDate" runat="server" type="text" style="width: 100%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">身份证号：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_RegisteredFund" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">联系电话：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_ContactName" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">台胞证号：</td>
                    <td class="auto-style3">

                        <asp:TextBox ID="tb_Telephone" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">台胞证有效期：</td>
                    <td class="auto-style3">
                        <input id="Text2" runat="server" type="text" style="width: 100%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">在台职业：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox1" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">在台单位：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox2" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">职务：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox4" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">台湾户籍地及住址：</td>
                    <td class="auto-style3">
                        <%--<asp:TextBox ID="TextBox5" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>--%>
                        <%--<asp:DropDownList runat="server" ID="ddl_sfmc" Width="100" Style="float: left">
                            <asp:ListItem Text="台湾省" Value="台湾省" />
                        </asp:DropDownList>--%>
                        <asp:DropDownList runat="server" ID="ddl_csmc" Width="250" Style="float: left">
                           <%-- <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="台北市" Value="台北市" />
                            <asp:ListItem Text="新北市" Value="新北市" />
                            <asp:ListItem Text="台中市" Value="台中市" />
                            <asp:ListItem Text="台南市" Value="台南市" />
                            <asp:ListItem Text="高雄市" Value="高雄市" />
                            <asp:ListItem Text="基隆市" Value="基隆市" />
                            <asp:ListItem Text="新竹市" Value="新竹市" />
                            <asp:ListItem Text="嘉义市" Value="嘉义市" />
                            <asp:ListItem Text="桃源县" Value="桃源县" />
                            <asp:ListItem Text="苗栗县" Value="苗栗县" />
                            <asp:ListItem Text="彰化县" Value="彰化县" />
                            <asp:ListItem Text="南投县" Value="南投县" />
                            <asp:ListItem Text="云林县" Value="云林县" />
                            <asp:ListItem Text="嘉义县" Value="嘉义县" />
                            <asp:ListItem Text="屏东县" Value="屏东县" />
                            <asp:ListItem Text="宜兰县" Value="宜兰县" />
                            <asp:ListItem Text="花莲县" Value="花莲县" />
                            <asp:ListItem Text="台东县" Value="台东县" />
                            <asp:ListItem Text="澎湖县" Value="澎湖县" />
                            <asp:ListItem Text="金门县" Value="金门县" />
                            <asp:ListItem Text="连江县" Value="连江县" />--%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">现住址：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox6" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">临时户口申报地：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox7" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">现住房性质：</td>
                    <td class="auto-style3">
                        <%--        <asp:TextBox ID="TextBox8" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" ID="DropDownList1" Width="100%">
                            <asp:ListItem Text="自购产权房" Value="1" />
                            <asp:ListItem Text="租赁房" Value="2" />
                            <asp:ListItem Text="住亲朋家" Value="3" />
                            <asp:ListItem Text="单位（学校）宿舍" Value="4" />
                            <asp:ListItem Text="其它" Value="5" />
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style5" colspan="2">来沪目的：</td>
                    <td class="auto-style3">
                        <%--<asp:TextBox ID="TextBox9" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" ID="ddl" onchange="lhmd_change(this.value)" Width="100%">
                            <asp:ListItem Text="投资经商" Value="1" />
                            <asp:ListItem Text="就读" Value="2" />
                            <asp:ListItem Text="工作" Value="3" />
                            <asp:ListItem Text="依亲" Value="4" />
                            <asp:ListItem Text="养老" Value="5" />
                            <asp:ListItem Text="探亲" Value="6" />
                            <asp:ListItem Text="其它" Value="7" />
                        </asp:DropDownList>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div id="div_gv_Periodical">
                            <asp:Button ID="btn_Refresh" runat="server" Text="刷新GridView" CssClass="hidden" OnClick="btn_Refresh_Click" />
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
                                Width="100%" DataKeyNames="Id" AllowPaging="True" OnRowDataBound="gv_Periodical_RowDataBound" OnRowCommand="gv_Periodical_RowCommand1">
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
                                    <asp:UpdatePanel runat="server" ID="updatepanel_1">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btn_Add_jlqk" OnClick="btn_Add_jlqk_Click" class="button button-rounded button-flat-primary button-tiny" Text="添加家庭成员"></asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
            <div id="tzjs" style="display: none">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                            <tr>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                                <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                                    <b id="1">投资经商</b>
                                </td>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">

                            <tr>
                                <td class="auto-style5" colspan="2">经营范围：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox9" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">企业性质：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox21" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">注册资金：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox11" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">注册地：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox12" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">企业名称：</td>
                                <td class="auto-style3">

                                    <asp:TextBox ID="TextBox13" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">企业地址：</td>
                                <td class="auto-style3">

                                    <asp:TextBox ID="TextBox14" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">企业电话：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox15" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">邮政编码：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox16" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="jd" style="display: none">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                            <tr>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                                <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                                    <b id="1">就读</b>
                                </td>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">

                            <tr>
                                <td class="auto-style5" colspan="2">就读学校：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox10" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">年级：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox17" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">专业：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox18" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">监护人：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox19" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">与监护人关系：</td>
                                <td class="auto-style3">

                                    <asp:TextBox ID="TextBox20" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">监护人联系电话：</td>
                                <td class="auto-style3">

                                    <asp:TextBox ID="TextBox22" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="gz" style="display: none">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                            <tr>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                                <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                                    <b id="1">工作</b>
                                </td>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">

                            <tr>
                                <td class="auto-style5" colspan="2">受聘（雇）单位：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox23" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">职务：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox24" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">单位地址：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox25" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">单位性质：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox26" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="yq" style="display: none">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                            <tr>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                                <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                                    <b id="1">依亲</b>
                                </td>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">

                            <tr>
                                <td class="auto-style5" colspan="2">沪籍亲人姓名：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox27" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">联系电话：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox28" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">工作状况：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox29" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">工作单位：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox30" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">与沪籍亲人关系：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox31" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="yl" style="display: none">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                            <tr>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                                <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                                    <b id="1">养老</b>
                                </td>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">

                            <tr>
                                <td class="auto-style5" colspan="2">身体状况：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox32" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">养老形式：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox33" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">照顾人：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox34" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">与养老人关系：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox35" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">联系电话：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox36" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="tq" style="display: none">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                            <tr>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                                <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                                    <b id="1">探亲</b>
                                </td>
                                <td style="text-align: center; background-color: #A2D0F2; width: 25%"></td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">

                            <tr>
                                <td class="auto-style5" colspan="2">被探亲人：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox37" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">与探亲人关系：
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox38" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">探亲时间（抵）：</td>
                                <td class="auto-style3">
                                    <input id="arriveTime" runat="server" type="text" style="width: 99%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                                </td>
                                <td class="auto-style5" colspan="2">探亲时间（离）：</td>
                                <td class="auto-style3">
                                    <input id="leaveTime" runat="server" type="text" style="width: 99%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5" colspan="2">被探亲人地址：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox41" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="auto-style5" colspan="2">被探亲人联系电话：</td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TextBox42" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
