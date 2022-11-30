using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCourse.Models
{
    public class Event
    {
        private int _id;
        private DateTime _date;
        private string _name;
        public int Id { get { return _id; } set { _id = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public string Name { get { return _name; } set { _name = value; } }

        public Event(DateTime date, string name)
        {
            _id = -1;
            _date = date;
            _name = name;
        }
    }
}
