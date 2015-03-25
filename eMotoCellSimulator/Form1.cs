using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;

namespace eMotoCellSimulator
{
    
    public partial class Form1 : Form
    {
        public static byte[] PREAMBLE = {(byte)0xEC,(byte)0xDF};
        public  const byte PREAMBLE0 = (byte)0xEC;
        public const byte PREAMBLE1 = (byte)0xDF;
        public  const byte GET_STATUS = (byte)0xA5;
        public  const byte RTS_IMAGE = (byte)0x4B;
        public  const byte ACK_IMAGE_INFO = (byte)0x6B;
        public  const byte ACK_IMAGE_DATA = (byte)0x4A;
        public const byte NACK_RTS = (byte)0x9E;

        string strBuff;
        byte[] incomingBuffer;
        byte[] mainBuffer = new byte[1];

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
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
                //serialPort1.Encoding = new UnicodeEncoding();
                serialPort1.PortName = tbSerialPortNo.Text;
                serialPort1.Open();
                serialPort1.DiscardInBuffer();

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.StackTrace);
            }
        }

        private void Data_rx(object sender, EventArgs e)
        {
            listBoxASCII.Items.Add(Encoding.ASCII.GetString(incomingBuffer));
            string hex = BitConverter.ToString(incomingBuffer).Replace("-", ":");
            listBoxHex.Items.Add(hex);

            byte[] tempBuffer = new byte[mainBuffer.Length + incomingBuffer.Length];
            Buffer.BlockCopy(mainBuffer, 0, tempBuffer, 0, mainBuffer.Length);
            Buffer.BlockCopy(incomingBuffer, 0, tempBuffer, mainBuffer.Length, incomingBuffer.Length);

            mainBuffer = tempBuffer;

            lblStatus.Text = BitConverter.ToString(tempBuffer).Replace("-", ":");

            for (int i =0; i<= mainBuffer.Length-8; i++)
            {
                if(mainBuffer[i] == PREAMBLE0)
                {
                    if(mainBuffer[i+1] == PREAMBLE1)
                    {
                        listBoxASCII.Items.Add("Preamble Detected");

                        byte transactionID = mainBuffer[i + 2];
                        byte command = mainBuffer[i + 3];
                        byte contentSize0 =mainBuffer[i + 4];
                        byte contentSize1  =mainBuffer[i + 5];
                        byte contentCRC  =mainBuffer[i + 6];
                        byte headerCRC  = mainBuffer[i + 7];
                        byte[] contentSizeArray = {contentSize0,contentSize1};

                        byte[] ContentSizeArray = new byte[2] { contentSize0, contentSize1 };
                        int iContentLength = (int)BitConverter.ToInt16(ContentSizeArray, 0);
                        listBoxASCII.Items.Add("transaction ID: " + ((int) transactionID).ToString());
                        listBoxASCII.Items.Add("Content Length:" + iContentLength.ToString());
                        listBoxASCII.Items.Add("Header CRC: " + headerCRC.ToString("X2"));
                        listBoxASCII.Items.Add("Content CRC:" + contentCRC.ToString("X2"));      

                        //Analyse Header
                        switch (command)
                        {

                            case GET_STATUS: listBoxASCII.Items.Add("Command: GET_STATUS");
                                break;
                            case RTS_IMAGE: listBoxASCII.Items.Add("Command: RTS_IMAGE");
                                break;
                            case ACK_IMAGE_DATA: listBoxASCII.Items.Add("Command: ACK_IMAGE_DATA");
                                break;
                            case ACK_IMAGE_INFO: listBoxASCII.Items.Add("Command: ACK_IMAGE_INFO");
                                break;
                            case NACK_RTS:
                                listBoxASCII.Items.Add("Command: ACK_IMAGE_INFO");
                                break;
                            default:
                                listBoxASCII.Items.Add("Unrecognized command");
                                break;
                        }

                        int iMessageLength = 8 + iContentLength; //Header Length
                        byte[] messageBytes = new byte[iMessageLength];
                        Buffer.BlockCopy(mainBuffer, i, messageBytes, 0, iMessageLength);

                        if (iMessageLength + i<= mainBuffer.Length)
                        {                      
                        int iNewRemainingMainBufferLength = mainBuffer.Length - iMessageLength - i;
                        byte[] newRemainingMainBuffer = new byte[iNewRemainingMainBufferLength];
                        

                        //Extract message from main Buffer
                        Buffer.BlockCopy(mainBuffer, iMessageLength + i, newRemainingMainBuffer, 0, iNewRemainingMainBufferLength);
                        

                        byte[] contentBytes = new byte[iContentLength];
                        Buffer.BlockCopy(messageBytes,8, contentBytes, 0, iContentLength);

                        mainBuffer = newRemainingMainBuffer;

                        listBoxASCII.Items.Add("messageBytes:" + BitConverter.ToString(messageBytes).Replace("-", ":"));
                        listBoxASCII.Items.Add("contentBytes:" + BitConverter.ToString(contentBytes).Replace("-", ":"));
                        }
                        else
                        {
                        
                            listBoxASCII.Items.Add("messageBytes:" + BitConverter.ToString(messageBytes).Replace("-", ":"));
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
