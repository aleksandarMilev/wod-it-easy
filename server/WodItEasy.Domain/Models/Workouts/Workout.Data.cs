namespace WodItEasy.Domain.Models.Workouts
{
    using Common.Domain.Models;

    public class WorkoutData : IInitialData
    {
        public Type EntityType
            => typeof(Workout);

        public IEnumerable<object> GetData()
            => new List<Workout>{
                new(
                    "Daily Strength",
                    "https://biolayne.com/app/uploads/2024/10/image2-1080x1080.jpeg",
                    "A powerlifting-inspired workout designed to improve raw strength through foundational lifts such as the squat, bench press, and deadlift.",
                    10,
                    DateTime.UtcNow.AddDays(1).Date.AddHours(8),
                    WorkoutType.Powerlifting
                ),

                new(
                    "Daily Gymnastics",
                    "https://blog.thewodlife.com.au/wp-content/uploads/2019/10/handstand-walks.jpg",
                    "A gymnastics-focused session that emphasizes fundamental bodyweight movements such as pull-ups, muscle-ups, and handstands.",
                    10,
                    DateTime.UtcNow.AddDays(1).Date.AddHours(9),
                    WorkoutType.Gymnastic
                ),

                new(
                    "Murph",
                    "https://wodwell.com/wp-content/uploads/share_images/236/square-murph.jpg?v=2022-12-07-01-52",
                    "The Murph is one of the most well-known WODs. It was the favorite workout of Navy Lieutenant Michael Murphy, " +
                    "who was killed in Afghanistan on June 28, 2005, at the young age of 29. The workout consists of a one-mile run, 100 pull-ups, " +
                    "200 push-ups, 300 air squats, followed by another one-mile run, all performed while wearing a weighted vest.",
                    15,
                    DateTime.UtcNow.AddDays(1).Date.AddHours(16).AddMinutes(30),
                    WorkoutType.CrossFit
                ),

                new(
                    "Daily Mobility",
                    "https://simplifaster.com/wp-content/uploads/2023/07/Olympic-Weightlifting-Mobility.jpg",
                    "A session focused on improving flexibility and mobility, essential for all strength sports. " +
                    "Includes stretching, dynamic movements, and joint mobility exercises.",
                    10,
                    DateTime.UtcNow.AddDays(1).Date.AddHours(17).AddMinutes(30),
                    WorkoutType.Mobility
                ),

                new(
                    "Daily Cardiovascular",
                    "https://www.shutterstock.com/image-vector/triathlon-activity-icons-swimming-running-260nw-1292174215.jpg",
                    "A cardio-focused session designed to enhance endurance and stamina. Includes running, biking, rowing, and other aerobic exercises.",
                    10,
                    DateTime.UtcNow.AddDays(1).Date.AddHours(18).AddMinutes(30),
                    WorkoutType.Cardiovascular
                ),

                new(
                    "Daily Weightlifting",
                    "https://characterstrength.co.uk/wp-content/uploads/2023/05/4faba-ea496f_e006b4d6fa314a2cbc9faf1f84b7c74fmv2-904x640.jpg",
                    "A weightlifting session focused on the snatch and clean and jerk movements.",
                    10,
                    DateTime.UtcNow.AddDays(1).Date.AddHours(19).AddMinutes(30),
                    WorkoutType.Weightlifting
                )
            };
    }
}