using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApplication.Core.Service.Service.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<bool> UpdateAllMovies();
    }
}
