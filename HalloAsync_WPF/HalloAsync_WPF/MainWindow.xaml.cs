using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HalloAsync_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(1000);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = !true;
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                    Thread.Sleep(20);
                }
                pb1.Dispatcher.Invoke(() => b.IsEnabled = true);
            });
        }

        private void StartTaskMitTs(object sender, RoutedEventArgs e)
        {
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            var b = (Button)sender;
            b.IsEnabled = !true;
            cts = new CancellationTokenSource();
            //cts.Cancel();
            var t1 = Task.Factory.StartNew(() =>
              {
                  for (int i = 0; i < 100; i++)
                  {
                      int ii = i;
                      Task.Factory.StartNew(() => pb1.Value = ii, default, TaskCreationOptions.None, ts);
                      Thread.Sleep(100);

                      if (i > 8)
                          throw new ArithmeticException();
                      if (cts.Token.IsCancellationRequested)
                      {
                          //break;
                          cts.Token.ThrowIfCancellationRequested();
                      }
                  }

              }, cts.Token);
            t1.ContinueWith(t => MessageBox.Show("🥨 Der Task wurde nicht mal gestartet"), TaskContinuationOptions.OnlyOnCanceled);
            t1.ContinueWith(t => b.IsEnabled = true, CancellationToken.None, TaskContinuationOptions.None, ts);
            t1.ContinueWith(t =>
            {
                throw new AccessViolationException();
            }, TaskContinuationOptions.OnlyOnFaulted).ContinueWith(t =>
            {
                MessageBox.Show($"Error {t1.Exception.InnerException.Message}");
            }, TaskContinuationOptions.OnlyOnFaulted);



        }

        private async void StartAsync(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = !true;

            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                await Task.Delay(100);
            }

            b.IsEnabled = true;
        }


        public long CalcAltesUndLangsam(int zahl)
        {
            Thread.Sleep(2000);
            return zahl * 9823457;
        }

        public Task<long> CalcAltesUndLangsamAsync(int zahl, CancellationToken c = default)
        {
            if (c.IsCancellationRequested)
                c.ThrowIfCancellationRequested();

            return Task.Run(() => CalcAltesUndLangsam(zahl));
        }

        private async void StartAltAsync(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(CalcAltesUndLangsam(DateTime.Now.Second).ToString());
            MessageBox.Show((await CalcAltesUndLangsamAsync(DateTime.Now.Second)).ToString());
        }

        CancellationTokenSource cts = default;
        private void StartCancel(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private void ParalellEx(object sender, RoutedEventArgs e)
        {

            try
            {
                var query = Enum.GetNames(typeof(DayOfWeek)).ToList().Where(x => x[122] != 'A').AsParallel();

                query.ForAll(x => Debug.WriteLine(x));
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void ThrowEx(object sender, RoutedEventArgs e)
        {
            try
            {
                StartEins();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void StartEins()
        {

            try
            {
                new Dinge();
            }
            catch (Exception ex)
            {

                throw ;
            }
        }
    }
}
