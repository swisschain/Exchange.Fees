using System.Threading.Tasks;
using Fees.WebApi;
using Fees.WebApi.Models.CashOperationsFee;
using Xunit;

namespace Fees.Tests
{
    public class CashOperationsFeeTests
    {
        // TODO: Implement all cases
        [Fact]
        public async Task Test()
        {
            //var cashOperationsFeeController = new CashOperationsFeeController();

            //cashOperationsFeeController.Request.Headers.Add("Authorization", GetAuthToken());

            //var getManyRequest = new CashOperationsFeeRequestManyModel();

            //var response = cashOperationsFeeController.GetManyAsync(getManyRequest);
        }

        private string GetAuthToken()
        {
            return "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsic3dpc3NjaGFpbi5pbyIsInNpcml1cy5zd2lzc2NoYWluLmlvIiwiZXhjaGFuZ2Uuc3dpc3NjaGFpbi5pbyJdLCJ1c2VyLWlkIjoiZjllNzE4ZDUtM2M3Yy00YTUxLWFkNDMtYTliNzlmMDlkZDcxIiwidGVuYW50LWlkIjoiODM4MjlhYTEtNTg4OC00NWU0LTk5N2MtYjEzM2U1OGI3YWI4IiwibmJmIjoxNTg4ODQyMTE2LCJleHAiOjE1ODk0NDY5MTYsImlhdCI6MTU4ODg0MjExNn0.JsSGCTnC7VkrXLz2yiSxJULDcPyrbgJc8rTKRvjboKs";
        }
    }
}
