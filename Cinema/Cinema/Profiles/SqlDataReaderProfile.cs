using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using Cinema.Models.Tickets;
using Cinema.Reports;
using Type = Cinema.Models.Tickets.Type;

namespace Cinema.Profiles
{
    public class SqlDataReaderProfile : Profile
    {
        public SqlDataReaderProfile()
        {
            CreateMap<SqlDataReader, Movie>()
                .ForMember(x => x.Id, x => x.MapFrom(z => z["Id"]))
                .ForMember(x => x.Name, x => x.MapFrom(z => z["Name"]))
                .ForMember(x => x.Name_ru, x => x.MapFrom(z => z["Name_ru"]))
                .ForMember(x => x.Description, x => x.MapFrom(z => z["Description"]))
                .ForMember(x => x.Description_ru, x => x.MapFrom(z => z["Description_ru"]))
                .ForMember(x => x.MinAge, x => x.MapFrom(z => z["MinAge"]))
                .ForMember(x => x.Duration, x => x.MapFrom(z => z["Duration"]))
                .ForMember(x => x.Rating, x => x.MapFrom(z => z["Rating"]))
                .ForMember(x => x.ImageUrl, x => x.MapFrom(z => z["ImageUrl"]))
                .ForMember(x => x.ImageUrl_ru, x => x.MapFrom(z => z["ImageUrl_ru"]))
                .ForMember(x => x.Types, x => x.Ignore())
                .ForMember(x => x.Genres, x => x.Ignore())

                .AfterMap((reader, movie) =>
                {
                    var types = (string)reader["Types"];
                    if (!string.IsNullOrEmpty(types))
                    {
                        var parsedTypes = types.Split(',').Select(x => (Type)Enum.Parse(typeof(Type), x));
                        movie.Types = parsedTypes.ToArray();
                    }
                    else
                    {
                        movie.Types = new Type[] { };
                    }
                    var genres = (string)reader["Genres"];
                    if (!string.IsNullOrEmpty(genres))
                    {
                        var parsedGenres = genres.Split(',').Select(x => (Genre)Enum.Parse(typeof(Genre), x));
                        movie.Genres = parsedGenres.ToArray();
                    }
                    else
                    {
                        movie.Genres = new Genre[] { };
                    }
                });

            CreateMap<SqlDataReader, Hall>()
                .ForMember(x => x.Id, x => x.MapFrom(z => z["Id"]))
                .ForMember(x => x.Name, x => x.MapFrom(z => z["Name"]));

            CreateMap<SqlDataReader, Tariff>()
                .ForMember(x => x.Id, x => x.MapFrom(z => z["Id"]))
                .ForMember(x => x.Cost, x => x.MapFrom(z => z["Cost"]))
                .ForMember(x => x.Name, x => x.MapFrom(z => z["Name"]));

            CreateMap<SqlDataReader, Timeslot>()
                .ForMember(x => x.Id, x => x.MapFrom(z => z["Id"]))
                .ForMember(x => x.HallId, x => x.MapFrom(z => z["HallId"]))
                .ForMember(x => x.MovieId, x => x.MapFrom(z => z["MovieId"]))
                .ForMember(x => x.TariffId, x => x.MapFrom(z => z["TariffId"]))
                .ForMember(x => x.StartTime, x => x.MapFrom(z => z["StartTime"]))
                .ForMember(x => x.RequestedSeats, x => x.Ignore())
                /*.AfterMap((reader, timeslot) =>
                {
                    var timeslotId = (int)reader["Id"];
                    var requestedSeats = new List<TimeslotSeatRequest>();
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            if ((int)reader["TimeslotId"] == timeslotId)
                            {
                                requestedSeats.Add(Mapper.Map<TimeslotSeatRequest>(reader));
                            }
                        }
                    }
                    timeslot.RequestedSeats = requestedSeats.ToArray();
                })*/;

            CreateMap<SqlDataReader, TimeslotSeatRequest>()
                .ForMember(x => x.Row, x => x.MapFrom(z => z["Row"]))
                .ForMember(x => x.Seat, x => x.MapFrom(z => z["Seat"]))
                .ForMember(x => x.Status, x => x.MapFrom(z => z["Status"]));

            CreateMap<SqlDataReader, PotentialRealProfitReportRow>()
                .ForMember(x => x.Name, x => x.MapFrom(z => z["Name"]))
                .ForMember(x => x.GuaranteedProfit, x => x.MapFrom(z => z["GuaranteedProfit"]))
                .ForMember(x => x.PotentialProfit, x => x.MapFrom(z => z["PotentialProfit"]));

            CreateMap<SqlDataReader, UnprofitableMoviesReportRow>()
                .ForMember(x => x.MovieName, x => x.MapFrom(z => z["MovieName"]))
                .ForMember(x => x.Profit, x => x.MapFrom(z => z["Profit"]));

            CreateMap<SqlDataReader, TimeslotTag>()
                .ForMember(x => x.TimeslotId, x => x.MapFrom(z => z["TimeslotId"]))
                .ForMember(x => x.Cost, x => x.MapFrom(z => z["Cost"]))
                .ForMember(x => x.StartTime, x => x.MapFrom(z => z["StartTime"]));

            CreateMap<SqlDataReader, MovieListItem>()
                .ForMember(x => x.Movie, x => x.MapFrom(z => Mapper.Map<Movie>(z)))
                .ForMember(x => x.AvailableTimeslots, x => x.Ignore())
                /*.AfterMap((reader, movieListItem) =>
                {
                    var movieId = (int)reader["Id"];
                    var timeslotTags = new List<TimeslotTag>();
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            if ((int)reader["MovieId"] == movieId)
                            {
                                timeslotTags.Add(Mapper.Map<TimeslotTag>(reader));
                            }
                        }
                    }
                    movieListItem.AvailableTimeslots = timeslotTags.ToArray();
                })*/;
        }
    }
}