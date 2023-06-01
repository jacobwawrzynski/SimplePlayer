using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private bool isRunnig = true;
        private readonly Regex regex = new Regex("[0-9][0-9]:[0-5][0-9]:[0-5][0-9]");

        public MainWindow()
        {
            InitializeComponent();
            this.Topmost = true;
            if (!this.IsActive || !this.IsKeyboardFocused || !this.IsFocused)
                Deactivated += MainWindow_Deactivated;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void MainWindow_Deactivated(object? sender, EventArgs e)
        {
            Activate();
            Focus();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private bool IsCorrectFormat(string rgx)
        {
            return regex.IsMatch(rgx);
        }

        private bool IsCorrectTime(TimeSpan time)
        {
            return mePlayer.NaturalDuration.TimeSpan >= time;
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                mePlayer.Source = new Uri(openFileDialog.FileName);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void FastForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void FastForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            if (IsCorrectFormat(CurrTime_Textbox.Text)
                && IsCorrectTime(TimeSpan.Parse(CurrTime_Textbox.Text)))
            {
                mePlayer.Position = TimeSpan.Parse(CurrTime_Textbox.Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(KeyboardBackground);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void KeyboardBackground()
        {
            try
            {
                while (isRunnig)
                {
                    Thread.Sleep(40); // for minimum CPU usage
                    if ((Keyboard.GetKeyStates(Key.F6) & KeyStates.Down) > 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            mePlayer.Pause();
                        });
                    }

                    if ((Keyboard.GetKeyStates(Key.F5) & KeyStates.Down) > 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            mePlayer.Play();
                        });
                    }

                    if ((Keyboard.GetKeyStates(Key.F7) & KeyStates.Down) > 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            mePlayer.Position -= TimeSpan.FromSeconds(1);
                        });
                    }

                    if ((Keyboard.GetKeyStates(Key.F8) & KeyStates.Down) > 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            mePlayer.Position += TimeSpan.FromSeconds(1);
                        });
                    }

                    if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0 && (Keyboard.GetKeyStates(Key.D1) & KeyStates.Down) > 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            mePlayer.SpeedRatio = 0.5;
                        });
                    }

                    if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0 && (Keyboard.GetKeyStates(Key.D2) & KeyStates.Down) > 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            mePlayer.SpeedRatio = 1;
                        });
                    }

                    if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0 && (Keyboard.GetKeyStates(Key.D3) & KeyStates.Down) > 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            mePlayer.SpeedRatio = 1.5;
                        });
                    }


                    this.Dispatcher.Invoke(() =>
                    {
                        if (!IsLoaded)
                        {
                            isRunnig = false;
                        }
                    });
                }
                
            }
            catch (TaskCanceledException)
            {
                isRunnig = false;
            }
            
        }

    }
}
