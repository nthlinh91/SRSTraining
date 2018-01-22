Imports System.Web
Imports System.Web.Optimization

Imports SRS.Standards.MvcFramework.Core.MVC

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' JS・CSSなどのバンドル設定
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class BundleConfig
    ' Bundling の詳細については、http://go.microsoft.com/fwlink/?LinkId=254725 を参照してください

    ''' <summary>
    ''' バンドル設定を登録する
    ''' </summary>
    ''' <param name="bundles">バンドル設定</param>
    Public Shared Sub RegisterBundles(bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                   "~/Scripts/jquery/jquery-{version}.js",
                   "~/Scripts/jquery/jquery.serializejson.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery/jquery-ui-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery/jquery.unobtrusive*",
                    "~/Scripts/jquery/jquery.validate*"))

        ' 開発と学習には、Modernizr の開発バージョンを使用します。次に、実稼働の準備が
        ' できたら、http://modernizr.com にあるビルド ツールを使用して、必要なテストのみを選択します。
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/jquery/modernizr-*"))

        bundles.Add(New StyleBundle("~/Content/themes/base/css").Include(
            "~/Content/themes/base/jquery-ui.css",
            "~/Content/themes/base/jquery-ui.structure.css",
            "~/Content/themes/base/jquery-ui.theme.css"))

        ' 以降カスタマイズ
        ' JavaScript
        bundles.Add(New ScriptBundle("~/bundles/srsScript").Include(
                    "~/Scripts/common_search.js",
                    "~/Scripts/commonAjax.js",
                    "~/Scripts/enter.js",
                    "~/Scripts/function.js",
                    "~/Scripts/cookies.js",
                    "~/Scripts/dropdownchecklist.js",
                    "~/Scripts/json2.js",
                    "~/Scripts/l10n.js",
                    "~/Scripts/fixedcolumnstable.js"))

        ' CSS 
        bundles.Add(New StyleBundle("~/Content/srsCss").Include("~/Content/base.css", "~/Content/project.css"))


    End Sub
End Class
