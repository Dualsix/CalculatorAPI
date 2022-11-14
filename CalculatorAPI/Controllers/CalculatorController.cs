using CalculatorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class CalculatorController : Controller
    {
        #region Variables

        private static List<string> _operationsList = new List<string>();

        #endregion

        #region Public Mehtods

        [HttpGet]
        [Route("calculate")]
        public IActionResult Calculate(string value1, string operation, string value2)
        {
            Calculator calculator = new Calculator();
            Response<float> result = new Response<float>();

            try
            {
                float fValue1 = float.Parse(value1, CultureInfo.InvariantCulture.NumberFormat);
                float fValue2 = float.Parse(value2, CultureInfo.InvariantCulture.NumberFormat);

                result = calculator.DoOperation(fValue1, operation, fValue2);
                _operationsList.Add(result.Message);
            }
            catch
            {
                result.Code = 400;
                result.Message = "Bad Request";
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("getOperationList")]
        public IActionResult GetOperationList()
        {
            return Ok(_operationsList );
        }

        #endregion
    }
}
