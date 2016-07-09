using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace WinCtl {
	public class WinCtl
	{
	    private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
	    private const int APPCOMMAND_VOLUME_UP = 0xA0000;
	    private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
	    private const int WM_APPCOMMAND = 0x319;

	    [DllImport("user32.dll")]
	    public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
	        IntPtr wParam, IntPtr lParam);

	    private void Mute()
	    {
	        SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
	            (IntPtr)APPCOMMAND_VOLUME_MUTE);
	    }

	    private void VolDown()
	    {
	        SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
	            (IntPtr)APPCOMMAND_VOLUME_DOWN);
	    }

	    private void VolUp()
	    {
	        SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
	            (IntPtr)APPCOMMAND_VOLUME_UP);
	    }

	    private void ShutdownTimer(int time) {
	    	ProcessStartInfo processInfo;
		    Process process;

		    fileLoc = "/file";

		    processInfo = new ProcessStartInfo("cmd.exe", "/c " + fileLoc);
		    processInfo.CreateNoWindow = true;
		    processInfo.UseShellExecute = false;
		    // *** Redirect the output ***
		    processInfo.RedirectStandardError = true;
		    processInfo.RedirectStandardOutput = true;

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