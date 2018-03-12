using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    [Serializable]
    class WorkerProcessingExeption : Exception
    {
        public int WorkerId { get; private set; }

        public WorkerProcessingExeption(int workerId)
        {
            WorkerId = workerId;
        }

        public WorkerProcessingExeption(int workerId, string message) : base(message + " WorkerId = " + workerId)
        {
            WorkerId = workerId;
        }

        public WorkerProcessingExeption(int workerId, string message, Exception innerException) : base(message, innerException)
        {
            WorkerId = workerId;
        }

        protected WorkerProcessingExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            WorkerId = (int)info.GetValue("WorkerId", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("WorkerId", WorkerId, typeof(int));
        }
    }
}
