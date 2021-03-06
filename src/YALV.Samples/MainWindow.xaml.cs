﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YALV.Samples
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var generationButton = sender as Button;
            if (generationButton == null)
            {
                return;
            }

            generationButton.IsEnabled = false;

            Task generateTask = Task.Factory.StartNew(GenerateRandomLogs);

            generateTask.ContinueWith(x =>
            {
                Dispatcher.Invoke(new Action(() => generationButton.IsEnabled = true));
            });
        }

        private void GenerateRandomLogs()
        {
            Random r = new Random();
            for (int i = 0; i < 5000; i++)
            {
                int value = r.Next(13);

                switch (value)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        GenerateDebugTrace();
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        GenerateInfoTrace();
                        break;
                    case 9:
                        GenerateWarningTrace();
                        break;
                    case 10:
                    case 11:
                        GenerateErrorTrace();
                        break;
                    case 12:
                        GenerateFatalTrace();
                        break;
                }
            }
            MessageBox.Show("Generation Complete!", "YALV! Samples", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void GenerateDebugTrace()
        {
            LogService.Trace.Debug("This is a debug message");
        }

        private void GenerateInfoTrace()
        {
            LogService.Trace.Info("This is an information message");
        }

        private void GenerateWarningTrace()
        {
            LogService.Trace.Warn("This is a warning message!", new Exception("Warning Exception!"));
        }

        private void GenerateErrorTrace()
        {
            LogService.Trace.Error("This is an error message!", new Exception("Error Exception!"));
        }

        private void GenerateFatalTrace()
        {
            LogService.Trace.Fatal("This is a fatal message!", new Exception("Fatal Exception!"));
        }
    }
}
