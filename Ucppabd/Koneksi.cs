using System;
using System.Net;
using System.Net.Sockets;



namespace Ucppabd

{
    internal class Koneksi

    {
        public string connectionString() //untuk membangun dan mengembalikan string koneksi ke database

        {
            string connectStr = "";

            try

            {
                string localIP = GetLocalIPAddress(); //mendeklarasikan ipaddress
                connectStr = $"Server={localIP};Initial Catalog=ProjecctPABD;" +$"Integrated Security=True;";
                return "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=ProjecctPABD;Integrated Security=True;";
            }

            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }

        }



        public static string GetLocalIPAddress() //untu mengambil IP Address pada PC yang menjalankan aplikasi

        {
            //mengambil infromasi tentang local host

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)

            {

                if (ip.AddressFamily == AddressFamily.InterNetwork) //Mengambil IPv4

                {
                    return ip.ToString();
                }

            }

            throw new Exception("Tidak ada alamat IP yang ditemukan.");

        }

    }

}

