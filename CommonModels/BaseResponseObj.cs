namespace CommonModelsLib;

public class BaseResponseObj
{
    // public HeaderObj HeaderObj { get; set; }

    public string StatusCode { get; set; }

    public string Message { get; set; }

    // public List<ValidationError> ErrorMessages { get; set; }

    public virtual byte[] RowVersion { get; set; }

    public BaseResponseObj()
    {
        StatusCode = "200";
        Message = "Success";
        // ErrorMessages = null;
        // HeaderObj = new HeaderObj();
    }
}