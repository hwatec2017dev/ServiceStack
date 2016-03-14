﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Funq;
using NUnit.Framework;
using ServiceStack.Text;

namespace ServiceStack.WebHost.Endpoints.Tests
{
    public class AutoQueryDataAppHost : AppSelfHostBase
    {
        public AutoQueryDataAppHost()
            : base("AutoQuerData", typeof(AutoQueryService).Assembly) {}

        public override void Configure(Container container)
        {
            Plugins.Add(new AutoQueryDataFeature()
                .AddDataSource(ctx => new QueryDataSource<Rockstar>(ctx, GetRockstars()))
                .AddDataSource(ctx => new QueryDataSource<Adhoc>(ctx, GetAdhoc()))
                .AddDataSource(ctx => new QueryDataSource<Movie>(ctx, GetMovies()))
                .RegisterQueryFilter<QueryDataRockstarsFilter, Rockstar>((q, dto, req) =>
                    q.And(x => x.LastName, new EndsWithCondition(), "son")
                )
                .RegisterQueryFilter<QueryDataCustomRockstarsFilter, Rockstar>((q, dto, req) =>
                    q.And(x => x.LastName, new EndsWithCondition(), "son")
                )
                .RegisterQueryFilter<IFilterRockstars, Rockstar>((q, dto, req) =>
                    q.And(x => x.LastName, new EndsWithCondition(), "son")
                )
            );
        }

        public static Rockstar[] GetRockstars()
        {
            return new[] {
                new Rockstar { Id = 1, FirstName = "Jimi", LastName = "Hendrix", LivingStatus = LivingStatus.Dead, Age = 27, DateOfBirth = new DateTime(1942, 11, 27), DateDied = new DateTime(1970, 09, 18), },
                new Rockstar { Id = 2, FirstName = "Jim", LastName = "Morrison", Age = 27, LivingStatus = LivingStatus.Dead, DateOfBirth = new DateTime(1943, 12, 08), DateDied = new DateTime(1971, 07, 03),  },
                new Rockstar { Id = 3, FirstName = "Kurt", LastName = "Cobain", Age = 27, LivingStatus = LivingStatus.Dead, DateOfBirth = new DateTime(1967, 02, 20), DateDied = new DateTime(1994, 04, 05), },
                new Rockstar { Id = 4, FirstName = "Elvis", LastName = "Presley", Age = 42, LivingStatus = LivingStatus.Dead, DateOfBirth = new DateTime(1935, 01, 08), DateDied = new DateTime(1977, 08, 16), },
                new Rockstar { Id = 5, FirstName = "David", LastName = "Grohl", Age = 44, LivingStatus = LivingStatus.Alive, DateOfBirth = new DateTime(1969, 01, 14), },
                new Rockstar { Id = 6, FirstName = "Eddie", LastName = "Vedder", Age = 48, LivingStatus = LivingStatus.Alive, DateOfBirth = new DateTime(1964, 12, 23), },
                new Rockstar { Id = 7, FirstName = "Michael", LastName = "Jackson", Age = 50, LivingStatus = LivingStatus.Dead, DateOfBirth = new DateTime(1958, 08, 29), DateDied = new DateTime(2009, 06, 05), },
            };
        }

        public static List<Adhoc> GetAdhoc()
        {
            return GetRockstars().Map(x => new Adhoc
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
            });
        }

        public static Movie[] GetMovies()
        {
            return new[] {
                new Movie { Id = 1, ImdbId = "tt0111161", Title = "The Shawshank Redemption", Score = 9.2m, Director = "Frank Darabont", ReleaseDate = new DateTime(1995,2,17), TagLine = "Fear can hold you prisoner. Hope can set you free.", Genres = new List<string>{"Crime","Drama"}, Rating = "R", },
                new Movie { Id = 2, ImdbId = "tt0068646", Title = "The Godfather", Score = 9.2m, Director = "Francis Ford Coppola", ReleaseDate = new DateTime(1972,3,24), TagLine = "An offer you can't refuse.", Genres = new List<string> {"Crime","Drama", "Thriller"}, Rating = "R", },
                new Movie { Id = 3, ImdbId = "tt1375666", Title = "Inception", Score = 9.2m, Director = "Christopher Nolan", ReleaseDate = new DateTime(2010,7,16), TagLine = "Your mind is the scene of the crime", Genres = new List<string>{"Action", "Mystery", "Sci-Fi", "Thriller"}, Rating = "PG-13", },
                new Movie { Id = 4, ImdbId = "tt0071562", Title = "The Godfather: Part II", Score = 9.0m, Director = "Francis Ford Coppola", ReleaseDate = new DateTime(1974,12,20), Genres = new List<string> {"Crime","Drama", "Thriller"}, Rating = "R", },
                new Movie { Id = 5, ImdbId = "tt0060196", Title = "The Good, the Bad and the Ugly", Score = 9.0m, Director = "Sergio Leone", ReleaseDate = new DateTime(1967,12,29), TagLine = "They formed an alliance of hate to steal a fortune in dead man's gold", Genres = new List<string>{"Adventure","Western"}, Rating = "R", },
                new Movie { Id = 6, ImdbId = "tt0114709", Title = "Toy Story", Score = 8.3m, Director = "John Lasseter", ReleaseDate = new DateTime(1995,11,22), TagLine = "A cowboy doll is profoundly threatened and jealous when a new spaceman figure supplants him as top toy in a boy's room.", Genres = new List<string>{"Animation","Adventure","Comedy"}, Rating = "G", },
                new Movie { Id = 7, ImdbId = "tt2294629", Title = "Frozen", Score = 7.8m, Director = "Chris Buck", ReleaseDate = new DateTime(2013,11,27), TagLine = "Fearless optimist Anna teams up with Kristoff in an epic journey, encountering Everest-like conditions, and a hilarious snowman named Olaf", Genres = new List<string>{"Animation","Adventure","Comedy"}, Rating = "PG", },
                new Movie { Id = 8, ImdbId = "tt1453405", Title = "Monsters University", Score = 7.4m, Director = "Dan Scanlon", ReleaseDate = new DateTime(2013,06,21), TagLine = "A look at the relationship between Mike and Sulley during their days at Monsters University -- when they weren't necessarily the best of friends.", Genres = new List<string>{"Animation","Adventure","Comedy"}, Rating = "G", },
                new Movie { Id = 9, ImdbId = "tt0468569", Title = "The Dark Knight", Score = 9.0m, Director = "Christopher Nolan", ReleaseDate = new DateTime(2008,07,18), TagLine = "When Batman, Gordon and Harvey Dent launch an assault on the mob, they let the clown out of the box, the Joker, bent on turning Gotham on itself and bringing any heroes down to his level.", Genres = new List<string>{"Action","Crime","Drama"}, Rating = "PG-13", },
                new Movie { Id = 10, ImdbId = "tt0109830", Title = "Forrest Gump", Score = 8.8m, Director = "Robert Zemeckis", ReleaseDate = new DateTime(1996,07,06), TagLine = "Forrest Gump, while not intelligent, has accidentally been present at many historic moments, but his true love, Jenny Curran, eludes him.", Genres = new List<string>{"Drama","Romance"}, Rating = "PG-13", },
            };
        }
    }

    [Route("/querydata/rockstars")]
    public class QueryDataRockstars : QueryData<Rockstar>
    {
        public int? Age { get; set; }
    }

    public class QueryDataRockstarsConventions : QueryData<Rockstar>
    {
        public DateTime? DateOfBirthGreaterThan { get; set; }
        public DateTime? DateDiedLessThan { get; set; }
        public int[] Ids { get; set; }
        public int? AgeOlderThan { get; set; }
        public int? AgeGreaterThanOrEqualTo { get; set; }
        public int? AgeGreaterThan { get; set; }
        public int? GreaterThanAge { get; set; }
        public string FirstNameStartsWith { get; set; }
        public string LastNameEndsWith { get; set; }
        public string LastNameContains { get; set; }
        public string RockstarAlbumNameContains { get; set; }
        public int? RockstarIdAfter { get; set; }
        public int? RockstarIdOnOrAfter { get; set; }
    }

    public class QueryDataCustomRockstars : QueryData<Rockstar, CustomRockstar>
    {
        public int? Age { get; set; }
    }

    public class QueryDataOverridedRockstars : QueryData<Rockstar>
    {
        public int? Age { get; set; }
    }

    public class QueryDataOverridedCustomRockstars : QueryData<Rockstar, CustomRockstar>
    {
        public int? Age { get; set; }
    }

    public class QueryDataFieldRockstars : QueryData<Rockstar>
    {
        public string FirstName { get; set; } //default to 'AND FirstName = {Value}'

        public string[] FirstNames { get; set; } //Collections default to 'FirstName IN ({Values})

        [QueryDataField(Condition = "GreaterEqualCondition")]
        public int? Age { get; set; }

        [QueryDataField(Condition = "CaseInsensitiveEqualCondition", Field = "FirstName")]
        public string FirstNameCaseInsensitive { get; set; }

        [QueryDataField(Condition = "StartsWithCondition", Field = "FirstName")]
        public string FirstNameStartsWith { get; set; }

        [QueryDataField(Condition = "EndsWithCondition", Field = "LastName")]
        public string LastNameEndsWith { get; set; }

        [QueryDataField(Condition = "InBetweenCondition", Field = "FirstName")]
        public string[] FirstNameBetween { get; set; }

        [QueryDataField(Term = QueryTerm.Or, Condition = "CaseInsensitiveEqualCondition", Field = "LastName")]
        public string OrLastName { get; set; }
    }

    public class QueryDataFieldRockstarsDynamic : QueryData<Rockstar>
    {
        public int? Age { get; set; }
    }

    public class QueryDataRockstarsFilter : QueryData<Rockstar>
    {
        public int? Age { get; set; }
    }

    public class QueryDataCustomRockstarsFilter : QueryData<Rockstar, CustomRockstar>
    {
        public int? Age { get; set; }
    }

    public class QueryDataRockstarsIFilter : QueryData<Rockstar>, IFilterRockstars
    {
        public int? Age { get; set; }
    }

    [QueryData(QueryTerm.Or)]
    [Route("/OrDataRockstars")]
    public class QueryDataOrRockstars : QueryData<Rockstar>
    {
        public int? Age { get; set; }
        public string FirstName { get; set; }
    }

    [Route("/OrDataRockstarsFields")]
    public class QueryDataOrRockstarsFields : QueryData<Rockstar>
    {
        [QueryDataField(Term = QueryTerm.Or)]
        public string FirstName { get; set; }

        [QueryDataField(Term = QueryTerm.Or)]
        public string LastName { get; set; }
    }

    [QueryData(QueryTerm.Or)]
    public class QueryDataGetRockstars : QueryData<Rockstar>
    {
        public int[] Ids { get; set; }
        public List<int> Ages { get; set; }
        public List<string> FirstNames { get; set; }
        public int[] IdsBetween { get; set; }
    }

    [QueryData(QueryTerm.Or)]
    public class QueryDataGetRockstarsDynamic : QueryData<Rockstar> { }

    [DataContract]
    [Route("/adhocdata-rockstars")]
    public class QueryDataAdhocRockstars : QueryData<Rockstar>
    {
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }
    }

    [DataContract]
    [Route("/adhocdata")]
    public class QueryDataAdhoc : QueryData<Adhoc> { }

    [Route("/moviesdata/search")]
    [QueryData(QueryTerm.And)] //Default
    public class SearchDataMovies : QueryData<Movie> { }

    [Route("/moviesdata")]
    [QueryData(QueryTerm.Or)]
    public class QueryDataMovies : QueryData<Movie>
    {
        public int[] Ids { get; set; }
        public string[] ImdbIds { get; set; }
        public string[] Ratings { get; set; }
    }

    public class StreamDataMovies : QueryData<Movie>
    {
        public string[] Ratings { get; set; }
    }

    public class AutoQueryDataService : Service
    {
        public IAutoQueryData AutoQuery { get; set; }

        //Override with custom impl
        public object Any(QueryDataOverridedRockstars dto)
        {
            var q = AutoQuery.CreateQuery(dto, Request.GetRequestParams(), Request);
            q.Take(1);
            return AutoQuery.Execute(dto, q);
        }

        public object Any(QueryDataOverridedCustomRockstars dto)
        {
            var q = AutoQuery.CreateQuery(dto, Request.GetRequestParams(), Request);
            q.Take(1);
            return AutoQuery.Execute(dto, q);
        }
    }

    [TestFixture]
    public class AutoQueryDataTests
    {
        private readonly ServiceStackHost appHost;
        public IServiceClient client;

        private static readonly int TotalRockstars = AutoQueryAppHost.SeedRockstars.Length;
        private static readonly int TotalAlbums = AutoQueryAppHost.SeedAlbums.Length;

        public AutoQueryDataTests()
        {
            appHost = new AutoQueryDataAppHost()
                .Init()
                .Start(Config.ListeningOn);

            client = new JsonServiceClient(Config.ListeningOn);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }

        [Test]
        public void Can_execute_basic_query()
        {
            var response = client.Get(new QueryDataRockstars());

            Assert.That(response.Offset, Is.EqualTo(0));
            Assert.That(response.Total, Is.EqualTo(TotalRockstars));
            Assert.That(response.Results.Count, Is.EqualTo(TotalRockstars));
        }

        [Test]
        public void Can_execute_overridden_basic_query()
        {
            var response = client.Get(new QueryDataOverridedRockstars());

            Assert.That(response.Offset, Is.EqualTo(0));
            Assert.That(response.Total, Is.EqualTo(TotalRockstars));
            Assert.That(response.Results.Count, Is.EqualTo(1));
        }

        [Test]
        public void Can_execute_AdhocRockstars_query()
        {
            var request = new QueryDataAdhocRockstars { FirstName = "Jimi" };

            Assert.That(request.ToGetUrl(), Is.EqualTo("/adhocdata-rockstars?first_name=Jimi"));

            var response = client.Get(request);

            Assert.That(response.Offset, Is.EqualTo(0));
            Assert.That(response.Total, Is.EqualTo(1));
            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results[0].FirstName, Is.EqualTo(request.FirstName));
        }

        [Test]
        public void Can_execute_Adhoc_query_alias()
        {
            var response = Config.ListeningOn.CombineWith("adhocdata")
                .AddQueryParam("first_name", "Jimi")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Adhoc>>();

            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results[0].FirstName, Is.EqualTo("Jimi"));
        }

        [Test]
        public void Can_execute_Adhoc_query_convention()
        {
            var response = Config.ListeningOn.CombineWith("adhocdata")
                .AddQueryParam("last_name", "Hendrix")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Adhoc>>();
            Assert.That(response.Results.Count, Is.EqualTo(7));

            JsConfig.EmitLowercaseUnderscoreNames = true;
            response = Config.ListeningOn.CombineWith("adhocdata")
                .AddQueryParam("last_name", "Hendrix")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Adhoc>>();
            JsConfig.Reset();

            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results[0].FirstName, Is.EqualTo("Jimi"));
        }

        [Test]
        public void Can_execute_explicit_equality_condition_on_overridden_CustomRockstar()
        {
            var response = client.Get(new QueryDataOverridedCustomRockstars { Age = 27 });

            Assert.That(response.Total, Is.EqualTo(3));
            Assert.That(response.Results.Count, Is.EqualTo(1));
        }

        [Test]
        public void Can_execute_basic_query_with_limits()
        {
            var response = client.Get(new QueryDataRockstars { Skip = 2 });
            Assert.That(response.Offset, Is.EqualTo(2));
            Assert.That(response.Total, Is.EqualTo(TotalRockstars));
            Assert.That(response.Results.Count, Is.EqualTo(TotalRockstars - 2));

            response = client.Get(new QueryDataRockstars { Take = 2 });
            Assert.That(response.Offset, Is.EqualTo(0));
            Assert.That(response.Total, Is.EqualTo(TotalRockstars));
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = client.Get(new QueryDataRockstars { Skip = 2, Take = 2 });
            Assert.That(response.Offset, Is.EqualTo(2));
            Assert.That(response.Total, Is.EqualTo(TotalRockstars));
            Assert.That(response.Results.Count, Is.EqualTo(2));
        }

        [Test]
        public void Can_execute_explicit_equality_condition()
        {
            var response = client.Get(new QueryDataRockstars { Age = 27 });

            Assert.That(response.Total, Is.EqualTo(3));
            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_execute_explicit_equality_condition_on_CustomRockstar()
        {
            var response = client.Get(new QueryDataCustomRockstars { Age = 27 });

            Assert.That(response.Total, Is.EqualTo(3));
            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_execute_implicit_equality_condition()
        {
            var response = Config.ListeningOn.CombineWith("json/reply/QueryDataRockstars")
                .AddQueryParam("FirstName", "Jim")
                .AddQueryParam("LivingStatus", "Dead")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Rockstar>>();

            Assert.That(response.Total, Is.EqualTo(1));
            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results[0].LastName, Is.EqualTo("Morrison"));
        }

        [Test]
        public void Can_execute_multiple_conditions_with_same_param_name()
        {
            var response = Config.ListeningOn.CombineWith("json/reply/QueryDataRockstars")
                .AddQueryParam("FirstName", "Jim")
                .AddQueryParam("FirstName", "Jim")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Rockstar>>();

            Assert.That(response.Total, Is.EqualTo(1));
            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results[0].LastName, Is.EqualTo("Morrison"));

            response = Config.ListeningOn.CombineWith("json/reply/QueryDataRockstars")
                .AddQueryParam("FirstNameStartsWith", "Jim")
                .AddQueryParam("FirstNameStartsWith", "Jimi")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Rockstar>>();

            Assert.That(response.Total, Is.EqualTo(1));
            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results[0].LastName, Is.EqualTo("Hendrix"));
        }

        [Test]
        public void Can_execute_implicit_IsNull_condition()
        {
            var response = Config.ListeningOn.CombineWith("json/reply/QueryDataRockstars?DateDied=")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Rockstar>>();

            Assert.That(response.Total, Is.EqualTo(2));
            Assert.That(response.Results.Count, Is.EqualTo(2));
        }

        [Test]
        public void Can_execute_custom_QueryFields()
        {
            QueryResponse<Rockstar> response;
            response = client.Get(new QueryDataFieldRockstars { FirstName = "Jim" });
            Assert.That(response.Results.Count, Is.EqualTo(1));

            response = client.Get(new QueryDataFieldRockstars { FirstNames = new[] { "Jim", "Kurt" } });
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = client.Get(new QueryDataFieldRockstars { FirstNameCaseInsensitive = "jim" });
            Assert.That(response.Results.Count, Is.EqualTo(1));

            response = client.Get(new QueryDataFieldRockstars { FirstNameStartsWith = "Jim" });
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = client.Get(new QueryDataFieldRockstars { LastNameEndsWith = "son" });
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = client.Get(new QueryDataFieldRockstars { FirstNameBetween = new[] { "A", "F" } });
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = client.Get(new QueryDataFieldRockstars
            {
                LastNameEndsWith = "son",
                OrLastName = "Hendrix"
            });
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = client.Get(new QueryDataFieldRockstars { Age = 42 });
            Assert.That(response.Results.Count, Is.EqualTo(4));
        }

        [Test]
        public void Can_execute_combination_of_QueryFields()
        {
            QueryResponse<Rockstar> response;

            response = client.Get(new QueryDataFieldRockstars
            {
                FirstNameStartsWith = "Jim",
                LastNameEndsWith = "son",
            });
            Assert.That(response.Results.Count, Is.EqualTo(1));

            response = client.Get(new QueryDataFieldRockstars
            {
                FirstNameStartsWith = "Jim",
                OrLastName = "Cobain",
            });
            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Does_escape_values()
        {
            QueryResponse<Rockstar> response;

            response = client.Get(new QueryDataFieldRockstars
            {
                FirstNameStartsWith = "Jim'\"",
            });
            Assert.That(response.Results.Count, Is.EqualTo(0));
        }

        [Test]
        public void Does_allow_adding_attributes_dynamically()
        {
            typeof(QueryDataFieldRockstarsDynamic)
                .GetProperty("Age")
                .AddAttributes(new QueryDataFieldAttribute { Condition = "GreaterEqualCondition" });

            var response = client.Get(new QueryDataFieldRockstarsDynamic { Age = 42 });
            Assert.That(response.Results.Count, Is.EqualTo(4));
        }

        [Test]
        public void Does_execute_typed_QueryFilters()
        {
            // QueryFilter appends additional: x => x.LastName.EndsWith("son")
            var response = client.Get(new QueryDataRockstarsFilter { Age = 27 });
            Assert.That(response.Results.Count, Is.EqualTo(1));

            var custom = client.Get(new QueryDataCustomRockstarsFilter { Age = 27 });
            Assert.That(custom.Results.Count, Is.EqualTo(1));

            response = client.Get(new QueryDataRockstarsIFilter { Age = 27 });
            Assert.That(response.Results.Count, Is.EqualTo(1));
        }

        [Test]
        public void Can_execute_OR_QueryFilters()
        {
            var response = client.Get(new QueryDataOrRockstars { Age = 42, FirstName = "Jim" });
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = Config.ListeningOn.CombineWith("OrDataRockstars")
                .AddQueryParam("Age", "27")
                .AddQueryParam("FirstName", "Kurt")
                .AddQueryParam("LastName", "Hendrix")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Rockstar>>();
            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_execute_OR_QueryFilters_Fields()
        {
            var response = client.Get(new QueryDataOrRockstarsFields
            {
                FirstName = "Jim",
                LastName = "Vedder",
            });
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = Config.ListeningOn.CombineWith("OrDataRockstarsFields")
                .AddQueryParam("FirstName", "Kurt")
                .AddQueryParam("LastName", "Hendrix")
                .GetJsonFromUrl()
                .FromJson<QueryResponse<Rockstar>>();
            Assert.That(response.Results.Count, Is.EqualTo(2));
        }

        [Test]
        public void Can_execute_implicit_conventions()
        {
            var baseUrl = Config.ListeningOn.CombineWith("json/reply/QueryDataRockstars");

            var response = baseUrl.AddQueryParam("AgeOlderThan", 42).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = baseUrl.AddQueryParam("AgeGreaterThanOrEqualTo", 42).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(4));

            response = baseUrl.AddQueryParam("AgeGreaterThan", 42).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(3));
            response = baseUrl.AddQueryParam("GreaterThanAge", 42).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(3));
            response = baseUrl.AddQueryParam("AgeNotEqualTo", 27).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(4));

            response = baseUrl.AddQueryParam(">Age", 42).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(4));
            response = baseUrl.AddQueryParam("Age>", 42).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(3));
            response = baseUrl.AddQueryParam("<Age", 42).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(3));
            response = baseUrl.AddQueryParam("Age<", 42).AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(4));
            response = baseUrl.AddQueryParam("Age!", "27").AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(4));

            response = baseUrl.AddQueryParam("FirstNameStartsWith", "jim").AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(2));
            response = baseUrl.AddQueryParam("LastNameEndsWith", "son").AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(2));
            response = baseUrl.AddQueryParam("LastNameContains", "e").AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_execute_Explicit_conventions()
        {
            QueryResponse<Rockstar> response;
            response = client.Get(new QueryDataRockstarsConventions { Ids = new[] { 1, 2, 3 } });
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = client.Get(new QueryDataRockstarsConventions { AgeOlderThan = 42 });
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = client.Get(new QueryDataRockstarsConventions { AgeGreaterThanOrEqualTo = 42 });
            Assert.That(response.Results.Count, Is.EqualTo(4));

            response = client.Get(new QueryDataRockstarsConventions { AgeGreaterThan = 42 });
            Assert.That(response.Results.Count, Is.EqualTo(3));
            response = client.Get(new QueryDataRockstarsConventions { GreaterThanAge = 42 });
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = client.Get(new QueryDataRockstarsConventions { FirstNameStartsWith = "Jim" });
            Assert.That(response.Results.Count, Is.EqualTo(2));
            response = client.Get(new QueryDataRockstarsConventions { LastNameEndsWith = "son" });
            Assert.That(response.Results.Count, Is.EqualTo(2));
            response = client.Get(new QueryDataRockstarsConventions { LastNameContains = "e" });
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = client.Get(new QueryDataRockstarsConventions { DateOfBirthGreaterThan = new DateTime(1960, 01, 01) });
            Assert.That(response.Results.Count, Is.EqualTo(3));
            response = client.Get(new QueryDataRockstarsConventions { DateDiedLessThan = new DateTime(1980, 01, 01) });
            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_execute_In_OR_Queries()
        {
            QueryResponse<Rockstar> response;
            response = client.Get(new QueryDataGetRockstars());
            Assert.That(response.Results.Count, Is.EqualTo(0));

            response = client.Get(new QueryDataGetRockstars { Ids = new[] { 1, 2, 3 } });
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = client.Get(new QueryDataGetRockstars { Ages = new[] { 42, 44 }.ToList() });
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = client.Get(new QueryDataGetRockstars { FirstNames = new[] { "Jim", "Kurt" }.ToList() });
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = client.Get(new QueryDataGetRockstars { IdsBetween = new[] { 1, 3 } });
            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_execute_In_OR_Queries_with_implicit_conventions()
        {
            var baseUrl = Config.ListeningOn.CombineWith("json/reply/QueryDataGetRockstarsDynamic");

            QueryResponse<Rockstar> response;
            response = baseUrl.AddQueryParam("Ids", "1,2,3").AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(3));

            response = baseUrl.AddQueryParam("Ages", "42, 44").AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = baseUrl.AddQueryParam("FirstNames", "Jim,Kurt").AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(2));

            response = baseUrl.AddQueryParam("IdsBetween", "1,3").AsJsonInto<Rockstar>();
            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_query_Movie_Ratings()
        {
            var response = client.Get(new QueryDataMovies { Ratings = new[] { "G", "PG-13" } });
            Assert.That(response.Results.Count, Is.EqualTo(5));

            var url = Config.ListeningOn + "moviesdata?ratings=G,PG-13";
            response = url.AsJsonInto<Movie>();
            Assert.That(response.Results.Count, Is.EqualTo(5));

            response = client.Get(new QueryDataMovies
            {
                Ids = new[] { 1, 2 },
                ImdbIds = new[] { "tt0071562", "tt0060196" },
                Ratings = new[] { "G", "PG-13" }
            });
            Assert.That(response.Results.Count, Is.EqualTo(9));

            url = Config.ListeningOn + "moviesdata?ratings=G,PG-13&ids=1,2&imdbIds=tt0071562,tt0060196";
            response = url.AsJsonInto<Movie>();
            Assert.That(response.Results.Count, Is.EqualTo(9));
        }

        [Test]
        public void Can_StreamMovies()
        {
            var results = client.GetLazy(new StreamDataMovies()).ToList();
            Assert.That(results.Count, Is.EqualTo(10));

            results = client.GetLazy(new StreamDataMovies { Ratings = new[] { "G", "PG-13" } }).ToList();
            Assert.That(results.Count, Is.EqualTo(5));
        }

        [Test]
        public void Does_implicitly_OrderBy_PrimaryKey_when_limits_is_specified()
        {
            var movies = client.Get(new SearchDataMovies { Take = 100 });
            var ids = movies.Results.Map(x => x.Id);
            var orderedIds = ids.OrderBy(x => x);
            Assert.That(ids, Is.EqualTo(orderedIds));

            var rockstars = client.Get(new SearchDataMovies { Take = 100 });
            ids = rockstars.Results.Map(x => x.Id);
            orderedIds = ids.OrderBy(x => x);
            Assert.That(ids, Is.EqualTo(orderedIds));
        }

        [Test]
        public void Can_OrderBy_queries()
        {
            var movies = client.Get(new SearchDataMovies { Take = 100, OrderBy = "ImdbId" });
            var ids = movies.Results.Map(x => x.ImdbId);
            var orderedIds = ids.OrderBy(x => x).ToList();
            Assert.That(ids, Is.EqualTo(orderedIds));

            movies = client.Get(new SearchDataMovies { Take = 100, OrderBy = "Rating,ImdbId" });
            ids = movies.Results.Map(x => x.ImdbId);
            orderedIds = movies.Results.OrderBy(x => x.Rating).ThenBy(x => x.ImdbId).Map(x => x.ImdbId);
            Assert.That(ids, Is.EqualTo(orderedIds));

            movies = client.Get(new SearchDataMovies { Take = 100, OrderByDesc = "ImdbId" });
            ids = movies.Results.Map(x => x.ImdbId);
            orderedIds = ids.OrderByDescending(x => x).ToList();
            Assert.That(ids, Is.EqualTo(orderedIds));

            movies = client.Get(new SearchDataMovies { Take = 100, OrderByDesc = "Rating,ImdbId" });
            ids = movies.Results.Map(x => x.ImdbId);
            orderedIds = movies.Results.OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.ImdbId).Map(x => x.ImdbId);
            Assert.That(ids, Is.EqualTo(orderedIds));

            movies = client.Get(new SearchDataMovies { Take = 100, OrderBy = "Rating,-ImdbId" });
            ids = movies.Results.Map(x => x.ImdbId);
            orderedIds = movies.Results.OrderBy(x => x.Rating)
                .ThenByDescending(x => x.ImdbId).Map(x => x.ImdbId);
            Assert.That(ids, Is.EqualTo(orderedIds));

            movies = client.Get(new SearchDataMovies { Take = 100, OrderByDesc = "Rating,-ImdbId" });
            ids = movies.Results.Map(x => x.ImdbId);
            orderedIds = movies.Results.OrderByDescending(x => x.Rating)
                .ThenBy(x => x.ImdbId).Map(x => x.ImdbId);
            Assert.That(ids, Is.EqualTo(orderedIds));

            var url = Config.ListeningOn + "moviesdata/search?take=100&orderBy=Rating,ImdbId";
            movies = url.AsJsonInto<Movie>();
            ids = movies.Results.Map(x => x.ImdbId);
            orderedIds = movies.Results.OrderBy(x => x.Rating).ThenBy(x => x.ImdbId).Map(x => x.ImdbId);
            Assert.That(ids, Is.EqualTo(orderedIds));

            url = Config.ListeningOn + "moviesdata/search?take=100&orderByDesc=Rating,ImdbId";
            movies = url.AsJsonInto<Movie>();
            ids = movies.Results.Map(x => x.ImdbId);
            orderedIds = movies.Results.OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.ImdbId).Map(x => x.ImdbId);
            Assert.That(ids, Is.EqualTo(orderedIds));
        }
    }
}