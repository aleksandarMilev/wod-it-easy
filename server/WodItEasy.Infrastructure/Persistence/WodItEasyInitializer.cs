namespace WodItEasy.Infrastructure.Persistence
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;

    internal class WodItEasyDbInitializer : IInitializer
    {
        private readonly WodItEasyDbContext data;

        public WodItEasyDbInitializer(WodItEasyDbContext data) 
            => this.data = data;

        public void Initialize() 
            => this.data.Database.Migrate();
    }
}
