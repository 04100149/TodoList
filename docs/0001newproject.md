# プロジェクト新規作成
Visual Studio 2022でBlazor Serverのプロジェクトを新規作成します。
1. Visual Studio 2022を起動する。
1. 新しいプロジェクトの作成をクリックする。  
![新しいプロジェクトの作成](../Images/NewProject-1.png)
1. 「Blazor Server アプリ」を選択して、「次へ」ボタンをクリックする。  
![Blazor Server アプリ](../Images/NewProject-2.png)
1. プロジェクト名に **TodoList** と入力し、「次へ」ボタンをクリックする。  
![プロジェクト構成](../Images/NewProject-3.png)
1. フレームワークで **.NET 6.0(長期的なサポート)** を選択、**Docker を有効にする** にチェックを入れ、Docker OS を **Linux**にする。その後、「次へ」ボタンをクリックする。  
![追加情報](../Images/NewProject-4.png)
1. プロジェクトが新規作成され、開発用のコンテナが開始されるまで待つ。  
![コンテナ開始](../Images/NewProject-5.png)  
code: ![tag](../Images/tag.png) [Step 1](https://github.com/04100149/TodoList/releases/tag/step1)


## 動作確認
1. ![デバックの開始](../Images/NewProject-6.png) ボタンをクリックする。  
1. ビルド後、開発用コンテナが開始され、ブラウザが起動する。  
![コンテナ開始](../Images/NewProject-7.png)

***
- Prev [前提条件](0000prerequisites.md)
- Next [プロジェクトの構造](0002projectstructure.md)

