using System;
using System.Runtime.InteropServices;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = LinqTasks.Task1();
            foreach (var emp in t1)
                Console.WriteLine(emp.Ename);
            
            var t2 = LinqTasks.Task2();
            foreach (var emp in t2)
                Console.WriteLine(emp.Ename);

            var t3 = LinqTasks.Task3();
            Console.WriteLine(t3);
            
            var t4 = LinqTasks.Task4();
            foreach (var emp in t4)
                Console.WriteLine(emp.Ename);
            
            var t5 = LinqTasks.Task5();
            foreach (var emp in t5)
                Console.WriteLine(emp.ToString());
            
            var t6 = LinqTasks.Task6();
            foreach (var emp in t6)
                Console.WriteLine(emp.ToString());
            
            var t7 = LinqTasks.Task7();
            foreach (var emp in t7)
                Console.WriteLine(emp.ToString());
            
            var t8 = LinqTasks.Task8();
            Console.WriteLine(t8);
            
            var t9 = LinqTasks.Task9();
            Console.WriteLine(t9.Ename);
            
            var t10 = LinqTasks.Task10();
            foreach (var emp in t10)
                Console.WriteLine(emp.ToString());
            
            var t11 = LinqTasks.Task11();
            foreach (var emp in t11)
                Console.WriteLine(emp.ToString());
            
            var t12 = LinqTasks.Task12();
            foreach (var emp in t12)
                Console.WriteLine(emp.Ename);
            
            var t13 = LinqTasks.Task13(new int[]{1,1,1,1,1,1,10,1,1,1,1});
            Console.WriteLine(t13);
            
            var t14 = LinqTasks.Task14();
            foreach (var dept in t14)
                Console.WriteLine(dept.Dname);
            
            var t15 = LinqTasks.Task15();
            foreach (var dept in t15)
                Console.WriteLine(dept.Dname);
            
            var t16 = LinqTasks.Task16();
            foreach (var dept in t16)
                Console.WriteLine(dept.Dname);

            
            
            
            

            
            
            

        }
    }
}
