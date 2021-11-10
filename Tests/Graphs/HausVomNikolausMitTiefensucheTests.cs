using Xunit;

namespace AUD.Graphs.Tests
{
    public class HausVomNikolausMitTiefensucheTests
    {
        [Fact]
        public void HausVomNikolausMitTIefensuche_FindSolutions()
        {
            var hvn = new HausVomNikolausMitTiefensuche();
            var solutions = hvn.FindSolutions();

            Assert.Equal(88, solutions.Count);
        }
    }
}