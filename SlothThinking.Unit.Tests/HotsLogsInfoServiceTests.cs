using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RestSharp;

namespace SlothThinking.Unit.Tests
{
    [TestFixture]
    public class HotsLogsInfoServiceTests
    {
        private const string HEROES_LOUNGE_URL = "https://heroeslounge.gg/api/v1/";
        private const string HOTSLOGS_URL = "https://api.hotslogs.com/";


        [Test]
        public void ShouldAcquireNonZeroRatingSumForTeam()
        {
            var slothQueryService = new SlothQueryService(new RestClient(HEROES_LOUNGE_URL));
            var hotsLogsInfoService = new HotsLogsInfoService(new RestClient(HOTSLOGS_URL), new WeightedSlothRatingsCalculator() );

            var slothAggregationSevice = new SlothAggregationService(slothQueryService);
            var randomteam = slothAggregationSevice.Get(3).ConfigureAwait(false).GetAwaiter().GetResult().FirstOrDefault();
            
            Assert.That(randomteam, Is.Not.Null);

            var rating = hotsLogsInfoService.GetRatingFor(randomteam).ConfigureAwait(false).GetAwaiter().GetResult();
            
            Assert.That(rating, Is.GreaterThan(0));
        }

        #region spam
        //[Test]
        //public void Print()
        //{
        //    var slothQueryService = new SlothQueryService(new RestClient("https://heroeslounge.gg/api/v1/"));
        //    var hotsLogsInfoService = new HotsLogsInfoService(new RestClient(HOTSLOGS_URL), new WeightedSlothRatingsCalculator());

        //    var slothAggregationSevice = new SlothAggregationService(slothQueryService);
            
        //    for (var i = 1; i <= 5; i++)
        //    {
        //        var teams = slothAggregationSevice.Get(i).ConfigureAwait(false).GetAwaiter().GetResult();

        //        foreach (var team in teams)
        //        {
        //            var rating = hotsLogsInfoService.GetRatingFor(team).ConfigureAwait(false).GetAwaiter().GetResult(); ;
        //            Console.WriteLine($"{team.Title} : {i} : {rating} ");
        //        }
        //    }
            
        //}
        #endregion
    }
}