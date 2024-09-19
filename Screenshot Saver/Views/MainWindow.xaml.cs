﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Screenshot_Saver.ViewModels;
using Screenshot_Saver.ViewModels;
using System.Runtime.InteropServices;


namespace Screenshot_Saver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();
        public MainWindow()
        {

            
            InitializeComponent();
            DataContext= new MainWindowViewModel();

           
        }
       
    }
}