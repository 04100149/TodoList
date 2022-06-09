# モジュール化する
code: ![tag](../Images/tag.png) [Step 8](https://github.com/04100149/TodoList/releases/tag/step8)  

## Point
- [razorコンポーネント化する]()
- [永続化コードをサービス化する]()

## 手順
### razorコンポーネント化する
- Todoの表示部分をRazorコンポーネント化する。
  - `TodoItem`を引数に受け取り情報を表示する。
  - 編集/完了/削除ボタンのイベントを用意する。 
1. ソリューション エクスプローラの **Shared** フォルダを右クリックし、 コンテキストメニューの **追加 - Razor コンポーネント** をクリックする。
1. 名前を **TodoBox.razor** にして **追加** ボタンを押す。
1. SharedフォルダにTodoBox.razorが追加される。  
![追加されたTodoBox.razor](../Images/modularization-1.png)
1. TodoBox.razorを編集し、**Todo.razor**からTodo表示部分をコピーする。    
```HTML+razor
    <div class="section" style="background-color: @GetBackgroundColor(todo)">
        <table>
            <tr>
                <td align="right" valign="top"><span class="oi oi-key"/></td>
                <td>
                    @todo.Id
                    <ul class="icon">
                        <li>
                            <span class="oi oi-pencil" @onclick=@(_=>EditTodo(todo)) />
                        </li>
                        <li>
                            <span class="oi oi-check" @onclick=@(_=>DoneTodo(todo)) />
                        </li>
                        <li>
                            <span class="oi oi-trash" @onclick=@(_=>RemoveTodo(todo)) />
                        </li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top"><span class="oi oi-tag"/></td>
                <td>@todo.Title</td>
            </tr>
            <tr>
                <td align="right" valign="top"><span class="oi oi-clock"/></td>
                <td>
                    @todo.TargetDate.ToString("yyyy/MM/dd")
                    @if(todo.IsDone)
                    {
                        <span class="oi oi-check" style="margin-left: 10px;"/>
                        @todo.EndDate.ToString("yyyy/MM/dd")
                    }
                    else
                    {
                        var days = (todo.TargetDate - DateTime.Today).Days;
                        @if(days == 0)
                        {
                            <span style="color: red; margin-left: 10px;">今日まで！</span>
                        }
                        else if(days<0)
                        {
                            <span style="color: red; margin-left: 10px;">@(-days)日 オーバー</span>
                        }
                        else
                        {
                            <span style="color: black; margin-left: 10px;">あと @(days)日</span>                            
                        }
                    }
                </td>
            </tr>
            <tr>
                <td align="right" valign="top"><span class="oi oi-comment-square"/></td>
                <td ><p class="ellipsis" title=@todo.Memo>@todo.Memo</p></td>
            </tr>
        </table>
    </div>
```
2. **Todo.razor**から`GetBackgroundColor()`を移動し、`@code{}`内に配置する。
```HTML+razor
@code {
    
    private string GetBackgroundColor(TodoItem todo)
    {
        if (todo.IsDone){ return "silver"; }
        if (todo.TargetDate == DateTime.Today) { return "khaki"; }
        if (todo.TargetDate < DateTime.Today) { return "lightpink"; }
        return "lightblue";
    }
}
```
3. `TodoItem`が使えるように、先頭に`@using TodoList.Data`を追加する。
```diff
+@using TodoList.Data
```
4. `TodoItem`を外部から渡せるように`[Parameter]`属性のプロパティを追加する。
```diff
 @code {
+    [Parameter]
+    public TodoItem Item { get; set; }
 
     private string GetBackgroundColor(TodoItem todo)
```
5. 表示部分の`todo`を`Item`に置換する。  
6. ボタン用の`[Parameter]`属性の`EventCallback`を追加する。
```diff
     [Parameter]
     public TodoItem Item { get; set; }
 
+    [Parameter]
+    public EventCallback<TodoItem> OnClickEditCallback { get; set; }
+
+    [Parameter]
+    public EventCallback<TodoItem> OnClickDoneCallback { get; set; }
+
+    [Parameter]
+    public EventCallback<TodoItem> OnClickRemoveCallback { get; set; }
 
     private string GetBackgroundColor(TodoItem Item)
```
7. ボタンの`@onclick`で`EventCallback`を呼ぶように変更する。
```diff
                     <ul class="icon">
                         <li>
-                            <span class="oi oi-pencil" @onclick=@(_=>EditTodo(Item)) />
+                            <span class="oi oi-pencil" @onclick=@(async ()=> await OnClickEditCallback.InvokeAsync(Item)) />
                         </li>
                         <li>
-                            <span class="oi oi-check" @onclick=@(_=>DoneTodo(Item)) />
+                            <span class="oi oi-check" @onclick=@(async ()=> await OnClickDoneCallback.InvokeAsync(Item)) />
                         </li>
                         <li>
-                            <span class="oi oi-trash" @onclick=@(_=>RemoveTodo(Item)) />
+                            <span class="oi oi-trash" @onclick=@(async ()=> await OnClickRemoveCallback.InvokeAsync(Item)) />
                         </li>
                     </ul>
```
8. [TodoBox.razor]()を保存する。
9. **Todo.razor** を開き、表示部分をコンポーネント呼び出しに変更する。
```diff
     @foreach(var todo in todos.Where<TodoItem>(x=>!x.IsDone || showClosed).OrderBy<TodoItem, DateTime>(x=>x.TargetDate))
     {
+        <TodoBox Item=@todo 
+            OnClickEditCallback=@(item=>EditTodo(item))
+            OnClickDoneCallback=@(item=>DoneTodo(item))
+            OnClickRemoveCallback=@(item=>RemoveTodo(item))/>
-    <div class="section" style="background-color: @GetBackgroundColor(todo)">
-        <table>
-            <tr>
-                <td align="right" valign="top"><span class="oi oi-key"/></td>
-                <td>
-                    @todo.Id
-                    <ul class="icon">
-                        <li>
-                            <span class="oi oi-pencil" @onclick=@(_=>EditTodo(todo)) />
-                        </li>
-                        <li>
-                            <span class="oi oi-check" @onclick=@(_=>DoneTodo(todo)) />
-                        </li>
-                        <li>
-                            <span class="oi oi-trash" @onclick=@(_=>RemoveTodo(todo)) />
-                        </li>
-                    </ul>
-                </td>
-            </tr>
-            <tr>
-                <td align="right" valign="top"><span class="oi oi-tag"/></td>
-                <td>@todo.Title</td>
-            </tr>
-            <tr>
-                <td align="right" valign="top"><span class="oi oi-clock"/></td>
-                <td>
-                    @todo.TargetDate.ToString("yyyy/MM/dd")
-                    @if(todo.IsDone)
-                    {
-                        <span class="oi oi-check" style="margin-left: 10px;"/>
-                        @todo.EndDate.ToString("yyyy/MM/dd")
-                    }
-                    else
-                    {
-                        var days = (todo.TargetDate - DateTime.Today).Days;
-                        @if(days == 0)
-                        {
-                            <span style="color: red; margin-left: 10px;">今日まで！</span>
-                        }
-                        else if(days<0)
-                        {
-                            <span style="color: red; margin-left: 10px;">@(-days)日 オーバー</span>
-                        }
-                        else
-                        {
-                            <span style="color: black; margin-left: 10px;">あと @(days)日</span>                            
-                        }
-                    }
-                </td>
-            </tr>
-            <tr>
-                <td align="right" valign="top"><span class="oi oi-comment-square"/></td>
-                <td ><p class="ellipsis" title=@todo.Memo>@todo.Memo</p></td>
-            </tr>
-        </table>
-    </div>
     }
```
10. Todo.razorを保存する。
11. ソリューション エクスプローラの **Shared** フォルダを右クリックし、 コンテキストメニューの **追加 - クラス** をクリックする。
12. `スタイル シート`を選択する。  
![新しい項目の追加](../Images/modularization-2.png)
13. 名前を **TodoBox.razor.css** にして **追加** ボタンを押す。
14. SharedフォルダにTodoBox.razor.cssが追加される。  
![追加されたTodoBox.razor.css](../Images/modularization-3.png)
15. TodoBox.razor.cssを編集し、Todo.razor.cssの内容を移動する。    
```CSS
div.section {
    display: inline-block;
    vertical-align: top;
    background-color: lightskyblue;
    border-radius: 10px;
    margin: 10px;
    padding: 10px;
    box-sizing: border-box;
}

    div.section:hover {
        background-color: lightblue;
    }

    div.section p {
        clear: both;
    }

    div.section ul.icon {
        list-style: none;
        float: right;
        margin-bottom: 0px;
    }

        div.section ul.icon li {
            display: inline;
            margin-left: 5px;
            cursor: pointer;
        }

        div.section ul.icon li {
            opacity: 0;
        }

    div.section:hover ul.icon li {
        opacity: 0.5;
    }

    div.section ul.icon li:hover {
        opacity: 1;
    }

.ellipsis {
    max-width: 300px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    margin-bottom: 0px;
}
```
16. [TodoBox.razor.css]()を保存する。
### 永続化コードをサービス化する


code: ![tag](../Images/tag.png) [Step 9](https://github.com/04100149/TodoList/releases/tag/step9)  

## 動作確認
1. ![デバックの開始](../Images/NewProject-6.png) ボタンをクリックする。  
1. ビルド後、開発用コンテナが開始され、ブラウザが起動する。  
![コンテナ開始](../Images/decoration-3.png)
1. サイドメニューの **Todo** をクリックすると、Todoページが開く。    
![Todoページ](../Images/decoration-4.png)
1. `Closed`をクリックする。  
- 期限順に表示される。
- 状況ごとに色分けされる。
![すべて表示](../Images/decoration-5.png)
