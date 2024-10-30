using System;
using System.Text.Json.Serialization;
namespace ShareLibrary
{
   public class Message
   {
      public Message(string text)
      {
         Id = Guid.NewGuid();
         Text = text;
      }

      [JsonConstructor()]
      public Message(Guid id, string text)
      {
         Id = id;
         Text = text;
      }

      [JsonPropertyName("id")]
      public Guid Id { get; }

      [JsonPropertyName("text")]
      public string Text { get; }

      public static Message Create(string text)
      {
         return new Message(text);
      }
   }
}