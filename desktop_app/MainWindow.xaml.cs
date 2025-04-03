using desktop_app.logic;
using MahApps.Metro.IconPacks;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//MahApps.Metro.IconPacks.Material;
using System.Windows.Shapes;

namespace desktop_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly MainViewModel model = new MainViewModel();
        //private VPNManager vpn;

        public MainWindow()
        {
            InitializeComponent();
            this.model = new MainViewModel();
            DataContext = this.model;
            Loaded += async (s,e) => await this.model.CheckVpnStatus();
        }

        public void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        public void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        public void SpotifyToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        public void SshDropdownButton_Click(object sender, RoutedEventArgs e)
        {
            if (SshDropdownPanel.Visibility == Visibility.Collapsed)
            {
                SshDropdownPanel.Visibility = Visibility.Visible;
                SshDropdownIcon.Kind = PackIconMaterialKind.ChevronUp; // Change to "Up" icon
            }
            else
            {
                SshDropdownPanel.Visibility = Visibility.Collapsed;
                SshDropdownIcon.Kind = PackIconMaterialKind.ChevronDown; // Change back to "Down"
            }
        }

        public void VolumeDown_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        public void VolumeSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            // TODO
        }


        public async void VpnToggleButton_Click(object sender, RoutedEventArgs e)
        {
            await this.model.ToggleVpn();
        }

        public void StatsToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        public void VolumeUp_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        public void OpenYansFolderButton_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = @"\\192.168.0.101\share";
            try
            {
                Process.Start("explorer.exe",folderPath);
            }
            catch
            {
                MessageBox.Show($"Error opening folder: {folderPath}");
            }
        }
        /*
         * 
           <application xmlns="urn:schemas-microsoft-com:asm.v3">
       <windowsSettings>
         <requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
       </windowsSettings>
     </application>
     
         
         */





        //public void 
        //public void 
        //private void InitializeComponent()
        //{
        //    // This method is usually auto-generated in the MainWindow.g.i.cs file.
        //    // Ensure that the XAML file has the correct Build Action set to "Page".
        //}
    }
}