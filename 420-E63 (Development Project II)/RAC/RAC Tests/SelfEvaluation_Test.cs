using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RAC.BusinessLogic;
using RAC.RACModels;

namespace RAC_Tests
{
    [TestClass]
    public class SelfEvaluation_Test
    {
        [TestMethod]
        public void SaveRacRequest_NoAnswers_NoComments()
        {
            /*
             * Setup
             */

            Candidate testUser = getTestUser();
            testUser.RACRequest.RACRequestCompetencies = new List<RACRequestCompetency>
            {
                new RACRequestCompetency
                {
                    Id = 2000,
                    RACRequestId = 999,
                    CompetencyId = 14,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2000,
                            Comment = "",
                            RACRequestCompetencyId = 2000,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3000,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 42
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3001,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 43
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3002,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 44
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3003,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 45
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3004,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 46
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3005,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 47
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2001,
                    RACRequestId = 999,
                    CompetencyId = 15,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2001,
                            Comment = "",
                            RACRequestCompetencyId = 2001,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3006,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 48
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3007,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 49
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2002,
                    RACRequestId = 999,
                    CompetencyId = 16,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2002,
                            Comment = "",
                            RACRequestCompetencyId = 2002,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3008,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 50
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3009,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 51
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3010,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 52
                        },
                        new RACRequestCompetencyElement
                        {
                        Id = 3011,
                        Answer = (int) SelfEvaluationAnswer.NotAnswered,
                        RACRequestCompetencyId = 2002,
                        CompetencyElementId = 53
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2003,
                    RACRequestId = 999,
                    CompetencyId = 17,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2003,
                            Comment = "",
                            RACRequestCompetencyId = 2003,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3012,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 54
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3013,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 55
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3014,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 56
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3015,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 57
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3016,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 58
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3017,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 59
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3018,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 60
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2004,
                    RACRequestId = 999,
                    CompetencyId = 18,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2004,
                            Comment = "",
                            RACRequestCompetencyId = 2004,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3019,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 61
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3020,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 62
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3021,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 63
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3022,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 64
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3023,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 65
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2005,
                    RACRequestId = 999,
                    CompetencyId = 19,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2005,
                            Comment = "",
                            RACRequestCompetencyId = 2005,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3024,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 66
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3025,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 67
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3026,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 68
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3027,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 69
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3028,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 70
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3029,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 71
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3030,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 72
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2006,
                    RACRequestId = 999,
                    CompetencyId = 20,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2006,
                            Comment = "",
                            RACRequestCompetencyId = 2006,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3031,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 73
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3032,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 74
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3033,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 75
                        }
                    }
                }
            };
            DbContext.Context.User.Add(testUser);
            DbContext.Context.SaveChanges();

            var racBll = new RACRequestBLL();
            RACRequestBLL.SaveRACRequest(testUser);

            /*
             * expected results
             */

            var expectedCompetencyIds = new List<int> {14, 15, 16, 17, 18, 19, 20};
            var expectedCompetencyElementId = 42;
            var expectedAnswerIndex = 0;
            var expectedRACStatus = (int) RACStatus.Started;

            List<SelfEvaluationAnswer> expectedAnswers = new List<SelfEvaluationAnswer>()
            {
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered
            };

            List<String> expectedComments = new List<string>()
            {
                ""
            };
            /*
             * Actual Results
             */
            
            var actualCompetencies = new List<RACRequestCompetency>();
            actualCompetencies = DbContext.Context.RACRequestCompetency.Where(r => r.RACRequestId == testUser.RACRequest.Id)
                .ToList();
            var actualStatus = ((RACRequest)DbContext.Context.RACRequest.Where(r => r.Id == testUser.RACRequest.Id).First()).Status;

            Assert.AreEqual(expectedRACStatus, actualStatus, "Expected RAC Status");
            if (actualCompetencies.Count != 0)
            {
                for (var i = 0; i < actualCompetencies.Count; i++)
                {
                    Assert.AreEqual(expectedCompetencyIds.ElementAt(i), actualCompetencies.ElementAt(i).CompetencyId, "Competency IDs index " + i);

                    var actualCompetencyElement = actualCompetencies.ElementAt(i).RACRequestCompetencyElements.ToList();
                    var actualCompetencyComments = actualCompetencies.ElementAt(i).CompetencyComments.ToList();

                    for (var j = 0; j < actualCompetencyElement.Count; j++)
                    {
                        var actualAnswer = actualCompetencyElement.ElementAt(j).Answer;
                        Assert.AreEqual(expectedCompetencyElementId, actualCompetencyElement.ElementAt(j).CompetencyElementId, "Expected competency element ID for " + actualCompetencyElement.ElementAt(j).CompetencyElementId);
                        Assert.AreEqual((int)expectedAnswers.ElementAt(expectedAnswerIndex), actualAnswer, "Expected answer for competency " + actualCompetencyElement.ElementAt(j).CompetencyElementId);

                        expectedAnswerIndex++;
                        expectedCompetencyElementId++;

                        DbContext.Context.RACRequestCompetencyElement.Remove(actualCompetencyElement.ElementAt(j));
                    }

                    for (var k = 0; k < actualCompetencyComments.Count; k++)
                    {
                        Assert.AreEqual(expectedComments.ElementAt(k), actualCompetencyComments.ElementAt(k).Comment, "Expected comment for competency " + actualCompetencies.ElementAt(i));
                        DbContext.Context.CompetencyComment.Remove(actualCompetencyComments.ElementAt(k));
                    }
                    DbContext.Context.RACRequestCompetency.Remove(actualCompetencies.ElementAt(i));
                }
            }
            /*
             * Remove added user
             */
           RACRequest userRAC = ((Candidate)DbContext.Context.User.Where(u => u.Id == testUser.Id).First()).RACRequest;
           DbContext.Context.RACRequest.Remove(userRAC);

           DbContext.Context.User.Remove(testUser);

           DbContext.Context.SaveChanges();
        }

        [TestMethod]
        public void SaveRacRequest_SomeAnswers_NoComments()
        {
            /*
             * Setup
             */

            Candidate testUser = getTestUser();
            testUser.RACRequest.RACRequestCompetencies = new List<RACRequestCompetency>
            {
                new RACRequestCompetency
                {
                    Id = 2000,
                    RACRequestId = 999,
                    CompetencyId = 14,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2000,
                            Comment = "",
                            RACRequestCompetencyId = 2000,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3000,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 42
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3001,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 43
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3002,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 44
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3003,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 45
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3004,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 46
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3005,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 47
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2001,
                    RACRequestId = 999,
                    CompetencyId = 15,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2001,
                            Comment = "",
                            RACRequestCompetencyId = 2001,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3006,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 48
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3007,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 49
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2002,
                    RACRequestId = 999,
                    CompetencyId = 16,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2002,
                            Comment = "",
                            RACRequestCompetencyId = 2002,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3008,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 50
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3009,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 51
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3010,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 52
                        },
                        new RACRequestCompetencyElement
                        {
                        Id = 3011,
                        Answer = (int) SelfEvaluationAnswer.NotAnswered,
                        RACRequestCompetencyId = 2002,
                        CompetencyElementId = 53
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2003,
                    RACRequestId = 999,
                    CompetencyId = 17,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2003,
                            Comment = "",
                            RACRequestCompetencyId = 2003,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3012,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 54
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3013,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 55
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3014,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 56
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3015,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 57
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3016,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 58
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3017,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 59
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3018,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 60
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2004,
                    RACRequestId = 999,
                    CompetencyId = 18,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2004,
                            Comment = "",
                            RACRequestCompetencyId = 2004,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3019,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 61
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3020,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 62
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3021,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 63
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3022,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 64
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3023,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 65
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2005,
                    RACRequestId = 999,
                    CompetencyId = 19,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2005,
                            Comment = "",
                            RACRequestCompetencyId = 2005,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3024,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 66
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3025,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 67
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3026,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 68
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3027,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 69
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3028,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 70
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3029,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 71
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3030,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 72
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2006,
                    RACRequestId = 999,
                    CompetencyId = 20,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2006,
                            Comment = "",
                            RACRequestCompetencyId = 2006,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3031,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 73
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3032,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 74
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3033,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 75
                        }
                    }
                }
            };
            DbContext.Context.User.Add(testUser);
            DbContext.Context.SaveChanges();

            var racBll = new RACRequestBLL();
            RACRequestBLL.SaveRACRequest(testUser);

            /*
             * expected results
             */

            var expectedCompetencyIds = new List<int> { 14, 15, 16, 17, 18, 19, 20 };
            var expectedCompetencyElementId = 42;
            var expectedAnswerIndex = 0;
            var expectedRACStatus = (int) RACStatus.Started;

            List<SelfEvaluationAnswer> expectedAnswers = new List<SelfEvaluationAnswer>()
            {
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.CanDoThis,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.CannotDoThis,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered
            };

            List<String> expectedComments = new List<string>()
            {
                ""
            };

            /*
             * Actual Results
             */

            var actualCompetencies = new List<RACRequestCompetency>();
            actualCompetencies = DbContext.Context.RACRequestCompetency.Where(r => r.RACRequestId == testUser.RACRequest.Id)
                .ToList();
            var actualStatus = ((RACRequest)DbContext.Context.RACRequest.Where(r => r.Id == testUser.RACRequest.Id).First()).Status;

            Assert.AreEqual(expectedRACStatus, actualStatus, "Expected RAC Status");
            if (actualCompetencies.Count != 0)
            {
                for (var i = 0; i < actualCompetencies.Count; i++)
                {
                    Assert.AreEqual(expectedCompetencyIds.ElementAt(i), actualCompetencies.ElementAt(i).CompetencyId, "Competency IDs index " + i);

                    var actualCompetencyElement = actualCompetencies.ElementAt(i).RACRequestCompetencyElements.ToList();
                    var actualCompetencyComments = actualCompetencies.ElementAt(i).CompetencyComments.ToList();

                    for (var j = 0; j < actualCompetencyElement.Count; j++)
                    {
                        var actualAnswer = actualCompetencyElement.ElementAt(j).Answer;
                        Assert.AreEqual(expectedCompetencyElementId, actualCompetencyElement.ElementAt(j).CompetencyElementId, "Expected competency element ID for " + actualCompetencyElement.ElementAt(j).CompetencyElementId);
                        Assert.AreEqual((int)expectedAnswers.ElementAt(expectedAnswerIndex), actualAnswer, "Expected answer for competency " + actualCompetencyElement.ElementAt(j).CompetencyElementId);

                        expectedAnswerIndex++;
                        expectedCompetencyElementId++;

                        DbContext.Context.RACRequestCompetencyElement.Remove(actualCompetencyElement.ElementAt(j));
                    }

                    for (var k = 0; k < actualCompetencyComments.Count; k++)
                    {
                        Assert.AreEqual(expectedComments.ElementAt(k), actualCompetencyComments.ElementAt(k).Comment, "Expected comment for competency " + actualCompetencies.ElementAt(i));
                        DbContext.Context.CompetencyComment.Remove(actualCompetencyComments.ElementAt(k));
                    }
                    DbContext.Context.RACRequestCompetency.Remove(actualCompetencies.ElementAt(i));
                }
            }
            /*
             * Remove added user
             */
            RACRequest userRAC = ((Candidate)DbContext.Context.User.Where(u => u.Id == testUser.Id).First()).RACRequest;
            DbContext.Context.RACRequest.Remove(userRAC);

            DbContext.Context.User.Remove(testUser);

            DbContext.Context.SaveChanges();
        }

        [TestMethod]
        public void SaveRacRequest_AllAnswered_NoComments()
        {
            /*
             * Setup
             */

            Candidate testUser = getTestUser();
            testUser.RACRequest.RACRequestCompetencies = new List<RACRequestCompetency>
            {
                new RACRequestCompetency
                {
                    Id = 2000,
                    RACRequestId = 999,
                    CompetencyId = 14,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2000,
                            Comment = "",
                            RACRequestCompetencyId = 2000,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3000,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 42
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3001,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 43
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3002,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 44
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3003,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 45
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3004,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 46
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3005,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 47
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2001,
                    RACRequestId = 999,
                    CompetencyId = 15,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2001,
                            Comment = "",
                            RACRequestCompetencyId = 2001,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3006,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 48
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3007,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 49
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2002,
                    RACRequestId = 999,
                    CompetencyId = 16,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2002,
                            Comment = "",
                            RACRequestCompetencyId = 2002,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3008,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 50
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3009,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 51
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3010,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 52
                        },
                        new RACRequestCompetencyElement
                        {
                        Id = 3011,
                        Answer = (int) SelfEvaluationAnswer.CanDoThis,
                        RACRequestCompetencyId = 2002,
                        CompetencyElementId = 53
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2003,
                    RACRequestId = 999,
                    CompetencyId = 17,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2003,
                            Comment = "",
                            RACRequestCompetencyId = 2003,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3012,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 54
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3013,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 55
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3014,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 56
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3015,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 57
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3016,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 58
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3017,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 59
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3018,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 60
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2004,
                    RACRequestId = 999,
                    CompetencyId = 18,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2004,
                            Comment = "",
                            RACRequestCompetencyId = 2004,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3019,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 61
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3020,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 62
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3021,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 63
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3022,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 64
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3023,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 65
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2005,
                    RACRequestId = 999,
                    CompetencyId = 19,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2005,
                            Comment = "",
                            RACRequestCompetencyId = 2005,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3024,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 66
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3025,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 67
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3026,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 68
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3027,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 69
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3028,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 70
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3029,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 71
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3030,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 72
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2006,
                    RACRequestId = 999,
                    CompetencyId = 20,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2006,
                            Comment = "",
                            RACRequestCompetencyId = 2006,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3031,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 73
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3032,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 74
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3033,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 75
                        }
                    }
                }
            };
            DbContext.Context.User.Add(testUser);
            DbContext.Context.SaveChanges();

            var racBll = new RACRequestBLL();
            RACRequestBLL.SaveRACRequest(testUser);

            /*
             * expected results
             */

            var expectedCompetencyIds = new List<int> { 14, 15, 16, 17, 18, 19, 20 };
            var expectedCompetencyElementId = 42;
            var expectedAnswerIndex = 0;
            var expectedRACStatus = (int) RACStatus.Started;

            List<SelfEvaluationAnswer> expectedAnswers = new List<SelfEvaluationAnswer>()
            {
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,

                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,

                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CanDoThis,

                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,

                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,

                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,

                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis
            };

            List<String> expectedComments = new List<string>()
            {
                ""
            };
            /*
             * Actual Results
             */

            var actualCompetencies = new List<RACRequestCompetency>();
            actualCompetencies = DbContext.Context.RACRequestCompetency.Where(r => r.RACRequestId == testUser.RACRequest.Id)
                .ToList();
            var actualStatus = ((RACRequest)DbContext.Context.RACRequest.Where(r => r.Id == testUser.RACRequest.Id).First()).Status;

            Assert.AreEqual(expectedRACStatus, actualStatus, "Expected RAC Status");

            if (actualCompetencies.Count != 0)
            {
                for (var i = 0; i < actualCompetencies.Count; i++)
                {
                    Assert.AreEqual(expectedCompetencyIds.ElementAt(i), actualCompetencies.ElementAt(i).CompetencyId, "Competency IDs index " + i);

                    var actualCompetencyElement = actualCompetencies.ElementAt(i).RACRequestCompetencyElements.ToList();
                    var actualCompetencyComments = actualCompetencies.ElementAt(i).CompetencyComments.ToList();

                    for (var j = 0; j < actualCompetencyElement.Count; j++)
                    {
                        var actualAnswer = actualCompetencyElement.ElementAt(j).Answer;
                        Assert.AreEqual(expectedCompetencyElementId, actualCompetencyElement.ElementAt(j).CompetencyElementId, "Expected competency element ID for " + actualCompetencyElement.ElementAt(j).CompetencyElementId);
                        Assert.AreEqual((int)expectedAnswers.ElementAt(expectedAnswerIndex), actualAnswer, "Expected answer for competency " + actualCompetencyElement.ElementAt(j).CompetencyElementId);

                        expectedAnswerIndex++;
                        expectedCompetencyElementId++;

                        DbContext.Context.RACRequestCompetencyElement.Remove(actualCompetencyElement.ElementAt(j));
                    }

                    for (var k = 0; k < actualCompetencyComments.Count; k++)
                    {
                        Assert.AreEqual(expectedComments.ElementAt(k), actualCompetencyComments.ElementAt(k).Comment, "Expected comment for competency " + actualCompetencies.ElementAt(i));
                        DbContext.Context.CompetencyComment.Remove(actualCompetencyComments.ElementAt(k));
                    }
                    DbContext.Context.RACRequestCompetency.Remove(actualCompetencies.ElementAt(i));
                }
            }
            /*
             * Remove added user
             */
            RACRequest userRAC = ((Candidate)DbContext.Context.User.Where(u => u.Id == testUser.Id).First()).RACRequest;
            DbContext.Context.RACRequest.Remove(userRAC);

            DbContext.Context.User.Remove(testUser);

            DbContext.Context.SaveChanges();
        }

        [TestMethod]
        public void SaveRacRequest_NoAnswers_SomeComments()
        {
            /*
             * Setup
             */

            Candidate testUser = getTestUser();
            testUser.RACRequest.RACRequestCompetencies = new List<RACRequestCompetency>
            {
                new RACRequestCompetency
                {
                    Id = 2000,
                    RACRequestId = 999,
                    CompetencyId = 14,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2000,
                            Comment = "Test comment 1",
                            RACRequestCompetencyId = 2000,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3000,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 42
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3001,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 43
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3002,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 44
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3003,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 45
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3004,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 46
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3005,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 47
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2001,
                    RACRequestId = 999,
                    CompetencyId = 15,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2001,
                            Comment = "Test comment 2",
                            RACRequestCompetencyId = 2001,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3006,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 48
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3007,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 49
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2002,
                    RACRequestId = 999,
                    CompetencyId = 16,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2002,
                            Comment = "",
                            RACRequestCompetencyId = 2002,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3008,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 50
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3009,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 51
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3010,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 52
                        },
                        new RACRequestCompetencyElement
                        {
                        Id = 3011,
                        Answer = (int) SelfEvaluationAnswer.NotAnswered,
                        RACRequestCompetencyId = 2002,
                        CompetencyElementId = 53
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2003,
                    RACRequestId = 999,
                    CompetencyId = 17,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2003,
                            Comment = "Test comment 3",
                            RACRequestCompetencyId = 2003,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3012,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 54
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3013,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 55
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3014,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 56
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3015,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 57
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3016,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 58
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3017,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 59
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3018,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 60
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2004,
                    RACRequestId = 999,
                    CompetencyId = 18,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2004,
                            Comment = "",
                            RACRequestCompetencyId = 2004,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3019,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 61
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3020,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 62
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3021,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 63
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3022,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 64
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3023,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 65
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2005,
                    RACRequestId = 999,
                    CompetencyId = 19,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2005,
                            Comment = "",
                            RACRequestCompetencyId = 2005,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3024,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 66
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3025,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 67
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3026,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 68
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3027,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 69
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3028,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 70
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3029,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 71
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3030,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 72
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2006,
                    RACRequestId = 999,
                    CompetencyId = 20,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2006,
                            Comment = "",
                            RACRequestCompetencyId = 2006,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3031,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 73
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3032,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 74
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3033,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 75
                        }
                    }
                }
            };
            DbContext.Context.User.Add(testUser);
            DbContext.Context.SaveChanges();

            var racBll = new RACRequestBLL();
            RACRequestBLL.SaveRACRequest(testUser);

            /*
             * expected results
             */

            var expectedCompetencyIds = new List<int> { 14, 15, 16, 17, 18, 19, 20 };
            var expectedCompetencyElementId = 42;
            var expectedAnswerIndex = 0;
            var expectedCommentIndex = 0;
            var expectedRACStatus = (int) RACStatus.Started;

            List<SelfEvaluationAnswer> expectedAnswers = new List<SelfEvaluationAnswer>()
            {
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered
            };

            List<String> expectedComments = new List<string>()
            {
                "Test comment 1",
                "Test comment 2",
                "",
                "Test comment 3",
                "",
                "",
                ""
            };
            /*
             * Actual Results
             */

            var actualCompetencies = new List<RACRequestCompetency>();
            actualCompetencies = DbContext.Context.RACRequestCompetency.Where(r => r.RACRequestId == testUser.RACRequest.Id)
                .ToList();
            var actualStatus = ((RACRequest)DbContext.Context.RACRequest.Where(r => r.Id == testUser.RACRequest.Id).First()).Status;

            Assert.AreEqual(expectedRACStatus, actualStatus, "Expected RAC Status");
            if (actualCompetencies.Count != 0)
            {
                for (var i = 0; i < actualCompetencies.Count; i++)
                {
                    Assert.AreEqual(expectedCompetencyIds.ElementAt(i), actualCompetencies.ElementAt(i).CompetencyId, "Competency IDs index " + i);

                    var actualCompetencyElement = actualCompetencies.ElementAt(i).RACRequestCompetencyElements.ToList();
                    var actualCompetencyComments = actualCompetencies.ElementAt(i).CompetencyComments.ToList();

                    for (var j = 0; j < actualCompetencyElement.Count; j++)
                    {
                        var actualAnswer = actualCompetencyElement.ElementAt(j).Answer;
                        Assert.AreEqual(expectedCompetencyElementId, actualCompetencyElement.ElementAt(j).CompetencyElementId, "Expected competency element ID for " + actualCompetencyElement.ElementAt(j).CompetencyElementId);
                        Assert.AreEqual((int)expectedAnswers.ElementAt(expectedAnswerIndex), actualAnswer, "Expected answer for competency " + actualCompetencyElement.ElementAt(j).CompetencyElementId);

                        expectedAnswerIndex++;
                        expectedCompetencyElementId++;
                        
                        DbContext.Context.RACRequestCompetencyElement.Remove(actualCompetencyElement.ElementAt(j));
                    }

                    for (var k = 0; k < actualCompetencyComments.Count; k++)
                    {
                        Assert.AreEqual(expectedComments.ElementAt(expectedCommentIndex), actualCompetencyComments.ElementAt(k).Comment, "Expected comment for competency " + actualCompetencies.ElementAt(i));
                        expectedCommentIndex++;
                        DbContext.Context.CompetencyComment.Remove(actualCompetencyComments.ElementAt(k));
                    }
                    DbContext.Context.RACRequestCompetency.Remove(actualCompetencies.ElementAt(i));
                }
            }
            /*
             * Remove added user
             */
            RACRequest userRAC = ((Candidate)DbContext.Context.User.Where(u => u.Id == testUser.Id).First()).RACRequest;
            DbContext.Context.RACRequest.Remove(userRAC);

            DbContext.Context.User.Remove(testUser);

            DbContext.Context.SaveChanges();
        }

        [TestMethod]
        public void SaveRacRequest_NoAnswers_AllComments()
        {
            /*
             * Setup
             */

            Candidate testUser = getTestUser();
            testUser.RACRequest.RACRequestCompetencies = new List<RACRequestCompetency>
            {
                new RACRequestCompetency
                {
                    Id = 2000,
                    RACRequestId = 999,
                    CompetencyId = 14,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2000,
                            Comment = "Test comment 1",
                            RACRequestCompetencyId = 2000,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3000,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 42
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3001,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 43
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3002,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 44
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3003,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 45
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3004,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 46
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3005,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 47
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2001,
                    RACRequestId = 999,
                    CompetencyId = 15,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2001,
                            Comment = "Test comment 2",
                            RACRequestCompetencyId = 2001,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3006,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 48
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3007,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 49
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2002,
                    RACRequestId = 999,
                    CompetencyId = 16,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2002,
                            Comment = "Test comment 3",
                            RACRequestCompetencyId = 2002,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3008,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 50
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3009,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 51
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3010,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 52
                        },
                        new RACRequestCompetencyElement
                        {
                        Id = 3011,
                        Answer = (int) SelfEvaluationAnswer.NotAnswered,
                        RACRequestCompetencyId = 2002,
                        CompetencyElementId = 53
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2003,
                    RACRequestId = 999,
                    CompetencyId = 17,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2003,
                            Comment = "Test comment 4",
                            RACRequestCompetencyId = 2003,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3012,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 54
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3013,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 55
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3014,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 56
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3015,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 57
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3016,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 58
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3017,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 59
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3018,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 60
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2004,
                    RACRequestId = 999,
                    CompetencyId = 18,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2004,
                            Comment = "Test comment 5",
                            RACRequestCompetencyId = 2004,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3019,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 61
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3020,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 62
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3021,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 63
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3022,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 64
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3023,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 65
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2005,
                    RACRequestId = 999,
                    CompetencyId = 19,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2005,
                            Comment = "Test comment 6",
                            RACRequestCompetencyId = 2005,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3024,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 66
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3025,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 67
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3026,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 68
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3027,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 69
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3028,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 70
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3029,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 71
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3030,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 72
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2006,
                    RACRequestId = 999,
                    CompetencyId = 20,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2006,
                            Comment = "Test comment 7",
                            RACRequestCompetencyId = 2006,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3031,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 73
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3032,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 74
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3033,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 75
                        }
                    }
                }
            };
            DbContext.Context.User.Add(testUser);
            DbContext.Context.SaveChanges();

            var racBll = new RACRequestBLL();
            RACRequestBLL.SaveRACRequest(testUser);

            /*
             * expected results
             */

            var expectedCompetencyIds = new List<int> { 14, 15, 16, 17, 18, 19, 20 };
            var expectedCompetencyElementId = 42;
            var expectedAnswerIndex = 0;
            var expectedCommentIndex = 0;
            var expectedRACStatus = RACStatus.Started;
            List<SelfEvaluationAnswer> expectedAnswers = new List<SelfEvaluationAnswer>()
            {
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered
            };

            List<String> expectedComments = new List<string>()
            {
                "Test comment 1",
                "Test comment 2",
                "Test comment 3",
                "Test comment 4",
                "Test comment 5",
                "Test comment 6",
                "Test comment 7"
            };

            /*
             * Actual Results
             */

            var actualCompetencies = new List<RACRequestCompetency>();
            actualCompetencies = DbContext.Context.RACRequestCompetency.Where(r => r.RACRequestId == testUser.RACRequest.Id)
                .ToList();
            var actualStatus = ((RACRequest)DbContext.Context.RACRequest.Where(r => r.Id == testUser.RACRequest.Id).First()).Status;

            Assert.AreEqual(expectedRACStatus, actualStatus, "Expected RAC Status");
            if (actualCompetencies.Count != 0)
            {
                for (var i = 0; i < actualCompetencies.Count; i++)
                {
                    Assert.AreEqual(expectedCompetencyIds.ElementAt(i), actualCompetencies.ElementAt(i).CompetencyId, "Competency IDs index " + i);

                    var actualCompetencyElement = actualCompetencies.ElementAt(i).RACRequestCompetencyElements.ToList();
                    var actualCompetencyComments = actualCompetencies.ElementAt(i).CompetencyComments.ToList();

                    for (var j = 0; j < actualCompetencyElement.Count; j++)
                    {
                        var actualAnswer = actualCompetencyElement.ElementAt(j).Answer;
                        Assert.AreEqual(expectedCompetencyElementId, actualCompetencyElement.ElementAt(j).CompetencyElementId, "Expected competency element ID for " + actualCompetencyElement.ElementAt(j).CompetencyElementId);
                        Assert.AreEqual((int)expectedAnswers.ElementAt(expectedAnswerIndex), actualAnswer, "Expected answer for competency " + actualCompetencyElement.ElementAt(j).CompetencyElementId);

                        expectedAnswerIndex++;
                        expectedCompetencyElementId++;

                        DbContext.Context.RACRequestCompetencyElement.Remove(actualCompetencyElement.ElementAt(j));
                    }

                    for (var k = 0; k < actualCompetencyComments.Count; k++)
                    {
                        Assert.AreEqual(expectedComments.ElementAt(expectedCommentIndex), actualCompetencyComments.ElementAt(k).Comment, "Expected comment for competency " + actualCompetencies.ElementAt(i));
                        expectedCommentIndex++;
                        DbContext.Context.CompetencyComment.Remove(actualCompetencyComments.ElementAt(k));
                    }
                    DbContext.Context.RACRequestCompetency.Remove(actualCompetencies.ElementAt(i));
                }
            }
            /*
             * Remove added user
             */
            RACRequest userRAC = ((Candidate)DbContext.Context.User.Where(u => u.Id == testUser.Id).First()).RACRequest;
            DbContext.Context.RACRequest.Remove(userRAC);

            DbContext.Context.User.Remove(testUser);

            DbContext.Context.SaveChanges();
        }

        [TestMethod]
        public void SaveRacRequest_SomeAnswers_SomeComments()
        {
            /*
             * Setup
             */

            Candidate testUser = getTestUser();
            testUser.RACRequest.RACRequestCompetencies = new List<RACRequestCompetency>
            {
                new RACRequestCompetency
                {
                    Id = 2000,
                    RACRequestId = 999,
                    CompetencyId = 14,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2000,
                            Comment = "Test comment 1",
                            RACRequestCompetencyId = 2000,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3000,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 42
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3001,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 43
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3002,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 44
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3003,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 45
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3004,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 46
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3005,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 47
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2001,
                    RACRequestId = 999,
                    CompetencyId = 15,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2001,
                            Comment = "Test comment 2",
                            RACRequestCompetencyId = 2001,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3006,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 48
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3007,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 49
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2002,
                    RACRequestId = 999,
                    CompetencyId = 16,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2002,
                            Comment = "Test comment 3",
                            RACRequestCompetencyId = 2002,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3008,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 50
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3009,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 51
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3010,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 52
                        },
                        new RACRequestCompetencyElement
                        {
                        Id = 3011,
                        Answer = (int) SelfEvaluationAnswer.NotAnswered,
                        RACRequestCompetencyId = 2002,
                        CompetencyElementId = 53
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2003,
                    RACRequestId = 999,
                    CompetencyId = 17,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2003,
                            Comment = "Test comment 4",
                            RACRequestCompetencyId = 2003,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3012,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 54
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3013,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 55
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3014,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 56
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3015,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 57
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3016,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 58
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3017,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 59
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3018,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 60
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2004,
                    RACRequestId = 999,
                    CompetencyId = 18,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2004,
                            Comment = "Test comment 5",
                            RACRequestCompetencyId = 2004,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3019,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 61
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3020,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 62
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3021,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 63
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3022,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 64
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3023,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 65
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2005,
                    RACRequestId = 999,
                    CompetencyId = 19,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2005,
                            Comment = "Test comment 6",
                            RACRequestCompetencyId = 2005,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3024,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 66
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3025,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 67
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3026,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 68
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3027,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 69
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3028,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 70
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3029,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 71
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3030,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 72
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2006,
                    RACRequestId = 999,
                    CompetencyId = 20,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2006,
                            Comment = "",
                            RACRequestCompetencyId = 2006,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3031,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 73
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3032,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 74
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3033,
                            Answer = (int) SelfEvaluationAnswer.NotAnswered,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 75
                        }
                    }
                }
            };
            DbContext.Context.User.Add(testUser);
            DbContext.Context.SaveChanges();

            var racBll = new RACRequestBLL();
            RACRequestBLL.SaveRACRequest(testUser);

            /*
             * expected results
             */

            var expectedCompetencyIds = new List<int> { 14, 15, 16, 17, 18, 19, 20 };
            var expectedCompetencyElementId = 42;
            var expectedAnswerIndex = 0;
            var expectedCommentIndex = 0;
            var expectedRACStatus = (int) RACStatus.Started;

            List<SelfEvaluationAnswer> expectedAnswers = new List<SelfEvaluationAnswer>()
            {
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,

                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered,
                SelfEvaluationAnswer.NotAnswered
            };

            List<String> expectedComments = new List<string>()
            {
                "Test comment 1",
                "Test comment 2",
                "Test comment 3",
                "Test comment 4",
                "Test comment 5",
                "Test comment 6",
                ""
            };

            /*
             * Actual Results
             */

            var actualCompetencies = new List<RACRequestCompetency>();
            actualCompetencies = DbContext.Context.RACRequestCompetency.Where(r => r.RACRequestId == testUser.RACRequest.Id)
                .ToList();
            var actualStatus = ((RACRequest)DbContext.Context.RACRequest.Where(r => r.Id == testUser.RACRequest.Id).First()).Status;

            Assert.AreEqual(expectedRACStatus, actualStatus, "Expected RAC Status");
            if (actualCompetencies.Count != 0)
            {
                for (var i = 0; i < actualCompetencies.Count; i++)
                {
                    Assert.AreEqual(expectedCompetencyIds.ElementAt(i), actualCompetencies.ElementAt(i).CompetencyId, "Competency IDs index " + i);

                    var actualCompetencyElement = actualCompetencies.ElementAt(i).RACRequestCompetencyElements.ToList();
                    var actualCompetencyComments = actualCompetencies.ElementAt(i).CompetencyComments.ToList();

                    for (var j = 0; j < actualCompetencyElement.Count; j++)
                    {
                        var actualAnswer = actualCompetencyElement.ElementAt(j).Answer;
                        Assert.AreEqual(expectedCompetencyElementId, actualCompetencyElement.ElementAt(j).CompetencyElementId, "Expected competency element ID for " + actualCompetencyElement.ElementAt(j).CompetencyElementId);
                        Assert.AreEqual((int)expectedAnswers.ElementAt(expectedAnswerIndex), actualAnswer, "Expected answer for competency " + actualCompetencyElement.ElementAt(j).CompetencyElementId);

                        expectedAnswerIndex++;
                        expectedCompetencyElementId++;

                        DbContext.Context.RACRequestCompetencyElement.Remove(actualCompetencyElement.ElementAt(j));
                    }

                    for (var k = 0; k < actualCompetencyComments.Count; k++)
                    {
                        Assert.AreEqual(expectedComments.ElementAt(expectedCommentIndex), actualCompetencyComments.ElementAt(k).Comment, "Expected comment for competency " + actualCompetencies.ElementAt(i));
                        expectedCommentIndex++;
                        DbContext.Context.CompetencyComment.Remove(actualCompetencyComments.ElementAt(k));
                    }
                    DbContext.Context.RACRequestCompetency.Remove(actualCompetencies.ElementAt(i));
                }
            }
            /*
             * Remove added user
             */
            RACRequest userRAC = ((Candidate)DbContext.Context.User.Where(u => u.Id == testUser.Id).First()).RACRequest;
            DbContext.Context.RACRequest.Remove(userRAC);

            DbContext.Context.User.Remove(testUser);

            DbContext.Context.SaveChanges();
        }

        [TestMethod]
        public void SaveRacRequest_AllAnswered_AllComments()
        {
            /*
             * Setup
             */

            Candidate testUser = getTestUser();
            testUser.RACRequest.RACRequestCompetencies = new List<RACRequestCompetency>
            {
                new RACRequestCompetency
                {
                    Id = 2000,
                    RACRequestId = 999,
                    CompetencyId = 14,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2000,
                            Comment = "Test comment 1",
                            RACRequestCompetencyId = 2000,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3000,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 42
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3001,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 43
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3002,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 44
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3003,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 45
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3004,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 46
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3005,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2000,
                            CompetencyElementId = 47
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2001,
                    RACRequestId = 999,
                    CompetencyId = 15,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2001,
                            Comment = "Test comment 2",
                            RACRequestCompetencyId = 2001,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3006,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 48
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3007,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2001,
                            CompetencyElementId = 49
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2002,
                    RACRequestId = 999,
                    CompetencyId = 16,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2002,
                            Comment = "Test comment 3",
                            RACRequestCompetencyId = 2002,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3008,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 50
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3009,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 51
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3010,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2002,
                            CompetencyElementId = 52
                        },
                        new RACRequestCompetencyElement
                        {
                        Id = 3011,
                        Answer = (int) SelfEvaluationAnswer.CanDoThis,
                        RACRequestCompetencyId = 2002,
                        CompetencyElementId = 53
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2003,
                    RACRequestId = 999,
                    CompetencyId = 17,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2003,
                            Comment = "Test comment 4",
                            RACRequestCompetencyId = 2003,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3012,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 54
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3013,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 55
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3014,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 56
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3015,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 57
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3016,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 58
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3017,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 59
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3018,
                            Answer = (int) SelfEvaluationAnswer.CanSomewhatDoThis,
                            RACRequestCompetencyId = 2003,
                            CompetencyElementId = 60
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2004,
                    RACRequestId = 999,
                    CompetencyId = 18,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2004,
                            Comment = "Test comment 5",
                            RACRequestCompetencyId = 2004,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3019,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 61
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3020,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 62
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3021,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 63
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3022,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 64
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3023,
                            Answer = (int) SelfEvaluationAnswer.CanDoThis,
                            RACRequestCompetencyId = 2004,
                            CompetencyElementId = 65
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2005,
                    RACRequestId = 999,
                    CompetencyId = 19,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2005,
                            Comment = "Test comment 6",
                            RACRequestCompetencyId = 2005,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3024,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 66
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3025,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 67
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3026,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 68
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3027,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 69
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3028,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 70
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3029,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 71
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3030,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2005,
                            CompetencyElementId = 72
                        }
                    }
                },
                new RACRequestCompetency
                {
                    Id = 2006,
                    RACRequestId = 999,
                    CompetencyId = 20,
                    CompetencyComments = new List<CompetencyComment>
                    {
                        new CompetencyComment
                        {
                            Id = 2006,
                            Comment = "Test comment 7",
                            RACRequestCompetencyId = 2006,
                            UserId = testUser.Id
                        }
                    },
                    RACRequestCompetencyElements = new List<RACRequestCompetencyElement>
                    {
                        new RACRequestCompetencyElement
                        {
                            Id = 3031,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 73
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3032,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 74
                        },
                        new RACRequestCompetencyElement
                        {
                            Id = 3033,
                            Answer = (int) SelfEvaluationAnswer.CannotDoThis,
                            RACRequestCompetencyId = 2006,
                            CompetencyElementId = 75
                        }
                    }
                }
            };
            DbContext.Context.User.Add(testUser);
            DbContext.Context.SaveChanges();

            var racBll = new RACRequestBLL();
            RACRequestBLL.SaveRACRequest(testUser);

            /*
             * expected results
             */

            var expectedCompetencyIds = new List<int> { 14, 15, 16, 17, 18, 19, 20 };
            var expectedCompetencyElementId = 42;
            var expectedAnswerIndex = 0;
            var expectedCommentIndex = 0;
            var expectedRACStatus = (int) RACStatus.Started;

            List<SelfEvaluationAnswer> expectedAnswers = new List<SelfEvaluationAnswer>()
            {
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,

                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,

                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CanDoThis,

                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,
                SelfEvaluationAnswer.CanSomewhatDoThis,

                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,
                SelfEvaluationAnswer.CanDoThis,

                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,

                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis,
                SelfEvaluationAnswer.CannotDoThis
            };

            List<String> expectedComments = new List<string>()
            {
                "Test comment 1",
                "Test comment 2",
                "Test comment 3",
                "Test comment 4",
                "Test comment 5",
                "Test comment 6",
                "Test comment 7"
            };
            /*
             * Actual Results
             */

            var actualCompetencies = new List<RACRequestCompetency>();
            actualCompetencies = DbContext.Context.RACRequestCompetency.Where(r => r.RACRequestId == testUser.RACRequest.Id)
                .ToList();
            var actualStatus = ((RACRequest)DbContext.Context.RACRequest.Where(r => r.Id == testUser.RACRequest.Id).First()).Status;
            Assert.AreEqual(expectedRACStatus, actualStatus, "Expected RAC Status");
            if (actualCompetencies.Count != 0)
            {
                for (var i = 0; i < actualCompetencies.Count; i++)
                {
                    Assert.AreEqual(expectedCompetencyIds.ElementAt(i), actualCompetencies.ElementAt(i).CompetencyId, "Competency IDs index " + i);

                    var actualCompetencyElement = actualCompetencies.ElementAt(i).RACRequestCompetencyElements.ToList();
                    var actualCompetencyComments = actualCompetencies.ElementAt(i).CompetencyComments.ToList();

                    for (var j = 0; j < actualCompetencyElement.Count; j++)
                    {
                        var actualAnswer = actualCompetencyElement.ElementAt(j).Answer;
                        Assert.AreEqual(expectedCompetencyElementId, actualCompetencyElement.ElementAt(j).CompetencyElementId, "Expected competency element ID for " + actualCompetencyElement.ElementAt(j).CompetencyElementId);
                        Assert.AreEqual((int)expectedAnswers.ElementAt(expectedAnswerIndex), actualAnswer, "Expected answer for competency " + actualCompetencyElement.ElementAt(j).CompetencyElementId);

                        expectedAnswerIndex++;
                        expectedCompetencyElementId++;

                        DbContext.Context.RACRequestCompetencyElement.Remove(actualCompetencyElement.ElementAt(j));
                    }

                    for (var k = 0; k < actualCompetencyComments.Count; k++)
                    {
                        Assert.AreEqual(expectedComments.ElementAt(expectedCommentIndex), actualCompetencyComments.ElementAt(k).Comment, "Expected comment for competency " + actualCompetencies.ElementAt(i));
                        expectedCommentIndex++;
                        DbContext.Context.CompetencyComment.Remove(actualCompetencyComments.ElementAt(k));
                    }
                    DbContext.Context.RACRequestCompetency.Remove(actualCompetencies.ElementAt(i));
                }
            }
            /*
             * Remove added user
             */
            RACRequest userRAC = ((Candidate)DbContext.Context.User.Where(u => u.Id == testUser.Id).First()).RACRequest;
            DbContext.Context.RACRequest.Remove(userRAC);

            DbContext.Context.User.Remove(testUser);

            DbContext.Context.SaveChanges();
        }

        [TestMethod]
        public void SubmitRacRequest()
        {
            /*
             * Setup
             */

            Candidate testUser = getTestUser();

            CandidateBLL.Register(testUser, 1000000);

            testUser = CandidateBLL.GetAllCandidates().Last();

            testUser.RACRequest.RACRequestCompetencies.ElementAt(0).CompetencyComments.ElementAt(0).Comment =
                "Test comment 1";
            testUser.RACRequest.RACRequestCompetencies.ElementAt(0).RACRequestCompetencyElements.ElementAt(0).Answer =
                (int) SelfEvaluationAnswer.CannotDoThis;
            testUser.RACRequest.RACRequestCompetencies.ElementAt(0).RACRequestCompetencyElements.ElementAt(1).Answer =
                (int)SelfEvaluationAnswer.CanDoThis;
            testUser.RACRequest.RACRequestCompetencies.ElementAt(0).RACRequestCompetencyElements.ElementAt(2).Answer =
                (int)SelfEvaluationAnswer.CanSomewhatDoThis;


            testUser.RACRequest.RACRequestCompetencies.ElementAt(1).CompetencyComments.ElementAt(0).Comment =
                "Test comment 2";
            testUser.RACRequest.RACRequestCompetencies.ElementAt(1).RACRequestCompetencyElements.ElementAt(0).Answer =
                (int)SelfEvaluationAnswer.CannotDoThis;
            testUser.RACRequest.RACRequestCompetencies.ElementAt(1).RACRequestCompetencyElements.ElementAt(1).Answer =
                (int)SelfEvaluationAnswer.CanDoThis;


            testUser.RACRequest.RACRequestCompetencies.ElementAt(2).CompetencyComments.ElementAt(0).Comment =
                "Test comment 3";
            testUser.RACRequest.RACRequestCompetencies.ElementAt(2).RACRequestCompetencyElements.ElementAt(0).Answer =
                (int)SelfEvaluationAnswer.CanDoThis;


            testUser.RACRequest.RACRequestCompetencies.ElementAt(3).CompetencyComments.ElementAt(0).Comment =
                "Test comment 4";
            testUser.RACRequest.RACRequestCompetencies.ElementAt(3).RACRequestCompetencyElements.ElementAt(0).Answer =
                (int)SelfEvaluationAnswer.CanSomewhatDoThis;
            testUser.RACRequest.RACRequestCompetencies.ElementAt(3).RACRequestCompetencyElements.ElementAt(1).Answer =
                (int)SelfEvaluationAnswer.CannotDoThis;


            testUser.RACRequest.RACRequestCompetencies.ElementAt(4).CompetencyComments.ElementAt(0).Comment =
                "Test comment 5";
            testUser.RACRequest.RACRequestCompetencies.ElementAt(4).RACRequestCompetencyElements.ElementAt(0).Answer =
                (int)SelfEvaluationAnswer.CanSomewhatDoThis;


            try
            {
                DbContext.Context.SaveChanges();
            }
            catch (Exception e)
            {
               String message = e.Message;
            }
            
            testUser.RACRequest = DbContext.Context.RACRequest.First(r => r.Candidate.Id == testUser.Id);
            var racBll = new RACRequestBLL();
            RACRequestBLL.SubmitRACRequest(testUser);

            /*
             * expected results
             */

            var expectedAnswerIndex = 0;
            var expectedCommentIndex = 0;
            var expectedAnswerSetIndex = 0;
            var expectedRACStatus = (int)RACStatus.Submitted;

            SelfEvaluationAnswer[][] expectedAnswers = 
            {
                new []
                {
                    SelfEvaluationAnswer.CannotDoThis,
                    SelfEvaluationAnswer.CanDoThis,
                    SelfEvaluationAnswer.CanSomewhatDoThis
                },
                new []
                {
                    SelfEvaluationAnswer.CannotDoThis,
                    SelfEvaluationAnswer.CanDoThis
                }, 
                new []
                {
                    SelfEvaluationAnswer.CanDoThis
                }, 
                new []
                {
                    SelfEvaluationAnswer.CanSomewhatDoThis,
                    SelfEvaluationAnswer.CannotDoThis
                }, 
                new []
                {
                    SelfEvaluationAnswer.CanSomewhatDoThis
                }
            };

            String[] expectedComments =
            {
                "Test comment 1",
                "Test comment 2",
                "Test comment 3",
                "Test comment 4",
                "Test comment 5"
            };
            /*
             * Actual Results
             */


            var actualCompetencies = new List<RACRequestCompetency>();
            actualCompetencies = DbContext.Context.RACRequestCompetency.Where(r => r.RACRequestId == testUser.RACRequest.Id)
                .ToList();

            var actualStatus = ((RACRequest)DbContext.Context.RACRequest.First(r => r.Id == testUser.RACRequest.Id)).Status;

            Assert.AreEqual(expectedRACStatus, actualStatus, "The RAC Request with the id " + testUser.RACRequest.Id + " has a status of " + (RACStatus)actualStatus + " when it should be " + expectedRACStatus + ".");

            if (actualCompetencies.Count != 0)
            {
                foreach (RACRequestCompetency comp in actualCompetencies)
                {
                    foreach (var comm in comp.CompetencyComments)
                    {
                        Assert.AreEqual(expectedComments[expectedCommentIndex], comm.Comment, "The comment for the competency " + comp.Competency.CompetencyMinistryData.Description + " has the comment " + comm.Comment + " when it should be " + expectedComments[expectedCommentIndex] + ".");
                    }

                    foreach (var elem in comp.RACRequestCompetencyElements)
                    {
                        Assert.AreEqual(expectedAnswers[expectedAnswerSetIndex][expectedAnswerIndex], (SelfEvaluationAnswer)elem.Answer, "The competency element " + elem.CompetencyElement.ElementMinistryData.Description + " has the answer " + (SelfEvaluationAnswer)elem.Answer + " when it should be " + expectedAnswers[expectedAnswerSetIndex][expectedAnswerIndex] + ".");
                        expectedAnswerIndex++;
                    }

                    expectedAnswerSetIndex++;
                    expectedCommentIndex++;
                }
            }
            /*
             * Remove added user
             */
            RACRequest userRAC = ((Candidate)DbContext.Context.User.First(u => u.Id == testUser.Id)).RACRequest;
            DbContext.Context.RACRequest.Remove(userRAC);

            DbContext.Context.User.Remove(testUser);

            DbContext.Context.SaveChanges();
        }

        public Candidate getTestUser()
        {
            int userId = 1;
            if (DbContext.Context.User.Any())
            {
                userId += DbContext.Context.User.Max(id => id.Id);
            }
            return new Candidate
            {
                Id = userId,
                FirstName = "Cody",
                LastName = "Berube",
                Email = "test@gmail.com",
                HomePhone = "8197710091",
                WorkPhone = "8197710091",
                UserType = 1,
                Street = "36 Rue What",
                City = "Test Land",
                Province = provinces.QC.ToString(),
                Country = "Canada",
                PreferredMethodOfContact = 1,
                RegistrationIP = "",
                RACRequest = new RACRequest(),
                DateRegistered = DateTime.Now
            };
        }
    }
}