using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WampSharp.V2.Rpc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSockets.Api.Controllers
{

    public static class TodoCollection
    {
        static IList<Todo> todos = new List<Todo>
        {
            new Todo { title = "Arrive to meetup", completed = true }
        };

        public static IList<Todo> Get()
        {
            return todos;
        }
    }

    public class Todo
    {
        public Todo()
        {
            this.id = Guid.NewGuid();
        }

        public Guid id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }

    [Route("api/[controller]")]
    public class TodosController : Controller
    {

        static IList<Todo> todos;
        public TodosController()
        {
            todos = TodoCollection.Get();
        }

        // GET: api/values
        [HttpGet]
        [WampProcedure("todos.get")]
        public IEnumerable<Todo> Get()
        {
            return todos;
        }

        // POST api/values
        [HttpPost]
        [WampProcedure("todos.create")]
        public Todo Post([FromBody] Todo todo)
        {
            if (todo != null)
            {
                todos.Add(todo);
            }
            
            return todo;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [WampProcedure("todos.update")]
        public void Put(Guid id, [FromBody]Todo newTodo)
        {
            var todo = todos.Where(x => Guid.Equals(x.id, id)).FirstOrDefault();

            if (todo != null && newTodo != null) {
                todo.title = newTodo.title;
                todo.completed = newTodo.completed;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [WampProcedure("todos.delete")]
        public void Delete(Guid id)
        {
            var todo = todos.Where(x => x.id == id).FirstOrDefault();

            if (todo != null)
            {
                todos.Remove(todo);
            }
        }

        [HttpDelete]
        [WampProcedure("todos.clear")]
        public void ClearCompleted()
        {
            var todosToRemove = todos.Where(x => x.completed).ToList();

            foreach (var todo in todosToRemove)
            {
                todos.Remove(todo);
            }
        }
    }
}
