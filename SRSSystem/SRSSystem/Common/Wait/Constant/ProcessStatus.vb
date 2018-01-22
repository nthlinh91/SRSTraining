Namespace Wait.Constant

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 処理状態を表す列挙体
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Enum ProcessStatus
        ''' <summary>処理中</summary>
        Processing = 0
        ''' <summary>処理済</summary>
        Completed = 1
        ''' <summary>警告リスト付き処理済(アップロード用)</summary>
        WarnList = 6
        ''' <summary>異常終了(排他制御エラー)</summary>
        ExclusiveError = 7
        ''' <summary>エラーリスト生成異常終了(アップロード用)</summary>
        ErrorList = 8
        ''' <summary>異常終了</summary>
        Aborted = 9
    End Enum

End Namespace
