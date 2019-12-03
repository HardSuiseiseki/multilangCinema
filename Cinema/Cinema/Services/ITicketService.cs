using Cinema.Models.Tickets;

namespace Cinema.Services
{
    public interface ITicketService
    {
        Movie GetMovieById(int id);
        bool RemoveMovie(int id);
        bool UpdateMovie(Movie updatedMovie);
        bool CreateMovie(Movie newMovie);
        Movie[] GetAllMovies();

        Hall GetHallById(int id);
        bool RemoveHall(int id);
        bool UpdateHall(Hall updatedHall);
        bool CreateHall(Hall newHall);
        Hall[] GetAllHalls();

        Tariff GetTariffById(int id);
        bool RemoveTariff(int id);
        bool UpdateTariff(Tariff updatedTariff);
        bool CreateTariff(Tariff newTariff);
        Tariff[] GetAllTariffs();

        Timeslot GetTimeslotById(int id);
        bool RemoveTimeslot(int id);
        bool UpdateTimeslot(Timeslot updatedTimeslot);
        bool CreateTimeslot(Timeslot newTimeslot);
        Timeslot[] GetTimeslotsByMovieId(int movieId);
        Timeslot[] GetAllTimeslots();

        MovieListItem[] SearchMoviesByTerm(string term);
        MovieListItem[] GetFullMoviesInfo();
        TimeslotTag[] GetTimeslotTagsByMovieId(int movid);
        bool AddRequestedSeatsToTimeslot(SeatsProcessRequest request);
    }
}
