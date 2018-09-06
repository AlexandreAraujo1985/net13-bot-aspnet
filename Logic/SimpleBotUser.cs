using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public static class Conexao
    {
        public static MongoClient ConMongo()
        {
            return new MongoClient("mongodb://localhost:27017");
        }
    }

    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {
            var doc = new BsonDocument
            {
                { "id", message.Id },
                { "texto", message.Text },
                { "user", message.User },
                { "Data", DateTime.Now }
            };

            var db = Conexao.ConMongo().GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela01");
            col.InsertOne(doc);

            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            var db = Conexao.ConMongo().GetDatabase("db01");
            var col = db.GetCollection<UserProfile>("tabela01");
        
            var userprofile = (UserProfile)col.Find(id);

            return userprofile;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
        }
    }
}