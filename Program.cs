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
        public string name {get; set;}
        public override int checkoutLength 
        {
            get{return 14;}
        }

        public override double fee
        {
            get{return .5;}
        }

        public override DateTime DueDate
        {
            get
            {
               return DateTime.Now.AddDays(checkoutLength);
            }
        }
        public Magazine ()
        {
        }

        public Magazine (string _name, DateTime _date)
        {
            this.name = _name;
            this.PublishDate = _date;
        }
    }

    class Customer
    {
        public string Name {get; set;}
        public int age {get; set;}

        public double finesDue = 0;

        public static List<Publication> CheckedOut = new List<Publication>();

        public Customer()
        {

        }
        public Customer(string _name, int _age){
            this.Name = _name;
            this.age = _age;
        }

        public DateTime Transaction(Publication pub)
        {
            CheckedOut.Add(pub);
            Console.WriteLine("Due on: " + pub.DueDate);
            return pub.DueDate;
        }
        public double Transaction(DateTime returnDate)
        {   
            foreach (var pub in CheckedOut)
            {
                pub.DayReturned = returnDate;
                finesDue += pub.lateFee();
            }
            Console.WriteLine("returned " + CheckedOut.Count + " items ");
            CheckedOut.Clear();
            Console.WriteLine("Late fines due: " + finesDue);
            return finesDue;
        }

        public double PayFine()
        {
            Console.WriteLine("paid fine of: " + finesDue);
            finesDue = 0;
            Console.WriteLine("fine balance is: " + finesDue);
            return finesDue;
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var book1 = new Book();
                book1.Author = "CareerDevs";
                book1.Title = "C# for Dummies";
            var magazine1 = new Magazine();
                magazine1.name = "Learning C#";

            var magazine2 = new Magazine();
                magazine2.name = "another magazine";
            var magazine3 = new Magazine();
                magazine3.name = "third magazine";

            var customer1 = new Customer("Jim", 32);
            var customer2 = new Customer("Sierra", 10);


            customer1.Transaction(book1);
            customer1.Transaction(magazine1);
            customer1.Transaction(new DateTime (2018,07,25));

            customer2.Transaction(magazine2);
            customer2.Transaction(magazine3);
            customer2.Transaction(new DateTime (2018, 07, 16));

            customer1.PayFine();

        }
    }
}
