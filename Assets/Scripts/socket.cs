using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
public class socket : MonoBehaviour
{
    public int port = 12345;
    Thread thread;
    TcpListener server;
    TcpClient client;
    bool running;
    void Start()
    {
        ThreadStart ts = new ThreadStart(GetData);
        thread=new Thread(ts);
        thread.Start();
    }
    void OnDestroy()
    {
        running = false;
        if (server != null)
        {
            client.Close();
        }
        if (client != null)
        {
            server.Stop();
        }
    }
    void GetData()
    {
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            client=server.AcceptTcpClient();
            running = true;
            while(running) 
            {
                Debug.Log("Waiting for process");
                Connection();
                Debug.Log("Connection");
            }

        }
        catch (SocketException e)
        {
            Debug.LogException(e);
        }
        finally
        {
            if (client != null)
                client.Close();
            if (server != null)
                server.Stop();
        }
    }
    void Connection()
    {
        NetworkStream nwStream=client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
        string dataReceived=Encoding.UTF8.GetString(buffer,0, bytesRead);
        if (dataReceived !=null && dataReceived!="")
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => Debug.Log("Data la " + dataReceived));        }
    }

    public static Vector3 ParseData(string dataString)
    {
        Debug.Log(dataString);
        // Remove the parentheses
        if (dataString.StartsWith("(") && dataString.EndsWith(")"))
        {
            dataString = dataString.Substring(1, dataString.Length - 2);
        }

        // Split the elements into an array
        string[] stringArray = dataString.Split(',');

        // Store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(stringArray[0]),
            float.Parse(stringArray[1]),
            float.Parse(stringArray[2]));

        return result;
    }
}
