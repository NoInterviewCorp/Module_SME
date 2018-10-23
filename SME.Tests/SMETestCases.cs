﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SME.Controllers;
using SME.Models;
using SME.Persistence;
using Xunit;
namespace SME.Tests
{
    public class SMETestCases
    {

        public List<Technology> GetMockDatabase()
        {
            List<Technology> technologies = new List<Technology>{
                new Technology{
                    TechnologyId=1,
                    Name="C#",
                    Topics = new List<Topic>{
                        new Topic{
                            TopicId=1,
                            Name="Introduction",
                            Questions=new List<Question>{
                                new Question{
                                    QuestionId=1,
                                    ProblemStatement="What is C# used for?",
                                    Options=new List<Option>(),
                                    ResourceLink="yur.web.site",
                                    BloomLevel = BloomTaxonomy.Knowledge,
                                    HasPublished=true,
                                    TopicId=1
                                },
                                new Question{
                                    QuestionId=2,
                                    ProblemStatement="What is C# ?",
                                    Options=new List<Option>(),
                                    ResourceLink="yur.web.site",
                                    BloomLevel = BloomTaxonomy.Comprehension,
                                    HasPublished=true,
                                    TopicId=1
                                }
                            },
                            TechnologyId = 1
                        }
                    }
                }
            };
            return technologies;
        }

        [Fact]
        public void GetAll_Positive_ReturnsList()
        {
            var dataRepo = new Mock<IDatabaseRepository>();
            List<Technology> technologies = GetMockDatabase();
            dataRepo.Setup(d => d.GetAllData()).Returns(technologies);
            SMEController sMEController = new SMEController(dataRepo.Object);

            var actionResult = sMEController.GetAll();

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Technology>;
            Assert.NotNull(model);

            Assert.Equal(technologies.Count, model.Count);
        }

        [Fact]
        public void GetAll_Negative_ReturnsEmptyList()
        {
            var dataRepo = new Mock<IDatabaseRepository>();
            List<Technology> list = null;
            dataRepo.Setup(d => d.GetAllData()).Returns(list);
            SMEController sMEController = new SMEController(dataRepo.Object);

            var actionResult = sMEController.GetAll();

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Technology>;
            Assert.NotNull(model);
            int expected = 0;
            Assert.Equal(expected, model.Count);
        }

        [Fact]
        public void GetTechnologies_Positive_ReturnsList()
        {
            var dataRepo = new Mock<IDatabaseRepository>();
            List<Technology> technologies = GetMockDatabase();
            dataRepo.Setup(d => d.GetAllTechnologies()).Returns(technologies);
            SMEController sMEController = new SMEController(dataRepo.Object);

            var actionResult = sMEController.Get();

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Technology>;
            Assert.NotNull(model);

            Assert.Equal(technologies.Count, model.Count);
        }

        [Fact]
        public void GetTechnologies_Negative_ReturnsEmptyList()
        {
            var dataRepo = new Mock<IDatabaseRepository>();
            List<Technology> list = null;
            dataRepo.Setup(d => d.GetAllTechnologies()).Returns(list);
            SMEController sMEController = new SMEController(dataRepo.Object);

            var actionResult = sMEController.Get();

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Technology>;
            Assert.NotNull(model);
            int expected = 0;
            Assert.Equal(expected, model.Count);
        }

        [Fact]
        public void GetAllTopicsInATechnology_Positive_ReturnsTopicsList()
        {
            var dataRepo = new Mock<IDatabaseRepository>();
            string technology = "C#";
            var list = GetMockDatabase().FirstOrDefault(t => t.Name == technology).Topics;
            dataRepo.Setup(d => d.GetAllTopicsInATechnology(technology)).Returns(list);
            SMEController sMEController = new SMEController(dataRepo.Object);

            var actionResult = sMEController.Get(technology, null, false);

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Topic>;
            Assert.NotNull(model);
            Assert.Equal(list.Count, model.Count);
        }
        [Fact]
        public void GetAllTopicsInATechnology_Negative_NotFound()
        {
            var dataRepo = new Mock<IDatabaseRepository>();
            string technology = "Java";
            List<Topic> list = null;
            dataRepo.Setup(d => d.GetAllTopicsInATechnology(technology)).Returns(list);
            SMEController sMEController = new SMEController(dataRepo.Object);

            var actionResult = sMEController.Get(technology,null,false);

            var okObjectResult = actionResult as NotFoundObjectResult;
        }

        [Fact]
        public void GetAllQuestionsInATech_Positive_ReturnsQuestionList()
        {
            var dataRepo = new Mock<IDatabaseRepository>();
            string technology = "C#";
            string topic = "Introduction";
            bool hasPublished = true;
            var list = GetMockDatabase().FirstOrDefault(t => t.Name == technology)
                        .Topics.FirstOrDefault(t => t.Name == topic)
                        .Questions;
            dataRepo.Setup(d => d.GetAllQuestionsFromTopic(technology, topic, hasPublished)).Returns(list);
            SMEController sMEController = new SMEController(dataRepo.Object);

            var actionResult = sMEController.Get(technology, topic, hasPublished);

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Question>;
            Assert.NotNull(model);
            Assert.Equal(list.Count, model.Count);
        }
        [Fact]
        public void GetAllQuestionsInATech_Negative_ReturnsNotFound(){
            var dataRepo = new Mock<IDatabaseRepository>();
            string technology = "Java";
            string topic = "OOPS";
            bool hasPublished = false;
            List<Question> list = null;
            dataRepo.Setup(d => d.GetAllQuestionsFromTopic(technology,topic,hasPublished)).Returns(list);
            SMEController sMEController = new SMEController(dataRepo.Object);

            var actionResult = sMEController.Get(technology,topic,hasPublished);

            var okObjectResult = actionResult as NotFoundObjectResult;
        }
    }
}
