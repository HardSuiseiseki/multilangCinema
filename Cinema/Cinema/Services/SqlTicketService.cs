using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AutoMapper;
using Cinema.Models.Tickets;
using Cinema.Utils;

namespace Cinema.Services
{
    public class SqlTicketService : ITicketService
    {
        private SqlDatabaseUtil DatabaseUtil { get; set; }

        public SqlTicketService(IMapper mapper)
        {
            DatabaseUtil = new SqlDatabaseUtil(mapper);
        }

        public Movie GetMovieById(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return DatabaseUtil.Execute<Movie>("SelectMovieById", parameters).FirstOrDefault();
        }

        public bool RemoveMovie(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return DatabaseUtil.ExecuteNonQuery("DeleteMovie", parameters) != 0;
        }

        public bool UpdateMovie(Movie updatedMovie)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", updatedMovie.Id),
                new SqlParameter("@Name", updatedMovie.Name),
                new SqlParameter("@Name_ru", updatedMovie.Name_ru),
                new SqlParameter("@Description", updatedMovie.Description),
                new SqlParameter("@Description_ru", updatedMovie.Description_ru),
                new SqlParameter("@MinAge", updatedMovie.MinAge),
                new SqlParameter("@Duration", updatedMovie.Duration),
                new SqlParameter("@Rating", updatedMovie.Rating),
                new SqlParameter("@Types", updatedMovie.Types!=null && updatedMovie.Types.Any()? string.Join(",",updatedMovie.Types):string.Empty),
                new SqlParameter("@Genres", updatedMovie.Genres!=null && updatedMovie.Genres.Any()? string.Join(",",updatedMovie.Genres):string.Empty),
                new SqlParameter("@ImageUrl", updatedMovie.ImageUrl),
                new SqlParameter("@ImageUrl_ru", updatedMovie.ImageUrl_ru)
            };

            return DatabaseUtil.ExecuteNonQuery("UpdateMovie", parameters) != 0;
        }

        public bool CreateMovie(Movie newMovie)
        {
            var parameters = new[]
            {
                new SqlParameter("@Name", newMovie.Name),
                new SqlParameter("@Name_ru", newMovie.Name_ru),
                new SqlParameter("@Description", newMovie.Description),
                new SqlParameter("@Description_ru", newMovie.Description_ru),
                new SqlParameter("@MinAge", newMovie.MinAge),
                new SqlParameter("@Duration", newMovie.Duration),
                new SqlParameter("@Rating", newMovie.Rating),
                new SqlParameter("@Types", newMovie.Types!=null && newMovie.Types.Any()? string.Join(",",newMovie.Types):string.Empty),
                new SqlParameter("@Genres", newMovie.Genres!=null && newMovie.Genres.Any()? string.Join(",",newMovie.Genres):string.Empty),
                new SqlParameter("@ImageUrl", newMovie.ImageUrl),
                new SqlParameter("@ImageUrl_ru", newMovie.ImageUrl_ru)
            };

            return DatabaseUtil.ExecuteNonQuery("AddMovie", parameters) != 0;
        }

        public Movie[] GetAllMovies()
        {
            return DatabaseUtil.Execute<Movie>("SelectAllMovies").ToArray();
        }

        public Hall GetHallById(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return DatabaseUtil.Execute<Hall>("SelectHallById", parameters).FirstOrDefault();
        }

        public bool RemoveHall(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return DatabaseUtil.ExecuteNonQuery("DeleteHall", parameters) != 0;
        }

        public bool UpdateHall(Hall updatedHall)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", updatedHall.Id),
                new SqlParameter("@Name", updatedHall.Name)
            };

            return DatabaseUtil.ExecuteNonQuery("UpdateHall", parameters) != 0;
        }

        public bool CreateHall(Hall newHall)
        {
            var parameters = new[]
            {
                new SqlParameter("@Name", newHall.Name)
            };

            return DatabaseUtil.ExecuteNonQuery("AddHall", parameters) != 0;
        }

        public Hall[] GetAllHalls()
        {
            return DatabaseUtil.Execute<Hall>("SelectAllHalls").ToArray();
        }

        public Tariff GetTariffById(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return DatabaseUtil.Execute<Tariff>("SelectTariffById", parameters).FirstOrDefault();
        }

        public bool RemoveTariff(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return DatabaseUtil.ExecuteNonQuery("DeleteTariff", parameters) != 0;
        }

        public bool UpdateTariff(Tariff updatedTariff)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", updatedTariff.Id),
                new SqlParameter("@Name", updatedTariff.Name),
                new SqlParameter("@Cost", updatedTariff.Cost)
            };

            return DatabaseUtil.ExecuteNonQuery("UpdateTariff", parameters) != 0;
        }

        public bool CreateTariff(Tariff newTariff)
        {
            var parameters = new[]
            {
                new SqlParameter("@Name", newTariff.Name),
                new SqlParameter("@Cost", newTariff.Cost)
            };

            return DatabaseUtil.ExecuteNonQuery("AddTariff", parameters) != 0;
        }

        public Tariff[] GetAllTariffs()
        {
            return DatabaseUtil.Execute<Tariff>("SelectAllTariffs").ToArray();
        }

        public Timeslot GetTimeslotById(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            Func<SqlDataReader, List<Timeslot>, List<Timeslot>> mappingFunc = (reader, rawTimeslots) =>
            {
                var processedCollection = new List<Timeslot>();
                processedCollection.AddRange(rawTimeslots);
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var targetTimeslot = processedCollection.FirstOrDefault(x => x.Id == (int)reader["TimeslotId"]);
                        if (targetTimeslot == null)
                            continue;
                        var targetTimeslotRequestedSeatsLists = targetTimeslot.RequestedSeats.ToList();
                        targetTimeslotRequestedSeatsLists.Add(Mapper.Map<TimeslotSeatRequest>(reader));
                        targetTimeslot.RequestedSeats = targetTimeslotRequestedSeatsLists.ToArray();
                    }
                }
                return processedCollection;
            };

            return DatabaseUtil.Execute("SelectTimeslotById", parameters, mappingFunc).FirstOrDefault();
        }

        public bool RemoveTimeslot(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return DatabaseUtil.ExecuteNonQuery("DeleteTimeslot", parameters) != 0;
        }

        public bool UpdateTimeslot(Timeslot updatedTimeslot)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", updatedTimeslot.Id),
                new SqlParameter("@StartTime", updatedTimeslot.StartTime),
                new SqlParameter("@MovieId", updatedTimeslot.MovieId),
                new SqlParameter("@HallId", updatedTimeslot.HallId),
                new SqlParameter("@TariffId", updatedTimeslot.TariffId)
            };

            return DatabaseUtil.ExecuteNonQuery("UpdateTimeslot", parameters) != 0;
        }

        public bool CreateTimeslot(Timeslot newTimeslot)
        {
            var parameters = new[]
            {
                new SqlParameter("@StartTime", newTimeslot.StartTime),
                new SqlParameter("@MovieId", newTimeslot.MovieId),
                new SqlParameter("@HallId", newTimeslot.HallId),
                new SqlParameter("@TariffId", newTimeslot.TariffId)
            };

            return DatabaseUtil.ExecuteNonQuery("AddTimeslot", parameters) != 0;
        }

        public Timeslot[] GetTimeslotsByMovieId(int movieId)
        {
            var parameters = new[]
            {
                new SqlParameter("@MovieId", movieId)
            };

            return DatabaseUtil.Execute<Timeslot>("SelectTimeslotByMovieId", parameters).ToArray();
        }

        public Timeslot[] GetAllTimeslots()
        {
            return DatabaseUtil.Execute<Timeslot>("SelectAllTimeslots").ToArray();
        }

        private readonly Func<SqlDataReader, List<MovieListItem>, List<MovieListItem>> _movieListItemMappingFunc = (reader, rawMovieList) =>
        {
            var processedCollection = new List<MovieListItem>();
            processedCollection.AddRange(rawMovieList);
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    var targetMovie = processedCollection.FirstOrDefault(x => x.Movie.Id == (int)reader["MovieId"]);
                    if (targetMovie == null)
                        continue;
                    var targetMovieTimeslotsLists = targetMovie.AvailableTimeslots.ToList();
                    targetMovieTimeslotsLists.Add(Mapper.Map<TimeslotTag>(reader));
                    targetMovie.AvailableTimeslots = targetMovieTimeslotsLists.ToArray();
                }
            }
            return processedCollection;
        };

        public MovieListItem[] SearchMoviesByTerm(string term)
        {
            var parameters = new[]
            {
                new SqlParameter("@Term", term)
            };

            return DatabaseUtil.Execute("SearchMoviesByTerm", parameters, _movieListItemMappingFunc).ToArray();
        }

        public MovieListItem[] GetFullMoviesInfo()
        {
            return DatabaseUtil.Execute("GetFullMoviesInfo", null, _movieListItemMappingFunc).ToArray();
        }

        public TimeslotTag[] GetTimeslotTagsByMovieId(int movid)
        {
            var parameters = new[]
            {
                new SqlParameter("@MovieId", movid)
            };

            return DatabaseUtil.Execute<TimeslotTag>("GetTimeslotTagsByMovieId", parameters).ToArray();
        }

        public bool AddRequestedSeatsToTimeslot(SeatsProcessRequest request)
        {
            var requestTable = new DataTable("TimeslotSeatRequest");
            requestTable.Columns.Add("Row", typeof(int));
            requestTable.Columns.Add("Seat", typeof(int));
            requestTable.Columns.Add("Status", typeof(int));
            foreach (var seatRequest in request.SeatsRequest.AddedSeats)
            {
                requestTable.Rows.Add(seatRequest.Row, seatRequest.Seat, request.SelectedStatus);
            }

            SqlParameter requestTableparameter = new SqlParameter
            {
                ParameterName = "@seatsRequest",
                SqlDbType = SqlDbType.Structured,
                Value = requestTable
            };

            var parameters = new[]
            {
                requestTableparameter,
                new SqlParameter("@TimeslotId", request.TimeslotId)
            };

            return DatabaseUtil.ExecuteNonQuery("AddRequestedSeatsToTimeslot", parameters) != 0;
        }
    }
}