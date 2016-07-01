using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSystem
{
    class MailManager
    {
        public event EventHandler<MailArrivedEventArgs> MailArrived;
        private Timer _timer;

        protected virtual void OnMailArrived(MailArrivedEventArgs argument)
        {
            EventHandler<MailArrivedEventArgs> mailArrived = MailArrived;
            if (mailArrived != null)
            {
                mailArrived(this, argument);
            }
        }

        public void SimulateMailArrived()
        {
            if (_timer != null)
            {
                MailArrivedEventArgs argument = new MailArrivedEventArgs("Test title", "Test body");
                OnMailArrived(argument);
            }
            else
            {
                _timer = new Timer(this._callBack, this, 0, 1000);
            }
        }

        private void _callBack(object state)
        {
            if (state is MailManager && state != null)
            {
                MailManager tempMailManager = state as MailManager;
                tempMailManager.SimulateMailArrived();
            }
        }


        public void MailArrivedEvent(object sender, MailArrivedEventArgs e)
        {
            Console.WriteLine($"Title is:\n{e.Title}\nBody is:\n{e.Body}");
        }
    }
}
