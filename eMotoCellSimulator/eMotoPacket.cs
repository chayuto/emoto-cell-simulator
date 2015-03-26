using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMotoCellSimulator
{
    class eMotoPacket
    {
        //const
        public  const byte PREAMBLE0 = (byte)0xEC;
        public  const byte PREAMBLE1 = (byte)0xDF;

        public  const byte GET_COMMAND = (byte)0xA5;
        public  const byte SET_COMMAND = (byte)0x4B;
        public  const byte ACK_COMMAND = (byte)0x6B;
        public  const byte NACK_COMMAND = (byte)0x8E;

        public  const int LEN_PKT_HEADER = 8;
        
        private byte command;
        private byte transationID;
        private byte[] Payload;

        public eMotoPacket(Byte mCommand,Byte mTransactionID, byte[] payload)
        { 
            this.command = mCommand;
            this.transationID = mTransactionID;
            this.Payload = payload;
        }


        public byte[] getHeader (){

            byte[] headerByte = new byte[LEN_PKT_HEADER];

            headerByte[0] = PREAMBLE0;
            headerByte[1] = PREAMBLE1;
            headerByte[2] = transationID;
            headerByte[3] = command;

            byte[] contentBytes  = BitConverter.GetBytes((ushort) Payload.Length);
            headerByte[4] = contentBytes[0];
            headerByte[5] = contentBytes[1];
            headerByte[6] = xCRCGen.crc_8_ccitt(Payload, Payload.Length);
            headerByte[7] = xCRCGen.crc_8_ccitt(headerByte, 7);

            return headerByte;
        }

        public byte[] getPacketByte()
        {
            byte[] packetByte = new byte[LEN_PKT_HEADER + Payload.Length];
            Buffer.BlockCopy(this.getHeader(), 0, packetByte, 0, LEN_PKT_HEADER);
            Buffer.BlockCopy(Payload, 0, packetByte, LEN_PKT_HEADER, Payload.Length);

            return packetByte;
        }

        public void getAnalysis()
        {
            switch(command)
            {
                case GET_COMMAND:
                case SET_COMMAND:
                case ACK_COMMAND:
                case NACK_COMMAND:
                default:
                    break;
            }
        }


    }
}
