using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Jobs
{
    static class NativeJob
    {
        [DllImport("kernel32")]
        public static extern IntPtr CreateJobObject(IntPtr sa, string name);

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool AssignProcessToJobObject(IntPtr hjob, IntPtr hprocess);

        [DllImport("kernel32")]
        public static extern bool CloseHandle(IntPtr h);

        [DllImport("kernel32")]
        public static extern bool TerminateJobObject(IntPtr hjob, uint code);
    }

    public class Job : IDisposable
    {
        private bool _disposed = false;
        private IntPtr _hJob;
        private List<Process> _processes;
        private Int64 _sizeInByte;

        public Job(string name)
        {
            this._hJob = NativeJob.CreateJobObject(IntPtr.Zero, name);
            IsHandleNotZero(this._hJob);
            _processes = new List<Process>();
        }


        public Job(Int64 size)
            :this(null)
        {
            this._sizeInByte = size;
            SetMemoryPressure();
        }


        public Job()
            : this(null)
        {
        }


        private void SetMemoryPressure()
        {
            try
            {
                GC.AddMemoryPressure(_sizeInByte);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Trace.TraceWarning(ex.Message);
            }
        }


        protected void AddProcessToJob(IntPtr hProcess)
        {
            try
            {
                CheckIfDisposed();
            }
            catch (ObjectDisposedException ex)
            {

                Trace.TraceError(ex.Message);
            }
            
            if (!NativeJob.AssignProcessToJobObject(_hJob, hProcess))
            {
                throw new InvalidOperationException("Failed to add process to job");
            }
        }

        private void CheckIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException($"{GetType().FullName} Object Disposed");
            }
        }


        private void IsHandleNotZero(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }
        }


        private void Close()
        {
            NativeJob.CloseHandle(_hJob);
            _hJob = IntPtr.Zero;
        }

        public void AddProcessToJob(int pid)
        {
            AddProcessToJob(Process.GetProcessById(pid));
        }

        public void AddProcessToJob(Process proc)
        {
            Debug.Assert(proc != null);
            AddProcessToJob(proc.Handle);
            _processes.Add(proc);
        }

        public void Kill()
        {
            NativeJob.TerminateJobObject(_hJob, 0);
            Dispose();
        }


        protected virtual void Dispose(bool disposing)
        {
            try
            {
                CheckIfDisposed();
            }
            catch (ObjectDisposedException ex)
            {

                Trace.TraceError(ex.Message);
            }

            if (disposing)
            {}

            Close();
            _disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~Job()
        {
            Dispose();
            try
            {
                GC.RemoveMemoryPressure(_sizeInByte);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Trace.TraceWarning(ex.Message);
            }
            FinalUserMessage();
        }

        private void FinalUserMessage()
        {
            Console.WriteLine("Job is released");
        }
    }
}
