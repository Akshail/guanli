<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckAffairList_tzqy_jcjl_Add.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_tzqy_jcjl_Add" %>

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
                    <td class="auto-style5">时间：
                    </td>
                    <td class="auto-style3">
                        <input id="input_RegisterDate" runat="server" type="text" style="width: 100%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">检查单位：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TextBox1" runat="server" Height="100%" Width="99%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>

                    <td class="auto-style5">检查结果及整改要求：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_PublishPeriod" runat="server" Height="100%" Width="99%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>

                    <td class="auto-style5">请上传安全重点照片如设备、车间等：
                    </td>
                    <td class="auto-style3">
                        <div style="width: 220px; margin: auto; padding-top: 10px">
                            <asp:Image runat="server" ID="Photo" ImageUrl="../../images/AddPhoto.jpg" Width="220" Height="150" />
                        </div>
                        <div style="width: 200px; margin: auto; padding-top: 20px;">
                            <asp:FileUpload runat="server" ID="FileUpload_Photo" Style="width: 200px;" />
                            <asp:HiddenField ID="hide_PhotoName" runat="server" />
                        </div>
                        <div style="width: 107px; margin: auto; padding-top: 10px;">
                            <asp:Button runat="server" ID="btn_upload" OnClick="btn_upload_Click" Text="上传照片" CssClass="button button-rounded button-flat-primary button-tiny" />
                        </div>
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

