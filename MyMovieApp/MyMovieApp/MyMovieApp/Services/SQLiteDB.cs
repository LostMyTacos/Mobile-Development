using MyMovieApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp.Services
{
    public class SQLiteDB
    {
        readonly SQLiteAsyncConnection database;

        public SQLiteDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Movie>().Wait();
        }

        public Task<List<Movie>> GetMoviesAsync()
        {
            return database.Table<Movie>().ToListAsync();
            //return database.QueryAsync<Movie>("SELECT * FROM [MyMovies] ORDER BY [Title] DESC");
        }

        public Task<Movie> GetMovieAsync(string id)
        {
            return database.Table<Movie>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> AddMovieAsync(Movie movie)
        {
            return database.InsertAsync(movie);            
        }

        public Task<int> DeleteMovieAsync(Movie movie)
        {
            return database.DeleteAsync(movie);
        }

    }
}
