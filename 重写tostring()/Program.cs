﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 重写tostring__
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            Console.WriteLine(p.ToString());
        }

    }

    public class Person
    {
        public override string ToString()
        {
            return "Hello World";
        }

    }
}
