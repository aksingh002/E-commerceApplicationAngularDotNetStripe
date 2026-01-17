using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecificationRepository<T>
{
    Expression<Func<T,bool>> Criteria { get;}
}
