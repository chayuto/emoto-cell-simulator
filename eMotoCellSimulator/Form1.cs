using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using System.IO.Ports;

namespace eMotoCellSimulator
{
    
    public partial class Form1 : Form
    {
        public byte[] PREAMBLE = {(byte)0xEC,(byte)0xDF};
        public const byte PREAMBLE0 = (byte)0xEC;
        public const byte PREAMBLE1 = (byte)0xDF;
        public const byte GET_STATUS = (byte)0xA5;
        public const byte RTS_IMAGE = (byte)0x4B;
        public const byte ACK_IMAGE_INFO = (byte)0x6B;
        public const byte ACK_IMAGE_DATA = (byte)0x4A;
        public const byte NACK_RTS = (byte)0x9E;
              
        public const byte GET_COMMAND = (byte)0xA5;
        public const byte SET_COMMAND = (byte)0x4B;
        public const byte ACK_COMMAND= (byte)0x6B;
        public const byte NACK_COMMAND = (byte)0x8E;
             
        public const byte DID_DEVICE_ID = (byte)0x00;
        public const byte DID_HW_VERSION = (byte)0x01;
        public const byte DID_FW_VERSION = (byte)0x02;
        public const byte DID_PROTOCOL = (byte)0x03;
        public const byte DID_C_TIME = (byte)0x10;
        public const byte DID_IMG_INFO = (byte)0x20;
        public const byte DID_IMG_DATA = (byte)0x21;
        public const byte DID_IMG_ONLIST = (byte)0x22;
            
        public const int LEN_DID_ACK_DEV_ID = 4;
        public const int LEN_DID_GET_DEV_ID = 1;
        public const int LEN_DID_HW_VER = 2;
        public const int LEN_DID_FW_VER = 2;
        public const int LEN_DID_C_TIME = 6;
        public const int LEN_DID_SET_IMG_INFO = 15;
             
        public const int LEN_PKT_HEADER = 8;
        public const int LEN_PKT_PREAMBLE = 2;

        string strBuff;
        byte[] incomingBuffer;
        byte[] mainBuffer = new byte[1];

        Thread ThreadSerialPortRead;

        public Form1()
        {
            InitializeComponent();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int totalBytes = serialPort1.BytesToRead;
            byte[] buffer = new byte[totalBytes];

            serialPort1.Read(buffer, 0, totalBytes);

            incomingBuffer = buffer;
            this.Invoke(new EventHandler(Data_rx));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
               // serialPort1.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
                //serialPort1.Encoding = new UnicodeEncoding();
                serialPort1.PortName = tbSerialPortNo.Text;
                serialPort1.Open();
                serialPort1.DiscardInBuffer();

                if (ThreadSerialPortRead == null)
                {
                    
                    ThreadSerialPortRead = new Thread(SerialPortRead);
                   ThreadSerialPortRead.Start();

                }

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.StackTrace);
            }
        }


        private void SerialPortRead()
        {
           while(true){

             byte firstByte =  (byte) serialPort1.ReadByte();
             Console.WriteLine("first byte" + firstByte.ToString());
             if (firstByte == PREAMBLE0)
             {
                 byte secondByte = (byte)serialPort1.ReadByte();
                 Console.WriteLine("second byte" + secondByte.ToString());
                 if (secondByte == PREAMBLE1)
                 {
                     Console.WriteLine("Preamble Detected" );
                     
                     byte[] headerBytes = new byte[LEN_PKT_HEADER];

                     int i = 0;
                     while (i != LEN_PKT_HEADER - LEN_PKT_PREAMBLE)
                     {
                         i += serialPort1.Read(headerBytes, i + LEN_PKT_PREAMBLE, LEN_PKT_HEADER - LEN_PKT_PREAMBLE - i);
                     }
                     Console.WriteLine("Heaeder Read completed");
                     Console.WriteLine(BitConverter.ToString(headerBytes).Replace("-", ":"));
                     byte transactionID = headerBytes[2];
                     byte command = headerBytes[3];
                     byte contentSize0 = headerBytes[4];
                     byte contentSize1 = headerBytes[5];
                     byte contentCRC = headerBytes[6];
                     byte headerCRC = headerBytes[7];

                     byte[] contentSizeArray = { contentSize0, contentSize1 };

                     byte[] ContentSizeArray = new byte[2] { contentSize0, contentSize1 };
                     int iContentLength = (int)BitConverter.ToInt16(ContentSizeArray, 0);
                     Console.WriteLine("Payload Length:" + iContentLength.ToString());

                     byte[] contentBytes = new byte[iContentLength];
                     if (iContentLength > 0)
                     {
                         i = 0; //reset counter 
                         while (i != iContentLength)
                         {
                             i += serialPort1.Read(contentBytes, i, iContentLength -  i);
                         }
                     }

                     Console.WriteLine("Content Read completed");
                     //Console.WriteLine(BitConverter.ToString(contentBytes).Replace("-", ":"));

                     //Log.d(TAG,String.format("Read %x",secondByte));

                     //  "Detect incoming PreAmble");
                 }
             }
           }

        }

        private void PacketData(object sender, EventArgs e)
        {

        }

        private void Data_rx(object sender, EventArgs e)
        {
       
            listBoxHex.Items.Add("Current:" + mainBuffer.Length.ToString());
            listBoxHex.Items.Add("Incoming:" + incomingBuffer.Length.ToString()); 

            byte[] tempBuffer = new byte[mainBuffer.Length + incomingBuffer.Length];
            Buffer.BlockCopy(mainBuffer, 0, tempBuffer, 0, mainBuffer.Length);
            Buffer.BlockCopy(incomingBuffer, 0, tempBuffer, mainBuffer.Length, incomingBuffer.Length);
            listBoxHex.Items.Add("Combined:" + tempBuffer.Length.ToString());
            mainBuffer = tempBuffer;

            lblStatus.Text = BitConverter.ToString(tempBuffer).Replace("-", ":");

            for (int i =0; i<= mainBuffer.Length-LEN_PKT_HEADER; i++)
            {
                if(mainBuffer[i] == PREAMBLE0)
                {
                    if(mainBuffer[i+1] == PREAMBLE1)
                    {
                        listBoxASCII.Items.Add(" ");
                        listBoxASCII.Items.Add("Preamble Detected");

                        byte[] headerBytes = new byte[LEN_PKT_HEADER];
                        Buffer.BlockCopy(mainBuffer, i, headerBytes, 0, LEN_PKT_HEADER);
                       
                        byte transactionID =    headerBytes[2];
                        byte command =          headerBytes[3];
                        byte contentSize0 =     headerBytes[4];
                        byte contentSize1  =    headerBytes[5];
                        byte contentCRC  =      headerBytes[6];
                        byte headerCRC  =       headerBytes[7];

                        byte[] contentSizeArray = {contentSize0,contentSize1};

                        byte[] ContentSizeArray = new byte[2] { contentSize0, contentSize1 };
                        int iContentLength = (int)BitConverter.ToInt16(ContentSizeArray, 0);
                        listBoxASCII.Items.Add("transaction ID: " + ((int) transactionID).ToString());
                        listBoxASCII.Items.Add("Content Length:" + iContentLength.ToString());
                        listBoxASCII.Items.Add("Header CRC: " + headerCRC.ToString("X2"));
                        listBoxASCII.Items.Add("Content CRC:" + contentCRC.ToString("X2"));      

                        //TODO: droppacket if the the header is corrupt
                        if (headerCRC != xCRCGen.crc_8_ccitt(headerBytes, LEN_PKT_HEADER - 1)){
                             listBoxASCII.Items.Add("Header Corrupt");    
                        }

                        int iMessageLength = LEN_PKT_HEADER + iContentLength; //get entire pkt length

                        if (iMessageLength + i<= mainBuffer.Length) //if the entire pkt is received
                        {                      
                            int iNewRemainingMainBufferLength = mainBuffer.Length - iMessageLength - i;
                            byte[] newRemainingMainBuffer = new byte[iNewRemainingMainBufferLength];
                            byte[] contentBytes = new byte[iContentLength];
                        
                            //Extract pkt from main Buffer
                            Buffer.BlockCopy(mainBuffer, iMessageLength + i, newRemainingMainBuffer, 0, iNewRemainingMainBufferLength);
                            Buffer.BlockCopy(mainBuffer, LEN_PKT_HEADER + i, contentBytes, 0, iContentLength);
                            listBoxHex.Items.Add("Remaining:" + newRemainingMainBuffer.Length.ToString());
                            listBoxHex.Items.Add("Content:" + contentBytes.Length.ToString());
                            mainBuffer = newRemainingMainBuffer;
                            i = 0;

                            listBoxASCII.Items.Add("messageBytes:" + BitConverter.ToString(headerBytes).Replace("-", ":"));
                            listBoxASCII.Items.Add("contentBytes:" + BitConverter.ToString(contentBytes).Replace("-", ":"));

                            // TODO: nack Pkt of content corrupt
                            if (contentCRC != xCRCGen.crc_8_ccitt(contentBytes, iContentLength))
                            {
                                listBoxASCII.Items.Add("Packet Content Corrupt");
                                return;
                            }

                            
                            //Analyse Header
                            switch (command)
                            {
                                case GET_COMMAND: listBoxASCII.Items.Add("Command: GET_COMMAND");
                                    // TODO: Process get command 
                                    break;
                                case SET_COMMAND: listBoxASCII.Items.Add("Command: SET_COMMAND");
                                    // TODO: Process data

                                    // HACK: if data is valid 
                                    byte[] bytePayload = new byte[30];
                                    for (int j = 0; j < 30; j++)
                                    {
                                        bytePayload[j] = (byte)j;
                                    }
                                    eMotoPacket mPacket = new eMotoPacket(ACK_COMMAND, transactionID, bytePayload);
                                    byte[] byteToSend = mPacket.getPacketByte();
                                    Console.WriteLine("Send:" + BitConverter.ToString(byteToSend).Replace("-", ":"));
                                    serialPort1.Write(byteToSend, 0, byteToSend.Length);

                                    break;

                                default:
                                    listBoxASCII.Items.Add("Unrecognized command");
                                    break;
                            }

                       
                        }
                        else
                        {

                            listBoxASCII.Items.Add("messageBytes:" + BitConverter.ToString(headerBytes).Replace("-", ":"));
                            listBoxASCII.Items.Add("content: Not All Data Received");
                        }
                    }
                }
            }
        }

        //sending 
        private void btnSend1_Click(object sender, EventArgs e)
        {
            serialPort1.Write(textBox1.Text);
        }

        private void btnSendPreAmble_Click(object sender, EventArgs e)
        {
            byte[] byteToSend = { PREAMBLE0, PREAMBLE1, (byte)0x41, (byte)0x42, (byte)0x43, (byte)0x44, (byte)0x45, (byte)0x46};

            serialPort1.Write(byteToSend, 0, byteToSend.Length);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            listBoxASCII.Items.Clear();
            listBoxHex.Items.Clear();
        }


 


    }
}
