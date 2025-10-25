PokerGame Evaluator API
========================

A clean, extensible .NET 8 solution for evaluating poker hands, determining winners, and supporting test-driven development with Gherkin scenarios via Reqnroll.

Features
-----------
- Score multiple poker hands with full rank detection
- Determine winning hand with tie-breaker and kicker logic
- Supports all standard poker ranks (Royal Flush → High Card)
- API endpoints for scoring and winner detection
- Domain-driven design with clean separation of concerns
- Unit tests and Gherkin-style feature coverage

Technologies
---------------
- .NET 8
- xUnit + FluentAssertions
- Gherkin-style feature testing via Reqnroll
- Dependency Injection
- ILogger for structured logging
- Interactive Swagger UI with auto-generated OpenAPI spec

Solution Structure
---------------------

| Project             | Purpose                                      |
|---------------------|----------------------------------------------|
| `PokerGame`         | Core logic: domain models, services, evaluators |
| ├─ `Domain`         | Models like `Card`, `Hand`, `HandRank`       |
| ├─ `Application`    | Interfaces and orchestration logic           |
| ├─ `Infrastructure` | Comparers, Evaluators, Validators            |
| `PokerGame.Api`     | ASP.NET Core Web API                         |
| ├─ `Controllers`    | API endpoints (`PokerController`)            |
| ├─ `Models`         | Request/Response DTOs                        |
| ├─ `Mappers`        | DTO ↔ Domain conversion                      |
| `PokerGame.Tests`   | Unit + BDD test coverage                     |
| ├─ `UnitTests`      | Component-level tests                        |
| ├─ `Features`       | Gherkin scenarios (`PokerHands.feature`)     |
| ├─ `StepDefinitions`| BDD glue code (`PokerHandSteps`)             |


API Endpoints
----------------

POST /api/poker/score
---------------------
Returns scores for all submitted hands.

**Sample Request:**
[
  {
    "handNo": 1,
    "cards": [
      { "value": "Ten", "suit": "Heart" },
      { "value": "Jack", "suit": "Heart" },
      { "value": "Queen", "suit": "Heart" },
      { "value": "King", "suit": "Heart" },
      { "value": "Ace", "suit": "Heart" }
    ]
  },
  {
    "handNo": 2,
    "cards": [
      { "value": "Nine", "suit": "Spade" },
      { "value": "Nine", "suit": "Club" },
      { "value": "Nine", "suit": "Diamond" },
      { "value": "Nine", "suit": "Heart" },
      { "value": "King", "suit": "Spade" }
    ]
  }
]

**Sample Response:**
[
  { "handNo": 1, "score": "Royal Flush" },
  { "handNo": 2, "score": "Four of a Kind" }
]

POST /api/poker/winner
-----------------------
Returns the winning hand and its score.

**Sample Request:**
[
  {
    "handNo": 1,
    "cards": [
      { "value": "Ten", "suit": "Heart" },
      { "value": "Jack", "suit": "Heart" },
      { "value": "Queen", "suit": "Heart" },
      { "value": "King", "suit": "Heart" },
      { "value": "Ace", "suit": "Heart" }
    ]
  },
  {
    "handNo": 2,
    "cards": [
      { "value": "Nine", "suit": "Spade" },
      { "value": "Nine", "suit": "Club" },
      { "value": "Nine", "suit": "Diamond" },
      { "value": "Nine", "suit": "Heart" },
      { "value": "King", "suit": "Spade" }
    ]
  }
]

**Sample Response:**
{
  "winner": 1,
  "score": "Royal Flush"
}

Running Tests
----------------
dotnet test

Includes:
- HandEvaluatorTests for rank detection
- HandComparerTests for tie-breakers
- PokerGameEngineTests for scoring and winner logic
- Gherkin feature tests for end-to-end scenarios


Getting Started
------------------
1. Clone the repo:
   git clone https://github.com/your-username/PokerGame.git

2. Open PokerGame.sln in Visual Studio

3. Run the API project and test endpoints via Swagger or Postman

Future Enhancements
----------------------
- Multiplayer draw detection
- Backend and Frontend additions
- Audit logs 
- Authentication
- CI/CD pipeline with GitHub Actions
