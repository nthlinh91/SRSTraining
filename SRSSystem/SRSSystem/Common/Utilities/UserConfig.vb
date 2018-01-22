Imports SRS.Standards.MvcFramework.Core.Configuration

Namespace Utilities

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ユーザ定義設定情報を取得します。
    ''' ここに定義したプロパティ名と、 MVCApplication/Config/User.config のプロパティ名を一致させると、
    ''' 自動的に値を読み込んで利用できます。
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class UserConfig

#Region "設定プロパティ"

        ' Public Property SettingName As String

#End Region

#Region "値取得用定義一式"

        ''' <summary>
        ''' 設定インスタンスを取得します。
        ''' </summary>
        ''' <returns>設定インスタンス。</returns>
        Public Shared Function Instance() As UserConfig
            Return ExtensionConfigurationManager.UserConfig(Of UserConfig)()
        End Function

#End Region

    End Class

End Namespace
