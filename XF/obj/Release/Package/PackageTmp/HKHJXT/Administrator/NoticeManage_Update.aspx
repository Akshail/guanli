<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" CodeBehind="NoticeManage_Update.aspx.cs" Inherits="XF.HKHJXT.Administrator.NoticeManage_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">
    <script src="../../utf8-net/ueditor.config.js"></script>
    <script src="../../utf8-net/ueditor.all.min.js"></script>
    <link href="../../utf8-net/themes/default/css/ueditor.min.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style10 {
            width: 20%;
            text-align: center;
            background-color: #A2D0F2;
            border: 1px solid #666;
        }

        .auto-style8 {
            width: auto;
            text-align: center;
            background-color: rgb(238, 238, 238);
            border: 1px solid #666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table border="0" cellspacing="5" cellpadding="0" runat="server" id="userlogin">
                    <tr>
                        <td colspan="2">欢迎进入上海市杨浦区人民政府台湾事务办公室工作信息管理系统：
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">今天：</td>
                        <td>
                            <asp:Label ID="lblWeek" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">时间：</td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                    <asp:Timer runat="server" OnTick="Unnamed1_Tick" ID="timer1">
                                    </asp:Timer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="left" valign="middle">天气：</td>
                        <td>
                            <iframe width="120" scrolling="no" height="50" frameborder="0" allowtransparency="true" src="http://i.tianqi.com/index.php?c=code&id=17&icon=1&num=3"></iframe>
                        </td>
                    </tr>--%>
                </table>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <div class="content2">
        <h2 class="title2">修改通知公告</h2>
        <div class="maincontent">

            <table style="width: 100%">
                <tr>
                    <td class="auto-style10" colspan="2">标题：</td>
                    <td colspan="4" class="auto-style8">
                        <asp:TextBox ID="tb_Title" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="2">内容：</td>
                    <td colspan="4" class="auto-style8">
                        <%--<asp:TextBox ID="tb_Content" runat="server" Width="99%" Height="400px" TextMode="MultiLine"></asp:TextBox>--%>
                        <!-- 加载编辑器的容器 -->
                        <textarea id="tb_Content" name="tb_Content" style="width: 580px; height: 380px;" runat="server"></textarea>
                        <!-- 实例化编辑器 -->
                        <script type="text/javascript">
                            var ue = UE.getEditor('CPHMainContent_tb_Content', {
                                "imagePathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                "snapscreenPathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                "filePathFormat": "upload/file/{yyyy}{mm}{dd}/{time}{rand:6}",
                                "videoPathFormat": "upload/video/{yyyy}{mm}{dd}/{time}{rand:6}"
                            });
                        </script>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="2">发布时间：</td>
                    <td colspan="4" style="text-align: left; background-color: rgb(238, 238, 238); border: 1px solid #666;">
                        <input id="input_ShowTime" runat="server" type="text" style="width: 35%" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy年MM月dd日 HH:mm:ss'})" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="text-align: center; width: 20%"></td>
                    <td style="text-align: center; width: 20%"></td>
                    <td style="text-align: center; width: 20%"></td>
                    <td style="text-align: center; width: 20%"></td>
                    <td style="text-align: center; width: 20%">
                        <asp:Button ID="btn_Update" runat="server" Text="更新" OnClick="btn_Update_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    <td style="text-align: center; width: 20%">
                        <asp:Button ID="btn_Cancel" runat="server" Text="取消" CssClass="button button-rounded button-flat-primary button-tiny" OnClick="btn_Cancel_Click" />
                </tr>

            </table>

        </div>
    </div>
</asp:Content>
