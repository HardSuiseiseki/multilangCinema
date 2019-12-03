using System;
using System.Collections.Generic;
using System.Linq;
using Cinema.Models.Tickets;
using Cinema.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeworkIssuesTest
{
    [TestClass]
    public class Homework1Issues
    {
        [TestMethod]
        public void Nazarenko_GetTimeslotsByMovieId_TakeWhile()
        {
            var timeslots = GetDataFromFile().Timeslots;

            var relatedTimeslots = timeslots.TakeWhile(timeslot => timeslot.MovieId == 1);
        }

        [TestMethod]
        public void Ershova_GetTimeslotsByMovieId_TakeWhile()
        {
            var movieId = 1;
            var fullModel = GetDataFromFile();
            var timeSlots = fullModel.Timeslots.Where(x => x.MovieId == movieId).Select(x => x);
            Timeslot[] result = new Timeslot[timeSlots.Count()];
            int index = 0;
            foreach (var timeSlot in timeSlots)
            {
                result[index++] = timeSlot;
            }
        }

        private TicketsJsonModel GetDataFromFile()
        {
            var timeslots = new List<Timeslot>
            {
                new Timeslot {Id = 1, MovieId = 1},
                new Timeslot {Id = 2, MovieId = 1},
                new Timeslot {Id = 3, MovieId = 1},
                new Timeslot {Id = 4, MovieId = 2},
                new Timeslot {Id = 5, MovieId = 2},
                new Timeslot {Id = 6, MovieId = 2}
            };
            var jsonModel = new TicketsJsonModel
            {
                Timeslots = timeslots.ToArray()
            };
            return jsonModel;
        }
    }
}
