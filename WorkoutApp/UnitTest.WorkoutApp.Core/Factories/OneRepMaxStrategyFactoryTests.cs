using WorkoutApp.Core.Constants;
using WorkoutApp.Core.Factories;
using WorkoutApp.Core.Library;

namespace UnitTest.WorkoutApp.Core.Factories
{
    public class OneRepMaxStrategyFactoryTests
    {
        private static OneRepMaxStrategyFactory GetFactory()
        {
            return new OneRepMaxStrategyFactory();
        }

        [Theory]
        [InlineData(OneRepMaxStrategy.Brzycki)]
        [InlineData(OneRepMaxStrategy.Epley)]
        [InlineData(OneRepMaxStrategy.Landers)]
        public void Create_ValidStrategy_True(OneRepMaxStrategy strategy)
        {
            var factory = GetFactory();
            var createResult = factory.Create(strategy);
            Assert.True(createResult.IsSuccess, createResult.ErrorMessage());
        }
    }
}
