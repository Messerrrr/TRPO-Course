using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCourse.Models
{
    public class Book : ICloneable
    {
        private int id;
        private string namebook;
        private string surname;
        private int shopnum;
        private int cost;
        private int sold;
        private int remain;

        public Book(string namebook, string surname, int shopnum, int cost, int sold, int remain)
        {
            this.id = -1;
            this.namebook = namebook;
            this.surname = surname;
            this.shopnum = shopnum;
            this.cost = cost;
            this.sold = sold;
            this.remain = remain;
        }
        public Book()
        { }
        public int Id { get { return id; } set { id = value; } }
        public string Namebook { get { return namebook; } set { namebook = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public int ShopNum { get { return shopnum; } set { shopnum = value; } }
        public int Cost { get { return cost; } set { cost = value; } }
        public int Sold { get { return sold; } set { sold = value; } }
        public int Remain { get { return remain; } set { remain = value; } }




        //protected bool Equals(Book other)
        //{
        //    return namebook == other.namebook && surname == other.surname && cost == other.cost && remain == other.remain;
        //}
        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj)) return false;
        //    if (ReferenceEquals(this, obj)) return true;
        //    if (obj.GetType() != GetType()) return false;
        //    return Equals((Book)obj);
        //}
        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(namebook, surname, numofshop, cost, sold, remain);
        //}
        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
