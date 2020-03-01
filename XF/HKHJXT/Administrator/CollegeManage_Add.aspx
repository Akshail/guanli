<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeManage_Add.aspx.cs" Inherits="XF.HKHJXT.Administrator.CollegeManage_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加学院</title>
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css" />
    <style type="text/css">
        .auto-style7 {
            width: 140px;
            text-align: center;
            background-color: rgb(238, 238, 238);
            border: 1px solid #666;
        }

        .auto-style10 {
            width: 105px;
            text-align: center;
            background-color: #A2D0F2;
            border: 1px solid #666;
        }
    </style>
    <script type="text/javascript">
        function close() {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">

        <div>
            <table style="width: 100%; border: 1px solid #666; border-spacing: 0px; border-collapse: collapse;margin-top:20px">
                <tr>
                    <td class="auto-style10">学院名称：
                    </td>
                    <td class="auto-style7">
                        <asp:TextBox ID="tb_account" runat="server" Width="150"></asp:TextBox>
                    </td>
                    <td class="auto-style10">学院代码：
                    </td>
                    <td class="auto-style7">
                        <asp:TextBox ID="tb_password" runat="server" Width="150"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;margin-top:10px">
                <tr>
                    <td style="width: 75%"></td>
                    <td style="width: 25%; text-align: right">
                        <asp:Button ID="btn_AddUser" runat="server" Text="确认添加" OnClick="btn_AddUser_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
