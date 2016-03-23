using System.Timers;

namespace Restaurant.DAL
{
    public class WaitTimer
    {
        public static void Main()
        {
            Timer MyTimer = new Timer();
            MyTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            MyTimer.Interval = 60000; //1000 = 1sec
            MyTimer.Enabled = true;

            
            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
           //Console.WriteLine("Hello World!");
        }
    }
}