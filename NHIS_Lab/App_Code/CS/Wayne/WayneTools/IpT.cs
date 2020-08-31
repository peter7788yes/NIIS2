using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

public class IpT
{
    public IPAddress[] address = new IPAddress[] { };

    public IpT(string ClientIP)
    {
        IPAddress ip = default(IPAddress);
        IPAddress.TryParse(ClientIP,out ip);
        address = new IPAddress[] { ip };
    }

    public  bool CheckNetWork()
    {
        return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
    }


    public  IPAddress LocalIPAddress()
    {
        if (!CheckNetWork())
        {
            return null;
        }

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

        return host
            .AddressList
            .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork && !ip.ToString().StartsWith("169"));
    }

    public  string LocalIPAddressString()
    {
        if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
        {
            return "";
        }

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

        return host
            .AddressList
            .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork && !ip.ToString().StartsWith("169")).ToString();
    }

    public  IPAddress[] GetIPs()
    {
        if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
        {
            return null;
        }

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

        return host.AddressList;

    }

    public  bool CheckInNowWifi(string Allow_IP_Range)
    {
        bool rtn = false;

        try
        {
            //string nowIP = IpTool.LocalIPAddressString();
            //IPAddress[] address = GetIPs();


            string[] ip_segment = Allow_IP_Range.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in ip_segment)
            {
                string[] ipRange = item.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (ipRange.Length < 2)
                {
                    if (address == null)
                        return false;
                    foreach (IPAddress ip in address)
                    {
                        if (ip.ToString().Equals(ipRange[0]))
                        {
                            rtn = true;
                            return true;
                        }
                    }
                }
                else
                {
                    if (address == null)
                        return false;
                    foreach (IPAddress ip in address)
                    {
                        bool inRange = IpT.IsIpInRange(ip.ToString(), ipRange[0], ipRange[1]);
                        if (inRange)
                        {
                            rtn = true;
                            return true;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            rtn = false;
            //LogTool.Debug(ex);
        }

        return rtn;
    }

    public static bool IsIpInRange(string ip, string ipStart, string ipEnd)
    {
        var pIP = IPAddress.Parse(ip);
        var pIPStart = IPAddress.Parse(ipStart);
        var pIPEnd = IPAddress.Parse(ipEnd);

        var bIP = pIP.GetAddressBytes().Reverse().ToArray();
        var bIPStart = pIPStart.GetAddressBytes().Reverse().ToArray();
        var bIPEnd = pIPEnd.GetAddressBytes().Reverse().ToArray();

        var uIP = BitConverter.ToUInt32(bIP, 0);
        var uIPStart = BitConverter.ToUInt32(bIPStart, 0);
        var uIPEnd = BitConverter.ToUInt32(bIPEnd, 0);

        return uIP >= uIPStart && uIP <= uIPEnd;
    }
}
