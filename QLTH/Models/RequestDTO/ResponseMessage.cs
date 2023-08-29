namespace QLTH.Models.RequestDTO
{
    public class ResponseMessage
    {
        public object Object { get; set; }
        public int Status { get; set; }
        public bool isOk { get; set; }
        public bool isError { get; set; }
        public ResponseMessage(Object token, int status, bool isok, bool iserror) {
            Object = token;
            Status = status;
            isOk = isok;
            isError = iserror;
        }
    }
}
