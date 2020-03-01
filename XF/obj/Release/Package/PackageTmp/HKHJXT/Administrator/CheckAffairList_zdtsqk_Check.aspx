<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckAffairList_zdtsqk_Check.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_zdtsqk_Check" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css" />
    <script type="text/javascript">
        function closeiframe() {
            var index = parent.layer.getFrameIndex(window.name);
            if (result == 1) {
                parent.layer.msg('添加成功', 2, -1)
            }
            if (result == 2) {
                parent.layer.msg('添加失败', 2, -1)
            }
            if (result == 3) {
                parent.layer.msg('数据加载错误', 2, -1)
            }
            parent.layer.close(index);

        };
    </script>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                        <asp:Label runat="server" ID="label1" Width="100%" Height="100%" />
                    </td>
                    <td class="auto-style5" colspan="2">性别：
                    </td>
                    <td class="auto-style3">
                        <asp:Label runat="server" ID="label4" Width="100%" Height="100%" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">证件号码：</td>
                    <td class="auto-style3">
                        <asp:Label ID="tb_OrganizationCode" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:Label></td>
                    <td class="auto-style5" colspan="2">出生年月：
                    </td>
                    <td class="auto-style3">
                        <asp:Label runat="server" ID="label8" Width="100%" Height="100%" />
                    </td>

                </tr>

                <tr>
                    <td class="auto-style5" colspan="2">就读学校：</td>
                    <td class="auto-style3">
                        <asp:Label ID="tb_RegisteredFund" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:Label>
                    </td>
                    <td class="auto-style5" colspan="2">专业：</td>
                    <td class="auto-style3">
                        <asp:Label ID="tb_ContactName" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">学制：</td>
                    <td class="auto-style3">

                        <asp:Label ID="tb_Telephone" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:Label>
                    </td>
                    <td class="auto-style5" colspan="2">联系电话：</td>
                    <td class="auto-style3">

                        <asp:Label ID="tb_Fax" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">在沪居住地：</td>
                    <td colspan="4" class="auto-style4">

                        <asp:Label ID="tb_ScopeOfBusiness" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">在台户籍地：</td>
                    <td colspan="4" class="auto-style4">
                        <asp:Label ID="Label21" runat="server" Width="100%" Height="100%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" colspan="2">备注：
                    </td>
                    <td class="auto-style3" colspan="4" style="height: 50px">
                        <asp:Label ID="Label3" runat="server" TextMode="MultiLine" Width="100%" Height="100%" BorderStyle="None"></asp:Label>
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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div id="div_gv_Periodical">
                                    <asp:GridView ID="gv_Periodical" runat="server" class="tableclass" AutoGenerateColumns="False"
                                        Width="100%" DataKeyNames="Id" AllowPaging="True">
                                        <Columns>
                                            <asp:BoundField DataField="PeriodicalName" HeaderText="姓名">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PeriodicalNum" HeaderText="性别">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PublishPeriod" HeaderText="出生年月">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PublishCount" HeaderText="与本人关系">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EIC_Name" HeaderText="工作单位">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EO_Address" HeaderText="备注">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <%--<AlternatingRowStyle BackColor="LightGray" BorderColor="White" />--%>
                                        <RowStyle Font-Strikeout="False" />
                                    </asp:GridView>
                                </div>
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
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div id="div_gv_Periodical">
                                    <asp:GridView ID="GridView1" runat="server" class="tableclass" AutoGenerateColumns="False"
                                        Width="100%" DataKeyNames="Id" AllowPaging="True">
                                        <Columns>
                                            <asp:BoundField DataField="PeriodicalName" HeaderText="开始时间">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PeriodicalNum" HeaderText="结束时间">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PublishPeriod" HeaderText="学校">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <%--<AlternatingRowStyle BackColor="LightGray" BorderColor="White" />--%>
                                        <RowStyle Font-Strikeout="False" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

