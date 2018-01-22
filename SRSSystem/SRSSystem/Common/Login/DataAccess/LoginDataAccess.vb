Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Security
Imports SRS.Standards.MvcFramework.Core.Information

Namespace Login.DataAccess

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ログイン データアクセスクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-05, 新規作成
    ''' </Version>
    Public Class LoginDataAccess
        Inherits DataAccessBase

        ''' <summary>
        ''' 認証情報取得処理
        ''' </summary>
        ''' <param name="authModel">認証モデル</param>
        ''' <returns>確認済認証モデルの集合</returns>
        Public Function GetAuthentication(authModel As AuthenticationModel) As IEnumerable(Of Model.LoginModel)

            ' ログイン情報取得SQL
            Dim _sbSQL As New StringBuilder
            _sbSQL.Append("SELECT A.LOGINNM AS UserName ").AppendLine()
            _sbSQL.Append("     , A.GYOMUSIKID AS BusinessIdentifyId ").AppendLine()
            _sbSQL.Append("     , A.PASSWORD AS Password ").AppendLine()
            _sbSQL.Append("     , B.LOCALE AS Locale ").AppendLine()
            _sbSQL.Append("     , B.YMDFORMAT AS YmdFormat ").AppendLine()
            _sbSQL.Append("     , B.DECIMALPOINT AS DecimalPoint ").AppendLine()
            _sbSQL.Append("     , B.NUMBERDELIMITER AS NumberDelimiter ").AppendLine()
            _sbSQL.Append("  FROM CMLOGINID AS A").AppendLine()
            _sbSQL.Append("  LEFT JOIN CMGYOMUSIK AS B").AppendLine()
            _sbSQL.Append("    ON A.GYOMUSIKID = B.GYOMUSIKID").AppendLine()
            _sbSQL.Append("   AND B.DELFLG = '0'").AppendLine()
            _sbSQL.Append(" WHERE A.LOGINID = @LoginId").AppendLine()
            _sbSQL.Append("   AND A.DELFLG = '0'").AppendLine()
            ' SQL実行

            'DB利用不可対応
            'DBを利用しないためSQL実行はコメントアウトし固定ユーザーSRS1の情報を生成
            Dim model As New Model.LoginModel
            model.UserName = "ユーザ１"
            model.BusinessIdentifyId = "SAMPL"
            model.Password = "B89F4A97F7CC3CC89B848DC072D34B42AA6735B6BF93D6B45990A7C87A5AD0B2"
            model.Locale = "ja-JP"
            model.YmdFormat = "dd-MMMM-yyyy"
            model.DecimalPoint = "."
            model.NumberDelimiter = ","

            Dim list As New List(Of Model.LoginModel)
            list.Add(model)

            'Return Me.ExecuteQuery(Of Model.LoginModel)(_sbSQL.ToString, New With {.LoginId = authModel.UserId})
            Return list.AsEnumerable()


        End Function

        '''' <summary>
        '''' 業務規制情報取得処理
        '''' </summary>
        '''' <returns>確認済認証モデルの集合</returns>
        'Public Function GetRegulation() As IEnumerable(Of Model.GyomuKiseiModel)

        ' 業務規制情報取得SQL
        'Dim _sbSQL As New StringBuilder
        ' _sbSQL.Append("SELECT GYOMUKISEIID ").AppendLine()
        ' _sbSQL.Append("     , STARTYMD").AppendLine()
        ' _sbSQL.Append("     , STARTHHMM").AppendLine()
        ' _sbSQL.Append("     , ENDYMD").AppendLine()
        ' _sbSQL.Append("     , ENDHHMM").AppendLine()
        '  _sbSQL.Append("  FROM CTGYOMUKISEI").AppendLine()
        '  _sbSQL.Append(" WHERE MENUID = @MenuId").AppendLine()
        ' _sbSQL.Append("   AND ( GYOMUSIKID = @BusinessIdentifyId").AppendLine()
        '_sbSQL.Append("      OR GYOMUSIKID IS NULL )").AppendLine()
        ' _sbSQL.Append("   AND DELFLG = '0'").AppendLine()
        ' _sbSQL.Append(" ORDER BY GYOMUKISEIID DESC").AppendLine()
        ' SQL実行
        'Return Me.ExecuteQuery(Of Model.GyomuKiseiModel)(_sbSQL.ToString,
        'New With {.MenuId = InformationManager.AppInfo.MenuId,
        '.BusinessIdentifyId = InformationManager.UserInfo.BusinessIdentifyId})

        ' End Function

        '''' <summary>
        '''' 権限情報取得処理
        '''' </summary>
        '''' <returns>業務権限モデルの集合</returns>
        'Public Function GetPermission() As IEnumerable(Of Model.PermissionModel)

        ' 権限情報取得SQL
        'Dim _sbSQL As New StringBuilder
        '  _sbSQL.Append("SELECT K.INSKANOFLG AS CanInsertFlag").AppendLine()
        '  _sbSQL.Append("     , K.UPDKANOFLG AS CanUpdateFlag").AppendLine()
        '  _sbSQL.Append("     , K.DELKANOFLG AS CanDeleteFlag").AppendLine()
        '  _sbSQL.Append("     , K.SCHKANOFLG AS CanSearchFlag").AppendLine()
        '  _sbSQL.Append("  FROM CMMENUKOSEI AS K").AppendLine()
        '  _sbSQL.Append(" INNER JOIN CMGYOMUSIK AS S").AppendLine()
        '  _sbSQL.Append("    ON K.MENUGROUPID = S.MENUGROUPID").AppendLine()
        '  _sbSQL.Append(" WHERE K.MENUID = @MenuId").AppendLine()
        '  _sbSQL.Append("   AND S.GYOMUSIKID = @BusinessIdentifyId").AppendLine()
        '   _sbSQL.Append("   AND K.DELFLG = '0'").AppendLine()
        ' SQL実行
        'Return Me.ExecuteQuery(Of Model.PermissionModel)(_sbSQL.ToString,
        ' New With {.MenuId = InformationManager.AppInfo.MenuId,
        '.BusinessIdentifyId = InformationManager.UserInfo.BusinessIdentifyId})

        ' End Function

    End Class

End Namespace

