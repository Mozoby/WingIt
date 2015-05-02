using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using System.IO;
using System.Security.Permissions;

public class SocketConnection : MonoBehaviour {
	private readonly float Y_MAX = 60f;
	private readonly float Y_MIN = 25f;
	private readonly float NUM_SECTIONS = 4;


	Connector test = new Connector();
	public ProjectileScript projectile;
	public ExplosionMat bombExplode;
	public OVRPlayerController player;
	public Texture2D bombRamp;

	public void Start(){
		Debug.Log(test.fnConnectResult("hacksc.servegame.com"));
	}

	public void Update(){
		if(test.doesHaveMessage()){
			//we have a new message read it and do the things in the game that we should be doing
			string message = test.getMessage();
			try{
				int num = int.Parse(message);
				Debug.Log(num);
				//fire a projectile in the appropriate direction
				float yCur = (((Y_MAX - Y_MIN)/NUM_SECTIONS)*num) + ((Y_MAX - Y_MIN)/(2*NUM_SECTIONS)) + Y_MIN;
				ProjectileScript proj = Instantiate(projectile, new Vector3(player.transform.position.x + 100f, yCur, 10f),Quaternion.identity) as ProjectileScript;
				Debug.Log("Projectile Launched: " + yCur);
				proj.bombExplode = bombExplode;
				proj.player = player;
				proj.bombRamp = bombRamp;
			}catch(Exception ex){
				Debug.Log("Unable to Instantiate Projectile: " + ex.ToString());
			}

		}
	}

	

	public void onDestroy(){
		test.fnDisconnect();
	}

	class Connector {
		const int READ_BUFFER_SIZE = 256;
		const int PORT_NUM = 8080;
		private TcpClient client;
		private byte[] readBuffer = new byte[READ_BUFFER_SIZE];
		public string strMessage=string.Empty;
		public string res=String.Empty;
		public bool hasMessage = false;
 
		public Connector(){}
 
		public string fnConnectResult(string sNetIP)
		{
			try 
			{
				// The TcpClient is a subclass of Socket, providing higher level 
				// functionality like streaming.
				client = new TcpClient(sNetIP, PORT_NUM);
				// Start an asynchronous read invoking DoRead to avoid lagging the user
				// interface.
				client.GetStream().BeginRead(readBuffer, 0, READ_BUFFER_SIZE, new AsyncCallback(DoRead), null);
				// Make sure the window is showing before popping up connection dialog.
				return "Connection Succeeded";
			} 
			catch(Exception ex)
			{
				return "Server is not active.  Please start server and try again.      " + ex.ToString();
			}
		}
 
		public void fnDisconnect()
		{
			//SendData("disconnect");
			client.Close();
		}
 
 		public bool doesHaveMessage(){
 			return hasMessage;
 		}

 		public String getMessage(){
 			hasMessage = false;
 			return res;
 		}
 
		private void DoRead(IAsyncResult ar)
		{ 
			hasMessage = true;
			int BytesRead;
			try
			{
				// Finish asynchronous read into readBuffer and return number of bytes read.
				BytesRead = client.GetStream().EndRead(ar);
				if (BytesRead < 1) 
				{
					// if no bytes were read server has close.  
					res="disconnected";
					return;
				}

				// Convert the byte array the message was saved into, minus two for the
				// Chr(13) and Chr(10)
				strMessage = Encoding.ASCII.GetString(readBuffer, 0, BytesRead);
				res = strMessage;


				// Start a new asynchronous read into readBuffer.
				client.GetStream().BeginRead(readBuffer, 0, READ_BUFFER_SIZE, new AsyncCallback(DoRead), null);
 
			} 
			catch
			{
				res="disconnected";
			}
			Debug.Log(res);
		}
 
		// Use a StreamWriter to send a message to server.
		private void SendData(string data)
		{
			StreamWriter writer = new StreamWriter(client.GetStream());
			writer.Write(data + (char) 13);
			writer.Flush();
		}
	}
}
