using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.BouncyCastle.Bcpg.OpenPgp;
using desktop_app.enums;

namespace desktop_app.logic
{
    public class VPNManager
    {
        private status vpnStatus;
        //   public VpnStatus VpnStatus { get; }

        public status Status => this.vpnStatus;


        public status getVPNStatus()
        {
            try
            {

                var processInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\WireGuard\wg.exe",
                    Arguments = "show",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string err = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        this.vpnStatus = status.Error;
                        return this.vpnStatus;
                    }

                    bool hasRecentHandshake = output.Contains("latest handshake:") &&
                                            !output.Contains("latest handshake: 0 seconds ago");
                    bool hasDataTransfer = output.Contains("transfer:") &&
                                         !output.Contains("transfer: 0 B received, 0 B sent");

                    this.vpnStatus = (hasRecentHandshake && hasDataTransfer) ? status.Connected : status.Disconnected;

                    return this.vpnStatus;
                }
            }
            catch (Exception)
            {
                this.vpnStatus = status.Error;
                return this.vpnStatus;
            }
        }

        public bool connect ()
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\WireGuard\wireguard.exe",
                    Arguments = "/installtunnelservice \"C:\\Program Files\\WireGuard\\Data\\Configurations\\acer.conf.dpapi\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string err = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        this.vpnStatus = status.Error;
                        return false;
                    }

                    bool isEmpty = output == "";

                    return isEmpty;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public bool disconect()
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\WireGuard\wireguard.exe",
                    Arguments = "/uninstalltunnelservice acer",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string err = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        this.vpnStatus = status.Error;
                        return false;
                    }

                    bool isEmpty = output == "";

                    return isEmpty;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
