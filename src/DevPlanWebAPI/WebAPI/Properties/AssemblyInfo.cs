using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般的情報は、以下の属性セットによって
// 制御されます。アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更します。
[assembly: AssemblyTitle("DevPlanWebAPI")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("DevPlanWebAPI")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、このアセンブリ内の型は
// COM コンポーネントから参照できなくなります。
// COM からこのアセンブリ内の型にアクセスする必要がある場合は、その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります。
[assembly: Guid("268ad0c3-7e0a-4dff-bc4a-ef6b385a47ce")]

// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      リビジョン
//
// すべての値を指定するか、以下のように "*" を使用してリビジョンとビルド番号を
// 既定値にすることができます:
[assembly: AssemblyVersion("0.6.0.0")]
[assembly: AssemblyFileVersion("0.6.0.0")]

// Log4Net コンフィグファイルの読み込み
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "./Log.Config", Watch = true)]