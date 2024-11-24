namespace MethodsParallellComputation;

using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static int[]? _cells;

    private static int _n;
    private static int _k;
    private static double _p;

    static List<Thread> _threads = new List<Thread>();

    private static bool _stop = false;

    static void Main(string[] args)
    {
        _n = int.Parse(args[0]);
        _k = int.Parse(args[1]);
        _p = double.Parse(args[2]);
        Console.WriteLine($"N = {_n}, K = {_k}, P = {_p}");
        _cells = new int[_n];
        _cells[0] = _k;

        for (int i = 0; i < _k; i++)
        {
            Thread t = new Thread(() => SimulateMethod());
            _threads.Add(t);
            t.Start();
        }

        Thread oneThread = new Thread(() => CellOutput());
        oneThread.Start();

        Thread.Sleep(10000);
        _stop = true;

        foreach (var t in _threads)
        {
            t.Join();
        }

        oneThread.Join();

        int number = 0;
        for (int i = 0; i < _n; i++)
        {
            number += _cells[i];
        }

        Console.WriteLine($"Atoms count: {number}");
    }

    static void SimulateMethod()
    {
        Random rand = new Random(Thread.CurrentThread.ManagedThreadId + DateTime.Now.Millisecond);
        int position = 0;

        while (!_stop)
        {
            double value = rand.NextDouble();
            int newCellPosition = value > _p ? position + 1 : position - 1;
            newCellPosition = newCellPosition < 0 || newCellPosition >= _n ? position : newCellPosition;

            _cells[position]--;
            _cells[newCellPosition]++;
            position = newCellPosition;
            Thread.Sleep(1);
        }
    }

    static void CellOutput()
    {
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(1000);
            for (int j = 0; j < _n; j++)
            {
                Console.Write($"{_cells[j]} ");
            }

            Console.WriteLine();
        }
    }
}