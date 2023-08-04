using System.ComponentModel.DataAnnotations;
using KuSys.Core;

namespace KuSys.Entities;

/// <summary>
/// Base class of entities. 
/// </summary>
/// <typeparam name="TKey">Key type of the entity</typeparam>
public abstract class EntityBase<TKey>
{
    /// <summary>
    /// Primary key of the entity
    /// </summary>
    [Key]
    public TKey Id { get; set; }
    
    /// <summary>
    /// Used for soft-delete data. 
    /// </summary>
    public bool IsDeleted { get; set; }
}