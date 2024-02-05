using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class YourController : ControllerBase
{
    // GET api/yourcontroller
    [HttpGet]
    public ActionResult<IEnumerable<Task>> Get()
    {
        // Your logic to retrieve and return data
        return Ok(/* Your data */);
    }

    // GET api/yourcontroller/{id}
    [HttpGet("{id}")]
    public ActionResult<Task> Get(int id)
    {
        // Your logic to retrieve a specific record and return it
        return Ok(/* Your data */);
    }

    // POST api/yourcontroller
    [HttpPost]
    public ActionResult<Task> Post([FromBody] Task model)
    {
        // Your logic to create a new record
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    // PUT api/yourcontroller/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Task model)
    {
        // Your logic to update a record
        return NoContent();
    }

    // DELETE api/yourcontroller/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // Your logic to delete a record
        return NoContent();
    }
}