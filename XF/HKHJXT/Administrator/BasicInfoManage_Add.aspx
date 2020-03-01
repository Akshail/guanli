<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicInfoManage_Add.aspx.cs" Inherits="XF.HKHJXT.Administrator.BasicInfoManage_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css" />
    <script language="javascript" type="text/javascript" src="../../My97DatePicker/WdatePicker.js"></script>
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
            width: 200px;
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        </asp:UpdatePanel>
        <div style="margin-top: 20px;">
            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse">
                <tr>
                    <td class="auto-style5">姓名：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_name" runat="server" Height="100%" Width="99%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5">手机号：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_phone" runat="server" Height="100%" Width="99%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">电子邮箱：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_email" runat="server" Width="99%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5">单位邮编：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_zipcode" runat="server" Width="99%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">单位地址：
                    </td>
                    <td class="auto-style3" colspan="3">
                        <asp:TextBox ID="tb_address" runat="server" Width="99%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%"></td>
                    <td style="width: 20%"></td>
                    <td style="width: 15%"></td>
                    <td style="width: 25%; text-align: right">
                        <asp:Button ID="btn_Add" runat="server" Text="添加" OnClick="btn_Add_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
