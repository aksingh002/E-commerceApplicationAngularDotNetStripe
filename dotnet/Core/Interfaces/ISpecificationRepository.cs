using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecificationRepository<T>
{
    Expression<Func<T,bool>> Criteria { get;}
    Expression<Func<T,object>> OrderBy { get;}
    Expression<Func<T,object>> OrderByDescending { get;}
}

public interface ISpecificationRepository<T, TResult>:ISpecificationRepository<T>
{
    Expression<Func<T,TResult>>? Select { get;}
}
