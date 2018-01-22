/********************************************************************
 * ユーザ名       : SRS
 * システム名     : トレーニング
 * サブシステム名 : 共通
 * 処理名称       : 共通JavaScript関数
 * 機能           : 共通JavaScript関数 ドロップダウンチェックリスト
 * 作成者         : SRSTaro
 * 改版履歴       : 001, 2018-01-11, SRSTaro, 新規作成
 *******************************************************************/

// このファイルが読込まれると、ASP.NET MVC標準のSRSDropDownListForで出力されたコントロールを自動的に処理します
(function () {

    // 表示名を取得します。
    // @param names  チェックが入っている名前の配列
    // @param defaultLabel  サーバ側で指定された、初期表示ラベル
    function getDisplayName(names, defaultLabel) {

        switch (names.length) {
            case 0:
                return defaultLabel;
            case 1:
                return names[0];
            default:
                return names[0] + "...";
        }
    }

    // チェックリスト全体を、状態に応じて表示更新します。
    function refreshCheckList(items) {
        items.each(function () {
            var item = $(this);
            var id = item.parent().attr("id");

            var boxes = item.find("input:checkbox");

            var container = item.parent();
            var basename = container.attr("basename");
            var defaultLabel = container.attr("optionlabel");
            if (!defaultLabel) {
                defaultLabel = "";
            }

            var i = 0;
            var names = boxes.map(function () {
                var box = $(this);
                if (box.prop("checked")) {
                    box.prop("name", basename + "[" + (i++).toString() + "]");
                    return box.parent().text();
                } else {
                    box.prop("name", "");
                }
            });

            var name = getDisplayName(names, defaultLabel);
            var messageBox = item.parent().find(".dropdown-checklist-dummybox-message");
            messageBox.text(name);
        });
    }

    function getIndex(input) {
        if (input == null || input.length == 0) {
            return 0;
        }
        var index = parseInt(input.val());
        if (isNaN(index) || index == null) {
            index = -1;
        }
        return index;
    }
    function setIndex(input, index) {
        if (input == null || input.length == 0) {
            return;
        }
        input.val(index);
    }
    function getQuery(input) {
        if (input == null || input.length == 0) {
            return;
        }
        var query = input.data("query");
        if (query === undefined || query == null) {
            query = "";
        }
        return query;
    }
    function setQuery(input, query) {
        if (input == null || input.length == 0) {
            return;
        }
        input.data("query", query);
    }

    function searchItem(itemChildren, query) {
        query = query.toLowerCase();
        for (var i = 0; i < itemChildren.length; i++) {
            var val = itemChildren.eq(i).find("label").text().toLowerCase();
            if (val != null && val !== undefined && val.lastIndexOf(query, 0) === 0) {
                return i;
            }
        }
        return -1;
    }

    // 公開用グローバルオブジェクト
    DropDownCheckList = {
        init: function () {

            $(".dropdown-checklist").each(function () {
                var target = $(this);
                var box = target.find(".dropdown-checklist-dummybox-message");
                var width = target.find(".dropdown-checklist-item").width();
                if (width > 0) {
                    box.css("min-width", (width + 7) + "px");
                }

                // 疑似フォーカス処理のための要素・イベント処理追加
                var controlHtml, tabindexHtml = '';
                // tabindexを親から疑似フォーカス要素に移動させる
                var tabindex = target.attr('tabindex');
                if (tabindex) {
                    tabindexHtml = ' tabindex="' + tabindex + '"';
                    target.removeAttr('tabindex');
                }
                controlHtml = '<input name="test" id="test" type="text" class="dropdown-checklist-dummyinput" readonly="readonly" style="width: 1px; position: absolute; top: -20px;"' + tabindexHtml + ' />';
                target.append(controlHtml);
                var input = $(this).find("input.dropdown-checklist-dummyinput");

                input.on("keydown", function (event) {

                    var items = target.find(".dropdown-checklist-items");
                    var itemChildren = items.find(".dropdown-checklist-item");

                    var keyCode = event.which;
                    // 疑似コントロール
                    if (keyCode === 9) {
                        // Tabのとき本体表示消す (blurで消すと、クリック時の挙動がおかしくなるため)
                        if (items.size() === 0) {
                            return;
                        }
                        items.css("visibility", "hidden");
                        itemChildren.eq(index).removeClass("dropdown-checklist-focus");

                        setIndex(input, -1);
                        target.find(".dropdown-checklist-item").each(function () {
                            $(this).removeClass("dropdown-checklist-focus");
                        });
                        setQuery(input, "");

                    } else if (keyCode === 38) {
                        // 上キー: 現在位置を上に
                        var index = getIndex(input);
                        itemChildren.eq(index).removeClass("dropdown-checklist-focus");
                        index--;
                        if (index < 0) {
                            index = 0;
                        }
                        setIndex(input, index);
                        // 本体表示する
                        if (items.length === 0) {
                            return;
                        }
                        items.css("visibility", "visible");
                        itemChildren.eq(index).addClass("dropdown-checklist-focus");
                        setQuery(input, "");

                    } else if (keyCode === 40) {
                        // 下キー: 現在位置を下に
                        var index = getIndex(input);
                        itemChildren.eq(index).removeClass("dropdown-checklist-focus");
                        index++;
                        if (index > itemChildren.size() - 1) {
                            index = itemChildren.size() - 1;
                        }
                        setIndex(input, index);

                        // 本体表示する
                        if (items.length === 0) {
                            return;
                        }
                        items.css("visibility", "visible");
                        itemChildren.eq(index).addClass("dropdown-checklist-focus");
                        setQuery(input, "");

                    } else if (keyCode === 32) {
                        // spaceキー: 現在位置のチェックを切替え
                        var index = getIndex(input);
                        if (index < 0) {
                            return;
                        }
                        var checkbox = itemChildren.eq(index).find(":checkbox");
                        checkbox.prop("checked", !checkbox.prop("checked")).change();
                        setQuery(input, "");
                    } else {
                        // 開いていない場合は検索しない
                        var displayValue = items.css("visibility");
                        if (displayValue === "hidden") {
                            return;
                        }

                        // 検索開始
                        var query = getQuery(input);
                        query += String.fromCharCode(keyCode);
                        var index = -1
                        // 一致する最上項目を選択
                        index = searchItem(itemChildren, query);

                        // 一致するものがなければ、クエリをリセットして再度検索する
                        if (index < 0) {
                            query = String.fromCharCode(keyCode);
                            index = searchItem(itemChildren, query);
                        }
                        // それでも一致しない場合、クエリをクリア
                        if (index < 0) {
                            query = "";
                        } else {
                            // 選択を変更
                            var orgIndex = getIndex(input);
                            itemChildren.eq(orgIndex).removeClass("dropdown-checklist-focus");
                            itemChildren.eq(index).addClass("dropdown-checklist-focus");
                            setIndex(input, index);
                        }
                        setQuery(input, query);
                    }
                });

                input.on("focus", function () {
                    box.addClass("dropdown-checklist-focus");
                });
                input.on("blur", function () {
                    box.removeClass("dropdown-checklist-focus");
                });

                // checkboxにはフォーカス不能としておく
                target.find(".dropdown-checklist-item input").each(function () {
                    $(this).attr("tabindex", "-1")
                });
                target.find(".dropdown-checklist-item input").on("focus", function () {
                    input.focus();
                });
            });

            // コントロール部(dummybox)をクリック時、本体表示をトグルする
            $(".dropdown-checklist > .dropdown-checklist-dummybox").on("click", function (event) {
                var box = $(this).find(".dropdown-checklist-dummybox-message");
                var items = $(this).parent().find(".dropdown-checklist-items");
                if (items.size() === 0) {
                    return;
                }
                var displayValue = items.css("visibility");
                if (displayValue === "hidden") {
                    // 表示する
                    items.css("visibility", "visible");
                } else {
                    // 非表示にする
                    items.css("visibility", "hidden");
                }
                box.addClass("dropdown-checklist-focus");
                event.stopPropagation();

                // 疑似コントロールにフォーカスする
                var input = $(this).parent().find("input.dropdown-checklist-dummyinput");
                input.focus();
                setQuery(input, "");
            });

            // チェックボックス切り替え時の処理
            $(".dropdown-checklist > .dropdown-checklist-items >.dropdown-checklist-item > label > input:checkbox").on("change", function () {
                // 表示更新
                var items = $(this).parent().parent().parent();
                var itemChildren = items.find(".dropdown-checklist-item");
                refreshCheckList(items);
                // indexを更新
                var input = items.parent().find("input.dropdown-checklist-dummyinput");
                var index = getIndex(input);
                itemChildren.eq(index).removeClass("dropdown-checklist-focus");
                var item = $(this).parent().parent();
                for (var i = 0; i < itemChildren.size(); i++) {
                    if (itemChildren.eq(i)[0] === item[0]) {
                        itemChildren.eq(i).addClass("dropdown-checklist-focus");
                        setIndex(input, i);
                    }
                }
            });
            // 全選択
            $(".dropdown-checklist-select-all").on("click", function () {
                var items = $(this).parent()
                items.find("input[type=checkbox]").prop("checked", true);
                refreshCheckList(items);
            });
            // 選択解除
            $(".dropdown-checklist-clear-all").on("click", function () {
                var items = $(this).parent()
                items.find("input[type=checkbox]").prop("checked", false);
                refreshCheckList(items);
            });

            // チェックリスト外をクリックしたとき、開いているチェックリストを閉じる
            var itemCached = $(".dropdown-checklist > .dropdown-checklist-items");
            $("html").on("click", function () {

                itemCached.each(function () {
                    var target = $(this);
                    var box = target.parent().find(".dropdown-checklist-dummybox-message");

                    var displayValue = target.css("visibility");
                    if (displayValue !== "hidden") {
                        var input = target.parent().find("input.dropdown-checklist-dummyinput");
                        // 非表示にする
                        target.css("visibility", "hidden");
                        setIndex(input, -1);
                        target.find(".dropdown-checklist-item").each(function () {
                            $(this).removeClass("dropdown-checklist-focus");
                        });
                    }
                    box.removeClass("dropdown-checklist-focus");

                });
            });

            // チェックリストを閉じる挙動は、チェックリスト内では阻止する
            $(".dropdown-checklist > .dropdown-checklist-items").on("click", function (event) {
                event.stopPropagation();
                // 疑似コントロールにフォーカスする
                var input = $(this).parent().find("input.dropdown-checklist-dummyinput");
                input.focus();
            });

        },

        redraw: function () {
            var items = $(".dropdown-checklist > .dropdown-checklist-items");
            refreshCheckList(items);
        }
    };
})();


$(document).ready(function () {
    // 初期化
    DropDownCheckList.init();
    DropDownCheckList.redraw();
});
