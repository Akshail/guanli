﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckAffairList_cztb_jtcy_Add.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_cztb_jtcy_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script language="javascript" type="text/javascript" src="../../My97DatePicker/WdatePicker.js"></script>
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
        function close() {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
        }
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
                    <td class="auto-style5">姓名：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_PeriodicalName" runat="server" Height="100%" Width="99%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5">性别：
                    </td>
                    <td class="auto-style3">
                        <asp:RadioButtonList runat="server" ID="radio_1" RepeatColumns="2">
                            <asp:ListItem Text="男" Value="1" Selected="True" />
                            <asp:ListItem Text="女" Value="2" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>

                    <td class="auto-style5">出生年月：
                    </td>
                    <td class="auto-style3">
                        <input id="input_RegisterDate" runat="server" type="text" style="width: 100%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>
                    <td class="auto-style5">与本人关系：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_PublishCount" runat="server" Height="100%" Width="99%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">工作单位：
                    </td>
                    <td class="auto-style3" colspan="3">
                        <asp:TextBox ID="tb_EIC_Name" runat="server" Height="100%" Width="99%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">备注：
                    </td>
                    <td class="auto-style3" colspan="3">

                        <asp:TextBox ID="tb_EO_Address" runat="server" Height="100%" Width="99%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%"></td>
                    <td style="width: 20%"></td>
                    <td style="width: 15%"></td>
                    <td style="width: 25%; text-align: right">
                        <asp:Button ID="btn_Add" runat="server" Text="添加" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Add_Click" />
                        <%--<button type="button" runat="server" onclick="Button1Click()">Add</button>--%>
                        <%--<asp:Button ID="btn_Update" runat="server" Text="修改" OnClick="btn_Update_Click" Visible="false" AutoPostBack="true" CssClass="button button-rounded button-flat-primary button-tiny" />--%>
                        <%--<asp:Button ID="btn_Clear" runat="server" Text="清空" OnClick="btn_Clear_Click" AutoPostBack="true" CssClass="button button-rounded button-flat-primary button-tiny" />--%>
                        <%--<asp:Button ID="btn_Cancle" runat="server" Text="取消" OnClick="btn_Cancle_Click" Visible="false" AutoPostBack="true" CssClass="button button-rounded button-flat-primary button-tiny" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>