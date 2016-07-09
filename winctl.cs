using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;


namespace WinCtl {
	public class WinCtl
	{
	 	[DllImport("user32.dll")]
		static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

	    private void Mute()
	    {
	    	keybd_event((byte)Keys.VolumeMute, 0, 0, 0); // decrease volume
	    }

	    private void VolDown()
	    {
	    	keybd_event((byte)Keys.VolumeDown, 0, 0, 0); // decrease volume
	    }

	    private void VolUp()
	    {
	    	keybd_event((byte)Keys.VolumeUp, 0, 0, 0); // increase volume
	    }

	    private void ShutdownTimer(int time) {
	    	ProcessStartInfo processInfo;
		    Process process;

		    string fileLoc = "/Users/Yuch/Desktop/shutdown N.bat";

		    processInfo = new ProcessStartInfo("cmd.exe", "/c " + fileLoc);
		    processInfo.CreateNoWindow = true;
		    processInfo.UseShellExecute = false;
		    // *** Redirect the output ***
		    processInfo.RedirectStandardError = true;
		    processInfo.RedirectStandardOutput = true;
		    processInfo.Arguments = time.ToString();

		    process = Process.Start(processInfo);
		    process.WaitForExit();
	    }


	    static void Main(string[] args)
	    {
	    	string MY_IP = args[0];
	    	WinCtl wc = new WinCtl();

	        try
	        {
	        	IPAddress ipAdress = IPAddress.Parse(MY_IP);
	        	TcpListener listener = new TcpListener(ipAdress,8777);
	        	listener.Start();
	        	Socket s = listener.AcceptSocket();
	        	byte[] b = new byte[4];
	        	while (true){
	        		s.Receive(b);
					if (BitConverter.IsLittleEndian)
					    Array.Reverse(b);
					int z = BitConverter.ToInt32(b, 0);
	        		switch (z) {
		        		case 1:
		        			wc.VolUp();
		        			break;
		        		case 2:
		        			wc.VolDown();
		        			break;
		        		case 3:
		        			wc.ShutdownTimer(4000);
		        			break;
		        		case 4:
		        			wc.ShutdownTimer(8000);
		        			break;

		        	}
	        	}
	        	s.Close();
	        	listener.Stop();

	        } catch (System.Exception e) {
	        	Console.WriteLine("Error..... " + e.StackTrace);
	        }

	        
	    }

	}
}