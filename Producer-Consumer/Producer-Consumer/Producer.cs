using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Producer_Consumer
{
    public class Producer
    {
        private Timer timer = new Timer(5000);

        private int i = 1;

        private bool disposed;

        public event EventHandler BatchAvailable;

        public void Start()
        {
            this.timer.Elapsed += this.TimerOnElapsed;
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
            this.timer.Elapsed -= this.TimerOnElapsed;
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.Stop();
            this.timer.Dispose();
            this.disposed = true;
        }

        public IEnumerable<int> GetNextBatch()
        {
            var range = Enumerable.Range(this.i, this.i + 10);

            this.i += 10;
            return range;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            this.BatchAvailable(sender, e);
        }
    }
}