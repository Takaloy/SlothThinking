using System;
using NSubstitute;
using NUnit.Framework;

namespace SlothThinking.Unit.Tests
{
    [TestFixture]
    public class SlothAggregationServiceTests
    {
        [TestCase(0)]
        [TestCase(6)]
        public void Get_ShouldNotAcceptZeroDivision(int division)
        {
            var slothQueryService = Substitute.For<ISlothQueryService>();
            var slothAggregationService = new SlothAggregationService(slothQueryService);
            Assert.Throws<ArgumentException>(() => slothAggregationService.Get(0).ConfigureAwait(false).GetAwaiter().GetResult());
        }

        [Test]
        public void Get_CallsSlothQueryService()
        {
            var slothQueryService = Substitute.For<ISlothQueryService>();
            var sut = new SlothAggregationService(slothQueryService);

            var result = sut.Get(3).ConfigureAwait(false).GetAwaiter().GetResult();
            slothQueryService.Received(1).GetTeams(3);
        }
    }
}
