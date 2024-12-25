namespace WodItEasy.Web.Features
{
    using Application.Contracts;
    using Domain.Models.Workouts;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly IRepository<Workout> workouts;

        public WorkoutController(IRepository<Workout> workouts) => this.workouts = workouts;

        public ActionResult<Workout> Get() => this.Ok(this.workouts.All());
    }
}
