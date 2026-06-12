using Bogus;
using FakeApi.Domain;

namespace FakeApi.Data;

public static class ArticleSeeder
{
    private const int FixedSeed = 20260611;
    private static readonly DateTimeOffset BaseInstant =
        new(2026, 6, 11, 9, 0, 0, TimeSpan.Zero);

    public static IReadOnlyList<Article> Seed()
    {
        Randomizer.Seed = new Random(FixedSeed);
        var articles = new List<Article>();
        var globalIndex = 0;
        foreach (var category in Category.All)
        {
            var faker = new Faker { Random = new Randomizer(FixedSeed) };
            for (var i = 0; i < 6; i++)
            {
                var headline = HeadlinePool.Next(faker, category);
                articles.Add(new Article
                {
                    Id = $"{category.ToLowerInvariant()}-{i:000}",
                    Headline = headline,
                    Dek = DekPool.Next(faker, category),
                    Category = category,
                    Author = faker.Name.FullName(),
                    AuthorRole = RolePool.Next(faker, category),
                    PublishedAt = BaseInstant - TimeSpan.FromHours(globalIndex * 3),
                    ReadMinutes = faker.Random.Int(3, 9),
                    BodyParagraphs = BodyPool.Next(faker, headline),
                    PullQuote = PullQuotePool.Next(faker),
                    PullQuoteAttribution = $"{faker.Name.FullName()}, {RolePool.Next(faker, category)}"
                });
                globalIndex++;
            }
        }
        return articles;
    }
}

internal static class HeadlinePool
{
    private static readonly Dictionary<string, string[]> Templates = new()
    {
        ["World"] = new[]
        {
            "World Leaders Convene for Emergency Summit on {0} Crisis",
            "UN Security Council Votes on Resolution Addressing {0} Tensions",
            "Diplomatic Channels Open as {0} Conflict Enters New Phase",
            "Global Coalition Forms to Combat Rising {0} Instability",
            "Foreign Ministers Reach Landmark Deal Over {0} Dispute",
            "Humanitarian Aid Reaches {0} Region Amid Ongoing Unrest",
            "Peace Talks Stall as {0} Factions Refuse Ceasefire Terms",
            "New Intelligence Report Reveals Depth of {0} Security Threat",
        },
        ["Technology"] = new[]
        {
            "Tech Giants Race to Deploy Next-Generation {0} Models",
            "Startup Raises $2.4B to Revolutionize {0} Infrastructure",
            "Researchers Achieve Breakthrough in {0} Processing Speed",
            "Regulators Propose New Framework for Governing {0} Systems",
            "Open-Source Community Challenges Corporate Control of {0}",
            "Security Flaw Exposes Millions of {0} Users to Data Breach",
            "Silicon Valley Bets Big on {0} as Next Computing Platform",
            "Study Links Excessive {0} Use to Declining Cognitive Scores",
        },
        ["Business"] = new[]
        {
            "Quarterly Earnings Disappoint as {0} Sector Faces Headwinds",
            "Merger Creates Industry Giant With $800B {0} Footprint",
            "Central Bank Signals Rate Pause Amid {0} Market Volatility",
            "Retail Chains Shutter Locations Following {0} Spending Slump",
            "Private Equity Firms Eye {0} Assets in Distressed Buyout Wave",
            "Supply Chain Disruptions Push {0} Prices to Record High",
            "IPO Market Reopens as Investors Return to {0} Sector",
            "Activist Investor Demands Board Overhaul at {0} Conglomerate",
        },
        ["Sports"] = new[]
        {
            "Championship Finals Draw Record Viewership as {0} Dominates",
            "Veteran Star Announces Retirement After {0} Title Run",
            "Coach Fired After {0} Season Ends in Third Straight Defeat",
            "Young Phenom Breaks {0} Record Set Forty Years Ago",
            "Club Signs Marquee Forward in {0}-Era Defining Transfer Deal",
            "Officials Under Fire as Controversial {0} Call Sparks Debate",
            "Underdog Squad Upsets Favorites in {0} Tournament Opener",
            "League Suspends Player Following {0} Altercation Investigation",
        },
        ["Entertainment"] = new[]
        {
            "Awards Season Upended as {0} Film Sweeps Major Categories",
            "Streaming Wars Intensify as {0} Platform Tops 200M Subscribers",
            "Beloved Director Returns with Ambitious {0} Adaptation",
            "Pop Icon Announces World Tour Behind Chart-Topping {0} Album",
            "Studio Bets $300M on Franchise Revival of Classic {0} IP",
            "Critics Divided as Polarizing {0} Series Drops Final Season",
            "Rising Star Signs Record Deal After Viral {0} Performance",
            "Nostalgia Wave Drives Demand for {0} Reboots and Reunions",
        },
        ["Science"] = new[]
        {
            "Astronomers Detect Unusual Signal Consistent With {0} Origin",
            "Clinical Trial Confirms Efficacy of Novel {0} Treatment",
            "Geologists Warn of Increased {0} Activity Along Major Fault",
            "Gene-Editing Tool Offers Hope for Patients With {0} Disorder",
            "Ocean Buoy Network Reveals Alarming Shifts in {0} Patterns",
            "Particle Accelerator Data Points to Undiscovered {0} Particle",
            "Epidemiologists Track Rapid Spread of New {0} Variant",
            "Fossil Discovery Rewrites Timeline of Early {0} Evolution",
        },
    };

    private static readonly Dictionary<string, string[]> Subjects = new()
    {
        ["World"] = new[] { "Regional", "Border", "Climate", "Economic", "Political", "Migration", "Energy", "Trade" },
        ["Technology"] = new[] { "AI", "Quantum", "Cloud", "Chip", "Blockchain", "Neural", "Edge Computing", "Robotics" },
        ["Business"] = new[] { "Consumer", "Financial", "Industrial", "Healthcare", "Energy", "Real Estate", "Tech", "Logistics" },
        ["Sports"] = new[] { "Premier League", "Championship", "Olympic", "Cup", "Playoff", "Draft", "Transfer", "Season" },
        ["Entertainment"] = new[] { "Blockbuster", "Indie", "Streaming", "Award-Winning", "Cult", "Documentary", "Animated", "Live-Action" },
        ["Science"] = new[] { "Cosmic", "Molecular", "Seismic", "Genetic", "Oceanic", "Atmospheric", "Viral", "Fossil" },
    };

    public static string Next(Faker faker, string category)
    {
        var templates = Templates.GetValueOrDefault(category, Templates["World"]);
        var subjects = Subjects.GetValueOrDefault(category, Subjects["World"]);
        var template = faker.PickRandom(templates);
        var subject = faker.PickRandom(subjects);
        return string.Format(template, subject);
    }
}

internal static class DekPool
{
    private static readonly Dictionary<string, string[]> Deks = new()
    {
        ["World"] = new[]
        {
            "Analysts say the situation could reshape alliances that have held for decades.",
            "The agreement marks the first substantive progress after eighteen months of stalled negotiations.",
            "Aid organizations warn that civilian populations face acute shortages of food and medicine.",
            "Senior officials declined to comment on the intelligence underlying the new assessment.",
            "A fragile ceasefire has held for 72 hours, but observers remain cautious.",
            "The vote passed by a narrow margin, reflecting deep divisions within the council.",
        },
        ["Technology"] = new[]
        {
            "The announcement accelerates a race that observers say is still far from settled.",
            "Engineers say the new architecture eliminates a fundamental bottleneck in current designs.",
            "Privacy advocates warn the rollout outpaces existing regulatory guardrails.",
            "The vulnerability affects an estimated 340 million devices running older firmware versions.",
            "Backers argue the open approach will outpace proprietary alternatives within three years.",
            "Researchers caution that benchmark results may not reflect real-world deployment conditions.",
        },
        ["Business"] = new[]
        {
            "Analysts had projected a more modest decline, making the miss particularly damaging.",
            "The combined entity will control roughly a third of the global market.",
            "Consumer confidence surveys suggest demand may not recover until well into next year.",
            "Restructuring costs are expected to weigh on margins through at least the next two quarters.",
            "Investors cheered the news, pushing the combined share price up seven percent.",
            "The deal still faces scrutiny from antitrust regulators in three jurisdictions.",
        },
        ["Sports"] = new[]
        {
            "The performance silenced critics who had written off the club after a dismal start.",
            "Sources close to the negotiations say a final decision is expected within 48 hours.",
            "The record had stood since 1984 and was widely considered unbreakable.",
            "Fans reacted with a mixture of shock and nostalgia as the announcement broke.",
            "Officiating controversies have overshadowed an otherwise compelling tournament.",
            "Team management declined to reveal details of the investigation.",
        },
        ["Entertainment"] = new[]
        {
            "Early tracking suggests an opening weekend figure that would rank among the all-time top ten.",
            "The project reunites a creative partnership last seen on a critically acclaimed 2019 release.",
            "Subscriber data obtained by analysts points to a significant bump following the premiere.",
            "Critics called the performance a career-defining turn for the lead actor.",
            "The announcement sent shock waves through an industry still adjusting to post-pandemic economics.",
            "Insiders say the deal was years in the making and nearly fell apart twice.",
        },
        ["Science"] = new[]
        {
            "Peer reviewers praised the methodology while noting the sample size warrants follow-up studies.",
            "The findings overturn a consensus that has guided research in the field for over two decades.",
            "Funding agencies are being asked to prioritize replication studies before clinical applications proceed.",
            "The team used a novel imaging technique that resolves structures at sub-nanometer scale.",
            "If confirmed, the discovery would be among the most significant in the past fifty years.",
            "Authorities are monitoring the development but say there is no immediate public health concern.",
        },
    };

    public static string Next(Faker faker, string category)
    {
        var pool = Deks.GetValueOrDefault(category, Deks["World"]);
        return faker.PickRandom(pool);
    }
}

internal static class RolePool
{
    private static readonly Dictionary<string, string[]> Roles = new()
    {
        ["World"] = new[] { "Foreign Affairs Correspondent", "Senior Diplomatic Editor", "International Bureau Chief", "Security Analyst", "UN Correspondent" },
        ["Technology"] = new[] { "Technology Reporter", "Senior Tech Editor", "Silicon Valley Correspondent", "Cybersecurity Analyst", "AI & Robotics Writer" },
        ["Business"] = new[] { "Markets Correspondent", "Business Editor", "Economics Reporter", "Financial Analyst", "M&A Desk Writer" },
        ["Sports"] = new[] { "Sports Correspondent", "Senior Sports Editor", "Athletics Reporter", "Transfer Desk Writer", "Match Analyst" },
        ["Entertainment"] = new[] { "Entertainment Reporter", "Culture Critic", "Film & TV Editor", "Music Correspondent", "Awards Season Writer" },
        ["Science"] = new[] { "Science Reporter", "Health & Medicine Editor", "Environment Correspondent", "Research Desk Writer", "Space & Astronomy Writer" },
    };

    public static string Next(Faker faker, string category)
    {
        var pool = Roles.GetValueOrDefault(category, Roles["World"]);
        return faker.PickRandom(pool);
    }
}

internal static class BodyPool
{
    private static readonly string[] OpeningTemplates = new[]
    {
        "The development came as a surprise to many observers who had expected a more measured response from the parties involved.",
        "For months the situation had been building toward an inflection point, and this week it finally arrived.",
        "Officials speaking on condition of anonymity described the internal deliberations as unusually tense.",
        "What began as a minor disagreement has escalated into one of the defining stories of the year.",
        "Experts had warned for years that conditions were ripe for exactly this kind of disruption.",
        "The announcement followed weeks of behind-the-scenes negotiations that nearly collapsed on multiple occasions.",
    };

    private static readonly string[] MiddleTemplates = new[]
    {
        "A senior figure close to the matter told reporters that the full implications will not be clear for some time.",
        "Independent analysts have offered sharply divergent assessments, reflecting genuine uncertainty about outcomes.",
        "Stakeholders on all sides are now reassessing their positions in light of the latest developments.",
        "The timeline for the next phase remains unclear, with multiple competing interests still to be reconciled.",
        "Observers note that comparable situations in recent history have played out in ways no one initially anticipated.",
        "Data released alongside the announcement painted a more nuanced picture than the headline figure suggested.",
        "Critics argue the response has been too slow and too cautious given the scale of what is at stake.",
        "Supporters counter that the measured approach is precisely what the complexity of the situation demands.",
    };

    private static readonly string[] ClosingTemplates = new[]
    {
        "The next scheduled review will take place in thirty days, at which point further guidance is expected.",
        "All parties have agreed to refrain from public comment until the formal process concludes.",
        "Whether this marks a turning point or merely a pause in a longer contest remains an open question.",
        "Observers will be watching closely over the coming weeks for signs of whether the momentum can be sustained.",
        "For now, cautious optimism appears to be the dominant mood among those most directly affected.",
        "The story continues to evolve rapidly, and further developments are expected before the week is out.",
    };

    public static IReadOnlyList<string> Next(Faker faker, string headline)
    {
        var paragraphCount = faker.Random.Int(3, 5);
        var paragraphs = new List<string>(paragraphCount);

        // Opening paragraph references the headline context
        var opening = faker.PickRandom(OpeningTemplates);
        paragraphs.Add($"{headline.TrimEnd('.')}. {opening}");

        // Middle paragraphs
        var middlePool = MiddleTemplates.ToList();
        for (var i = 1; i < paragraphCount - 1; i++)
        {
            var picked = faker.PickRandom(middlePool);
            middlePool.Remove(picked);
            paragraphs.Add(picked);
        }

        // Closing paragraph
        paragraphs.Add(faker.PickRandom(ClosingTemplates));

        return paragraphs.AsReadOnly();
    }
}

internal static class PullQuotePool
{
    private static readonly string[] Quotes = new[]
    {
        "We are at a crossroads. The decisions made in the next 90 days will define the trajectory for a generation.",
        "What we are seeing is not an aberration. It is the logical conclusion of trends that have been building for years.",
        "The consensus that held this field together has fractured. Rebuilding it will require honesty about why it failed.",
        "Speed matters, but not more than getting it right. History will judge us on outcomes, not on who moved fastest.",
        "There is no scenario in which doing nothing is the safe choice. Inaction carries its own enormous risks.",
        "We have done the analysis. The fundamentals are sound. The noise you are hearing is just that — noise.",
        "Anyone who tells you they know exactly how this resolves is either lying or hasn't read the data.",
        "The public deserves transparency, not reassurance dressed up to look like transparency.",
        "This is not the end of the story. It's the beginning of a much harder chapter.",
        "Every indicator we track is pointing in the same direction. At some point you have to trust the evidence.",
        "The old playbooks don't apply anymore. The environment has changed too fundamentally.",
        "Our obligation is to the people who depend on us, not to the narratives that are convenient.",
    };

    public static string Next(Faker faker) => faker.PickRandom(Quotes);
}
