using Microsoft.AspNetCore.Mvc;

namespace ng_todo_server.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private static readonly List<Task> Tasks = new List<Task>();

    // GET: api/task
    [HttpGet]
    public ActionResult<IEnumerable<Task>> GetTasks()
    {
        return Tasks;
    }

    // POST: api/task
    [HttpPost]
    public ActionResult<Task> AddTask(Task task)
    {
        task.Id = System.Guid.NewGuid().ToString();
        Tasks.Add(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    // DELETE: api/task/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteTask(string id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }

        Tasks.Remove(task);
        return NoContent();
    }

    // PUT: api/task/{id}/done
    [HttpPut("{id}/done")]
    public ActionResult MarkTaskAsDone(string id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        task.IsDone = !task.IsDone;
        return NoContent();
    }

    // Helper method to retrieve a task by ID
    public ActionResult<Task> GetTaskById(string id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }

        return task;
    }
}
