namespace PaymentsService.Application.DTOs.Erros
{
    public class MsDtoResponse<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public string traceid { get; set; }
        public T data { get; set; }

        public MsDtoResponse(T data, string traceId, int code = 200, string message = "Operación exitosa")
        {
            this.code = code;
            this.message = message;
            this.traceid = traceId;
            this.data = data;
        }
    }

}

