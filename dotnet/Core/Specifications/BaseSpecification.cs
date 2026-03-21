using System;
using System.Dynamic;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecificationRepository<T>
{
    public BaseSpecification():this(null)
    {
        
    }
    private readonly Expression<Func<T, bool>>? criteria = criteria;

    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set;}

    public Expression<Func<T, object>>? OrderByDescending { get; private set;}

    public bool IsDistinct {get; private set;}

    protected void AddOrderBy(Expression<Func<T,object>> expression)
    {
        OrderBy = expression;
    }
    protected void AddOrderByDescending(Expression<Func<T,object>> expressionDesc)
    {
        OrderByDescending = expressionDesc;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) : BaseSpecification<T>(criteria), ISpecificationRepository<T, TResult>
{
    
    public BaseSpecification():this(null!)
    {
        
    }
    public Expression<Func<T, TResult>>? Select {get; private set;}
    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }

}
