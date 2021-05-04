using System.ComponentModel.DataAnnotations;
using MediatR;
using MyStagram.Core.Logic.Responses.Command.Connection;

namespace MyStagram.Core.Logic.Requests.Command.Connection
{
    public class CloseConnectionRequest : IRequest<CloseConnectionResponse>
    {
        [Required]
        public string ConnectionId { get; set; }
    }
}