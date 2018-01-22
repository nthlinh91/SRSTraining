Imports SRS.Standards.MvcFramework.Core.Information

Namespace Utilities

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' システム基盤に関係するユーティリティ関数を定義します。
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public NotInheritable Class InformationUtility

        ''' <summary>
        ''' ダミーコンストラクタ
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' ログインしているユーザの年月日フォーマットを取得します。
        ''' </summary>
        ''' <returns>年月日フォーマット。</returns>
        Public Shared Function GetUserYmdFormat() As String

            If InformationManager.UserInfo.UserAttribute.ContainsKey("YmdFormat") Then
                Return InformationManager.UserInfo.UserAttribute("YmdFormat")
            End If

            Return "yyyy/MM/dd"

        End Function

        ''' <summary>
        ''' ログインしているユーザの年月フォーマットを取得します。
        ''' </summary>
        ''' <returns>年月フォーマット。</returns>
        Public Shared Function GetUserYmFormat() As String

            If InformationManager.UserInfo.UserAttribute.ContainsKey("YmFormat") Then
                Return InformationManager.UserInfo.UserAttribute("YmFormat")
            End If

            Return "yyyy/MM"

        End Function

    End Class

End Namespace
