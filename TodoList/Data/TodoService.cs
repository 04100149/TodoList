using System.Text;
using System.Text.Json;

namespace TodoList.Data
{
    public class TodoService
    {
        public void SaveTodo(TodoItem todo)
        {
            SaveTodoFile(todo);
        }
        public List<TodoItem> LoadTodos()
        {
            return LoadTodoFiles();
        }
        public void RemoveTodo(TodoItem todo)
        {
            RemoveTodoFile(todo.Id);
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
