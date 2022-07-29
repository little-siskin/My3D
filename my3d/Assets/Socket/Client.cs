using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using System.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading; 

public class Client : MonoBehaviour {
    //string editString="hello wolrd"; //编辑框文字

    //以下默认都是私有的成员
    Socket socket; //目标socket
    EndPoint serverEnd; //服务端
    IPEndPoint ipEnd; //服务端端口
    string recvStr; //接收的字符串
    string sendStr; //发送的字符串
    string sendStr1 = "connect";
    string sendStr2 = "run_server_once";
    string sendStr3 = "set_gnss";
    byte[] recvData=new byte[1024]; //接收的数据，必须为字节
    byte[] sendData=new byte[1024]; //发送的数据，必须为字节
    int recvLen; //接收的数据长度
    Thread connectThread; //连接线程
    int Time_Dlay_connect = 0;
    int send_num = 0;
    int recv_num = 0;
    bool Time_flag = true;
    bool Connect_flag = true;
    bool Setgnss_flag = false;   //
    int timeoutMSec = 1000;
    //ist<String> Data = new List<String>();
    string[] aryLine = null;

    public Text StatusText; //解状态
    public Text LongitudeText; //经度
    public Text LatitudeText; //纬度
    public Text NumberOfSatellitesText; //卫星数量
    public Text TurnText; //转向角

    // List<double> x_list = new List<double>();
    // List<double> y_list = new List<double>();
    // List<double> z_list = new List<double>();
    int i = 0;
    int j = 0;
    double x = 0;
    double y = 0;
    double z = 0;
    string strLine = "";
    //string[] posAryLine = null;
    bool IsFirst = true;
    //bool IsSecond = true;
    double orx = 0;
    double ory = 0;
    double orz = 0;
    double orAngle = 0;
    double A = 0;
    public GameObject mytractor;

    //初始化
    public void InitSocket()
    {
        //定义连接的服务器ip和端口，可以是本机ip，局域网，互联网
        ipEnd=new IPEndPoint(IPAddress.Parse("127.0.0.1"),8001);
        //ipEnd=new IPEndPoint(IPAddress.Parse("172.17.0.1"),4000);
        //定义套接字类型,在主线程中定义
        socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        //定义服务端
        IPEndPoint sender=new IPEndPoint(IPAddress.Any,0);
        serverEnd=(EndPoint)sender;
        //print("waiting for sending UDP dgram");

        //建立初始连接，这句非常重要，第一次连接初始化了serverEnd后面才能收到消息
        //SocketSend("hello");

        //开启一个线程连接，必须的，否则主线程卡死
        connectThread=new Thread(new ThreadStart(SocketCommunity));
        connectThread.Start();
    }

    public void pauseSocket(){
        //connectThread.Pause();
    }

    public void controlSocket(){

    }
    void SocketSend(string sendStr)
    {
        //清空发送缓存
        sendData=new byte[1024];
        //数据类型转换
        sendData=Encoding.UTF8.GetBytes(sendStr);
        //发送给指定服务端
        socket.SendTo(sendData,sendData.Length,SocketFlags.None,ipEnd);
    }

    void SocketCommunity(){
        while(true){
            sendData=new byte[1024];
            sendData=Encoding.UTF8.GetBytes(sendStr1);
            send_num = socket.SendTo(sendData,sendData.Length,SocketFlags.None,ipEnd);
            if(send_num < 0){
                Debug.Log("完蛋1");
                continue;
            }
            string connected = "@connected.";
            Time_Dlay_connect++;
            if(Time_Dlay_connect > 10) {
                Connect_flag = false;
                Time_flag = false;
                break;
            }
            recvData=new byte[1024];
            recv_num = socket.ReceiveFrom(recvData,ref serverEnd);


            //recv_num=socket.ReceiveFrom(recvData,ref serverEnd);
            if(recv_num < 0){
                continue;
            }
            //Debug.Log("message from: "+serverEnd.ToString());
            recvStr=Encoding.UTF8.GetString(recvData,0,recv_num);
            Debug.Log(recvStr);
            if(recvStr.CompareTo(connected) == 0){
                break;
                //Debug.Log(connected);
                //Debug.Log("message: " + recvStr);
            }
        }
        while(Connect_flag){
            if(Setgnss_flag){
                sendData=new byte[1024];
                sendData=Encoding.UTF8.GetBytes(sendStr3);
                send_num = socket.SendTo(sendData,sendData.Length,SocketFlags.None,ipEnd);
                if(send_num < 0){
                    continue;
                }
                Thread.Sleep(1);
                Setgnss_flag = false;
            }else{
                sendData=new byte[1024];
                sendData=Encoding.UTF8.GetBytes(sendStr2);
                send_num = socket.SendTo(sendData,sendData.Length,SocketFlags.None,ipEnd);
                if(send_num < 0){
                    continue;
                }
                recvData=new byte[1024];
                recv_num=socket.ReceiveFrom(recvData,ref serverEnd);
                //recvData=new char[1024];
                if(recv_num < 0)
                {
                    break;
                }
                //recvData[recv_num] = '\0';
                string command = "@start rosnode fail.";
                //recvStr=new string(recvData);
                recvStr=Encoding.UTF8.GetString(recvData,0,recv_num);
                //recvLen=socket.ReceiveFrom(recvData,ref clientEnd);
                if(recvStr.CompareTo(command) == 0){
                    break;
                }
                Debug.Log(recvStr);
                // public Text StatusText; //解状态
                // public Text LongitudeText; //经度
                // public Text LatitudeText; //纬度
                // public Text NumberOfSatellitesText; //卫星数量
                aryLine = recvStr.Split(',');
                // string _StatusText;
                // if(aryLine[2] == '0'){
                //     _StatusText = "NONE";
                // }
                // StatusText.GetComponent<Text>().text = "解状态" + aryLine[2];
                // LongitudeText.GetComponent<Text>().text = "经度" + aryLine[4];
                // LatitudeText.GetComponent<Text>().text = "纬度" + aryLine[5];
                // NumberOfSatellitesText.GetComponent<Text>().text = "卫星数量" + aryLine[6];
                //#1607475572.301428,0,0,0,nan,nan,0,0.000000,,0,nan,nan,0.000000,nan,nan,nan,nan,nan,nan,nan,nan,nan,nan,nan,nan,nan,0.000000,0.000000,0.000000,0.000000
                Thread.Sleep(100);
            }
        }
    }

    public string Status(string value){
        string _StatusText;
        if(String.Compare(value, "0") == 0){
            _StatusText = "NONE";
        }else if(String.Compare(value, "1") == 0){
            _StatusText = "FIXEDPOS";
        }else if(String.Compare(value, "2") == 0){
            _StatusText = "FIXEDHEIGHT";
        }else if(String.Compare(value, "8") == 0){
            _StatusText = "DOPPLER_VELOCITY";
        }else if(String.Compare(value, "16") == 0){
            _StatusText = "SINGLE";
        }else if(String.Compare(value, "17") == 0){
            _StatusText = "PSRDIFF";
        }else if(String.Compare(value, "18") == 0){
            _StatusText = "WASS";
        }else if(String.Compare(value, "32") == 0){
            _StatusText = "L1_FLOAT";
        }else if(String.Compare(value, "33") == 0){
            _StatusText = "IONOFREE_FLOAT";
        }else if(String.Compare(value, "34") == 0){
            _StatusText = "NARROW_FLOAT";
        }else if(String.Compare(value, "48") == 0){
            _StatusText = "L1_INT";
        }else if(String.Compare(value, "49") == 0){
            _StatusText = "WIDE_INT";
        }else if(String.Compare(value, "50") == 0){
            _StatusText = "NARROW_INT";
        }else if(String.Compare(value, "52") == 0){
            _StatusText = "INS";
        }else if(String.Compare(value, "53") == 0){
            _StatusText = "INS_PSRSP";
        }else if(String.Compare(value, "54") == 0){
            _StatusText = "INS_PSRDIFF";
        }else if(String.Compare(value, "55") == 0){
            _StatusText = "INS_RTKFLOA";
        }else if(String.Compare(value, "56") == 0){
            _StatusText = "INS_RTKFIXED";
        }else{
            _StatusText = "error";
        }
        return _StatusText;
    }
    //连接关闭
    void SocketQuit()
    {
        //关闭线程
        if(connectThread!=null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭socket
        if(socket!=null)
            socket.Close();
    }

    // Use this for initialization
    void Start()
    {
        //InitSocket(); //在这里初始化
    }

    void OnGUI()
    {
        // editString=GUI.TextField(new Rect(10,10,100,20),editString);
        // if(GUI.Button(new Rect(10,30,60,20),"send"))
        //     SocketSend(editString);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //StatusText.GetComponent<Text>().text = "解状态:  " + aryLine[2];status
        if(recvStr != null){
            StatusText.GetComponent<Text>().text = "解状态:  " + Status(aryLine[2]);
            LongitudeText.GetComponent<Text>().text = "经度:   " + aryLine[4];
            LatitudeText.GetComponent<Text>().text = "纬度:   " + aryLine[5];
            NumberOfSatellitesText.GetComponent<Text>().text = "卫星数量: " + aryLine[6];
            TurnText.GetComponent<Text>().text = "转向角:  " + aryLine[26];
            if(IsFirst == true){
                orx = double.Parse(aryLine[4]);
				//ory = double.Parse(aryLine[2]);
				orz = double.Parse(aryLine[5]);
                orAngle = double.Parse(aryLine[26]);
                IsFirst = false;
            }else{
                x = double.Parse(aryLine[4]) - orx;
                y = 0;
                z = double.Parse(aryLine[5]) - orz;
                A = double.Parse(aryLine[26]) - orAngle;
                mytractor.transform.Translate(new Vector3 ((float)x,(float)y,(float)z) * Time.deltaTime, Space.World);
                //mytractor.transform.rotation = Quaternion.LookRotation(new Vector3 ((float)x,(float)y,(float)z));
                //mytractor.transform.Rotate(0, (float)A * Time.deltaTime, 0);
                orx = double.Parse(aryLine[4]);
                orz = double.Parse(aryLine[5]);
                orAngle = double.Parse(aryLine[26]);
            }
        }
    }

    void OnApplicationQuit()
    {
        SocketQuit();
    }
}
