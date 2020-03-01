<%@ Page Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="CheckAffairList_tzqy_Add.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_tzqy_Add" %>

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

        function btn_Add_jcjl_Click(option, url) {
            //if ((document.getElementById("CPHMainContent_label_ResultCount").innerHTML * 1) >= 5) {
            //    layer.alert('检查记录最多只能5条记录！');
            //    return;
            //}
            $.layer({
                type: 2,
                shade: [0],
                fix: false,
                title: [option, true],
                iframe: { src: url },
                area: ['600px', '400px'],
                offset: ['150px', ''],
                end: function () {
                    document.getElementById("CPHMainContent_btn_Refresh").click();
                }
            });
        }
        function btn_Add_xxjl_Click(option, url) {
            //if ((document.getElementById("CPHMainContent_label6").innerHTML * 1) >= 5) {
            //    layer.alert('协调记录最多只能5条记录！');
            //    return;
            //}
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
                    <asp:Label ID="label_des" runat="server" Style="-ms-word-break: break-all; word-break: break-all; -ms-word-wrap: break-word; word-wrap: break-word">
                        <asp:Label ID="label1" runat="server" Style="-ms-word-break: break-all; word-break: break-all; -ms-word-wrap: break-word; word-wrap: break-word">
                        导出：<br />
                        &nbsp&nbsp&nbsp;若当前操作是新增，请先将信息填<br />写完成后点击上方的确定添加按钮，<br />添加成功后在进入该页面进行导出；<br />若当前操作是修改则可直接导出。导出<br />功能只导出已提交到系统的信息。</asp:Label>
                        <br />
                        <br />
                    年份：<asp:DropDownList runat="server" ID="ddl_year" />&nbsp
                    <asp:Button ID="Button4" runat="server" Text="导出PDF" Enabled="false" OnClick="Button4_Click" CssClass="button button-rounded button-flat-caution button-tiny" />&nbsp&nbsp&nbsp
                    <br />
                        <br />
                        说明：<br />
                        &nbsp&nbsp&nbsp系统管理员可对台资企业信息情况<br />
                        信息进行修改。只需修改信息内容，<br />点击"确认
                        修改"即可。</asp:Label>
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
        <h2 class="title2">新增台资企业信息</h2>
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
                    <td class="auto-style5" colspan="2">企业名称：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_AssociationName" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">注册类别：</td>
                    <td class="auto-style3">
                        <asp:DropDownList runat="server" ID="ddl_RegisterType" Width="99%" Height="100%" OnSelectedIndexChanged="ddl_RegisterType_SelectedIndexChanged" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">经营地：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_OrganizationCode" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">法定代表人：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox4" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>

                </tr>

                <tr>
                    <td class="auto-style5" colspan="2">联系人：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_RegisteredFund" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">联系电话：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_ContactName" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">邮箱地址：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Telephone" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">性质：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Fax" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">注册资金：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox5" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">币种：</td>
                    <td class="auto-style3">
                        <asp:DropDownList runat="server" ID="ddl_MoneyType" Width="99%" Height="100%" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">台籍投资人：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox6" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">陆方投资人：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox1" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">台资占比：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox7" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">投资额：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox8" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">经营范围：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox9" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">主要业务及产品：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox10" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">企业员工数：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_EmployerNum" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">企业年税收(万)：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_AnnualTax" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">台干人数：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox11" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" colspan="2">信息来源：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox12" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">企业地址：
                    </td>
                    <td class="auto-style3" colspan="4">
                        <asp:TextBox ID="TextBox2" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                <tr>
                    <td style="background-color: #A2D0F2; text-align: center;">
                        <b>企业大门（带铭牌）</b>
                    </td>
                    <td style="background-color: #A2D0F2; text-align: center">
                        <b>营业执照</b>
                    </td>
                    <td style="background-color: #A2D0F2; text-align: center;">
                        <b>批复证书</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="width: 220px; margin: auto; padding-top: 10px">
                            <asp:Image runat="server" ID="Photo_CompanyDoor" ImageUrl="../../images/AddPhoto.jpg" Width="220" Height="150" />
                        </div>
                        <div style="width: 200px; margin: auto; padding-top: 20px;">
                            <asp:FileUpload runat="server" ID="FileUpload_Photo_CompanyDoor" Style="width: 200px;" />
                            <asp:HiddenField ID="hide_PhotoName_CompanyDoor" runat="server" />
                        </div>
                        <div style="width: 167px; margin: auto; padding-top: 10px;">
                            <asp:Button runat="server" ID="btn_upload_CompanyDoor" OnClick="btn_upload_CompanyDoor_Click" Text="上传企业大门（带铭牌）" CssClass="button button-rounded button-flat-primary button-tiny" />
                        </div>
                    </td>
                    <td>
                        <div style="width: 220px; margin: auto; padding-top: 10px">
                            <asp:Image runat="server" ID="Photo_OpenLicence" ImageUrl="../../images/AddPhoto.jpg" Width="220" Height="150" />
                        </div>
                        <div style="width: 200px; margin: auto; padding-top: 20px;">
                            <asp:FileUpload runat="server" ID="FileUpload_Photo_OpenLicence" Style="width: 200px;" />
                            <asp:HiddenField ID="hide_PhotoName_OpenLicence" runat="server" />
                        </div>
                        <div style="width: 107px; margin: auto; padding-top: 10px;">
                            <asp:Button runat="server" ID="btn_upload_OpenLicence" OnClick="btn_upload_OpenLicence_Click" Text="上传营业执照" CssClass="button button-rounded button-flat-primary button-tiny" />
                        </div>
                    </td>
                    <td>
                        <div style="width: 220px; margin: auto; padding-top: 10px">
                            <asp:Image runat="server" ID="Photo_PiFuLicence" ImageUrl="../../images/AddPhoto.jpg" Width="220" Height="150" />
                        </div>
                        <div style="width: 200px; margin: auto; padding-top: 20px;">
                            <asp:FileUpload runat="server" ID="FileUpload_Photo_PiFuLicence" Style="width: 200px;" Enabled="false" />
                            <asp:HiddenField ID="hide_PhotoName_PiFuLicence" runat="server" />
                        </div>
                        <div style="width: 107px; margin: auto; padding-top: 10px;">
                            <asp:Button runat="server" ID="btn_upload_PiFuLicence" OnClick="btn_upload_PiFuLicence_Click" Enabled="false" Text="上传批复证书" CssClass="button button-rounded button-flat-primary button-tiny" />
                        </div>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                <tr>
                    <td style="background-color: #A2D0F2; text-align: center; width: 25%"></td>
                    <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                        <b id="7">安全生产重点及检查记录</b>
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
                                        Width="100%" DataKeyNames="Id" AllowPaging="True" OnRowCommand="gv_Periodical_RowCommand1" OnRowDataBound="gv_Periodical_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Time" HeaderText="时间">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit" HeaderText="检查单位">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Result" HeaderText="检查结果及整改要求">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="安全重点照片如设备、车间等" ItemStyle-Width="200">
                                                <ItemTemplate>
                                                    <%-- <asp:Image runat="server" ImageUrl='<%#"../../images/"+Eval("PhotoName") %>' Width="220" Height="150" />--%>
                                                    <div style="width:110px;margin:auto">
                                                    <asp:Image ID="image_aqsczd" runat="server" style="text-align:center" ImageUrl='<%# String.Format("../../upload/photos/{0}",Eval("PhotoName"))%>' Width="110" Height="75" />
                                                        </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                            <asp:Button runat="server" ID="btn_Add_jlqk" OnClick="btn_Add_jcjl_Click" class="button button-rounded button-flat-primary button-tiny" Text="添加检查记录"></asp:Button>
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
                        <b id="7">问题协调记录</b>
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
                                            <asp:BoundField DataField="Time" HeaderText="时间">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Problem" HeaderText="问题和协调情况">
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
                                            <asp:Button runat="server" ID="Button2" OnClick="btn_Add_wtxt_Click" class="button button-rounded button-flat-primary button-tiny" Text="添加问题协调"></asp:Button>
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
