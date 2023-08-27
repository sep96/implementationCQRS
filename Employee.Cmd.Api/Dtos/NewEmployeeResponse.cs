using Employee.Common.Dtos;

namespace Employee.Cmd.Api.Dtos
{
    public class NewEmployeeResponse : BaseResponse
    {
        public Guid Id { get; set; }
        
    }
}
