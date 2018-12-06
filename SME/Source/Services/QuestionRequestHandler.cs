using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Core.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SME.Models;
namespace SME.Services
{
    public class QuestionRequestHandler : IQuestionRequestHandler
    {
        private MongoDbConnection db;
        private RabbitMQConnection rabbit;
        public QuestionRequestHandler(MongoDbConnection db, RabbitMQConnection rabbit)
        {
            this.db = db;
            this.rabbit = rabbit;
            HandleQuestionRequestFromQueue();
        }
        public QuestionBatchResponse ProvideQuestionsFromId(QuestionBatchRequest batchRequest)
        {
            var response = new List<Question>();
            foreach (var request in batchRequest.IdRequestList)
            {
                var result = db.Questions.Find(q => q.QuestionId == request).SingleOrDefault();
                if (result == null)
                {
                    throw new Exception($"Question with the QuestionId {request} does not exist inside SME MongoDB");
                }
                response.Add(result);
            }
            return new QuestionBatchResponse(batchRequest.Username, response);
        }

        public void HandleQuestionRequestFromQueue()
        {
            var channel = rabbit.Connection.CreateModel();
            var consumer = new AsyncEventingBasicConsumer(channel);
            Console.WriteLine("----------------------------------------------------------------");
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine("Consuming from KnowledgeGraph ");
                    channel.BasicAck(ea.DeliveryTag, false);
                    var body = ea.Body;
                    var request = (QuestionBatchRequest)body.DeSerialize(typeof(QuestionBatchRequest));
                    Console.WriteLine("Username " + request.Username + " is requesting " + request.IdRequestList.Count + " Questions");
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine("-----------------------------------------------------------------------");
                    var qbr = ProvideQuestionsFromId(request);
                    var response = ObjectSerialize.Serialize(qbr);
                    Console.WriteLine("Questions requested are->");
                    foreach (var item in qbr.ResponseList)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(item));
                    }
                    Console.WriteLine($"Sending " + qbr.ResponseList.Count + " Questions to Quiz Engine ");
                    // Send a message back to QuizEngine with the necessary question as response
                    rabbit.Model.BasicPublish(
                                exchange: rabbit.ExchangeName,
                                routingKey: "Send.Question",
                                basicProperties: null,
                                body: response
                            );
                    Console.WriteLine("Published to Question Response QuizEngine");
                    await Task.Yield();
                }
                catch (Exception e)
                {
                    ConsoleWriter.ConsoleAnException(e);
                }
            };
            Console.WriteLine("Listening to Knowledge Graph microservice for Question ID request ");
            channel.BasicConsume("KnowledgeGraph_Contributer_Ids", false, consumer);
        }
    }
}