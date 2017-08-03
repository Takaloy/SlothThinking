using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SlothThinking.Unit.Tests
{
    [TestFixture]
    public class SlothRatingsCalculatorTests
    {
        [TestCase(100,200,300, 170)]
        [TestCase(500,500,500, 500)]
        public void GivenASloth_WithAllModesAvailable_ShouldCalculateCorrectly(int heroLeagueRating, int teamLeagueRating, 
            int unrankedRating, int expectation)
        {
            var calculator = new WeightedSlothRatingsCalculator();
            var profile = new HotsLogsPlayerProfile
            {
                LeaderboardRankings = new List<LeaderboardRanking>
                {
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.HeroLeague,
                        CurrentMMR = heroLeagueRating,
                        LeagueRank = heroLeagueRating
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.TeamLeague,
                        CurrentMMR = teamLeagueRating,
                        LeagueRank = teamLeagueRating
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.UnrankedDraft,
                        CurrentMMR = unrankedRating,
                        LeagueRank = unrankedRating
                    },
                }
            };

            var result = calculator.GetRatingFor(profile);
            Assert.That(result, Is.EqualTo(expectation));
        }

        [TestCase(100, 200, 300, 240)]
        [TestCase(500, 500, 500, 500)]
        public void GivenASloth_WithNoHeroLeague_ShouldCalculateCorrectly(int heroLeagueRating, int teamLeagueRating,
            int unrankedRating, int expectation)
        {
            var calculator = new WeightedSlothRatingsCalculator();
            var profile = new HotsLogsPlayerProfile
            {
                LeaderboardRankings = new List<LeaderboardRanking>
                {
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.HeroLeague,
                        CurrentMMR = heroLeagueRating,
                        LeagueRank = null
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.TeamLeague,
                        CurrentMMR = teamLeagueRating,
                        LeagueRank = teamLeagueRating
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.UnrankedDraft,
                        CurrentMMR = unrankedRating,
                        LeagueRank = unrankedRating
                    },
                }
            };

            var result = calculator.GetRatingFor(profile);
            Assert.That(result, Is.EqualTo(expectation));
        }

        [TestCase(100, 200, 300, 157)]
        [TestCase(500, 500, 500, 500)]
        public void GivenASloth_WithNoTeamLeague_ShouldCalculateCorrectly(int heroLeagueRating, int teamLeagueRating,
            int unrankedRating, int expectation)
        {
            var calculator = new WeightedSlothRatingsCalculator();
            var profile = new HotsLogsPlayerProfile
            {
                LeaderboardRankings = new List<LeaderboardRanking>
                {
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.HeroLeague,
                        CurrentMMR = heroLeagueRating,
                        LeagueRank = heroLeagueRating
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.TeamLeague,
                        CurrentMMR = teamLeagueRating,
                        LeagueRank = null
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.UnrankedDraft,
                        CurrentMMR = unrankedRating,
                        LeagueRank = unrankedRating
                    },
                }
            };

            var result = calculator.GetRatingFor(profile);
            Assert.That(result, Is.EqualTo(expectation));
        }


        [TestCase(100, 200, 300, 137)]
        [TestCase(500, 500, 500, 500)]
        public void GivenASloth_WithNoUnranked_ShouldCalculateCorrectly(int heroLeagueRating, int teamLeagueRating,
            int unrankedRating, int expectation)
        {
            var calculator = new WeightedSlothRatingsCalculator();
            var profile = new HotsLogsPlayerProfile
            {
                LeaderboardRankings = new List<LeaderboardRanking>
                {
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.HeroLeague,
                        CurrentMMR = heroLeagueRating,
                        LeagueRank = heroLeagueRating
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.TeamLeague,
                        CurrentMMR = teamLeagueRating,
                        LeagueRank = teamLeagueRating
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.UnrankedDraft,
                        CurrentMMR = unrankedRating,
                        LeagueRank = null
                    },
                }
            };

            var result = calculator.GetRatingFor(profile);
            Assert.That(result, Is.EqualTo(expectation));
        }

        [TestCase(100, 200, 300, 100)]
        [TestCase(500, 500, 500, 500)]
        public void GivenASloth_WithOnlyHeroLeague_ShouldCalculateCorrectly(int heroLeagueRating, int teamLeagueRating,
            int unrankedRating, int expectation)
        {
            var calculator = new WeightedSlothRatingsCalculator();
            var profile = new HotsLogsPlayerProfile
            {
                LeaderboardRankings = new List<LeaderboardRanking>
                {
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.HeroLeague,
                        CurrentMMR = heroLeagueRating,
                        LeagueRank = heroLeagueRating
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.TeamLeague,
                        CurrentMMR = teamLeagueRating,
                        LeagueRank = null
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.UnrankedDraft,
                        CurrentMMR = unrankedRating,
                        LeagueRank = null
                    },
                }
            };

            var result = calculator.GetRatingFor(profile);
            Assert.That(result, Is.EqualTo(expectation));
        }

        [TestCase(100, 200, 300, 200)]
        [TestCase(500, 500, 500, 500)]
        public void GivenASloth_WithOnlyTeamLeague_ShouldCalculateCorrectly(int heroLeagueRating, int teamLeagueRating,
            int unrankedRating, int expectation)
        {
            var calculator = new WeightedSlothRatingsCalculator();
            var profile = new HotsLogsPlayerProfile
            {
                LeaderboardRankings = new List<LeaderboardRanking>
                {
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.HeroLeague,
                        CurrentMMR = heroLeagueRating,
                        LeagueRank = null
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.TeamLeague,
                        CurrentMMR = teamLeagueRating,
                        LeagueRank = teamLeagueRating
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.UnrankedDraft,
                        CurrentMMR = unrankedRating,
                        LeagueRank = null
                    },
                }
            };

            var result = calculator.GetRatingFor(profile);
            Assert.That(result, Is.EqualTo(expectation));
        }

        [TestCase(100, 200, 300, 300)]
        [TestCase(500, 500, 500, 500)]
        public void GivenASloth_WithOnlyUnranked_ShouldCalculateCorrectly(int heroLeagueRating, int teamLeagueRating,
            int unrankedRating, int expectation)
        {
            var calculator = new WeightedSlothRatingsCalculator();
            var profile = new HotsLogsPlayerProfile
            {
                LeaderboardRankings = new List<LeaderboardRanking>
                {
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.HeroLeague,
                        CurrentMMR = heroLeagueRating,
                        LeagueRank = null
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.TeamLeague,
                        CurrentMMR = teamLeagueRating,
                        LeagueRank = null
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.UnrankedDraft,
                        CurrentMMR = unrankedRating,
                        LeagueRank = unrankedRating
                    },
                }
            };

            var result = calculator.GetRatingFor(profile);
            Assert.That(result, Is.EqualTo(expectation));
        }


        [TestCase(100, 200, 300, 0)]
        [TestCase(500, 500, 500, 0)]
        public void GivenASloth_WithNoRelevantStats_ShouldCalculateCorrectly(int heroLeagueRating, int teamLeagueRating,
            int unrankedRating, int expectation)
        {
            var calculator = new WeightedSlothRatingsCalculator();
            var profile = new HotsLogsPlayerProfile
            {
                LeaderboardRankings = new List<LeaderboardRanking>
                {
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.HeroLeague,
                        CurrentMMR = heroLeagueRating,
                        LeagueRank = null
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.TeamLeague,
                        CurrentMMR = teamLeagueRating,
                        LeagueRank = null
                    },
                    new LeaderboardRanking
                    {
                        GameMode = GameMode.UnrankedDraft,
                        CurrentMMR = unrankedRating,
                        LeagueRank = null
                    },
                }
            };

            var result = calculator.GetRatingFor(profile);
            Assert.That(result, Is.EqualTo(expectation));
        }
    }
}
