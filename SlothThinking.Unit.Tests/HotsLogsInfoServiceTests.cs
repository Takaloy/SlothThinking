using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using RestSharp;

namespace SlothThinking.Unit.Tests
{
    [TestFixture, Category("Integration")]
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
            
            Assert.That(rating.WeightedRating, Is.GreaterThan(0));
        }

        #region spam
        [Test, Ignore("run this if you want to output the result. this is usually slow")]
        public void Print()
        {
            var slothQueryService = new SlothQueryService(new RestClient(HEROES_LOUNGE_URL));
            var hotsLogsInfoService = new HotsLogsInfoService(new RestClient(HOTSLOGS_URL), new WeightedSlothRatingsCalculator());

            var slothAggregationSevice = new SlothAggregationService(slothQueryService);

            var results = new List<string>();
            try
            {
                for (var i = 1; i <= 5; i++)
                {
                    var teams = slothAggregationSevice.Get(i).ConfigureAwait(false).GetAwaiter().GetResult();

                    results.AddRange(from team in teams
                        let rating = hotsLogsInfoService.GetRatingFor(team)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult()
                        select $"{team.Title},{i},{rating.WeightedRating},{rating.PlayersRated}");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"exception : {exception.Message}");
            }

            System.IO.File.WriteAllLines($"{AppDomain.CurrentDomain.BaseDirectory}\\Print.csv", results);
        }
        #endregion
    }
}