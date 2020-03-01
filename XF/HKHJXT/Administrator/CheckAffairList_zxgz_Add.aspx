<%@ Page Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="CheckAffairList_zxgz_Add.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_zxgz_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css" />
    <style type="text/css">
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
            width: 140px;
            text-align: center;
            background-color: #A2D0F2;
            border: 1px solid #666;
        }
    </style>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

        })
    </script>
    <script type="text/javascript">
        function btn_Add_jlqk_Click(option, url) {
            //if ((document.getElementById("CPHMainContent_label_ResultCount").innerHTML * 1) >= 5) {
            //    layer.alert('交流情况最多只能5条记录！');
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
                    年份：<asp:DropDownList runat="server" ID="ddl_year" />&nbsp
                    <asp:Button ID="Button4" runat="server" Text="导出PDF" Enabled="false" OnClick="Button4_Click" CssClass="button button-rounded button-flat-caution button-tiny" />&nbsp&nbsp&nbsp
                    <br />
                    <br />
                    <asp:Label ID="label_des" runat="server" Style="-ms-word-break: break-all; word-break: break-all; -ms-word-wrap: break-word; word-wrap: break-word">
                        说明：<br />
                        &nbsp&nbsp&nbsp系统管理员可针对专项工作情况<br />信息进行修改。只需修改信息内容，<br />点击"确认修改"即可。</asp:Label>
                    <br />
                    <br />
                    <br />
                    &nbsp&nbsp
                    <asp:Button ID="btn_submit" runat="server" Text="确定添加" OnClick="btn_submit_Click" CssClass="button button-rounded button-flat-primary button-tiny" />&nbsp&nbsp&nbsp
                    <asp:Button ID="btn_cancel" runat="server" Text="取消" OnClick="btn_cancel_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    &nbsp&nbsp&nbsp
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <div class="content2">
        <h2 class="title2">新增专项工作信息</h2>
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
                    <td class="auto-style5">对口交流地区：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Exchange" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5">交流对象类别：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Type" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td rowspan="8" style="width: 200px;">
                        <div style="width: 120px; margin: auto; padding-top: 10px">
                            <asp:Image runat="server" ID="Photo" ImageUrl="../../images/头像1.png" Width="120" Height="140" />
                        </div>
                        <div style="width: 200px; margin: auto; padding-top: 20px;">
                            <%--<input id="Button1" type="button" value="选择照片" class="button button-rounded button-flat-primary button-tiny"  />--%>
                            <asp:FileUpload runat="server" ID="FileUpload_Photo" Style="width: 200px;" />

                        </div>
                        <div style="width: 80px; margin: auto; padding-top: 10px;">
                            <asp:Button runat="server" ID="btn_upload" Text="上传" OnClick="btn_upload_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">类别：</td>
                    <td class="auto-style3">
                        <asp:DropDownList runat="server" ID="ddl_SpecialWorkType" Width="99%" Height="100%" />
                    </td>
                    <td class="auto-style5">姓名：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Name" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>


                </tr>
                <tr>
                    <td class="auto-style5">性别：
                    </td>
                    <td class="auto-style3">
                        <asp:RadioButton ID="Radio_man" GroupName="Sex" runat="server" Text="男" Checked="true" />
                        <asp:RadioButton ID="Radio_woman" GroupName="Sex" runat="server" Text="女" />
                    </td>
                    <td class="auto-style5">出生年月：</td>
                    <td class="auto-style3">
                        <input id="tb_birth" runat="server" type="text" style="width: 99%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>


                </tr>

                <tr>
                    <td class="auto-style5">工作单位：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_company" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5">联系电话：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_phoneNo" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5">联系地址：</td>
                    <td class="auto-style3" colspan="3">
                        <asp:TextBox ID="tb_address" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">任期开始：</td>
                    <td class="auto-style3">
                        <input id="tb_workStartDate" runat="server" type="text" style="width: 99%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>
                    <td class="auto-style5">任期结束：</td>
                    <td class="auto-style3">
                        <input id="tb_workEndDate" runat="server" type="text" style="width: 99%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">当选得票数率：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_ballot" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5">是否连任：</td>
                    <td class="auto-style3">
                        <asp:RadioButton ID="Radio_IsReselect_Yes" GroupName="IsReselect" runat="server" Text="是" Checked="true" />
                        <asp:RadioButton ID="Radio_IsReselect_No" GroupName="IsReselect" runat="server" Text="否" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">主要社会关系及人脉背景：
                    </td>
                    <td class="auto-style3" colspan="3" style="height: 50px">
                        <asp:TextBox ID="tb_Background" runat="server" TextMode="MultiLine" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">工作单位基本情况：
                    </td>
                    <td class="auto-style3" colspan="3" style="height: 50px">
                        <asp:TextBox ID="tb_Situation" runat="server" TextMode="MultiLine" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                <tr>
                    <td style="background-color: #A2D0F2; text-align: center; width: 25%"></td>
                    <td style="background-color: #A2D0F2; text-align: center" colspan="2">
                        <b id="7">交流情况记录</b>
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
                                <div id="div_gv_situation">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label runat="server" ID="label2">共找到</asp:Label>
                                                <asp:Label runat="server" ID="label_ResultCount"></asp:Label>
                                                <asp:Label runat="server" ID="label3">条记录</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="gv_Situation" runat="server" class="tableclass" AutoGenerateColumns="False"
                                        Width="100%" DataKeyNames="Id" AllowPaging="True" OnRowCommand="gv_Situation_RowCommand" OnRowDataBound="gv_Situation_RowDataBound" OnPageIndexChanging="gv_Situation_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="序号Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Time" HeaderText="时间">
                                                <ItemStyle HorizontalAlign="Center" Width="120" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Location" HeaderText="地点">
                                                <ItemStyle HorizontalAlign="Center" Width="120" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SituationName" HeaderText="交流情况">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="修改">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Update" runat="server" CommandName="Upd" CommandArgument='<%#Eval("Id") %>'>修改</asp:LinkButton>
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
                                            <asp:Button runat="server" ID="btn_Add_jlqk" OnClick="btn_Add_jlqk_Click" class="button button-rounded button-flat-primary button-tiny" Text="添加交流情况"></asp:Button>
                                            <%--<asp:Button ID="btn_Add" runat="server" Text="添加" AutoPostBack="true" CssClass="hidden" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Add" EventName="Click" />
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>

        </div>
    </div>
</asp:Content>
