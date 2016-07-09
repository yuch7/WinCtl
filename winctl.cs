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

		    fileLoc = "/Users/Yuch/Desktop/shutdown\ N.bat";

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
	    	int MY_IP = args[0];

	        try
	        {
	        	IPAddress ipAdress = IPAddress.Parse(MY_IP);
	        	TcpListener litstener = new TcpListener(ipAdress,8777);
	        	listener.Start();
	        	Socket s = listener.AcceptSocket();
	        	byte[] b = new byte[1];
	        	while (true){
		        	char z = Convert.ToChar(s.Receive(b));
		        	switch (z) {
		        		case 1:
		        			VolUp();
		        			break;
		        		case 2:
		        			VolDown();
		        			break;
		        		case 3:
		        			ShutdownTimer(4000);
		        			break;
		        		case 4:
		        			ShutdownTimer(8000);
		        			break;

		        	}
	        	}

	        } catch (System.Exception e) {
	        	Console.WriteLine("Error..... " + e.StackTrace);
	        }
	        s.Close();
	        listener.Stop();
	        
	    }

	}
}