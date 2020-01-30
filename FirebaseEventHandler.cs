using System;
using System.Threading.Tasks;

namespace Modetor.Data.Firebase
{
    public class FirebaseEventHandler
    {
        public static readonly int TaskTimeout = 20000;

        private Func<FirebaseOperation> Process;
        private int timeout;
        public FirebaseEventHandler() { timeout = TaskTimeout; }

        internal void SetProcess(Func<FirebaseOperation> process) { Process = process; }
        /// <summary>Time in milliseconds</summary>
        public int Timeout  { get => timeout; set { timeout = value; } }
        public Action<string> OnSuccess = null;
        public Action<Exception> OnFail = null;
        public Action<FirebaseOperation> OnComplete = null;

        public void Start()
        {
            Task<FirebaseOperation> task = Task.Run(Process);
            if(task.Wait(Timeout))
            {
                if(task.Result.OperationResult != null)
                    task.Result.OperationResult = task.Result.OperationResult.Equals("null") ? null : task.Result.OperationResult;

                OnComplete?.Invoke(task.Result);
                if (task.IsCompleted && task.Result.OperationState) OnSuccess?.Invoke(task.Result.OperationResult);
                else
                    OnFail?.Invoke(task.Result.ThrownException);
            }
            else
            {
                OnComplete?.Invoke(task.Result);
                OnFail?.Invoke(task.Result.ThrownException);
            }
            //task.
        }
        public async void StartAsync()
        {
            FirebaseOperation task = await Task.Run(Process);
            
            if(task.OperationResult != null)
                task.OperationResult = task.OperationResult.Equals("null") ? null : task.OperationResult;

            OnComplete?.Invoke(task);
            if (task.OperationState)
                OnSuccess?.Invoke(task.OperationResult);
            else
                OnFail?.Invoke(task.ThrownException);
        }
    }

    public class FirebaseOperation
    {
        public bool OperationState;
        public Exception ThrownException;
        public string OperationResult;
    }
}
