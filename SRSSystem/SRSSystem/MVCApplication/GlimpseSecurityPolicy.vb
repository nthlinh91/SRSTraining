Imports Glimpse.AspNet.Extensions
Imports Glimpse.Core.Extensibility

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' Glimpse の実行時セキュリティポリシーをカスタマイズします。
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class GlimpseSecurityPolicy
    Implements IRuntimePolicy

    ''' <summary>
    ''' Glimpse を実行する際の実行許可を判定します。
    ''' </summary>
    ''' <param name="policyContext">ポリシー情報。</param>
    ''' <returns>実行許可。</returns>
    Public Function Execute(policyContext As IRuntimePolicyContext) As RuntimePolicy Implements IRuntimePolicy.Execute

        '' 下記の例のように、アプリケーション内で Glimpse の実行許可を制御できます。
        '' 実行時ポリシーに関するより詳しい情報はサイトを参照ください: http://getglimpse.com/Docs/Custom-Runtime-Policies
        '' Dim httpContext = policyContext.GetHttpContext()
        '' if Not httpContext.User.IsInRole("Administrator") Then
        ''     return RuntimePolicy.Off
        '' End If

        Return RuntimePolicy.On
    End Function

    ''' <summary>
    ''' 実行タイミングを表します。
    ''' </summary>
    Public ReadOnly Property ExecuteOn As RuntimeEvent Implements IRuntimePolicy.ExecuteOn
        '' RuntimeEvent.ExecuteResource は、セキュリティポリシーを作成した場合にのみ必要です。
        '' 詳細は http://blog.getglimpse.com/2013/12/09/protect-glimpse-axd-with-your-custom-runtime-policy/ を参照ください。
        Get
            Return RuntimeEvent.EndRequest Or RuntimeEvent.ExecuteResource
        End Get
    End Property

End Class
