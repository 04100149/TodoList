# 複数クライアント対応
code: ![tag](../Images/tag.png) [Step 9](https://github.com/04100149/TodoList/releases/tag/step9)  

## Point
- [TodoServiceにイベントを追加する](#todoservice%E3%81%AB%E3%82%A4%E3%83%99%E3%83%B3%E3%83%88%E3%82%92%E8%BF%BD%E5%8A%A0%E3%81%99%E3%82%8B)
- [イベントにより表示を更新する](#%E3%82%A4%E3%83%99%E3%83%B3%E3%83%88%E3%81%AB%E3%82%88%E3%82%8A%E8%A1%A8%E7%A4%BA%E3%82%92%E6%9B%B4%E6%96%B0%E3%81%99%E3%82%8B)

## 手順
### TodoServiceにイベントを追加する
1. TodoService.csを編集し、`EventHandler`を追加する。`Todo`が変化するタイミングでイベントを発行する。      
```diff
+        public event EventHandler TodoChanged = delegate { };
         public void SaveTodo(TodoItem todo)
         {
             SaveTodoFile(todo);
+            TodoChanged(this, EventArgs.Empty);
         }
         public List<TodoItem> LoadTodos()
         {
             return LoadTodoFiles();
         }
         public void RemoveTodo(TodoItem todo)
         {
             RemoveTodoFile(todo.Id);
+            TodoChanged(this, EventArgs.Empty);
         }
```
2. [TodoService.cs]()を保存する。
### イベントにより表示を更新する
1. **Todo.razor**を編集し、`OnInitialized()`で、`TodoService`のイベントをハンドルする。  
ハンドラでは、`Todo`を読み直し、画面のスレッドで`StateHasChanged`を実行する。
```diff
     protected override void OnInitialized()
     {
         todos = TodoService.LoadTodos();
         latestId = todos.Select<TodoItem, int>(x => x.Id).DefaultIfEmpty().Max() + 1;
+        TodoService.TodoChanged += TodoChanged;
     }
 
+    private async void TodoChanged(object? sender, EventArgs args)
+    {
+        todos = TodoService.LoadTodos();    
+        await InvokeAsync(StateHasChanged);
+    }
```
2. 終了時にイベントを解放するために、`Todo`を`IDisposable`にする。  
```diff
 @page "/todo"
 @using System.Text
 @using System.Text.Json
 @using TodoList.Data
 @inject TodoService TodoService
+@implements IDisposable
 
 <PageTitle>Todo</PageTitle>
```
3. `Dispose`メソッドでイベントを解放する。
```C#
    public void Dispose()
    {
        TodoService.TodoChanged -= TodoChanged;
    }
```
4. [Todo.razor]()を保存する。

code: ![tag](../Images/tag.png) [Step 10](https://github.com/04100149/TodoList/releases/tag/step10)  

## 動作確認
1. ![デバックの開始](../Images/NewProject-6.png) ボタンをクリックする。  
1. ビルド後、開発用コンテナが開始され、ブラウザが起動する。  
![コンテナ開始](../Images/multiclient-1.png)
1. サイドメニューの **Todo** をクリックすると、Todoページが開く。    
![Todoページ](../Images/multiclient-2.png)
1. サイドメニューの **Todo** を右クリックして、新しいウィンドウで開く。  
![すべて表示](../Images/multiclient-3.png)
1. `Todo`を追加すると、両方のウィンドウに表示される。
![すべて表示](../Images/multiclient-4.png)

***
- Prev [モジュール化する](docs/0010modularization.md)
- Next [Dockerでホストする](docs/0012docker.md)

