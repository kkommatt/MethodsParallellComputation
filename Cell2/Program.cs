using System.Text;

namespace Cell2;

class Program
{
    private static int[]? _cells;
    private static object[]? _cellsLock;
    private static int _n;
    private static int _k;
    private static double _p;

    private static bool _stop = false;
    private static Task[] _atomTasks;

    public static void Main(string[] args)
    {
        _n = int.Parse(args[0]);
        _k = int.Parse(args[1]);
        _p = double.Parse(args[2]);
        Console.WriteLine($"N = {_n}, K = {_k}, P = {_p}");

        _cells = new int[_n];
        _cells[0] = _k;
        _cellsLock = new object[_n];
        for (int i = 0; i < _n; i++)
        {
            _cellsLock[i] = new object();
        }

        _atomTasks = new Task[_k];
        for (int i = 0; i < _k; i++)
        {
            _atomTasks[i] = Task.Run(() => ParticleRun());
        }

        for (int second = 1; second <= 60; second++)
        {
            Thread.Sleep(1000);
            for (int j = 0; j < _n; j++)
            {
                lock (_cellsLock[j])
                {
                    Console.Write($"{_cells[j]} ");
                }
            }

            Console.WriteLine("");
        }

        _stop = true;

        try
        {
            Task.WaitAll(_atomTasks);
        }
        catch (AggregateException)
        {
        }

        int count = 0;
        for (int i = 0; i < _n; i++)
        {
            lock (_cellsLock[i])
            {
                count += _cells[i];
            }
        }

        Console.WriteLine($"Atom count: {count}");
    }

    private static void ParticleRun()
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());
        int cell = 0;

        while (!_stop)
        {
            double value = random.NextDouble();

            int newPosition = value > _p ? cell + 1 : cell - 1;
            newPosition = newPosition < 0 || newPosition >= _n ? cell : newPosition;

            int firstIndex, secondIndex;

            if (cell < newPosition)
            {
                firstIndex = cell;
                secondIndex = newPosition;
            }
            else
            {
                firstIndex = newPosition;
                secondIndex = cell;
            }

            var firstLock = _cellsLock[firstIndex];
            var secondLock = _cellsLock[secondIndex];

            lock (firstLock)
            {
                lock (secondLock)
                {
                    _cells[cell]--;
                    _cells[newPosition]++;
                }
            }


            cell = newPosition;

            try
            {
                Thread.Sleep(100);
            }
            catch (ThreadInterruptedException)
            {
                break;
            }
        }
    }
}