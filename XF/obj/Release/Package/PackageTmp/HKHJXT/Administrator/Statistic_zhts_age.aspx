<%@ Page Title="" Language="C#" MasterPageFile="~/HKHJXT/Administrator/Admin.Master" AutoEventWireup="true" CodeBehind="Statistic_zhts_age.aspx.cs" Inherits="XF.HKHJXT.Administrator.Statistic_zhts_age" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var array_first = $($(".menu-left").get(0)).children().first().siblings().show();//显示在沪台生

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
                    <li class="menu-left-head">在沪台生信息</li>
                    <li style="display: none">
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="Statistic_zhts_school.aspx">学校分布统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Statistic_zhts_sex.aspx">性别统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Statistic_zhts_age.aspx">年龄统计</asp:HyperLink>
                    </li>
                </ul>
                <ul class="menu-left">
                    <li class="menu-left-head">两岸婚姻信息</li>
                    <li style="display: none">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="Statistic_lahy_sex_tai.aspx">台籍性别统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="Statistic_lahy_education.aspx">学历统计</asp:HyperLink>
                    </li>
                </ul>
                <ul class="menu-left">
                    <li class="menu-left-head">常驻台胞信息</li>
                    <li style="display: none">
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="Statistic_cztb_sex.aspx">性别统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="Statistic_cztb_age.aspx">年龄统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="Statistic_cztb_lhmd.aspx">来沪目的统计</asp:HyperLink>
                    </li>
                </ul>
                <ul class="menu-left" id="menu_zxgzxx" runat="server">
                    <li class="menu-left-head">专项工作信息</li>
                    <li style="display: none">
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="Statistic_zxgz_sex.aspx">性别统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="Statistic_zxgz_age.aspx">年龄统计</asp:HyperLink>
                    </li>
                </ul>
                <ul class="menu-left" id="menu_dkjlgzxx" runat="server">
                    <li class="menu-left-head">对口交流工作信息</li>
                    <li style="display: none">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Statistic_dkjl_sex.aspx">性别统计</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="Statistic_dkjl_age.aspx">年龄统计</asp:HyperLink>
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
                    text: '在沪台生信息 - 年龄统计'
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
        <h2 class="title2">在沪台生信息 - 年龄统计</h2>
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
                    <td style="width: 40px">学校：
                    </td>
                    <td style="width: 155px">
                        <asp:DropDownList runat="server" ID="ddl_School" Width="145" />
                    </td>
                    <td style="width: 40px">地域：</td>
                    <td>
                        <%--<asp:TextBox ID="tb_Area" runat="server" Width="90" Height="18" />--%>
                         <asp:DropDownList runat="server" ID="ddl_csmc" Width="90" Style="float: left">
                           <%-- <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="台北市" Value="台北市" />
                            <asp:ListItem Text="新北市" Value="新北市" />
                            <asp:ListItem Text="台中市" Value="台中市" />
                            <asp:ListItem Text="台南市" Value="台南市" />
                            <asp:ListItem Text="高雄市" Value="高雄市" />
                            <asp:ListItem Text="基隆市" Value="基隆市" />
                            <asp:ListItem Text="新竹市" Value="新竹市" />
                            <asp:ListItem Text="嘉义市" Value="嘉义市" />
                            <asp:ListItem Text="桃源县" Value="桃源县" />
                            <asp:ListItem Text="苗栗县" Value="苗栗县" />
                            <asp:ListItem Text="彰化县" Value="彰化县" />
                            <asp:ListItem Text="南投县" Value="南投县" />
                            <asp:ListItem Text="云林县" Value="云林县" />
                            <asp:ListItem Text="嘉义县" Value="嘉义县" />
                            <asp:ListItem Text="屏东县" Value="屏东县" />
                            <asp:ListItem Text="宜兰县" Value="宜兰县" />
                            <asp:ListItem Text="花莲县" Value="花莲县" />
                            <asp:ListItem Text="台东县" Value="台东县" />
                            <asp:ListItem Text="澎湖县" Value="澎湖县" />
                            <asp:ListItem Text="金门县" Value="金门县" />
                            <asp:ListItem Text="连江县" Value="连江县" />--%>
                        </asp:DropDownList>
                    </td>

                    <%--<td style="width: 60px">出生年份：
                    </td>
                    <td style="width: 180px">
                        <input id="input_start" runat="server" type="text" style="width: 70px" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy'})" />
                        -
                        <input id="input_end" runat="server" type="text" style="width: 70px" class="Wdate" errdealmode="0" onfocus="WdatePicker({dateFmt:'yyyy'})" />
                    </td>--%>
                    <td>
                        <asp:Button ID="btn_Search" runat="server" Text="搜索" OnClick="btn_Search_Click" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                    <td style="width: 110px"></td>
                    <td style="width: 120px">
                       <asp:Button ID="btn_Output" runat="server" Text="导出" OnClick="btn_Output_Click" Style="float:right" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                </tr>
            </table>
            <%--<table style="width: 100%; text-align: right" class="hide">
                <tr>
                    <td style="width: 20%"></td>
                    <td style="width: 20%"></td>
                    <td style="width: 20%"></td>
                    <td style="width: 20%"></td>
                    <td style="width: 20%">
                        <asp:Button ID="btn_Output" runat="server" Text="导出" CssClass="button button-rounded button-flat-primary button-tiny" />
                    </td>
                </tr>
            </table>--%>
            <div style="width: 778px; margin-top: 10px; text-align: center">
                <asp:Label runat="server" ID="label_head" Text="全体在读台生年龄统计" CssClass="statistic_head" />
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
