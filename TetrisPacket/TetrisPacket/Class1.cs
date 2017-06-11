using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//가송 구현 사항 -Start
namespace TetrisPacket
{
    public enum PacketType
    {
        스샷 = 0, 상대속도증가 = 1, 상대줄추가 = 2, 상대화면가리기 = 3
    }

    [Serializable]
    public class Packet
    {
        public int Length;
        public int Type;

        public Packet()
        {
            this.Length = 0;
            Type = 0;
        }

        public static byte[] Serialize(Object o)
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }

        public static Object Deserialize(byte[] bt)
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            foreach (byte b in bt)
            {
                ms.WriteByte(b);
            }
            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }

    [Serializable]
    public class ScreenShotPacket : Packet
    {
        public byte[] data;
    }
    [Serializable]
    public class EnemySpeedUpPacket : Packet
    {
       // public byte[] data;
    }

    [Serializable]
    public class EnemyAddLinePacket : Packet
    {
       // public byte[] data;
    }

    [Serializable]
    public class EnemyBlindPacket : Packet
    {
      //  public byte[] data;
    }
}
//가송 구현 사항 -End