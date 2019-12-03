using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cinema.Attributes;
using Cinema.Models.Tickets;
using Cinema.Services;
using Newtonsoft.Json;

namespace Cinema.Controllers
{
    public class TicketsAdminController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsAdminController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public ActionResult GetMoviesList()
        {
            var movies = _ticketService.GetAllMovies();
            return View("~/Views/TicketsAdmin/MoviesList.cshtml", movies);
        }

        [HttpGet]
        public ActionResult EditMovie(int movieId)
        {
            var movie = _ticketService.GetMovieById(movieId);
            return View("~/Views/TicketsAdmin/EditMovie.cshtml", movie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie updatedMovie)
        {
            var updateResult = _ticketService.UpdateMovie(updatedMovie);
            if (updateResult)
                return RedirectToAction("GetMoviesList");

            return Content("Update failed. Plese, contact system administrator.");
        }

        [HttpGet]
        public ActionResult RemoveMovie(int movieId)
        {
            var removeResult = _ticketService.RemoveMovie(movieId);
            if (removeResult)
                return RedirectToAction("GetMoviesList");

            return Content("Removing failed. Please, contact system administrator.");
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
            return View("~/Views/TicketsAdmin/AddMovie.cshtml");
        }

        [HttpPost]
        public ActionResult AddMovie(Movie newMovie)
        {
            var creationResult = _ticketService.CreateMovie(newMovie);
            if (creationResult)
                return RedirectToAction("GetMoviesList");

            return Content("Update failed. Plese, contact system administrator.");
        }

        public ActionResult GetHallsList()
        {
            var halls = _ticketService.GetAllHalls();
            return View("~/Views/TicketsAdmin/HallsList.cshtml", halls);
        }

        [HttpGet]
        public ActionResult EditHall(int hallId)
        {
            var hall = _ticketService.GetHallById(hallId);
            return View("~/Views/TicketsAdmin/EditHall.cshtml", hall);
        }

        [HttpPost]
        public ActionResult EditHall(Hall updatedHall)
        {
            var updateResult = _ticketService.UpdateHall(updatedHall);
            if (updateResult)
                return RedirectToAction("GetHallsList");

            return Content("Update failed. Plese, contact system administrator.");
        }

        [HttpGet]
        public ActionResult RemoveHall(int hallId)
        {
            var removeResult = _ticketService.RemoveHall(hallId);
            if (removeResult)
                return RedirectToAction("GetHallsList");

            return Content("Removing failed. Please, contact system administrator.");
        }

        [HttpGet]
        public ActionResult AddHall()
        {
            return View("~/Views/TicketsAdmin/AddHall.cshtml");
        }

        [HttpPost]
        public ActionResult AddHall(Hall newHall)
        {
            var creationResult = _ticketService.CreateHall(newHall);
            if (creationResult)
                return RedirectToAction("GetHallsList");

            return Content("Update failed. Please, contact system administrator.");
        }

        public ActionResult GetTariffsList()
        {
            var tariffs = _ticketService.GetAllTariffs();
            return View("~/Views/TicketsAdmin/TariffsList.cshtml", tariffs);
        }

        [HttpGet]
        public ActionResult EditTariff(int tariffId)
        {
            var tariff = _ticketService.GetTariffById(tariffId);
            return View("~/Views/TicketsAdmin/EditTariff.cshtml", tariff);
        }

        [HttpPost]
        public ActionResult EditTariff(Tariff updatedTariff)
        {
            var updateResult = _ticketService.UpdateTariff(updatedTariff);
            if (updateResult)
                return RedirectToAction("GetTariffsList");

            return Content("Update failed. Plese, contact system administrator.");
        }

        [HttpGet]
        public ActionResult RemoveTariff(int tariffId)
        {
            var removeResult = _ticketService.RemoveTariff(tariffId);
            if (removeResult)
                return RedirectToAction("GetTariffsList");

            return Content("Removing failed. Please, contact system administrator.");
        }

        [HttpGet]
        public ActionResult AddTariff()
        {
            return View("~/Views/TicketsAdmin/AddTariff.cshtml");
        }

        [HttpPost]
        public ActionResult AddTariff(Tariff newTariff)
        {
            var creationResult = _ticketService.CreateTariff(newTariff);
            if (creationResult)
                return RedirectToAction("GetTariffsList");

            return Content("Update failed. Please, contact system administrator.");
        }

        public ActionResult GetTimeslotsList()
        {
            var timeslots = _ticketService.GetAllTimeslots();
            var resultModel = ProcessTimeslots(timeslots);
            return View("~/Views/TicketsAdmin/TimeslotsList.cshtml", resultModel);
        }

        public ActionResult GetMovieTimeslotsList(int movieId)
        {
            var timeslots = _ticketService.GetTimeslotsByMovieId(movieId);
            var resultModel = ProcessTimeslots(timeslots);
            return View("~/Views/TicketsAdmin/TimeslotsList.cshtml", resultModel);
        }

        private TimeslotGridRow[] ProcessTimeslots(Timeslot[] timeslots)
        {
            var movies = _ticketService.GetAllMovies();
            var halls = _ticketService.GetAllHalls();
            var tariffs = _ticketService.GetAllTariffs();

            var resultModel = new List<TimeslotGridRow>();
            foreach (var timeslot in timeslots)
            {
                resultModel.Add(new TimeslotGridRow
                {
                    Id = timeslot.Id,
                    StartTime = timeslot.StartTime,
                    HallName = halls.FirstOrDefault(x => x.Id == timeslot.HallId)?.Name ?? "Hall not found",
                    MovieName = movies.FirstOrDefault(x => x.Id == timeslot.MovieId)?.Name ?? "Movie not found",
                    TariffName = tariffs.FirstOrDefault(x => x.Id == timeslot.TariffId)?.Name ?? "Tariff not found"
                });
            }

            return resultModel.ToArray();
        }

        [HttpGet]
        [PopulateMoviesList, PopulateHallsList, PopulateTariffsList]
        public ActionResult EditTimeslot(int timeslotId)
        {
            var timeslot = _ticketService.GetTimeslotById(timeslotId);
            return View("~/Views/TicketsAdmin/EditTimeslot.cshtml", timeslot);
        }

        [HttpPost]
        public ActionResult EditTimeslot(Timeslot updatedTimeslot)
        {
            var updateResult = _ticketService.UpdateTimeslot(updatedTimeslot);
            if (updateResult)
                return RedirectToAction("GetTimeslotsList");

            return Content("Update failed. Please, contact system administrator.");
        }

        [HttpGet]
        public ActionResult RemoveTimeslot(int timeslotId)
        {
            var removeResult = _ticketService.RemoveTimeslot(timeslotId);
            if (removeResult)
                return RedirectToAction("GetTimeslotsList");

            return Content("Removing failed. Please, contact system administrator.");
        }

        [HttpGet]
        [PopulateMoviesList, PopulateHallsList, PopulateTariffsList]
        public ActionResult AddTimeslot()
        {
            return View("~/Views/TicketsAdmin/AddTimeslot.cshtml");
        }

        [HttpPost]
        public ActionResult AddTimeslot(Timeslot newTimeslot)
        {
            var creationResult = _ticketService.CreateTimeslot(newTimeslot);
            if (creationResult)
                return RedirectToAction("GetTimeslotsList");

            return Content("Update failed. Please, contact system administrator.");
        }

        public ActionResult FindMovieById(int id)
        {
            var movie = _ticketService.GetMovieById(id);
            if (movie == null)
                return Content("Movie with such id do not exist");

            var modelJson = JsonConvert.SerializeObject(movie);
            return Content(modelJson, "application/json");
        }

        public ActionResult FindHallById(int id)
        {
            var hall = _ticketService.GetHallById(id);
            if (hall == null)
                return Content("Hall with such id do not exist");

            var modelJson = JsonConvert.SerializeObject(hall);
            return Content(modelJson, "application/json");
        }

        public ActionResult FindTimeslotById(int id)
        {
            var timeslot = _ticketService.GetTimeslotById(id);
            if (timeslot == null)
                return Content("Timeslot with such id do not exist");

            var modelJson = JsonConvert.SerializeObject(timeslot);
            return Content(modelJson, "application/json");
        }

        public ActionResult FindTariffById(int id)
        {
            var tariff = _ticketService.GetTariffById(id);
            if (tariff == null)
                return Content("Tariff with such id do not exist");

            var modelJson = JsonConvert.SerializeObject(tariff);
            return Content(modelJson, "application/json");
        }
    }
}