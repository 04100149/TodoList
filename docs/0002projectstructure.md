# プロジェクトの構造
code: ![tag](../Images/tag.png) [Step 01](https://github.com/04100149/TodoList/tree/step01)

プロジェクトの構造は次の通り。  
![プロジェクト構造](../Images/ProjectStructure-1.png)  

| 項目 | 概要 |
|:-----------|:------------|
| ソリューション'TodoList' | ソリューションフォルダ |
| TodoList | プロジェクトフォルダ |
| Propertiesフォルダ | ローカル開発環境のプロファイル設定 |
| wwwrootフォルダ | Webサイトの静的ファイル置き場 |
| Dataフォルダ | データクラスやサービスを配置する |
| Pagesフォルダ | Webページを配置する |
| \_Layout.cshtml | レイアウト用のhtml。全てのページに適用されるため、JavaScriptはここで足す |
| Sharedフォルダ | サイドメニューやRazorコンポーネントなどの共有モジュールを配置する |
| MainLayout.razor | レイアウト用Razorコンポーネント |
| NavMenu.razor | サイドメニューRazorコンポーネント |
| \_Imports.razor | インポートファイル。このファイルに **@using** を書いておくと全てのrazorテンプレートに適用される |
| App.razor | アプリ向けルートコンポーネント |
| appsetting.json | 構成ファイル |
| Dockerfile | Docker用定義ファイル |
| program.cs | サーバーを起動するアプリのエントリ ポイントであり、アプリ サービスとミドルウェアを構成する場所 |

**参考** [ASP.NET Core Blazor プロジェクトの構造](https://docs.microsoft.com/ja-jp/aspnet/core/blazor/project-structure?view=aspnetcore-6.0)

***
- Prev [プロジェクト新規作成](0001newproject.md)
- Next [Webページの追加](0003addrazorcomponent.md)

