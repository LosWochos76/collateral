namespace SeminarManager.API.Misc
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public OperationResult(string message)
        {
            IsError = true;
            IsSuccess = false;
            ErrorMessage = message;
        }

        public OperationResult()
        {
            IsError = false;
            IsSuccess = true;
            ErrorMessage = null;
        }
    }
}
