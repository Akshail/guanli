<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="Statistic_cztb_age.aspx.cs" Inherits="XF.HKHJXT.Administrator.Statistic_cztb_age" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var array_first = $($(".menu-left").get(2)).children().first().siblings().show();//显示教职工

            $(".menu-left").click(function () {
                //$(this).siblings().slideToggle();
                $(this).children().first().siblings().slideDown();
                var array = $(this).siblings();
                for (var i = 0; i < array.length; i++) {
                    $(array[i]).children().first().siblings().slideUp();
                }

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
    <div class="content1">
        <div class="maincontent" style="padding-top: 27px;">
            <div id="navcontainer">
                <ul class="menu-left">
                    <li class="menu-left-head">在校本科生信息</li>
                    <li style="display: none">
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="Statistic_zhts_school.aspx">学院分布统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Statistic_zhts_sex.aspx">性别统计</asp:HyperLink>
                    </li>
                </ul>
                <ul class="menu-left">
                    <li class="menu-left-head">教职工信息</li>
                    <li style="display: none">
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="Statistic_cztb_sex.aspx">性别统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="Statistic_cztb_age.aspx">年龄统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="Statistic_cztb_lhmd.aspx">担任职务统计</asp:HyperLink>
                    </li>
                </ul>
                <ul class="menu-left" id="menu_zxgzxx" runat="server">
                    <li class="menu-left-head">专项工作信息</li>
                    <li style="display: none">
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="Statistic_zxgz_sex.aspx">性别统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="Statistic_zxgz_age.aspx">年龄统计</asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHMainContent" runat="server">
    <link rel="stylesheet" href="../../Buttons/Buttons/css/buttons.css">

    <script src="../../highcharts/jquery-1.8.3.min.js"></script>
    <script src="../../highcharts/highcharts.js"></script>
    <script src="../../highcharts/exporting.js"></script>
    <script>
        $(function () {
            $('#container').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: '教职工信息 - 年龄统计'
                },

                xAxis: {
                    categories: [<%=GetxAxis()%>]
                },
                yAxis: {
                    min: 0,
                    allowDecimals: false,
                    title: {
                        text: '人数（名）'
                    }
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: false
                },

                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.x}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.0f} 名</b></td></tr>',
                    footerFormat: '</table>',
                    shared: false,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: [<%=GetData()%>]
            });
        });
    </script>
    <div class="content2">
        <h2 class="title2">教职工信息 - 年龄统计</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%" class="table_search">
                <tr>
                    <td style="width: 40px">性别：
                    </td>
                    <td style="width: 100px">
                        <asp:DropDownList runat="server" ID="ddl_sex" Width="80">
                            <asp:ListItem Value="99" Text="不限" Selected="True" />
                            <asp:ListItem Value="False" Text="男" />
                            <asp:ListItem Value="True" Text="女" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 60px">担任职务：
                    </td>
                    <td style="width: 100px">
                        <asp:DropDownList runat="server" ID="ddl_ComeReason" Width="80">
                            <asp:ListItem Text="不限" Value="99" Selected="True" />
                            <asp:ListItem Text="教师" Value="1" />
                            <asp:ListItem Text="主任" Value="2" />
                            <asp:ListItem Text="保安" Value="3" />
                            <asp:ListItem Text="清洁工" Value="4" />
                            <asp:ListItem Text="后勤" Value="5" />
                            <asp:ListItem Text="书记" Value="6" />
                            <asp:ListItem Text="其它" Value="7" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 60px">
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" OnClick="btn_Search_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td colspan="2" style="width: 190px">
                        <asp:Button ID="btn_Output" runat="server" Text="导出" OnClick="btn_Output_Click" Style="float:right" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                </tr>
            </table>
            <div style="width: 778px; margin-top: 10px; text-align: center">
                <asp:Label runat="server" ID="label_head" Text="" CssClass="statistic_head" />
            </div>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="Label1">共找到</asp:Label>
                        <asp:Label runat="server" ID="Label_Result"></asp:Label>
                        <asp:Label runat="server" ID="Label2">条记录</asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gv_result" runat="server" class="tableclass" AutoGenerateColumns="False"
                            Width="100%"
                            AllowPaging="False">
                            <Columns>
                                <asp:BoundField DataField="ItemName" HeaderText="年龄范围">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Count" HeaderText="总人数（名）">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Percent" HeaderText="所占比例">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <AlternatingRowStyle BackColor="LightGray" BorderColor="White" />

                            <RowStyle Font-Strikeout="False" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; text-align: right">
                <tr>
                    <td>
                        <div id="container" style="min-width: 600px; height: 400px; text-align: center"></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
