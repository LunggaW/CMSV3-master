using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.UNITTEST
{
    public class Program
    {
        public static function CMSfunction = new function();
        private static DataTable DTuser = new DataTable();

        public static void Main(string[] args)
        {
            //DTuser = CMSfunction.GetAllUser();
            Console.ReadLine();
        }

    }
}
