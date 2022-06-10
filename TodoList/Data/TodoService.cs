using System.Text;
using System.Text.Json;

namespace TodoList.Data
{
    public class TodoService
    {
        public event EventHandler TodoChanged = delegate { };
        private int latestId;
        public TodoService()
        {
            lock (this)
            {
                List<TodoItem> todos = LoadTodoFiles();
                latestId = todos.Select<TodoItem, int>(x => x.Id).DefaultIfEmpty().Max() + 1;
            }
        }
        public void AddTodo(TodoItem todo)
        {
            lock (this)
            {
                todo.Id = latestId++;
                SaveTodoFile(todo);
                TodoChanged(this, EventArgs.Empty);
            }
        }
        public void SaveTodo(TodoItem todo)
        {
            lock (this)
            {
                SaveTodoFile(todo);
                TodoChanged(this, EventArgs.Empty);
            }
        }
        public List<TodoItem> LoadTodos()
        {
            lock (this)
            {
                return LoadTodoFiles();
            }
        }
        public void RemoveTodo(TodoItem todo)
        {
            lock (this)
            {
                RemoveTodoFile(todo.Id);
                TodoChanged(this, EventArgs.Empty);
            }
        }

        #region 永続化
        private const string todoFolder = @"./wwwroot/todos";

        private string GetPath(int id)
        {
            return string.Format(@"{0}/{1}.json", todoFolder, id);
        }

        private void SaveTodoFile(TodoItem todo)
        {
            string json = JsonSerializer.Serialize(todo);
            string path = GetPath(todo.Id);
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(json);
            }
        }

        private List<TodoItem> LoadTodoFiles()
        {
            List<TodoItem> todos = new List<TodoItem>();
            foreach (var path in Directory.EnumerateFiles(todoFolder))
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    string json = sr.ReadToEnd();
                    TodoItem? todo = JsonSerializer.Deserialize<TodoItem>(json);
                    if (todo != null)
                    {
                        todos.Add(todo);
                    }
                }
            }
            return todos;
        }

        private void RemoveTodoFile(int id)
        {
            string path = GetPath(id);
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        #endregion 永続化
    }
}
