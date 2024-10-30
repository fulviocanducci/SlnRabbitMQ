using System;
using System.Text;
using System.Text.Json;

namespace ShareLibrary.Extensions
{
   public static class Transform
   {
      public static byte[] ToByteArray(this object obj)
      {
         if (obj != null)
         {
            string json = JsonSerializer.Serialize(obj);
            return Encoding.UTF8.GetBytes(json);
         }
         return default;
      }

      public static Message ToMessage(this ReadOnlyMemory<byte> obj)
      {
         string text = Encoding.UTF8.GetString(obj.ToArray());
         return JsonSerializer.Deserialize<Message>(text);
      }
   }
}
