using MyGuide.Models;
using System.Collections.Generic;

namespace MyGuide.Domain.Services
{
    public interface ITspService
    {
        List<Destination> SolveTsp(List<Destination> destinations);
    }
}
