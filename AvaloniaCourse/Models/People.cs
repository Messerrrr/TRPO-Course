using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCourse.Models
{
    public class People
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _patronymic;
        private string _email;
        private string _phone;
        private string _job;
        private string _function;

        public People(string name, string surname, string patronymic, string email, string phone, string job, string function)
        {
            _id = -1;
            _name = name;
            _surname = surname;
            _patronymic = patronymic;
            _email = email;
            _phone = phone;
            _job = job;
            _function = function;
        }
        public People()
        { }
        [Key]
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public string Patronymic { get { return _patronymic; } set { _patronymic = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }
        public string Job { get { return _job; } set { _job = value; } }
        public string Function { get { return _function; } set { _function = value; } }
    }
}
