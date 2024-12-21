namespace WodItEasy.Web.Features
{
    using System;
    using Domain.Models.Workouts;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private static readonly Workout Workout = new("Name", "Description", 15, DateTime.Now.AddDays(1), TimeSpan.FromMinutes(60), WorkoutType.CrossFit);

        public ActionResult<Workout> Get() => this.Ok(Workout);
    }
}
