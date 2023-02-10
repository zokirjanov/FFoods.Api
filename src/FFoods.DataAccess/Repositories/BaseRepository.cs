﻿using FFoods.DataAccess.DbContexts;
using FFoods.DataAccess.Interfaces;
using FFoods.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FFoods.DataAccess.Repositories;
public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected AppDbContext _dbcontext;
    protected DbSet<T> _dbSet;
    public BaseRepository(AppDbContext context)
    {
        _dbcontext = context;
        _dbSet = context.Set<T>();
    }
    public virtual void Add(T entity) => _dbSet.Add(entity);

    public virtual void Delete(long id)
    {
        var entity = _dbSet.Find(id);
        if (entity is not null)
            _dbSet.Remove(entity);
    }

    public virtual async Task<T?> FindByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public virtual void Update(long id, T entity)
    {
        entity.Id = id;
        _dbSet.Update(entity);
    }
}

