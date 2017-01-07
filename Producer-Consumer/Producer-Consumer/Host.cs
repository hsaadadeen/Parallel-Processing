using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer_Consumer
{
    public class Host
    {
        private readonly Producer producer = new Producer();

        private readonly Consumer consumer = new Consumer();

        private bool disposed;

        public Host()
        {
            this.producer.BatchAvailable += (s, e) => this.ProducerOnBatchAvailable();
        }

        public void Start()
        {
            this.producer.Start();
            this.consumer.Start();
        }

        public void Stop()
        {
            this.producer.Stop();
            this.consumer.Stop();
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.Stop();
            this.producer.Dispose();
            this.consumer.Dispose();
            this.disposed = true;
        }

        private void ProducerOnBatchAvailable()
        {
            if (!this.consumer.Ready)
            {
                Console.WriteLine("Consumer is Not ready");
                return;
            }

            Console.WriteLine("Producer is ready for another Batch...");

            var batch = this.producer.GetNextBatch().ToList();

            batch.ForEach(this.consumer.Add);
        }
    }
}
