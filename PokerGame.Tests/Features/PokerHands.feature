# language: en
Feature: Determine The Winning Hand

Background:
  Given The cards have the following rank
    | Rank | Card |
    | 0    | 2    |
    | 1    | 3    |
    | 2    | 4    |
    | 3    | 5    |
    | 4    | 6    |
    | 5    | 7    |
    | 6    | 8    |
    | 7    | 9    |
    | 8    | Ten  |
    | 9    | Jack |
    | 10   | Queen |
    | 11   | King |
    | 12   | Ace  |
@SimpleTests
Scenario: One Pair beats High Card
  Given I have the following hands
    | HandNo | Card1_Value | Card1_Suit | Card2_Value | Card2_Suit | Card3_Value | Card3_Suit | Card4_Value | Card4_Suit | Card5_Value | Card5_Suit |
    | 1      | Two          | Spade      | Two          | Heart      | Five         | Diamond    | Seven        | Club       | Nine         | Spade      |
    | 2      | Ace          | Spade      | King         | Heart      | Ten          | Diamond    | Eight        | Club       | Three        | Spade      |
  When I Score the Hands
  Then HandNo 1 should have a "One Pair with High Card Two"
  And HandNo 2 should have a "High Card with High Card Ace"
  And The result of the game should be "Winning Hand: 1 with One Pair with High Card Two"

@SimpleTests
Scenario: Three of a Kind beats all lower hands
  Given I have the following hands
    | HandNo | Card1_Value | Card1_Suit | Card2_Value | Card2_Suit | Card3_Value | Card3_Suit | Card4_Value | Card4_Suit | Card5_Value | Card5_Suit |
    | 1      | Four         | Spade      | Four         | Heart      | Four         | Diamond    | Nine         | Club       | King         | Spade      |
    | 2      | Eight        | Spade      | Eight        | Heart      | King         | Diamond    | King         | Club       | Two          | Spade      |
    | 3      | Ace          | Spade      | King         | Heart      | Queen        | Diamond    | Jack         | Club       | Nine         | Spade      |
    | 4      | Two          | Spade      | Two          | Heart      | Five         | Diamond    | Seven        | Club       | Nine         | Spade      |
    | 5      | Ten          | Spade      | Jack         | Heart      | Queen        | Diamond    | King         | Club       | Ace          | Spade      |
  When I Score the Hands
  Then HandNo 1 should have a "Three of a Kind with High Card Four"
  And HandNo 2 should have a "Two Pairs with High Card King"
  And HandNo 3 should have a "High Card with High Card Ace"
  And HandNo 4 should have a "One Pair with High Card Two"
  And HandNo 5 should have a "Straight with High Card Ace"
  And The result of the game should be "Winning Hand: 5 with Straight with High Card Ace"

@RankTests
Scenario: Player with Royal Flush should win the game
  Given I have the following hands
    | HandNo | Card1_Value | Card1_Suit | Card2_Value | Card2_Suit | Card3_Value | Card3_Suit | Card4_Value | Card4_Suit | Card5_Value | Card5_Suit |
    | 1      | Ten          | Heart      | Jack         | Heart      | Queen        | Heart      | King         | Heart      | Ace          | Heart      |
    | 2      | Two          | Heart      | Three        | Heart      | Four         | Heart      | Five         | Heart      | Six          | Heart      |
  When I Score the Hands
  Then HandNo 1 should have a "Royal Flush with High Card Ace"
  And HandNo 2 should have a "Straight Flush with High Card Six"
  And The result of the game should be "Winning Hand: 1 with Royal Flush with High Card Ace"

@RankTests
Scenario: Player with Four of a Kind should beat Full House
  Given I have the following hands
    | HandNo | Card1_Value | Card1_Suit | Card2_Value | Card2_Suit | Card3_Value | Card3_Suit | Card4_Value | Card4_Suit | Card5_Value | Card5_Suit |
    | 1      | Nine         | Spade      | Nine         | Heart      | Nine         | Diamond    | Nine         | Club       | King         | Spade      |
    | 2      | Eight        | Spade      | Eight        | Heart      | Eight        | Diamond    | Two          | Club       | Two          | Spade      |
  When I Score the Hands
  Then HandNo 1 should have a "Four of a Kind with High Card Nine"
  And HandNo 2 should have a "Full House with High Card Eight"
  And The result of the game should be "Winning Hand: 1 with Four of a Kind with High Card Nine"

@TieBreakerTests
Scenario: Tie-breaker with same rank and different high card
  Given I have the following hands
    | HandNo | Card1_Value | Card1_Suit | Card2_Value | Card2_Suit | Card3_Value | Card3_Suit | Card4_Value | Card4_Suit | Card5_Value | Card5_Suit |
    | 1      | Ten          | Spade      | Jack         | Heart      | Queen        | Diamond    | King         | Club       | Ace          | Spade      |
    | 2      | Nine         | Spade      | Ten          | Heart      | Jack         | Diamond    | Queen        | Club       | King         | Spade      |
  When I Score the Hands
  Then HandNo 1 should have a "Straight with High Card Ace"
  And HandNo 2 should have a "Straight with High Card King"
  And The result of the game should be "Winning Hand: 1 with Straight with High Card Ace"

@MultiPlayerTests
Scenario: Three players with different ranks
  Given I have the following hands
    | HandNo | Card1_Value | Card1_Suit | Card2_Value | Card2_Suit | Card3_Value | Card3_Suit | Card4_Value | Card4_Suit | Card5_Value | Card5_Suit |
    | 1      | Ten          | Spade      | Jack         | Heart      | Queen        | Diamond    | King         | Club       | Ace          | Spade      |
    | 2      | Two          | Spade      | Two          | Heart      | Five         | Diamond    | Seven        | Club       | Nine         | Spade      |
    | 3      | Eight        | Spade      | Eight        | Heart      | Eight        | Diamond    | King         | Club       | King         | Spade      |
  When I Score the Hands
  Then HandNo 1 should have a "Straight with High Card Ace"
  And HandNo 2 should have a "One Pair with High Card Two"
  And HandNo 3 should have a "Full House with High Card Eight"
  And The result of the game should be "Winning Hand: 3 with Full House with High Card Eight"

@MultiPlayerTests
Scenario: Royal Flush beats all other hands
  Given I have the following hands
    | HandNo | Card1_Value | Card1_Suit | Card2_Value | Card2_Suit | Card3_Value | Card3_Suit | Card4_Value | Card4_Suit | Card5_Value | Card5_Suit |
    | 1      | Ten          | Heart      | Jack         | Heart      | Queen        | Heart      | King         | Heart      | Ace          | Heart      |
    | 2      | Two          | Heart      | Three        | Heart      | Four         | Heart      | Five         | Heart      | Six          | Heart      |
    | 3      | Nine         | Spade      | Nine         | Heart      | Nine         | Diamond    | Nine         | Club       | King         | Spade      |
    | 4      | Eight        | Spade      | Eight        | Heart      | Eight        | Diamond    | King         | Club       | King         | Spade      |
  When I Score the Hands
  Then HandNo 1 should have a "Royal Flush with High Card Ace"
  And HandNo 2 should have a "Straight Flush with High Card Six"
  And HandNo 3 should have a "Four of a Kind with High Card Nine"
  And HandNo 4 should have a "Full House with High Card Eight"
  And The result of the game should be "Winning Hand: 1 with Royal Flush with High Card Ace"