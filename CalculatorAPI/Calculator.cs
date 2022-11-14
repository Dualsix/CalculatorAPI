using System.Globalization;

namespace CalculatorAPI.Models
{
    public class Calculator
    {
        #region Variables

        private Response<float> _response;

        #endregion

        #region Public Methods

        public Calculator() 
        {
            _response = new Response<float>()
            {
                Code = 200,
                Message = "OK"
        };
        }

        public Response<float> DoOperation(float value1, string operation, float value2)
        {
            Operate(value1, operation, value2);

            return _response;
        }

        #endregion

        #region Private Methods

        private void Operate(float value1, string operation, float value2)
        {
            float solution = 0;
            switch (operation)
            {
                case "add":
                case "+":
                    solution = Add(value1, value2);
                    WriteTheResponseMessage("sum", value1, value2, solution);
                    break;

                case "substract":
                case "-":
                    solution = Substract(value1, value2);
                    WriteTheResponseMessage("substraction", value1, value2, solution);
                    break;

                case "multiply":
                case "*":
                    solution = Multiply(value1, value2);
                    WriteTheResponseMessage("multiplication", value1, value2, solution);
                    break;

                case "division":
                case "/":
                    if (CanDivide(value2, _response))
                    {
                        solution = Divide(value1, value2);
                        WriteTheResponseMessage("division", value1, value2, solution);
                    }
                    break;

                default:
                    _response.Code = 400;
                    _response.Message = "Bad Request";
                    break;
            }

            _response.Data = solution;
        }

        private void WriteTheResponseMessage(string operation, float value1, float value2, float solution)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            _response.Message = "The " + operation + " of " + value1.ToString(nfi) + " and " + value2.ToString(nfi) + " gives as a result " + solution.ToString(nfi);
        }

        private bool CanDivide(float divider, Response<float> response)
        {
            if(divider == 0)
            {
                response.Code = 406;
                response.Message = "Cannot be divided by 0";
                return false;
            }

            return true;
        }

        private float Add(float value1, float value2)
        {
            return value1 + value2;
        }

        private float Substract(float value1, float value2)
        {
            return value1 - value2;
        }

        private float Multiply(float value1, float value2)
        {
            return value1 * value2;
        }

        private float Divide(float value1, float value2)
        {
            return value1 / value2;
        }

        #endregion
    }
}
