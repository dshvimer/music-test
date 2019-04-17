﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System;


public class cubeMove : MonoBehaviour
{
    private static Thread clientRecieveThread;
    static Vector3 position;

    // Use this for initialization
    void Start()
    {
        StartClient();
        GetComponent<Rigidbody>().isKinematic = true;
    }

    static void StartClient() 
    {
        //try{
            clientRecieveThread =  new Thread(new ThreadStart(GetMessages));
            clientRecieveThread.IsBackground = true;
            clientRecieveThread.Start();

        //}
        //catch (Exception e)
        //{
        //    print(e);
        //}
    }


    // Update is called once per frame
    void Update()
    {
        //GetMessages();
        //transform.Translate(1f * Time.deltaTime , 0f , 0f);
        //float m = 5;
        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");
        //transform.Translate(x * Time.deltaTime * m, 0f * m, y * Time.deltaTime * m);

        // Vector3 lastPosition = transform.position;
        // float fr = Math.Min(Math.Abs(lastPosition.z - position.z) / 4.0f + 0.15f, 1);
        // float r = lastPosition.z * (1 - fr) + position.z * fr;
        // float fx = Math.Min(Math.Abs(lastPosition.x - position.x) / 7.0f + 0.15f, 1);
        // float x = lastPosition.x * (1 - fx) + position.x * fx;
        // float fy = Math.Min(Math.Abs(lastPosition.y - position.y) / 7.0f + 0.15f, 1);
        // float y = lastPosition.y * (1 - fy) + position.y * fy;
        //float m = 0.5;
        Vector3 translation = position - transform.localPosition;
        //Vector3 translation = position - transform.position;
        //print(translation);
        //transform.Translate(translation.x * Time.deltaTime * m, translation.y * Time.deltaTime * m, translation.z * Time.deltaTime * m);
        transform.Translate(translation);

        //Vector3 p = new Vector3(x, y, r);
        //print(position);

        //transform.position = position;

        //transform.position = Vector3.Lerp(lastPosition, position, 0.9f);
    }

    static void GetMessages ()
    {
        TcpClient client;
        NetworkStream stream;

        try
        {
            client = new TcpClient("192.168.43.221", 8000);

            stream = client.GetStream();

            while (true)
            {
                // if (stream == null) continue;

                byte[] data = new byte[1024];
                int bytes = stream.Read(data, 0, data.Length);
                string serverMessage = Encoding.ASCII.GetString(data);
                const float scale = 2;

                if (bytes == 0) { continue; }

                string[] coords = serverMessage.Split(' ');
                float x = float.Parse(coords[0]) - 0.5f;
                float y = float.Parse(coords[1]);
                float z = float.Parse(coords[2]) - 0.5f;
                position = new Vector3(x, z, y);


            }
        }
        catch (SocketException e)
        {
            print(e);
        }

 
    }

    //private void OnDestroy()
    //{
    //    if (stream != null)
    //    {
    //        stream.Close();
    //    }
    //    if (client != null)
    //    {
    //        client.Close();
    //    }
    //}

}
