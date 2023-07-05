using System;
using System.Collections;
using System.Collections.Generic;

namespace Service
{
    public interface IMovieService
    {
        public IEnumerable<Domain.Movie> GetAllMovies();
        public Domain.Movie GetSpecificMovie(Guid? id);
    }
}