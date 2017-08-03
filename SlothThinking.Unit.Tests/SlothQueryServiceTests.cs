using System.Linq;
using NUnit.Framework;
using RestSharp;

namespace SlothThinking.Unit.Tests
{
    [TestFixture, Category("Integration")]
    public class SlothQueryServiceTests
    {
        private const string HEROES_LOUNGE_URL = "https://heroeslounge.gg/api/v1/";
        #region spam
        //[Test]
        //public void Test()
        //{
        //    var slothService = new SlothQueryService(new RestClient("https://heroeslounge.gg/api/v1/"));

        //    for (var i = 1; i <= 5; i++)
        //    {
        //        var division3Teams = slothService.GetTeams(i).ConfigureAwait(false).GetAwaiter().GetResult();

        //        foreach (var team in division3Teams)
        //        {
        //            Console.WriteLine($"{team.Title} : {i} : {team.SlothRating} ");
        //        }
        //    }
        //}
        #endregion


        [TestCase(3)]
        public void GetTeamsReturnNotEmptyResponse(int division)
        {
            var slothService = new SlothQueryService(new RestClient(HEROES_LOUNGE_URL));
            var teams = slothService.GetTeams(division).ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.That(teams.Count(), Is.GreaterThan(0));
        }

        [TestCase(51)]
        public void GetPlayersReturnsNonEmptyResponse(int teamId)
        {
            var slothService = new SlothQueryService(new RestClient(HEROES_LOUNGE_URL));
            var players = slothService.GetPlayers(teamId).ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.That(players.Count(), Is.GreaterThan(0));
        }
    }
}
