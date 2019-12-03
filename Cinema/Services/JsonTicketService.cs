using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.Extensions;
using Cinema.Models.Tickets;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace Cinema.Services
{
    public class JsonTicketService : ITicketService
    {
        private HttpContext Context { get; set; }
        private const string PathToJson = "/Files/Tickets.json";

        public JsonTicketService()
        {
            Context = HttpContext.Current;
        }

        public Movie GetMovieById(int id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Movies.FirstOrDefault(movie => movie.Id == id);
        }

        public bool RemoveMovie(int id)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var existingMoviesList = fullModel.Movies.ToList();
                existingMoviesList.RemoveAll(x => x.Id == id);
                fullModel.Movies = existingMoviesList.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool UpdateMovie(Movie updatedMovie)
        {
            var fullModel = GetDataFromFile();
            var movieToUpdate = fullModel.Movies.FirstOrDefault(movie => movie.Id == updatedMovie.Id);
            if (movieToUpdate == null)
                return false;

            movieToUpdate.Name = updatedMovie.Name;
            movieToUpdate.Description = updatedMovie.Description;
            if (updatedMovie.Genres != null)
            {
                movieToUpdate.Genres = updatedMovie.Genres;
            }
            movieToUpdate.ImageUrl = updatedMovie.ImageUrl;
            movieToUpdate.MinAge = updatedMovie.MinAge;
            movieToUpdate.Duration = updatedMovie.Duration;
            movieToUpdate.Rating = updatedMovie.Rating;
            if (updatedMovie.Types != null)
            {
                movieToUpdate.Types = updatedMovie.Types;
            }

            SaveDataToFile(fullModel);
            return true;
        }

        public bool CreateMovie(Movie newMovie)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newMovieId = 1;
                if (fullModel.Movies != null && fullModel.Movies.Any())
                {
                    newMovieId = fullModel.Movies.Max(movie => movie.Id) + 1;
                }
                newMovie.Id = newMovieId;
                var existingMoviesList = fullModel.Movies?.ToList() ?? new List<Movie>();
                existingMoviesList.Add(newMovie);
                fullModel.Movies = existingMoviesList.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Movie[] GetAllMovies()
        {
            var fullModel = GetDataFromFile();
            return fullModel.Movies;
        }

        public Hall GetHallById(int id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Halls.FirstOrDefault(halls => halls.Id == id);
        }

        public bool RemoveHall(int id)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var existingHallsList = fullModel.Halls.ToList();
                existingHallsList.RemoveAll(x => x.Id == id);
                fullModel.Halls = existingHallsList.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool UpdateHall(Hall updatedHall)
        {
            var fullModel = GetDataFromFile();
            var hallToUpdate = fullModel.Halls.FirstOrDefault(hall => hall.Id == updatedHall.Id);
            if (hallToUpdate == null)
                return false;

            hallToUpdate.Name = updatedHall.Name;

            SaveDataToFile(fullModel);
            return true;
        }

        public bool CreateHall(Hall newHall)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newHallId = 1;
                if (fullModel.Halls != null && fullModel.Halls.Any())
                {
                    newHallId = fullModel.Halls.Max(hall => hall.Id) + 1;
                }
                newHall.Id = newHallId;
                var existingHallsList = fullModel.Halls?.ToList() ?? new List<Hall>();
                existingHallsList.Add(newHall);
                fullModel.Halls = existingHallsList.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Hall[] GetAllHalls()
        {
            var fullModel = GetDataFromFile();
            return fullModel.Halls;
        }

        public Tariff GetTariffById(int id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Tariffs.FirstOrDefault(tariff => tariff.Id == id);
        }

        public bool RemoveTariff(int id)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var existingTariffsList = fullModel.Tariffs.ToList();
                var tariffToDelete = GetTariffById(id);
                existingTariffsList.Remove(tariffToDelete);
                fullModel.Tariffs = existingTariffsList.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool UpdateTariff(Tariff updatedTariff)
        {
            var fullModel = GetDataFromFile();
            var tariffToUpdate = fullModel.Tariffs.FirstOrDefault(tariff => tariff.Id == updatedTariff.Id);
            if (tariffToUpdate == null)
                return false;

            tariffToUpdate.Name = updatedTariff.Name;
            tariffToUpdate.Cost = updatedTariff.Cost;

            SaveDataToFile(fullModel);
            return true;
        }

        public bool CreateTariff(Tariff newTariff)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newTariffId = 1;
                if (fullModel.Tariffs != null && fullModel.Tariffs.Any())
                {
                    newTariffId = fullModel.Tariffs.Max(tariff => tariff.Id) + 1;
                }
                newTariff.Id = newTariffId;
                var existingTariffsList = fullModel.Tariffs?.ToList() ?? new List<Tariff>();
                existingTariffsList.Add(newTariff);
                fullModel.Tariffs = existingTariffsList.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Tariff[] GetAllTariffs()
        {
            var fullModel = GetDataFromFile();
            return fullModel.Tariffs;
        }

        public Timeslot GetTimeslotById(int id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Timeslots.FirstOrDefault(timeslot => timeslot.Id == id);
        }

        public bool RemoveTimeslot(int id)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var existingTimeslotsList = fullModel.Timeslots.ToList();
                existingTimeslotsList.RemoveAll(x => x.Id == id);
                fullModel.Timeslots = existingTimeslotsList.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool UpdateTimeslot(Timeslot updatedTimeslot)
        {
            var fullModel = GetDataFromFile();
            var timeslotToUpdate = fullModel.Timeslots.FirstOrDefault(timeslot => timeslot.Id == updatedTimeslot.Id);
            if (timeslotToUpdate == null)
                return false;

            timeslotToUpdate.StartTime = updatedTimeslot.StartTime;
            timeslotToUpdate.HallId = updatedTimeslot.HallId;
            timeslotToUpdate.MovieId = updatedTimeslot.MovieId;
            timeslotToUpdate.TariffId = updatedTimeslot.TariffId;

            SaveDataToFile(fullModel);
            return true;
        }

        public bool CreateTimeslot(Timeslot newTimeslot)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newTimeslotId = 1;
                if (fullModel.Timeslots != null && fullModel.Timeslots.Any())
                {
                    newTimeslotId = fullModel.Timeslots.Max(timeslot => timeslot.Id) + 1;
                }
                newTimeslot.Id = newTimeslotId;
                var existingTimeslotList = fullModel.Timeslots?.ToList() ?? new List<Timeslot>();
                existingTimeslotList.Add(newTimeslot);
                fullModel.Timeslots = existingTimeslotList.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Timeslot[] GetTimeslotsByMovieId(int movieId)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Timeslots.Where(x => x.MovieId == movieId).ToArray();
        }

        public Timeslot[] GetAllTimeslots()
        {
            var fullModel = GetDataFromFile();
            return fullModel.Timeslots;
        }

        public MovieListItem[] SearchMoviesByTerm(string term)
        {
            var allMovies = GetFullMoviesInfo();
            var filteredList = allMovies.Where(x => x.Movie.Description.CaseInsensitiveContains(term) || x.Movie.Name.CaseInsensitiveContains(term));
            return filteredList.ToArray();
        }

        public MovieListItem[] GetFullMoviesInfo()
        {
            var allMovies = GetAllMovies();
            var resultModel = new List<MovieListItem>();
            foreach (var movie in allMovies)
            {
                resultModel.Add(new MovieListItem
                {
                    Movie = movie,
                    AvailableTimeslots = GetTimeslotTagsByMovieId(movie.Id)
                });
            }

            return resultModel.ToArray();
        }

        public TimeslotTag[] GetTimeslotTagsByMovieId(int movid)
        {
            var timeslots = GetTimeslotsByMovieId(movid);
            var tariffs = GetAllTariffs();
            var resultModel = new List<TimeslotTag>();
            foreach (var timeslot in timeslots)
            {
                resultModel.Add(new TimeslotTag
                {
                    TimeslotId = timeslot.Id,
                    StartTime = timeslot.StartTime,
                    Cost = tariffs.FirstOrDefault(x => x.Id == timeslot.TariffId)?.Cost ?? 0
                });
            }

            return resultModel.ToArray();
        }

        public bool AddRequestedSeatsToTimeslot(SeatsProcessRequest request)
        {
            var fullModel = GetDataFromFile();
            var timeslotToUpdate = fullModel.Timeslots.
                FirstOrDefault(timeslot => timeslot.Id == request.TimeslotId);
            if (timeslotToUpdate == null)
                return false;

            List<TimeslotSeatRequest> requestToProcess;
            if (timeslotToUpdate.RequestedSeats != null && timeslotToUpdate.RequestedSeats.Any())
            {
                requestToProcess = timeslotToUpdate.RequestedSeats.ToList();
            }
            else
            {
                requestToProcess = new List<TimeslotSeatRequest>();
            }

            if (request?.SeatsRequest?.AddedSeats == null || !request.SeatsRequest.AddedSeats.Any())
                return false;

            foreach (var addedSeats in request.SeatsRequest.AddedSeats)
            {
                requestToProcess.Add(new TimeslotSeatRequest
                {
                    Row = addedSeats.Row,
                    Seat = addedSeats.Seat,
                    Status = request.SelectedStatus
                });
            }

            timeslotToUpdate.RequestedSeats = requestToProcess.ToArray();
            SaveDataToFile(fullModel);
            return true;
        }

        private void SaveDataToFile(TicketsJsonModel fullModel)
        {
            var jsonFilePath = Context.Server.MapPath(PathToJson);
            var serializedModel = JsonConvert.SerializeObject(fullModel);
            System.IO.File.WriteAllText(jsonFilePath, serializedModel);
        }

        private TicketsJsonModel GetDataFromFile()
        {
            var jsonFilePath = Context.Server.MapPath(PathToJson);
            if (!System.IO.File.Exists(jsonFilePath))
                return null;

            var jsonModel = System.IO.File.ReadAllText(jsonFilePath);
            var deserializedModel = JsonConvert.DeserializeObject<TicketsJsonModel>(jsonModel);
            return deserializedModel;
        }
    }
}