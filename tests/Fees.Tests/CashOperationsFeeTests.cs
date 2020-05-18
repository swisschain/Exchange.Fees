using System.Globalization;
using System.Threading.Tasks;
using Assets.Client.Models.Assets;
using Fees.Domain.Entities;
using Fees.Domain.Exceptions;
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

        [Fact]
        public void Test2()
        {
            //var cashOperationsFee = new CashOperationsFee();
            //cashOperationsFee.CashInValue = 0.0001m;
            //cashOperationsFee.CashOutValue = 0.00001m;
            //cashOperationsFee.CashTransferValue = 0.000001m;
            //var asset = new AssetModel();
            //asset.Accuracy = 4;

            //var cashInStr = cashOperationsFee.CashInValue.ToString(CultureInfo.InvariantCulture);
            //var cashInFractionLength = cashInStr.Substring(cashInStr.IndexOf(".") + 1).Length;
            //if (cashInFractionLength > asset.Accuracy)
            //    throw new IncorrectAccuracyException(ErrorCode.IncorrectAccuracy, "CashIn accuracy is bigger then asset accuracy.");

            //var cashOutStr = cashOperationsFee.CashOutValue.ToString(CultureInfo.InvariantCulture);
            //var cashOutFractionLength = cashOutStr.Substring(cashOutStr.IndexOf(".") + 1).Length;
            //if (cashOutFractionLength > asset.Accuracy)
            //    throw new IncorrectAccuracyException(ErrorCode.IncorrectAccuracy, "CashOut accuracy is bigger then asset accuracy.");

            //var cashTransferStr = cashOperationsFee.CashTransferValue.ToString(CultureInfo.InvariantCulture);
            //var cashTransferFractionLength = cashTransferStr.Substring(cashTransferStr.IndexOf(".") + 1).Length;
            //if (cashTransferFractionLength > asset.Accuracy)
            //    throw new IncorrectAccuracyException(ErrorCode.IncorrectAccuracy, "CashTransfer accuracy is bigger then asset accuracy.");
        }

        private string GetAuthToken()
        {
            return "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsic3dpc3NjaGFpbi5pbyIsInNpcml1cy5zd2lzc2NoYWluLmlvIiwiZXhjaGFuZ2Uuc3dpc3NjaGFpbi5pbyJdLCJ1c2VyLWlkIjoiZjllNzE4ZDUtM2M3Yy00YTUxLWFkNDMtYTliNzlmMDlkZDcxIiwidGVuYW50LWlkIjoiODM4MjlhYTEtNTg4OC00NWU0LTk5N2MtYjEzM2U1OGI3YWI4IiwibmJmIjoxNTg4ODQyMTE2LCJleHAiOjE1ODk0NDY5MTYsImlhdCI6MTU4ODg0MjExNn0.JsSGCTnC7VkrXLz2yiSxJULDcPyrbgJc8rTKRvjboKs";
        }
    }
}
