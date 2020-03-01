<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="Statistic_zhts_school.aspx.cs" Inherits="XF.HKHJXT.Administrator.Staticstic_zdtsqk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var array_first = $($(".menu-left").get(0)).children().first().siblings().show();//显示在校本科生

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
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false
                },
                title: {
                    text: '在校本科生信息 - 学院分布统计'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.2f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            color: '#000000',
                            connectorColor: '#000000',
                            format: '<b>{point.name}</b>: {point.percentage:.2f} %'
                        }
                    }
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: false
                },
                series: [{
                    type: 'pie',
                    name: '百分比',
                    data: [<%=GetPercentData()%>]
                }]
            });
        });
    </script>
    <div class="content2">
        <h2 class="title2">在校本科生信息 - 学院分布统计</h2>
        <div class="maincontent">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table style="width: 100%;" class="table_search">
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
                    <td style="width: 60px">出生年份：
                    </td>
                    <td style="width: 180px">
                        <input id="input_start" runat="server" type="text" style="width: 70px" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy'})" />
                        <input id="input_end" runat="server" type="text" style="width: 70px" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy'})" />
                    </td>
                    <td>
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" OnClick="btn_Search_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td style="width: 190px;">
                        <asp:Button ID="btn_Output" runat="server" Text="导出" OnClick="btn_Output_Click" Style="float: right" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                </tr>
            </table>

            <div style="width: 778px; margin-top: 10px; text-align: center">
                <asp:Label runat="server" ID="label_head" Text="全体在校本科生学院分布统计" CssClass="statistic_head" />
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
                        <asp:GridView ID="gv_result" runat="server" class="tableclass" AutoGenerateColumns="false"
                            Width="100%"
                            AllowPaging="false">
                            <Columns>
                                <asp:BoundField DataField="ItemName" HeaderText="学院名称">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Count" HeaderText="总人数（名）">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Percent" HeaderText="所占比例">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                            </Columns>
                            <AlternatingRowStyle BackColor="lightgray" BorderColor="white" />

                            <RowStyle Font-Strikeout="false" />
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
