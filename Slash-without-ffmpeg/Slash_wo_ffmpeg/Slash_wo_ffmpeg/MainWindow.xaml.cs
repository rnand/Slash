using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace Slash_wo_ffmpeg
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string ffmppeg=System.Environment.GetEnvironmentVariable("PATH");
            Regex pathregex = new Regex("ffmpeg");
            Match pathmatch = pathregex.Match(ffmppeg);
            string matched=ffmppeg;
            if (pathmatch.Success)
            {
                string[] pathArray = matched.Split(';');
                for (int i = 0; i < pathArray.Length; i++)
                {
                    if(pathregex.IsMatch(pathArray[i]))
                    {
                        textBlock1.Text = pathArray[i];
                    }
                }
            }

            if (textBlock1.Text != null)
            {
                if(File.Exists(System.IO.Path.Combine(textBlock1.Text, "ffmpeg.exe")))
                {
                    Process myProcess = new Process();
                    string filepath = System.IO.Path.Combine(textBlock1.Text, "ffmpeg.exe");
                    //MessageBox.Show(filepath);
                    try
                    {
                        myProcess.StartInfo.UseShellExecute = false;
                        
                        myProcess.StartInfo.FileName = filepath;
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.StartInfo.Arguments = @"-i "+textBoxLocation.Text+@" -ss "+textBoxFrom.Text+" -t "+textBoxTo.Text+" -async 1 -strict -2 "+textBoxLocation.Text+"_cut.mp4";
                        myProcess.Start();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Error");
                    }
                }
            }
            //textBlock1.Text = pathmatch.Value;
        }
    }
}
