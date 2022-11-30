using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCourse.Models
{
    public class Shop
    {
        private int _Num;
        private int _Income;

        public Shop(int num, int income)
        {
            _Num = num;
            _Income = income;
        }
        [Key]
        public int Num { get { return _Num; } set { _Num = value; } }
        public int Income { get { return _Income; } set { _Income = value;} }
    }
}
