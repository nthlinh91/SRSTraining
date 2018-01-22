/********************************************************************
 * ユーザ名       : SRS
 * システム名     : トレーニング
 * サブシステム名 : 共通
 * 処理名称       : 共通JavaScript関数
 * 機能           : 共通JavaScript関数 一部列固定テーブル生成
 * 作成者         : SRSTaro
 * 改版履歴       : 001, 2018-01-11, SRSTaro, 新規作成
 *******************************************************************/

// このファイルが読込まれると、一覧画面中の対象テーブルが自動的に列固定テーブルに変換されます。
// 対象テーブルの指定方法は次の通り。
//    ・table-list クラスが付加された div 要素に、 data-fixed-columns="2" などと固定列数を記載する
//    例) <div class="table-list selectable scrollable broad" data-fixed-columns="2">
(function () {

    // TableのDOMを解析し、中間オブジェクトを生成
    // @titem:  tbody, thead, tfoot のいずれかの配列
    // @fixedcols: 固定列数
    function parseTable(titem, fixedcols) {
        var result = [], di = 0, j, k, l;

        for (j = 0; j < titem.length; j++) {
            var items = titem[j].childNodes;

            // 種類別に、行集合を格納する
            var obj = { Fixed: [], Flow: [], Row: [], Hidden: "" };
            var ri = 0, hi = 0;
            // rowspan と colspanが併用されているときに使う
            var rowspans = [], colspans = [];

            for (k = 0; k < items.length; k++) {
                // 種類別に、セル集合
                if (items[k].tagName && items[k].tagName.toLowerCase() === "tr") {
                    var ci = 0;
                    var arr = [], arr2 = [];
                    var cols = items[k].childNodes

                    for (l = 0; l < cols.length; l++) {
                        var cell = cols[l];
                        if (cell.tagName && (cell.tagName.toLowerCase() === "td" || cell.tagName.toLowerCase() === "th")) {

                            while (rowspans[ci] && rowspans[ci] > 0) {
                                rowspans[ci]--;
                                ci += colspans[ci];
                            }

                            var colspan = (cols[l].colSpan || 1);
                            var rowspan = (cols[l].rowSpan || 1);
                            if (ci < fixedcols) {
                                arr[ci] = cols[l];
                            } else {
                                arr2[ci - fixedcols] = cols[l];
                            }

                            if (rowspan > 1) {
                                rowspans[ci] = rowspan - 1;
                                colspans[ci] = colspan;
                            }
                            ci += colspan;
                        } else if (cell.tagName && cell.tagName.toLowerCase() === "input") {
                            obj.Hidden += cols[l].outerHTML;
                        }
                    }
                    obj.Row[ri] = items[k].getAttribute('class');
                    obj.Fixed[ri] = arr;
                    obj.Flow[ri++] = arr2;
                } else if (items[k].tagName && items[k].tagName.toLowerCase() === "input") {
                    obj.Hidden += items[k].outerHTML;
                }
            }
            result[di++] = obj;
        }

        return result;
    }

    // 指定したセレクタで特定される一覧を、列固定一覧に変換する
    // @selector: jQueryセレクタ
    // @fixedcols: 固定する列数
    function convertDomDataToJsonData(selector, fixedcols) {
        data = $(selector)[0];

        var rows, row, cells, i, j,
        json = { Data: [], DataContainerOpen: "", DataContainerClose: "", Header: [], HeaderContainerOpen: "", HeaderContainerClose: "", Hidden: "" };

        if (((data || {}).tagName || "").toLowerCase() === "div") {
            for (i = 0, j = 0, rows = data.childNodes; row = rows[i]; i++) {
                if (row.tagName) {
                    var name = row.tagName.toLowerCase();
                    if (name === "div") {
                        json.Data = parseTable($(row).find("tbody"), fixedcols);
                        $(row).find("tbody").remove();
                        var h = row.outerHTML;
                        var lastIndex = h.toLowerCase().lastIndexOf("</table>");
                        json.DataContainerOpen = h.substring(0, lastIndex);
                        json.DataContainerClose = h.substring(lastIndex, h.length);
                        continue;
                    }
                    if (name === "table") {
                        json.Header = parseTable($(row).find("thead"), fixedcols);
                        $(row).find("thead").remove();
                        var h = row.outerHTML;
                        var lastIndex = h.toLowerCase().lastIndexOf("</table>");
                        json.HeaderContainerOpen = h.substring(0, lastIndex);
                        json.HeaderContainerClose = h.substring(lastIndex, h.length);
                        continue;
                    }
                    if (name === "input") {
                        json.Hidden += row.outerHTML;
                    }
                }
            }
        }

        createGridTable(selector, json);

        return json;
    };

    // 中間オブジェクトをHTMLに変換する
    // @tagName: 生成するテーブル要素のタグ。tbody, thead, tfootのいずれか。
    // @propName: 使用する部分中間オブジェクトのプロパティ名。FixedかFlowのいずれか。
    // @tableObj: 使用する部分中間オブジェクト。
    function createTableContentHtml(tagName, propName, tableObj) {

        var result = "", i, k, len = tableObj.length;
        for (i = 0; i < len; i++) {
            result += "<" + tagName;
            if (tagName === "tbody") {
                result += ' data-groupno="' + i + '">';
            } else {
                result += ">";
            }
            var rows = tableObj[i][propName];
            var classes = tableObj[i]['Row'];
            for (var j = 0; j < rows.length; j++) {

                if (classes[j]) {
                    result += '<tr class="' + classes[j] + '" data-rowno="' + i + '-' + j + '">';
                } else {
                    result += '<tr data-rowno="' + i + '-' + j + '">';
                }
                for (k = 0; k < rows[j].length; k++) {
                    if (rows[j][k]) {
                        result += rows[j][k].outerHTML;
                    }
                }
                result += "</tr>";
            }
            result += "</" + tagName + ">";
        }

        return result;
    }

    // 中間オブジェクトからHiddenタグを作成します。
    // @json: 中間オブジェクト。
    function createHiddenHtml(json) {
        var result, i;
        result = json.Hidden;
        for (i = 0; i < json.Data.length; i++) {
            result += json.Data[i].Hidden;
        }
        for (i = 0; i < json.Header.length; i++) {
            result += json.Header[i].Hidden;
        }
        return result;
    }

    // 列固定一覧全体のHTMLを生成します。
    // @selector: jQueryセレクタ。
    // @json: 中間オブジェクト。
    function createGridTable(selector, json) {

        var html = '<table class="grid-table"><tbody><tr><td><div id="fixed-header" class="grid-fixed-tables">'
                 + json.HeaderContainerOpen
                 + createTableContentHtml('thead', 'Fixed', json.Header)
                 + json.HeaderContainerClose
                 + '</div></td><td><div id="flow-header" class="grid-flow-tables">'
                 + json.HeaderContainerOpen
                 + createTableContentHtml('thead', 'Flow', json.Header)
                 + json.HeaderContainerClose
                 + '</div></td></tr><tr><td><div id="fixed-data" class="grid-fixed-tables">'
                 + json.DataContainerOpen
                 + createTableContentHtml('tbody', 'Fixed', json.Data)
                 + json.DataContainerClose
                 + '</div></td><td><div id="flow-data" class="grid-flow-tables">'
                 + json.DataContainerOpen
                 + createTableContentHtml('tbody', 'Flow', json.Data)
                 + json.DataContainerClose
                 + '</div></td></tr></tbody></table>'
                 + createHiddenHtml(json);

        $(selector)[0].innerHTML = html;
    }

    // 公開用グローバルオブジェクト
    FixedColumnsTable = {
        init: function (selector) {
            $(selector).each(function () {
                // 1つだけ処理する
                var node = $(this).first();
                convertDomDataToJsonData(".table-list", node.attr("data-fixed-columns"));
            });
        },

        setHandlers: function (selector) {
            $(selector).each(function () {
                // 1つだけ処理する
                var node = $(this).first();
                var fixedData = node.find("#fixed-data > div")[0];
                var flowData = node.find("#flow-data > div")[0];
                var flowHeader = node.find("#flow-header")[0];
                var fixedHeader = node.find("#fixed-header")[0];
                var fixedDataObj = $(fixedData);
                var flowDataObj = $(flowData);

                // スクロール同期
                flowDataObj.on("scroll", function () {
                    fixedData.scrollTop = this.scrollTop;
                    flowHeader.scrollLeft = this.scrollLeft;
                });
                fixedDataObj.on("scroll", function () {
                    flowData.scrollTop = this.scrollTop;
                    fixedHeader.scrollLeft = this.scrollLeft;
                });

                // ホバー行同期
                flowDataObj.find("tr[data-rowno]").hover(function () {
                    var rowno = $(this).data("rowno");
                    $(document.querySelector("#fixed-data > div tr[data-rowno='" + rowno + "']")).addClass("table-row-hover");
                }, function () {
                    var rowno = $(this).data("rowno");
                    $(document.querySelector("#fixed-data > div tr[data-rowno='" + rowno + "']")).removeClass("table-row-hover");
                });
                fixedDataObj.find("tr[data-rowno]").hover(function () {
                    var rowno = $(this).data("rowno");
                    $(document.querySelector("#flow-data > div tr[data-rowno='" + rowno + "']")).addClass("table-row-hover");
                }, function () {
                    var rowno = $(this).data("rowno");
                    $(document.querySelector("#flow-data > div tr[data-rowno='" + rowno + "']")).removeClass("table-row-hover");
                });

                // ホバーグループ同期
                flowDataObj.find("tbody[data-groupno]").hover(function () {
                    var groupno = $(this).data("groupno");
                    $(document.querySelector("#fixed-data > div tbody[data-groupno='" + groupno + "']")).addClass("table-group-hover");
                }, function () {
                    var groupno = $(this).data("groupno");
                    $(document.querySelector("#fixed-data > div tbody[data-groupno='" + groupno + "']")).removeClass("table-group-hover");
                });
                fixedDataObj.find("tbody[data-groupno]").hover(function () {
                    var groupno = $(this).data("groupno");
                    $(document.querySelector("#flow-data > div tbody[data-groupno='" + groupno + "']")).addClass("table-group-hover");
                }, function () {
                    var groupno = $(this).data("groupno");
                    $(document.querySelector("#flow-data > div tbody[data-groupno='" + groupno + "']")).removeClass("table-group-hover");
                });

                // *自動編集を再設定
                // 数値テキストボックス編集処理 (桁区切り)
                node.find("input.number-delimited").on("focus", function () {
                    var node = $(this);
                    node.val(noNumberFormat(node.val()));
                    var elm = node[0];
                    if (elm.createTextRange) {
                        var range = elm.createTextRange();
                        range.moveEnd('character', elm.value.length);
                        range.select();
                    } else if (elm.setSelectionRange) {
                        elm.setSelectionRange(0, elm.value.length);
                    }
                });
                node.find("input.number-delimited").on("blur", function () {
                    var node = $(this);
                    node.val(numberFormat(node.val()));
                });
                node.find("input.number-delimited").each(function () {
                    var node = $(this);
                    node.val(numberFormat(node.val()));
                });

                // 年月日テキストボックス編集処理 (2桁年入力対応)
                node.find("input.ymd").on("blur", function () {
                    var node = $(this);
                    node.val(ymdFormat(node.val()));
                });
                // 年月テキストボックス編集処理 (2桁年入力対応)
                node.find("input.ym").on("blur", function () {
                    var node = $(this);
                    node.val(ymFormat(node.val()));
                });

            });
        }
    };
})();

$(document).ready(function () {
    // 初期化
    FixedColumnsTable.init("div.table-list[data-fixed-columns]");
    FixedColumnsTable.setHandlers("div.table-list[data-fixed-columns]");
});
