using FluentAssertions;
using PokerGame.Application;
using PokerGame.Domain;
using PokerGame.Infrastructure.Evaluators;
using PokerGame.Infrastructure.Validators;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;

namespace PokerGame.Tests.StepDefinitions
{
    [Binding]
    public class PokerHandSteps
    {
        private readonly List<Hand> _hands = new();
        private readonly PokerGameEngine _engine;
        private Dictionary<int, HandScore> _scores = new();
        private Hand _winner;

        public PokerHandSteps()
        {
            var evaluator = new HandEvaluator();
            var validator = new HandValidator();
            _engine = new PokerGameEngine(evaluator, validator, NullLogger<PokerGameEngine>.Instance);
        }

        [Given(@"The cards have the following rank")]
        public void GivenTheCardsHaveTheFollowingRank(Table table)
        {
            // No logic needed — used for documentation only
        }

        [Given(@"I have the following hands")]
        public void GivenIHaveTheFollowingHands(Table table)
        {
            foreach (var row in table.Rows)
            {
                var cards = Enumerable.Range(1, 5).Select(i =>
                    new Card(
                        Enum.Parse<SuitValue>(row[$"Card{i}_Value"]),
                        Enum.Parse<Suit>(row[$"Card{i}_Suit"])
                    )
                );

                var hand = new Hand(int.Parse(row["HandNo"]), cards);
                _hands.Add(hand);
            }
        }

        [When(@"I Score the Hands")]
        public void WhenIScoreTheHands()
        {
            _scores = _engine.ScoreHands(_hands);
            _winner = _engine.DetermineWinner(_hands);
        }

        [Then(@"HandNo (\d+) should have a ""(.*)""")]
        public void ThenHandShouldHave(int handNo, string expectedDescription)
        {
            var actual = _scores[handNo].Description;
            actual.Should().Be(expectedDescription);
        }

        [Then(@"The result of the game should be ""(.*)""")]
        public void ThenTheResultShouldBe(string expectedResult)
        {
            var score = _scores[_winner.HandNo];
            var actual = $"Winning Hand: {_winner.HandNo} with {score.Description}";
            actual.Should().Be(expectedResult);
        }
    }
}