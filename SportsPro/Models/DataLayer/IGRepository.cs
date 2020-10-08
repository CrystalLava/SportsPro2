using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace SportsPro.Models.DataLayer
{
    public interface IGRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        void Insert(T t);
        void Update(T t);

        void Delete(T t);
    }

    // interface Transporter
    // {
    //     void move();
    //
    //     int measureDistance();
    // }
    //
    // class LandTransporter : Transporter
    // {
    //     public void move()
    //     {
    //         Console.WriteLine("i am a land transporter");
    //     }
    //
    //     public int measureDistance()
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
}
