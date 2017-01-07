using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;


namespace Producer_Consumer
{
    public class Consumer
    {
        private BlockingCollection<int> entries = new BlockingCollection<int>();

        private TaskFactory factory = new TaskFactory();

        private CancellationTokenSource tokenSource;

        private Task task;

        public void Start()
        {
            try
            {
                this.tokenSource = new CancellationTokenSource();
                this.task = this.factory.StartNew(
                    () =>
                    {
                        Parallel.ForEach(
                            this.entries.GetConsumingEnumerable(),
                            new ParallelOptions { MaxDegreeOfParallelism = 3, CancellationToken = tokenSource.Token },
                            (i, loopState) =>
                            {

                                //Console.WriteLine("Cancellation Requested: " + this.tokenSource.IsCancellationRequested);
                                if (!this.tokenSource.IsCancellationRequested)
                                {
                                    ProcessEntry(i);
                                }
                                else
                                {
                                    this.entries.CompleteAdding();
                                    Console.WriteLine("Collection Completely Added!");
                                    loopState.Stop();
                                }
                            });
                    },
                    this.tokenSource.Token);
            }
            catch (OperationCanceledException oce)
            {
                System.Diagnostics.Debug.WriteLine(oce);
            }
        }

        public void Stop()
        {
            this.Dispose();
        }

        public void Add(int entry)
        {
            this.entries.Add(entry);
        }

        public void Dispose()
        {
            if (this.task == null)
            {
                return;
            }

            this.tokenSource.Cancel();
            while (!this.task.IsCanceled)
            {
            }

            this.task.Dispose();
            this.tokenSource.Dispose();
            this.task = null;
        }

        public bool Ready
        {
            get
            {
                return this.entries.Count == 0;
            }
        }

        private static void ProcessEntry(int entry)
        {
            Console.WriteLine("Processing {0}", entry);
            Thread.Sleep(2000);
        }
    }
}
