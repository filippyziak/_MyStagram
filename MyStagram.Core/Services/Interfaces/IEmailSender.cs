using System.Threading.Tasks;
using MyStagram.Core.Models.Helpers.Email;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IEmailSender
    {
         Task<bool> Send(EmailMessage emailMessage);
    }
}