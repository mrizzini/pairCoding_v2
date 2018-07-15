using System;
using System.Collections.Generic;

namespace pairCoding_v2
{
    abstract class Publication
    {
        public DateTime PublishDate {get; set;} 

        abstract public DateTime DueDate {get;}

        abstract public double fee {get;}

        public DateTime DayReturned {get; set;}

        abstract public int checkoutLength {get;}

        public  double lateFee()
        {
            var late = this.DaysLate();
            if (late>0)
            return fee  * late;
            else
                return 0;
        }

         public int DaysLate()
        {
            TimeSpan difference = DayReturned.Date - DueDate.Date;
            var days = (int) difference.TotalDays;
            Console.WriteLine("Days Late: " + days);
            return days;
        }
    }

    class Book : Publication
    {
        public string Author {get; set;}
        public string Title {get; set;}

        public override int checkoutLength 
        {
            get{return 7;}
        }

        public override double fee
        {
            get {return .25;}
        }

        public override DateTime DueDate
        {
            get
            {
               return DateTime.Now.AddDays(checkoutLength);
            }
        }
        public Book ()
        {
        }

        public Book (string _author, string _title, DateTime _date)
        {
            this.Author = _author;
            this.Title = _title;
            this.PublishDate = _date;
        }
    }

    class Magazine : Publication
    {
 
 
    }

    class Customer
    {
        
        
    }
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
