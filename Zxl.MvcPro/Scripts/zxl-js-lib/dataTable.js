// <div id="DataViewDiv"></div>
function mBindTable(ajaxUrl) {

    $("#DataViewDiv").html("<table id='dataTable'></table>");
    oTable = $('#dataTable').dataTable({
        "sDom": 'rt<"bottom"pfil>', //p paginate i info l length
        "bPaginate": true,
        "bJQueryUI": true,
        "bAjaxDataGet": true,
        "bAutoWidth": false,
        "bProcessing": true,
        "bServerSide": true,
        "bFilter": false,
        "bLengthChange": true,
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Scripts/DataTables/jquery.dataTable.cn.txt"
        },
        "aaSorting": [[0, "desc"]],
        "aoColumns": [
                        { "sTitle": "ID", "sClass": "", "sWidth": "5%", "bSortable": false },
                        { "sTitle": "事件名称", "sClass": "", "sWidth": "25%", "bSortable": false },
                        { "sTitle": "类别", "sClass": "", "sWidth": "10%", "bSortable": false },
                        { "sTitle": "监测代码", "sClass": "", "sWidth": "40%", "bSortable": false },
                        { "sTitle": "操作", "sClass": "", "sWidth": "20%", "bSortable": false }
                    ],
        "sAjaxSource": ajaxUrl,
        "fnServerData": function (sSource, aoData, fnCallback) {
            if (typeof keyword != "undefined") {
                aoData.push({ "name": "keyword", "value": keyword });
            }
            $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": aoData,
                "success": fnCallback
            });
        }
    });
}