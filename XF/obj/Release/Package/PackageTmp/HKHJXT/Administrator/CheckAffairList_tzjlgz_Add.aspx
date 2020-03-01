<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="CheckAffairList_tzjlgz_Add.aspx.cs" Inherits="XF.HKHJXT.Administrator.CheckAffairList_tzjlgz_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css" />
    <script src="../../utf8-net/ueditor.config.js"></script>
    <script src="../../utf8-net/ueditor.all.min.js"></script>
    <link href="../../utf8-net/themes/default/css/ueditor.min.css" rel="stylesheet" />
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
                        &nbsp&nbsp
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
        <h2 class="title2">团组交流工作信息</h2>
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
                    <td class="auto-style5" >交流形式：
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList runat="server" ID="ddl_CommunicationType" Width="99%" Height="100%" />
                    </td>
                    <td class="auto-style5" >组团地区：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_GroupArea" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" >交流团组名称：</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_GroupName" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="auto-style5" >交流团团长：
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_GroupMonitor" runat="server" Width="100%" Height="100%" BorderStyle="None"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5" >团组成员名单：</td>
                    <td class="auto-style3" colspan="3">
                         <textarea id="tb_GroupMembers" name="tb_GroupMembers" style="width: 100%; height: 150px;" runat="server"></textarea>
                        <!-- 实例化编辑器 -->
                        <script type="text/javascript">
                            var ue = UE.getEditor('CPHMainContent_tb_GroupMembers', {
                                //"imageUrlPrefix":"/utf8-net/net/",
                                //"imagePathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"snapscreenPathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"filePathFormat": "upload/file/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"videoPathFormat": "upload/video/{yyyy}{mm}{dd}/{time}{rand:6}"
                            });
                        </script>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5" >交流行程：</td>
                    <td class="auto-style3" colspan="3">
                         <textarea id="tb_CommunicationGenerate" name="tb_CommunicationGenerate" style="width: 100%; height: 150px;" runat="server"></textarea>
                        <!-- 实例化编辑器 -->
                        <script type="text/javascript">
                            var ue = UE.getEditor('CPHMainContent_tb_CommunicationGenerate', {
                                //"imageUrlPrefix":"/utf8-net/net/",
                                //"imagePathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"snapscreenPathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"filePathFormat": "upload/file/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"videoPathFormat": "upload/video/{yyyy}{mm}{dd}/{time}{rand:6}"
                            });
                        </script>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style5" >交流工作总结：</td>
                    <td class="auto-style3" colspan="3">
                         <textarea id="tb_CommunicationSummarize" name="tb_CommunicationSummarize" style="width: 100%; height: 150px;" runat="server"></textarea>
                        <!-- 实例化编辑器 -->
                        <script type="text/javascript">
                            var ue = UE.getEditor('CPHMainContent_tb_CommunicationSummarize', {
                                //"imageUrlPrefix":"/utf8-net/net/",
                                //"imagePathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"snapscreenPathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"filePathFormat": "upload/file/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"videoPathFormat": "upload/video/{yyyy}{mm}{dd}/{time}{rand:6}"
                            });
                        </script>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style5" >专项工作报告：</td>
                    <td class="auto-style3" colspan="3">
                         <textarea id="tb_CommunicationReport" name="tb_CommunicationReport" style="width: 100%; height: 150px;" runat="server"></textarea>
                        <!-- 实例化编辑器 -->
                        <script type="text/javascript">
                            var ue = UE.getEditor('CPHMainContent_tb_CommunicationReport', {
                                //"imageUrlPrefix":"/utf8-net/net/",
                                //"imagePathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"snapscreenPathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"filePathFormat": "upload/file/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"videoPathFormat": "upload/video/{yyyy}{mm}{dd}/{time}{rand:6}"
                            });
                        </script>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style5" >交流照片：</td>
                    <td class="auto-style3" colspan="3">
                         <textarea id="tb_CommunicationPhotos" name="tb_CommunicationPhotos" style="width: 100%; height: 150px;" runat="server"></textarea>
                        <!-- 实例化编辑器 -->
                        <script type="text/javascript">
                            var ue = UE.getEditor('CPHMainContent_tb_CommunicationPhotos', {
                                //"imageUrlPrefix":"/utf8-net/net/",
                                //"imagePathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"snapscreenPathFormat": "upload/image/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"filePathFormat": "upload/file/{yyyy}{mm}{dd}/{time}{rand:6}",
                                //"videoPathFormat": "upload/video/{yyyy}{mm}{dd}/{time}{rand:6}"
                            });
                        </script>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
