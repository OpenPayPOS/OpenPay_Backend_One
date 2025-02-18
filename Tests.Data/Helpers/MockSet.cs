using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Tests.Data.Helpers;
public static class MockSet
{
    public static DbSet<T> CreateDbSetMock<T>(IQueryable<T> data) where T : class
    {
        var dbSet = Substitute.For<DbSet<T>, IQueryable<T>>();
        ((IQueryable<T>)dbSet).Provider.Returns(data.Provider);
        ((IQueryable<T>)dbSet).Expression.Returns(data.Expression);
        ((IQueryable<T>)dbSet).ElementType.Returns(data.ElementType);
        ((IQueryable<T>)dbSet).GetEnumerator().Returns(data.GetEnumerator());
        return dbSet;
    }
}