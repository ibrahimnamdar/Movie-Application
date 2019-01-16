using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hangfire;

namespace MovieApplication.Helpers.Hangfire
{
    public class HangfireService 
    {
        public static Task<string> UpdateMovies()
        {
            return Task.Delay(1000)
                .ContinueWith(t => "Hello");
        }


    }
    
}
