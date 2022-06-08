# Webページの追加
code: ![tag](../Images/tag.png) [Step 1](https://github.com/04100149/TodoList/releases/tag/step1)  

## Point
- [Todoページを追加する。](#todo%E3%83%9A%E3%83%BC%E3%82%B8%E3%82%92%E8%BF%BD%E5%8A%A0%E3%81%99%E3%82%8B)
- [サイドメニューから呼び出せるようにする。](#%E3%82%B5%E3%82%A4%E3%83%89%E3%83%A1%E3%83%8B%E3%83%A5%E3%83%BC%E3%81%8B%E3%82%89%E5%91%BC%E3%81%B3%E5%87%BA%E3%81%9B%E3%82%8B%E3%82%88%E3%81%86%E3%81%AB%E3%81%99%E3%82%8B)

## 手順
### Todoページを追加する
1. ソリューション エクスプローラの **Pages** フォルダを右クリックし、 コンテキストメニューの **追加 - Razorコンポーネント** をクリックする。
1. 名前を **Todo.razor** にして **追加** ボタンを押す。
1. PagesフォルダにTodo.razorが追加される。  
![追加されたTodo.razor](../Images/AddRazorComponent-1.png)
1. Todo.razorを編集し、次のようにする。    
```HTML+razor
@page "/todo"

<PageTitle>Todo</PageTitle>

<h1>Todo</h1>

@code {

}
```
5. Todo.razorを保存する。  
### サイドメニューから呼び出せるようにする。
1. **Shared** フォルダの **NavMenu.razor** を開く。  
1. [NavMenu.razor](https://github.com/04100149/TodoList/blob/6e3732fc737448440d44eebc03fa161cb38d2f79/TodoList/Shared/NavMenu.razor#L27-L31) に次のコードを追加する。    
```HTML+razor
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="todo">
                <span class="oi oi-task" aria-hidden="true"></span> Todo
            </NavLink>
        </div>
```
3. **NavLink** がクリックされると、**href** アトリビュートに設定されているページに遷移する。この場合、**@page "/todo"** と宣言されているTodo.razorに遷移する。  
1. NavMenu.razorを保存する。  

code: ![tag](../Images/tag.png) [Step 2](https://github.com/04100149/TodoList/releases/tag/step2)  

## 動作確認
1. ![デバックの開始](../Images/NewProject-6.png) ボタンをクリックする。  
1. ビルド後、開発用コンテナが開始され、ブラウザが起動する。  
![コンテナ開始](../Images/AddRazorComponent-2.png)
1. サイドメニューの **Todo** をクリックすると、Todoページが開く。    
![Todoページ](../Images/AddRazorComponent-3.png)


