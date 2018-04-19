using DAL.Core.Models;
using System;

namespace DAL.UnitTests.Helpers
{
    internal static class RandomizerHelper
    {
        public static Event GetNewRandomEvent()
        {
            return new Event()
            {
                Id = GetRandomInt(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                Price = GetRandomDecimal(),
                StudentPrice = GetRandomDecimal(),
                Date = GetCurrentDate(),
                Place = GetRandomString()
            };
        }

        public static Student GetNewRandomStudent()
        {
            return new Student()
            {
                Id = GetRandomInt(),
                Email = GetRandomString()
            };
        }

        public static Ticket GetNewRandomTicket()
        {
            return new Ticket()
            {
                Id = GetRandomInt(),
                EventId = GetRandomInt(),
                IsUsed = GetRandomBool(),
                StudentId = RandomizerHelper.GetRandomInt()
            };
        }

        public static string GetRandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public static int GetRandomInt()
        {
            return new Random().Next();
        }

        public static int GetRandomNegativeInt()
        {
            return new Random().Next(int.MinValue, -1);
        }

        public static decimal GetRandomDecimal()
        {
            return new decimal(GetRandomInt());
        }

        public static bool GetRandomBool()
        {
            return GetRandomInt() % 2 == 0;
        }

        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
