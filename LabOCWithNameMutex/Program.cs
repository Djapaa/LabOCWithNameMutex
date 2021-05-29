using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

class Пример
{
    public static Random rnd = new Random();
    public static Mutex m = new Mutex(false, "MyMutex");
    public static Thread t1, t2, t3;
    public static List<int> myList = new List<int>();
    public static int i;
    public static bool Ready1, Ready2;

    public static void DoWork(string Источник, int num)
    {
        Thread.Sleep(rnd.Next(100));
        m.WaitOne();
        myList.Add(num);
        Console.WriteLine("{0}:{1}", Источник, num);
        Thread.Sleep(rnd.Next(100));
        m.ReleaseMutex();
    }

    public static void thread1()
    {
        for (i = 1; i <= 100; i++)
        {
            //int nextnum = rnd.Next(min / 2, max / 2) * 2;
            if (i % 2 == 0)
                DoWork("Поток 1", i);
        }
        Ready1 = true;
    }

    public static void thread2()
    {
        for (i = 1; i <= 100; i++)
        {
            if (i % 2 == 1)
                DoWork("Поток 2", i);

            // int nextnum = rnd.Next(min / 2, max / 2) * 2 + 1;
        }
        Ready2 = true;
    }
    public static void thread3()
    {
        foreach (int a in myList)
            Console.WriteLine(a);
    }

    public static void Main()
    {
        Ready1 = false;
        Ready2 = false;
        t1 = new Thread(thread1);
        t2 = new Thread(thread2);
        t3 = new Thread(thread3);

        t1.Start();
        t2.Start();

        while (!Ready1 && !Ready2) ;
        t3.Start();
        t3.Join();
        Console.ReadLine();
    }


}
