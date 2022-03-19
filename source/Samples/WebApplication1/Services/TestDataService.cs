using DotFramework.Core;
using DotFramework.Infra;
using DotFramework.Infra.ExceptionHandling;
using DotFramework.Infra.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class TestRequestService : RequestServiceBase<TestDataRequest, TestDataResponse>
    {
        public override TestDataResponse ProcessRequest(TestDataRequest request)
        {
            var response = DataServiceFactory.Instance.Resolve<TestDataService>().ProcessRequest(request);
            return response;
        }
    }

    public class TestDataService : DataServiceBase<TestDataRequest, TestDataResponse>
    {
        public override TestDataResponse ProcessRequest(TestDataRequest request)
        {
            EnsureRequestIsValid(request);
            return Calculate(request);
        }

        private static TestDataResponse Calculate(TestDataRequest request)
        {
            TestDataResponse response = new TestDataResponse();
            response.Result = request.Number1 / request.Number2;

            if (response.Result < 3)
            {
                throw new Exception("Low result!");
            }

            return response;
        }

        private static void EnsureRequestIsValid(TestDataRequest request)
        {
            if (request.Number2 == 0)
            {
                throw new DataServiceCustomException("Number1 cannot be zero.");
            }
        }
    }

    public class TestDataRequest : RequestBase
    {
        [Required]
        public int Number1 { get; set; }
        
        [Required]
        public int Number2 { get; set; }

        [MinLength(5)]
        public string Name { get; set; }
    }

    public class TestDataResponse : ResponseBase
    {
        public double Result { get; set; }
    }
}
