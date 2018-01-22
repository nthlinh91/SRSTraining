@Imports SRS.Standards.MvcFramework.Core.Utilities
@Imports SRS.Standards.MvcFramework.Core.Facade
@Imports SRS.Standards.MvcFramework.Core.MVC
@ModelType SRSSystem.Common.Menu.Model.MenuModel
@*
'--------------------------------------------------------------------
'  ユーザ名          : SRS
'  システム名        : トレーニング
'  サブシステム名    : 共通
'  処理名称          : 共通画面
'  機能              : メニュー画面
'  作成者            : SRSTaro
'  改版履歴          : 001, 2018-01-11, SRSTaro, 新規作成
'--------------------------------------------------------------------
*@
@Section BusinessScript
    <!-- 画面固有JavaScript -->
    <script type="text/javascript">

    $(document).ready(function () {
        // メニュー開閉
        $(".menu-content-title").on("click", function () {
            $(this).next(".menu-content-list").toggle()
        });
        // メニュー項目を別ウィンドウで開く
        $(".menu-content-list-item a").on("click", function () {
            var target = $(this);
            var url = target.attr("data-menu-url");
            var menuId = target.attr("data-menu-Id");
            openNextPage(url, menuId);
        });
    });
    </script>
End Section

<div class="menu-message-container">
<pre>@Html.SRSRaw(Model.NoticeMessage)</pre>
</div>

@* メニューリストの出力 *@
<div class="menu-container">
    @For Each _elementModel In Model.MenuData
      @* 管理業務メニュー設定 *@
      @<div class="menu-content">
        <div class="content-header menu-content-title">
            <span class="menu-link-text">@Html.SRSRaw(_elementModel.Title)</span>
        </div>
            @* メニュー項目設定 *@
            <div class="menu-content-list">
              <ul>
                @For Each _elementMenu In _elementModel.Children

                  @<li class="menu-content-list-item">
                    <span class="menu-link-text"><a data-menu-url="@Html.SRSRaw(_elementMenu.Url)" data-menu-id="@Html.SRSRaw(_elementMenu.MenuId)">@Html.SRSRaw(_elementMenu.Title)</a></span>
                  </li>
                Next
              </ul>
            </div>
       </div>
    Next
</div>
