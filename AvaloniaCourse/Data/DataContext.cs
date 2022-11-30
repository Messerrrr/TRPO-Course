using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvaloniaCourse.Models;
using System.Threading.Tasks;
using AvaloniaCourse.ViewModels;
using System.Collections.ObjectModel;
using Npgsql;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace AvaloniaCourse.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Book> BookTable { get; set; } = null!;
        public DbSet<People> PeopleTable { get; set; } = null!;
        public DbSet<Event> EventTable { get; set; } = null!;

        private int _PriceAllRemain;

        private int _AveragePrice;

        public DataContext()
        {
           Database.EnsureCreated();
        }

        public int AveragePrice
        {
            get => _AveragePrice;
            set
            {
                _PriceAllRemain = value;
            }
        }
        public int PriceAllRemain
        {
            get => _PriceAllRemain;
            set
            {
                _PriceAllRemain = value;
            }
        }



        public void AddBook(Book book)
        {
            if (BookTable.Any())
            {
                book.Id = BookTable.Max(p => p.Id) + 1;
            }
            else
            {
                book.Id = 1;
            }
            BookTable.Add(book);
            SaveChanges();
        }
        
        public void RemoveBook(Book parameter)
        {
            BookTable.Remove(parameter);
            SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            List<Book> books = BookTable.ToList<Book>();
            var tmp = books.SingleOrDefault(p => p.Id == book.Id);
            if (tmp == null)
            {
                Trace.WriteLine($"Не удалось найти book с id={book.Id} для обновления.");
                return;
            }
            tmp.Namebook = book.Namebook;
            tmp.Surname = book.Surname;
            tmp.ShopNum = book.ShopNum;
            tmp.Cost = book.Cost;
            tmp.Remain = book.Remain;
            tmp.Sold = book.Sold;
            SaveChanges();
        }
        public void RecalculateValues()
        {
            var shelf = BookTable.ToList();
            for (int i = 0; i < shelf.Count; i++)
                PriceAllRemain += shelf[i].Sold * shelf[i].Remain;
            int remainnumber = 0;
            int allprice = 0;
            for (int i = 0; i < shelf.Count; i++)
            {
                remainnumber += shelf[i].Remain;
                allprice += shelf[i].Cost;
            }
            if (remainnumber != 0)
            AveragePrice = allprice / remainnumber;
            else AveragePrice = 0;
        }
        public void AddPerson(People person)
        {
            if (PeopleTable.Any())
            {
                person.Id = PeopleTable.Max(p => p.Id) + 1;
            }
            else
            {
                person.Id = 1;
            }
            PeopleTable.Add(person);
            SaveChanges();
        }

        public void RemovePerson(People parameter)
        {
            PeopleTable.Remove(parameter);
            SaveChanges();
        }

        public void UpdatePerson(People person)
        {
            List<People> Pepl = PeopleTable.ToList<People>();
            var tmp = Pepl.SingleOrDefault(p => p.Id == person.Id);
            if (tmp == null)
            {
                Trace.WriteLine($"Не удалось найти book с id={person.Id} для обновления.");
                return;
            }
            tmp.Surname = person.Surname;
            tmp.Name = person.Name;
            tmp.Patronymic = person.Patronymic;
            tmp.Email = person.Email;
            tmp.Phone = person.Phone;
            tmp.Job = person.Job;
            tmp.Function = person.Function;
            SaveChanges();
        }
        public void AddEvent(Event @event)
        {
            if (EventTable.Any())
            {
                @event.Id = EventTable.Max(p => p.Id) + 1;
            }
            else
            {
                @event.Id = 1;
            }
            EventTable.Add(@event);
            SaveChanges();
        }

        public void RemoveEvent(Event parameter)
        {
            EventTable.Remove(parameter);
            SaveChanges();
        }

        public void UpdateEvent(Event @event)
        {
            List<Event> Events = EventTable.ToList<Event>();
            var tmp = Events.SingleOrDefault(p => p.Id == @event.Id);
            if (tmp == null)
            {
                Trace.WriteLine($"Не удалось найти book с id={@event.Id} для обновления.");
                return;
            }
            tmp.Date = @event.Date;
            tmp.Name = @event.Name;
            SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConfigurationManager.AppSettings["ConnectionString"]);
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}
