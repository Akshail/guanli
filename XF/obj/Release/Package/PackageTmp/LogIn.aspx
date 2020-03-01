<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="XF.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="Buttons/Buttons/css/buttons.css" />

    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#changePic").click(function () {
                $("#yzimage").click();
            });
            $("#ImageButton1").click(function () {
                if ($("#txtAccount").val() == "") {
                    alert("请输入帐号");
                    return false;
                }
                if ($("#txtPassword").val() == "") {
                    alert("请输入密码");
                    return false;
                }
                if ($("#tb_yzm").val() == "") {
                    alert("请输入验证码");
                    return false;
                }
                var index = parent.layer.getFrameIndex(window.name);
                var a = "{'Account':'" + $("#txtAccount").val() + "','Password':'" + $("#txtPassword").val() + "','YZM':'" + $("#tb_yzm").val() + "'}";
                $.ajax({
                    type: "Post",
                    url: "LogIn.aspx/Login",
                    data: "{'Account':'" + $("#txtAccount").val() + "','Password':'" + $("#txtPassword").val() + "','YZM':'" + $("#tb_yzm").val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //返回的数据用data.d获取内容 
                        if (data.d == "1") { //系统管理员
                            setTimeout(function () {
                                window.parent.location.href = "./HKHJXT/Administrator/NoticeManage.aspx";
                            }, 0)
                        }
                        else if (data.d == "2") {//基础操作员登录
                            setTimeout(function () {
                                window.parent.location.href = "./HKHJXT/Administrator/NoticeManage.aspx";
                            }, 0)
                        }
                        else if (data.d == "3") {//管理员登录
                            setTimeout(function () {
                                window.parent.location.href = "./HKHJXT/Administrator/NoticeManage.aspx";
                            }, 0)
                        }
                        else if (data.d == "6") {  //账号密码错误
                            setTimeout(function () {
                                parent.layer.msg('帐号或密码错误', 3, 3);
                            }, 0)
                        }
                        else if(data.d == "7")
                        {
                            setTimeout(function () {
                                parent.layer.msg('验证码错误', 3, 3);
                            }, 0)
                        }
                        else {
                            setTimeout(function () {
                                parent.layer.msg('登录失败', 3, 3);
                            }, 0)
                            //parent.layer.msg('登录失败', 3, 3);
                        }
                    },
                    error: function (err) {
                        alert(err);
                    }
                });

                //禁用按钮的提交 
                return false;
            })
        })
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".imgcode").click(function () {
                imgchange();
            });
            $(".changecode").click(function () {
                imgchange();
            });
            function imgchange() {
                var radom = "CheckCodeHandler.ashx?" + Math.random();
                $(".imgcode").attr("src", radom);
            }
        });
    </script>
</head>
<body style="background: none;">
    <form id="form1" runat="server">
        <table class="table" width="100%">
            <tr>
                <td align="left" valign="middle">
                    <img alt="" class="auto-style1" src="images/login_4.png" /></td>
                <td align="left" valign="middle">
                    <asp:TextBox ID="txtAccount" runat="server" MaxLength="50" Width="122px" Height="20px" Style="border-color: #A4C2CA; line-height: 18px;"
                        BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" valign="middle">
                    <img alt="" class="auto-style1" src="images/login_7.png" /></td>
                <td align="left" valign="middle">
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" TextMode="Password" Width="122px" Height="20px" Style="border-color: #A4C2CA; line-height: 18px;" BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" valign="middle" style="width:65px">
                    <img alt="" class="auto-style1" src="images/login_8.png" /></td>
                <td align="left" valign="middle" style="width:125px">
                    <asp:TextBox ID="tb_yzm" runat="server" MaxLength="50"  Width="122px" Height="20px" Style="border-color: #A4C2CA; line-height: 18px;" BorderWidth="1px"></asp:TextBox>
                </td>
                <td style="width:85px">
                    <asp:Image ID="yzimage" runat="server" alt="" ImageAlign="AbsMiddle" onclick="this.src='../GetPic.aspx?abc='+Math.random()" ImageUrl="~/GetPic.aspx" class="big pull-left" />   
                </td>
                <td style="padding:0px">
                    <div style="height:30px;width:85px;font-size:10px;padding:0px;line-height:15px"><a href="#" id="changePic" style="padding:0px">看不清?<br />点击图片刷新</a></div>
                </td>
            </tr>
            <%--<tr>
                <td style="text-align:left;vertical-align:middle">验证码：</td>
                <td style="text-align:left;vertical-align:middle">
                      <table border="0" cellpadding="0" cellspacing="0">
                                       <tr>
                                           <td><asp:TextBox ID="yzm" runat="server" Width="122px" Height="20px" Font-Bold="False" Font-Size="11pt" ForeColor="Black" MaxLength="4" onkeypress="yzm1_Focus_fun();" Font-Names="Batang" BorderColor="#BBBBBB" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                           </td>
                                           <td>&nbsp;<img alt="看不清，换一张" class="imgcode" src="CheckCodeHandler.ashx" height="22" 
                                                   width="64" style="cursor:hand"/>
                                           </td>
                                           <td>
                                           &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="yzm" ErrorMessage="*" Font-Bold="False" Font-Names="Arial Black" Font-Size="Large"></asp:RequiredFieldValidator></td>
                                       </tr>
                                    </table>
                </td>
            </tr>--%>
            <tr>
                <td align="left" valign="middle"></td>
                <td align="left" valign="middle">
                    <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/login_btn.gif" Style="margin-top: 10px;" />--%>
                    <%--<asp:ImageButton ID="ImageButton1" runat="server"  CssClass="button button-rounded button-flat-action button-tiny" Style="margin-top: 10px;"/>--%>
                    <asp:Button ID="ImageButton1" runat="server" CssClass="button button-rounded button-flat-primary button-tiny" Width="80px" Height="30" Text="登录" OnClick="ImageButton1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LabelError" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
    </form>
</body>
</html>

