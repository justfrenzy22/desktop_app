using desktop_app.logic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace desktop_app
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly VPNManager _manager = new VPNManager();
        private bool _isVpnActive;
        private string _vpnStatusText = "Connect VPN";
        private bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsVpnActive
        {
            get => _isVpnActive;
            private set
            {
                if (_isVpnActive != value)
                {
                    _isVpnActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public string VpnStatusText
        {
            get => _vpnStatusText;
            private set
            {
                if (_vpnStatusText != value)
                {
                    _vpnStatusText = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task ToggleVpn()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                if (IsVpnActive)
                {
                    await DisconnectVpn();
                }
                else
                {
                    await ConnectVpn();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"VPN toggle failed: {ex.Message}");
                VpnStatusText = "Error - Try Again";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ConnectVpn()
        {
            try
            {
                VpnStatusText = "Connecting...";
                bool result = await Task.Run(() => _manager.connect());

                if (!result)
                {
                    VpnStatusText = "Connection Failed";
                    IsVpnActive = false;
                    return;
                }

                IsVpnActive = true;
                VpnStatusText = "Connected VPN";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"VPN connection failed: {ex.Message}");
                VpnStatusText = "Error - Connection Failed";
                IsVpnActive = false;
            }
        }

        private async Task DisconnectVpn()
        {
            try
            {
                VpnStatusText = "Disconnecting...";
                bool result = await Task.Run(() => _manager.disconect());

                if (!result)
                {
                    VpnStatusText = "Disconnect Failed";
                    IsVpnActive = true;
                    return;
                }

                IsVpnActive = false;
                VpnStatusText = "Disconnected VPN";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"VPN disconnection failed: {ex.Message}");
                VpnStatusText = "Error - Disconnect Failed";
                IsVpnActive = true;
            }
        }

        public async Task CheckVpnStatus()
        {
            try
            {
                IsBusy = true;
                enums.status vpnStatus = await Task.Run(() => _manager.getVPNStatus());
                IsVpnActive = vpnStatus == enums.status.Connected;

                if (IsVpnActive)
                {
                    VpnStatusText = "Connected VPN";
                }
                else
                {
                    VpnStatusText = "Disconnected VPN";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"VPN status check failed: {ex.Message}");
                VpnStatusText = "Error - Unable to check status";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
